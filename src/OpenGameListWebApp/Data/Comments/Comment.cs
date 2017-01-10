using OpenGameListWebApp.Data.Items;
using OpenGameListWebApp.Data.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameListWebApp.Data.Comments
{
    public class Comment
    {
        public Comment() { }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int? ParentId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }


        #region Related Properties

        /// <summary>
        /// Current Comment's Item
        /// </summary>
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        /// <summary>
        /// Current Comment's Author
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ApplicationUser Author { get; set; }

        /// <summary>
        /// Parent comment, or NULLL if this is a root comment
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual Comment Parent { get; set; }
        
        /// <summary>
        /// Children comments
        /// </summary>
        public virtual List<Comment> Children { get; set; }
        
        #endregion
    }
}
