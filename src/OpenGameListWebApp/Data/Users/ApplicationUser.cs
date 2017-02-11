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
        public ApplicationUser() { }

        #region IdentityUser Properties

        // commented them out once we inherited IdentityUser class, which it already contains these properties

        //[Key]
        //[Required]
        //public string Id { get; set; }

        //[Required]
        //[MaxLength(128)]
        //public string UserName { get; set; }

        //[Required]
        //public string Email { get; set; }

        #endregion IdentityUser Properties

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

        #region Related Properties

        /// <summary>
        /// A list of items wrote by this user
        /// </summary>
        public virtual List<Item> Items { get; set; }

        /// <summary>
        /// A list of comments wrote by this user
        /// </summary>
        public virtual List<Comment> Comments { get; set; }

        #endregion

    }
}
