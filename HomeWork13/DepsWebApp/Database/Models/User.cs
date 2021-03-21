using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepsWebApp.Database.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string Login { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }

    }
}
