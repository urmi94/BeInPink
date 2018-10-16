using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeInPink.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public String PostHeading { get; set; }
        public String PostContent { get; set; }
        public _PostType PostType { get; set; }
        public enum _PostType {
            ClientPost = 1,
            CoachPost = 2
        }

        public DateTime PostDate { get; set; }
        public DateTime? PostUpdatedOn { get; set; }
        [ForeignKey("AuthorId")]
        [InverseProperty("PostAuthor")]
        public virtual ApplicationUser Author { get; set; }
        public String AuthorId { get; set; }
        [ForeignKey("UpdatedById")]
        [InverseProperty("PostUpdatedByAuthor")]
        public virtual ApplicationUser Updater { get; set; }
        public String UpdatedById { get; set; }
        
    }
}