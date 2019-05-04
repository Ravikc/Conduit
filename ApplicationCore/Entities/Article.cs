using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Conduit.Common.Extensions;

namespace Conduit.ApplicationCore.Entities
{
    public class Article : AuditableEntity<int>
    {
        [MaxLength(512)]
        public string Slug => Title.Slugify();

        [Required, MaxLength(256)]
        public string Title { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        [Required] 
        public string Body { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public string AuthorId { get; set; }
        public UserProfile Author { get; set; }
    }
}