using System;
using System.Collections.Generic;
using Web.Model.Comments;
using Web.Model.Items;

namespace Web.Model.Users
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
