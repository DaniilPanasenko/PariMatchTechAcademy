using System;
using System.ComponentModel.DataAnnotations;

namespace DepsWebApp.Contracts
{
    /// <summary>
    /// Auth request model.
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// User login.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 6)]
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
