﻿using System.ComponentModel.DataAnnotations;

namespace FlyIBooking.Dtos
{
    /// <summary>
    ///  The contract coming from UI, for the registration of the user
    /// </summary>
    public sealed class AccountRegisterDto
    {
        /// <summary>
        /// User mail by which the user will be remembered
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@".+@.+\..+")]
        public string Email { get; set; }

        /// <summary>
        /// User password which will be allowed to enter the account
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}