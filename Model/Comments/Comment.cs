using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Model.Items;
using Web.Model.Users;

namespace Web.Model.Comments
{
    public class Comment
    {
        public Comment()
        {
        }
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public string UserId { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        #region Related properties
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Author { get; set; }
        [ForeignKey("ParentId")]
        public virtual Comment Parent { get; set; }
        public virtual List<Comment> Children { get; set; }
        #endregion
    }
}