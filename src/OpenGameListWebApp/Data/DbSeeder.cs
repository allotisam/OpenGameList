using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OpenGameListWebApp.Data;
using OpenGameListWebApp.Data.Comments;
using OpenGameListWebApp.Data.Items;
using OpenGameListWebApp.Data.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameListWebApp.Data
{
    public class DbSeeder
    {
        #region Private Members

        private ApplicationDbContext _dbContext;

        #endregion

        #region Constructor

        public DbSeeder(ApplicationDbContext dbContext) { _dbContext = dbContext; }

        #endregion

        #region Public Methods

        public async Task SeedAsync()
        {
            // Create Db if it doesn't exist
            _dbContext.Database.EnsureCreated();

            // Create default users if they don't exist
            if (await _dbContext.Users.CountAsync() == 0)
                CreateUsers();

            // Create default Items (if there are none) and Comments if they don't exist
            if (await _dbContext.Items.CountAsync() == 0)
                CreateItems();
        }

        #endregion

        #region Seed Methods

        private void CreateUsers()
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModified = DateTime.Now;

            // create the "Admin" ApplicationUser account
            var userAdmin = new ApplicationUser() { Id = Guid.NewGuid().ToString(), UserName = "Admin", Email = "admin@opengamelist.com", CreatedDate = createdDate, LastModifiedDate = lastModified };

            // insert "Admin" into Database
            _dbContext.Users.Add(userAdmin);

#if DEBUG

            // create some sample registered user accounts
            var userRyan = new ApplicationUser() { Id = Guid.NewGuid().ToString(), UserName = "Ryan", Email = "ryan@opengamelist.com", CreatedDate = createdDate, LastModifiedDate = lastModified };
            var userSolice = new ApplicationUser() { Id = Guid.NewGuid().ToString(), UserName = "Solice", Email = "solice@opengamelist.com", CreatedDate = createdDate, LastModifiedDate = lastModified };
            var userVodan = new ApplicationUser() { Id = Guid.NewGuid().ToString(), UserName = "Vodan", Email = "vodan@opengamelist.com", CreatedDate = createdDate, LastModifiedDate = lastModified };

            _dbContext.Users.AddRange(userRyan, userSolice, userVodan);
#endif
            _dbContext.SaveChanges();
        }

        private void CreateItems()
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;

            var authorId = _dbContext.Users.Where(u => u.UserName == "Admin").FirstOrDefault().Id;

#if DEBUG
            var num = 100;
            for (int id = 1; id <= num; id++)
            {
                _dbContext.Items.Add(GetSampleItem(id, authorId, num - id, new DateTime(2015, 12, 31).AddDays(-num)));
            }
#endif

            EntityEntry<Item> e1 = _dbContext.Items.Add(new Item()
            {
                UserId = authorId,
                Title = "Magarena",
                Description = "Single-player fantasy card game similar to Magic: The Gathering",
                Text = @"Loosely based on Magic: The Gathering, the game lets you play against a computer opponent or another human being. The game features a well-developed AI, an intuitive and clear interface and an enticing level of gameplay.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Item> e2 = _dbContext.Items.Add(new Item()
            {
                UserId = authorId,
                Title = "Minetest",
                Description = "Open-Source alternative to Minecraft",
                Text = @"The Minetest gameplay is very similar to Minecraft's: you are playing in a 3D open world, where you can create and/or remove various types of blocks. Minetest feature both single-player and multiplayer game modes. It also has support for custom mods, additional texture packs and other custom/personalization options. Minetest has been released in 2015 under GNU Lesser General Public License.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 4180,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Item> e3 = _dbContext.Items.Add(new Item()
            {
                UserId = authorId,
                Title = "Relic Hunters Zero",
                Description = "A free game about shooting evil space ducks with tiny, cute guns.",
                Text = @"Relic Hunters Zero is fast, tactical and also very smooth to play. It also enables the users to look at the source code, so they can can get creative and keep this game alive, fun and free for years to come. The game is also available on Steam.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 5203,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Item> e4 = _dbContext.Items.Add(new Item()
            {
                UserId = authorId,
                Title = "SuperTux",
                Description = "A classic 2D jump and run, side-scrolling game similar to the Super Mario series.",
                Text = @"The game is currently under Milestone 3. The Milestone 2, which is currently out, features the following:
                    - a nearly completely rewritten game engine based on OpenGL, OpenAL, SDL2, ...
                    - support for translations
                    - in-game manager for downloadable add-ons and translations
                    - Bonus Island III, a for now unfinished Forest Island and the development levels in Incubator Island
                    - a final boss in Icy Island
                    - new and improved soundtracks and sound effects
                    ... and much more!
                    The game has been released under the GNU GPL license.",
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = 9602,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Item> e5 = _dbContext.Items.Add(new Item()
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
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            // Create default comments
            if (_dbContext.Comments.Count() == 0)
            {
                int numComments = 10;
                for (int i = 1; i <= numComments; i++)
                {
                    _dbContext.Comments.Add(GetSampleComment(i, e1.Entity.Id, authorId, createdDate.AddDays(i)));
                    _dbContext.Comments.Add(GetSampleComment(i, e2.Entity.Id, authorId, createdDate.AddDays(i)));
                    _dbContext.Comments.Add(GetSampleComment(i, e3.Entity.Id, authorId, createdDate.AddDays(i)));
                    _dbContext.Comments.Add(GetSampleComment(i, e4.Entity.Id, authorId, createdDate.AddDays(i)));
                    _dbContext.Comments.Add(GetSampleComment(i, e5.Entity.Id, authorId, createdDate.AddDays(i)));
                }
            }

            _dbContext.SaveChanges();
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Generate a sample item to populate the db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorId"></param>
        /// <param name="viewCount"></param>
        /// <param name="createdDate"></param>
        /// <returns></returns>
        private Item GetSampleItem(int id, string authorId, int viewCount, DateTime createdDate)
        {
            return new Item()
            {
                UserId = authorId,
                Title = string.Format("Item {0} Title", id),
                Description = string.Format("This is a sample description for item {0}: Lorem ipsum dolor sit amet.", id),
                Notes = "This is a sample record created by the Code-First Configuration class",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };
        }

        private Comment GetSampleComment(int n, int itemId, string authorId, DateTime createdDate)
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

        #endregion
    }
}
