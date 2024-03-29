﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RockwellBlog.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
       
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string DisplayName { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        //public byte[] imagedata { get; set; }
        //public string contenttype { get; set; }

    }
}
