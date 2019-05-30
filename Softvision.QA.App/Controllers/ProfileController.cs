using Softvision.QA.App.Helpers;
using Softvision.QA.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Softvision.QA.App.Controllers
{
    public class ProfileController : Controller
    {
        private readonly QaDbContext _db = new QaDbContext();
        Paging page = new Paging();
        private const int PageSize = 3;

        // GET: Profile
        public ActionResult Index()
        {
            if (Session["userName"]!=null)
            {
                string user = Session["userName"].ToString();
                page.MyQuestions = _db.Questions.Where(q => q.CreatedBy == user).Take(PageSize).ToList();
                page.QuestionPages= Convert.ToInt32(Math.Ceiling((double)_db.Questions.Where(q => q.CreatedBy == user).Count() / PageSize));
                page.MyAnswers = _db.Answers.Where(ans => ans.AnsweredBy == user).Take(PageSize).ToList();
                page.AnswerPages = Convert.ToInt32(Math.Ceiling((double)_db.Answers.Count() / PageSize));
                ViewBag.CurrentQuestionPage = 1;
                ViewBag.CurrentAnswerPage = 1;
                return View(page);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult GetQuestions(int pageIndex)
        {
            if (Session["userName"] != null)
            {
                string user = Session["userName"].ToString();
                page.MyQuestions = _db.Questions.Where(q => q.CreatedBy == user).ToList().Skip(PageSize * (pageIndex - 1)).Take(PageSize);
                page.QuestionPages = Convert.ToInt32(Math.Ceiling((double)_db.Questions.Where(q => q.CreatedBy == user).Count() / PageSize));
                ViewBag.CurrentQuestionPage = pageIndex;
                return View("_MyQuestions", page);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult GetAnswers(int pageIndex)
        {
            if (Session["userName"] != null)
            {
                string user = Session["userName"].ToString();
                page.MyAnswers = _db.Answers.Where(q => q.AnsweredBy == user).ToList().Skip(PageSize * (pageIndex - 1)).Take(PageSize);
                page.AnswerPages = Convert.ToInt32(Math.Ceiling((double)_db.Answers.Where(q => q.AnsweredBy == user).Count() / PageSize));
                ViewBag.CurrentAnswerPage = pageIndex;
                return View("_MyAnswers", page);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}