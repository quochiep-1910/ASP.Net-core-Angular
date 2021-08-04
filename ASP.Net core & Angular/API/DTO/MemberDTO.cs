using System;
using System.Collections.Generic;

namespace API.DTO
{
    public class MemberDTO
    {
         public int Id { get; set; }
        public string UserName { get; set; }
    
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public DateTime DayOfBirth{get;set;}
        public ICollection<PhotoDTO> Photos {get;set;}
    }
}