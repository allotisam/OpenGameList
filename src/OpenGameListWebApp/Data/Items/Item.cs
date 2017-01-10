using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenGameListWebApp.Data.Items
{
    public class Item
    {
        public Item()
        {

        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public string Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ViewCount { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

    }
}
