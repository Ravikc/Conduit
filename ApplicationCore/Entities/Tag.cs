using System.ComponentModel.DataAnnotations;

namespace Conduit.ApplicationCore.Entities
{
    public class Tag : BaseEntity<int>
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }
    }
}
