using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RockwellBlog.Enums;

namespace RockwellBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Abstract { get; set; }
        [Required]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Update Date")]
        public DateTime? Updated { get; set; }

        public string Slug { get; set; }

        //Give myself the ability to record the state of any post
        //public bool IsPublished { get; set; }
        [Display(Name = "Published State")]
        public PublishState PublishState { get; set; }

        //Navigational Properties
        public virtual Blog Blog { get; set; }

    }
}
