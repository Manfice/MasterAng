using System;

namespace Web.Model.Comments
{
    public class Comment
    {
        public Comment()
        {
        }

        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public string UserId { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}