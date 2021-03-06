using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public DateTime Created { get; set; }
        public string PhotoUrl { get; set; }

        public int Age { get; set; }

        public DateTime LastActive { get; set; }

        public DateTime DayOfBirth { get; set; }

        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDTO> Photos { get; set; }
    }
}