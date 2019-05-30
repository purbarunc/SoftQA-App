using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Models
{
    public class Question
    {
        public Question()
        {
            CreatedOn = DateTime.Now;
            Views = 0;
            this.Answers = new HashSet<Answer>();
            this.Tags = new HashSet<Tag>();
            this.Votes = new HashSet<QuestionVote>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Title is missing.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Body is missing")]
        [DataType(DataType.MultilineText)]
        [MinLength(30, ErrorMessage = "Body must be atleast 30 characters")]
        public string Description { get; set; }

        [MaxLength]
        public string ShortDescription { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }

        [MaxLength]
        public string Preview { get; set; }

        public long Views { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<QuestionVote> Votes { get; set; }

        public string CreatedBy { get; set; }
    }

    public class Tag
    {
        public Tag()
        {
            CreatedOn = DateTime.Now;
            Views = 0;
            this.Questions = new HashSet<Question>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Name is missing.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Body is missing")]
        [DataType(DataType.MultilineText)]
        [MinLength(30, ErrorMessage = "Body must be atleast 30 characters")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }

        public long Views { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}