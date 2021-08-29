using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Topic : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Url { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public bool IsDeleted { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        public AppUser User { get; set; }

        public Topic()
        {
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
        

    }
}
