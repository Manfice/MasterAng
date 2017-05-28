using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Model.Users;
using Web.Model.Comments;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Model.Items
{
    public class Item
    {
        public Item()
        {
            
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public string UserId { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        #region Related properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser Author { get; set; }
        public virtual List<Comment> Comments { get; set; }
        #endregion
    }
}