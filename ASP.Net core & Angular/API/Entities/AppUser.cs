using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
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
        public ICollection<UserLike> LikedByUsers { get; set; }
        public ICollection<UserLike> LikedUsers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}