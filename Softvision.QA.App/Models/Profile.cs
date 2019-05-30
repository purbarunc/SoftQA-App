using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Models
{
    public class Profile
    {
        public Int64 Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}