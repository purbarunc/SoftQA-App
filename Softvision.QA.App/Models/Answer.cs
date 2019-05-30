using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Models
{
    public class Answer
    {
        public Answer()
        {
            CreatedOn = DateTime.Now;
            Title = "Dummy";
            IsCorrect = false;
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Title is missing.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Answer is missing")]
        [DataType(DataType.MultilineText)]
        [MinLength(30, ErrorMessage = "Answer must be atleast 30 characters")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }

        public string AnsweredBy { get; set; }

        public bool IsCorrect { get; set; }

        public DateTime? AcceptedOn { get; set; }

        [MaxLength]
        public string Preview { get; set; }
        
        public virtual long QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<AnswerVote> Votes { get; set; }
    }
}