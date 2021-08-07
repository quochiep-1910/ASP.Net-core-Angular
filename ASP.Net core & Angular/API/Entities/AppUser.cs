using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Created { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime LastActive { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DayOfBirth { get; set; }

        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}