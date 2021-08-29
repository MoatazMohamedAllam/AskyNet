using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string About { get; set; }
        
        public string ImageUrl { get; set; }

        public ICollection<Topic> Topics { get; set; }

        public AppUser()
        {
            CreatedAt = DateTime.UtcNow;
            Topics = new List<Topic>();
        }

    }
}
