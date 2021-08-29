using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Color { get; set; }

        [Required]
        public string Uri { get; set; }

        public ICollection<Topic> Topics { get; set; }

        public Category()
        {
            Topics = new List<Topic>();
        }

    }
}
