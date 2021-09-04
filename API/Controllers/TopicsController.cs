using API.Dtos;
using API.Errors;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
    }
}
