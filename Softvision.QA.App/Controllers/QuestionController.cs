using Softvision.QA.App.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Softvision.QA.App.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QaDbContext _db = new QaDbContext();

        [Authorize]
        public ActionResult Ask()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Ask([Bind(Include = "Id,Title,Description,Preview")] Question question, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                IList<string> tags;
                if (frm["Tags"].Length > 1)
                {
                    tags = frm["Tags"].Split(',').ToList();
                    foreach (string tagId in tags)
                    {
                        var t = _db.Tags.Find(Convert.ToInt64(tagId));
                        if (t != null)
                        {
                            question.Tags.Add(t);
                        }
                    }
                }

                var noHtml = Regex.Replace(question.Preview, @"<[^>]*>", String.Empty);
                var noSpecialChars = Regex.Replace(noHtml, @"[^0-9a-zA-Z\[\]\(\)\"".,]+", " ");
                question.ShortDescription = noSpecialChars.Length > 380 ? noSpecialChars.Substring(0, 380).Trim() : noSpecialChars.Trim();
                question.CreatedBy = User.Identity.Name;

                _db.Questions.Add(question);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(question);
        }

        [Authorize]
        public JsonResult GetTags()
        {
            var d = _db.Tags.Select(s => new { id = s.Id, name = s.Name }).ToList();
            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = _db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            question.Views = question.Views + 1;
            _db.Entry(question).State = EntityState.Modified;
            _db.SaveChanges();

            ViewBag.Answers = _db.Answers.Where(a => a.QuestionId == id).ToList();
            ViewBag.NewAnswer = new Answer { Question = question, QuestionId = question.Id };
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Answer(/*[Bind(Include = "Id,Title,Description,Preview,QuestionId")]*/ Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.AnsweredBy = User.Identity.Name;
                _db.Answers.Add(answer);
                _db.SaveChanges();
            }
            return RedirectToAction("Details", "Question", new { id = answer.QuestionId });
        }

        [Authorize, HttpPost]
        public ActionResult QuestionVote(int questionId, int point)
        {
            var vote = _db.QuestionVotes.FirstOrDefault(v => v.QuestionId == questionId && v.VotedBy == User.Identity.Name);
            if (vote != null)
            {
                if (vote.Point != point)
                {
                    vote.Point = point;
                    _db.Entry(vote).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            else
            {
                var v = new QuestionVote
                {
                    Point = point,
                    QuestionId = questionId,
                    VotedBy = User.Identity.Name,
                    VotedOn = DateTime.Now
                };

                _db.QuestionVotes.Add(v);
                _db.SaveChanges();
            }

            var updatedCount = _db.QuestionVotes.GroupBy(v => v.QuestionId == questionId).Select(g => new { Total = g.Sum(x => x.Point) }).FirstOrDefault();
            var d = new
            {
                NewCount = updatedCount.Total,
                Message = "Thanks for the feedback!"
            };
            return Json(d, JsonRequestBehavior.AllowGet);
        }


        [Authorize, HttpPost]
        public ActionResult AnswerVote(int answerId, int point)
        {
            var vote = _db.AnswerVotes.FirstOrDefault(v => v.AnswerId == answerId && v.VotedBy == User.Identity.Name);
            if (vote != null)
            {
                if (vote.Point != point)
                {
                    vote.Point = point;
                    _db.Entry(vote).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            else
            {
                var v = new AnswerVote
                {
                    Point = point,
                    AnswerId = answerId,
                    VotedBy = User.Identity.Name,
                    VotedOn = DateTime.Now
                };

                _db.AnswerVotes.Add(v);
                _db.SaveChanges();
            }

            var updatedCount = _db.AnswerVotes.GroupBy(v => v.AnswerId == answerId).Select(g => new { Total = g.Sum(x => x.Point) }).FirstOrDefault();
            var d = new
            {
                NewCount = updatedCount.Total,
                Message = "Thanks for the feedback!"
            };
            return Json(d, JsonRequestBehavior.AllowGet);
        }

        [Authorize, HttpPost]
        public ActionResult AnswerStatus(int answerId, int status)
        {
            var d = new { Status = "Failed" };
            var answer = _db.Answers.Find(answerId);
            if (answer != null)
            {
                IQueryable<Answer> answers = _db.Answers.Where(a => a.QuestionId == answer.QuestionId);
                foreach (var item in answers)
                {
                    item.IsCorrect = false;
                }
                _db.SaveChanges();

                answer.IsCorrect = Convert.ToBoolean(status);
                _db.Entry(answer).State = EntityState.Modified;
                _db.SaveChanges();
                d = new{Status = "Success"};
            }
            return Json(d, JsonRequestBehavior.AllowGet);
        }
    }
}