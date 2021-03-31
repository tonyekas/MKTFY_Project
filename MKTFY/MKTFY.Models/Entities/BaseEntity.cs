using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class BaseEntity<TId>
    {
        public BaseEntity()
        {
            Created = DateTime.UtcNow;
        }

        [Key]
        public TId Id { get; set; }

        [Required]
        public DateTime Created { get; set; }

    }
}
