using System.ComponentModel.DataAnnotations;

namespace Conduit.ApplicationCore.Entities
{
    public class Comment : AuditableEntity<int>
    {
        [Required, MaxLength(1024)] 
        public string Body { get; set; }

        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}