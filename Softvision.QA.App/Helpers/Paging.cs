using Softvision.QA.App.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Helpers
{
    public class Paging
    {
        public IEnumerable<Question> MyQuestions { get; set; }
        public int QuestionPages { get; set; }
        public IEnumerable<Answer> MyAnswers { get; set; }
        public int AnswerPages { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}