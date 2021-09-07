using API.Dtos;
using API.Errors;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class TopicsController : BaseApiController
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TopicsController(ITopicRepository topicRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<TopicDto>>> GetAllTopics()
        {
            var topics = await _topicRepository.ListAllAsync();

            return Ok(_mapper.Map<IReadOnlyList<Topic>, IReadOnlyList<TopicDto>>(topics));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TopicDto>> GetTopic(int id)
        {
            var topic = await _topicRepository.GetByIdAsync(id);
            
            if (topic == null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Topic, TopicDto>(topic));
        }

        [Authorize]
        [HttpPost("addtopic")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TopicDto>> AddTopic(string userId, [FromBody] TopicDto topicDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400));
            
            if (userId == null)
                return Unauthorized(new ApiResponse(401));

            var topic = new Topic
            {
                Title = topicDto.Title,
                CategoryId = topicDto.CategoryId,
                Content = topicDto.Content,
                UserId = userId,
                CreatedAt = topicDto.CreatedAt,
                IsDeleted = topicDto.IsDeleted,
                Url = topicDto.Url
            };

            var addedTopic = await _topicRepository.AddAsync(topic);
            await _unitOfWork.Complete();

            return Created(nameof(GetTopic), _mapper.Map < Topic, TopicDto>(addedTopic));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TopicDto>>> GetAll()
        {
            var topics = await _topicRepository.ListAllAsync();

            return Ok(_mapper.Map<IReadOnlyList<Topic>, IReadOnlyList<TopicDto>>(topics));
        }

        [HttpGet("user")]
        public async Task<ActionResult<IReadOnlyList<TopicDto>>> GetUserTopics(string userId)
        {
            var topics = await _topicRepository.GetUserTopics(userId);

            return Ok(_mapper.Map<IReadOnlyList<Topic>, IReadOnlyList<TopicDto>>(topics));
        }


    }
}
