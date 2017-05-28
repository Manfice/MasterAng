using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Model.Users;

namespace Web.Model
{
    public class DbSeeder
    {
        private ApplicationDbContext _context;

        public DbSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();
            if (await _context.Users.CountAsync() == 0) CreateUsers();
        }

        private void CreateUsers()
        {
            var crdt = new DateTime(1984, 8, 29, 08, 40, 00);
            var user_admin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Email = "c592@yandex.ru",
                CreatedDate = crdt,
                LastModifiedDate = DateTime.Now,
                DisplayName = "Manfice"
            };
            _context.Users.Add(user_admin);
            _context.SaveChanges();
        }
    }
}