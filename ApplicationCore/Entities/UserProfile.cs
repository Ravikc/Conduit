using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conduit.ApplicationCore.Entities
{
    public class UserProfile : BaseEntity<string>
    {
        [MaxLength(1024)]
        public string Bio { get; set; }

        [MaxLength(1024)]
        public string Image { get; set; }


        [Key, ForeignKey(nameof(ApplicationUser))]
        public override string Id { get; set; }
        public ConduitUser ApplicationUser { get; set; }
    }
}
