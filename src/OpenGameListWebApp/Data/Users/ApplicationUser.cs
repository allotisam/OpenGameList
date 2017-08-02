using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OpenGameListWebApp.Data.Comments;
using OpenGameListWebApp.Data.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGameListWebApp.Data.Users
{
    public class ApplicationUser : IdentityUser
    {
        #region Properties

        public string DisplayName { get; set; }

        public string Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        #endregion Properties

        #region Related Properties

        public virtual List<Item> Items { get; set; }

        public virtual List<Comment> Comments { get; set; }

        #endregion Related Properties
    }
}
