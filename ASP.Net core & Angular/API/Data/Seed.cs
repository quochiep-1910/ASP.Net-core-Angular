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
                new AppUser { UserName="QuocHiep", DayOfBirth = DateTime.Now },
                new AppUser { UserName="QuocHiep1", DayOfBirth = DateTime.Now }
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