using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Model.Comments;
using Web.Model.Items;
using Web.Model.Users;

namespace Web.Model
{
    public class DbSeeder
    {
        private readonly ApplicationDbContext _context;

        public DbSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();
            if (await _context.Users.CountAsync() == 0) CreateUsers();
            if (await _context.Items.CountAsync() == 0) CreateItems();
        }

        private void CreateUsers()
        {
            var crdt = new DateTime(1984, 8, 29, 08, 40, 00);
            var userAdmin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Email = "c592@yandex.ru",
                CreatedDate = crdt,
                LastModifiedDate = DateTime.Now,
                DisplayName = "Manfice"
            };
            _context.Users.Add(userAdmin);
#if DEBUG
            for (var i = 0; i < 5; i++)
            {
                var u = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = $"User_{i}",
                    Email = $"{i}@{i}.com",
                    CreatedDate = crdt.AddDays(i + 2 * 3),
                    LastModifiedDate = DateTime.Now,
                    DisplayName = $"User_name_{i}"
                };
                _context.Users.Add(u);
            }
#endif
            _context.SaveChanges();
        }

        private void CreateItems()
        {
            var crDt = new DateTime(2017, 1, 1, 0, 0, 0);
            var authorId =
                _context.Users.FirstOrDefault(
                    user => user.UserName.Equals("admin", StringComparison.CurrentCultureIgnoreCase)).Id;
#if DEBUG
            const int num = 1000;
            for (var i = 0; i < num; i++)
            {
                _context.Items.Add(GetSampleItem(i, authorId, num - i, DateTime.Now.AddDays(-num)));
            }
#endif
            var e1 = _context.Items.Add(new Item
            {
                UserId = authorId,
                Title = "Magarena",
                Description = "Single-player fantasy card game similar to Magic: The Gathering",
                Text = @"Loosely based on Magic: The Gathering, the game lets
                        you play against a computer opponent or another human being.
                        The game features a well-developed AI, an
                        intuitive and clear interface and an enticing level of gameplay.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 2343,
                CreatedDate = crDt,
                LastModifiedDate = DateTime.Now
            });
            var e2 = _context.Items.Add(new Item
            {
                UserId = authorId,
                Title = "Minetest",
                Description = "Open-Source alternative to Minecraft",
                Text = @"The Minetest gameplay is very similar to Minecraft's:
                        you are playing in a 3D open world, where you can create and/or remove
                        various types of blocks.
                        Minetest feature both single-player and multiplayer game modes.
                        It also has support for custom mods, additional
                        texture packs and other custom/personalization options.
                        Minetest has been released in 2015 under GNU Lesser
                        General Public License.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 4180,
                CreatedDate = crDt,
                LastModifiedDate = DateTime.Now
            });
            var e3 = _context.Items.Add(new Item
            {
                UserId = authorId,
                Title = "Relic Hunters Zero",
                Description = "A free game about shooting evil space ducks with tiny, cute guns.",
                Text = @"Relic Hunters Zero is fast, tactical and also very
                        smooth to play.
                        It also enables the users to look at the source
                        code, so they can can get creative and keep this game alive, fun and free
                        for years to come.
                        The game is also available on Steam.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 5203,
                CreatedDate = crDt,
                LastModifiedDate = DateTime.Now
            });
            var e4 = _context.Items.Add(new Item
            {
                UserId = authorId,
                Title = "SuperTux",
                Description = "A classic 2D jump and run, side-scrolling game similar to the Super Mario series.",
                Text = @"The game is currently under Milestone 3. The Milestone
                        2, which is currently out, features the following:
                        - a nearly completely rewritten game engine based
                        on OpenGL, OpenAL, SDL2, ...
                        - support for translations
                        - in-game manager for downloadable add-ons and
                        translations
                        - Bonus Island III, a for now unfinished Forest
                        Island and the development levels in Incubator Island
                        - a final boss in Icy Island
                        - new and improved soundtracks and sound effects
                        ... and much more!
                        The game has been released under the GNU GPL
                        license.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 9602,
                CreatedDate = crDt,
                LastModifiedDate = DateTime.Now
            });
            var e5 = _context.Items.Add(new Item
            {
                UserId = authorId,
                Title = "Scrabble3D",
                Description = "A 3D-based revamp to the classic Scrabble game.",
                Text = @"Scrabble3D extends the gameplay of the classic game
                        Scrabble by adding a new whole third dimension.
                        Other than playing left to right or top to bottom,
                        you'll be able to place your tiles above or beyond other tiles.
                        Since the game features more fields, it also uses a
                        larger letter set.
                        You can either play against the computer, players
                        from your LAN or from the Internet.
                        The game also features a set of game servers where
                        you can challenge players from all over the world and get ranked into an
                        official, ELO-based rating/ladder system.
                        ",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 6754,
                CreatedDate = crDt,
                LastModifiedDate = DateTime.Now
            });
            // Create default Comments (if there are none)
            if (!_context.Comments.Any())
            {
                const int numComments = 10; // comments per item
                for (var i = 1; i <= numComments; i++)
                    _context.Comments.Add(GetSampleComment(i, e1.Entity.Id, authorId,
                    crDt.AddDays(i)));
                for (var i = 1; i <= numComments; i++)
                    _context.Comments.Add(GetSampleComment(i, e2.Entity.Id, authorId,
                    crDt.AddDays(i)));
                for (var i = 1; i <= numComments; i++)
                    _context.Comments.Add(GetSampleComment(i, e3.Entity.Id, authorId,
                    crDt.AddDays(i)));
                for (var i = 1; i <= numComments; i++)
                    _context.Comments.Add(GetSampleComment(i, e4.Entity.Id, authorId,
                    crDt.AddDays(i)));
                for (var i = 1; i <= numComments; i++)
                    _context.Comments.Add(GetSampleComment(i, e5.Entity.Id, authorId,
                    crDt.AddDays(i)));
            }
            _context.SaveChanges();
        }

        private static Item GetSampleItem(int id, string authorId, int viewCount, DateTime createdDate)
        {
            var item = new Item()
            {
                UserId = authorId,
                Title = $"Item {id} Title",
                Description =
                    $"This is a sample description for item { id }: Lorem ipsum dolor sit amet.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };
            return item;
        }

        private static Comment GetSampleComment(int n, int itemId, string authorId,
        DateTime createdDate)
        {
            return new Comment()
            {
                ItemId = itemId,
                UserId = authorId,
                ParentId = null,
                Text = string.Format("Sample comment #{0} for the item #{1}", n, itemId),
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };
        }
    }
}