using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Models
{
    public class QuestionVote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        public long QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int Point { get; set; }

        public DateTime VotedOn { get; set; }

        public string VotedBy { get; set; }
    }

    public class AnswerVote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        public long AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public int Point { get; set; }

        public DateTime VotedOn { get; set; }

        public string VotedBy { get; set; }
    }
}