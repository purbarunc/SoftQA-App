using Softvision.QA.App.Helpers;
using Softvision.QA.App.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Softvision.QA.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly QaDbContext _db = new QaDbContext();
        private int pageSize = 4;
        Paging page = new Paging();

        [AllowAnonymous]
        public ActionResult Index()
        {
            page.Questions = _db.Questions.Include("Tags").Include("Answers").ToList().Take(pageSize);
            ViewBag.QuestionList = page;
            return View();
        }

        [AllowAnonymous]
        public ActionResult AllUsers()
        {
            StringBuilder sb = new StringBuilder();
            using (var context = new PrincipalContext(ContextType.Domain, "10.0.103.200"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        sb.Append("First Name: " + de.Properties["givenName"].Value + "</br>");
                        sb.Append("Last Name : " + de.Properties["sn"].Value + "</br>");
                        sb.Append("SAM account name   : " + de.Properties["samAccountName"].Value + "</br>");
                        sb.Append("User principal name: " + de.Properties["userPrincipalName"].Value + "</br>");
                        sb.Append("----------------------------------------------------------------------</br>");
                    }
                }
            }
            return Content(sb.ToString());
        }

        public ActionResult FetchData(int pageIndex)
        {
            System.Threading.Thread.Sleep(1000);
            page.Questions = _db.Questions.Include("Tags").Include("Answers").ToList().Skip(pageIndex * pageSize).Take(pageSize);
            return View("_QuestionList", page);
        }

    }
}