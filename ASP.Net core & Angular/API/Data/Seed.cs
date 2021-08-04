using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var users = new List<AppUser> {
                new AppUser { UserName="QuocHiep", DayOfBirth = DateTime.Now , Introduction= "Sunt esse aliqua ullamco in incididunt consequat commodo. Nisi ad esse elit ipsum commodo fugiat est ad. Incididunt nostrud incididunt nostrud sit excepteur occaecat.\r\n",
                LookingFor="Dolor anim cupidatat occaecat aliquip et Lorem ut elit fugiat. Mollit eu pariatur est sunt. Minim fugiat sit do dolore eu elit ex do id sunt. Qui fugiat nostrud occaecat nisi est dolor qui fugiat laborum cillum. Occaecat consequat ex mollit commodo ad irure cillum nulla velit ex pariatur veniam cupidatat. Officia veniam officia non deserunt mollit.\r\n",
                Interests="Sit sit incididunt proident velit.",
                City="Greenbush" ,Country="Martinique"},
                new AppUser { UserName="QuocHiep1", DayOfBirth = DateTime.Now , Introduction= "Laborum dolore aliquip voluptate sunt cupidatat fugiat. Aliqua cillum deserunt do sunt ullamco aute Lorem nisi irure velit esse excepteur ex qui. Aliquip cupidatat officia ullamco duis veniam quis elit dolore nisi proident exercitation id cillum ullamco. In exercitation aliqua commodo veniam culpa duis commodo mollit et sint culpa.\r\n",
                LookingFor="Dolor magna eu reprehenderit nostrud do et. Amet voluptate ut laboris ut officia eiusmod exercitation elit labore anim. In consequat ut ex adipisicing irure. Sit proident sint laboris est proident aute mollit minim et mollit. Cillum in enim velit occaecat aliquip. Voluptate aliquip et culpa est ad proident elit duis.\r\n",
                Interests="Dolor aliquip velit amet aliqua minim labore sit laboris non aliquip cillum.",
                City="Celeryville" ,Country="Grenada"}
                };
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));

                await context.Users.AddAsync(user);
            }
            await context.SaveChangesAsync();
        }
    }
}