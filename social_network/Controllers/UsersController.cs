using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using social_network.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace social_network.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<RoleManager<IdentityRole>>();
            }
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Users.AsEnumerable().OrderBy(u => u.FullName));
        }

        [HttpPost]
        public ActionResult Search(SearchModel searchModel)
        {
            IQueryable<ApplicationUser> users = UserManager.Users;
            if (!String.IsNullOrEmpty(searchModel.FirstName))
                users = users.Where(u => u.FirstName.Contains(searchModel.FirstName));
            if (!String.IsNullOrEmpty(searchModel.LastName))
                users = users.Where(u => u.LastName.Contains(searchModel.LastName));
            if (searchModel.MinAge != null)
                users = users.Where(u => DbFunctions.DiffYears(u.BirthDate, DateTime.Today) >= searchModel.MinAge);
            if (searchModel.MaxAge != null)
                users = users.Where(u => DbFunctions.DiffYears(u.BirthDate, DateTime.Today) <= searchModel.MaxAge);
            if (!String.IsNullOrEmpty(searchModel.Gender))
                users = users.Where(u => u.Gender == searchModel.Gender);
            if (!String.IsNullOrEmpty(searchModel.HomeTown))
                users = users.Where(u => u.HomeTown == searchModel.HomeTown);
            return PartialView(users.AsEnumerable().OrderBy(u => u.FullName));
        }

        [HttpGet]
        public ActionResult Show(string id)
        {
            UserViewModel userModel = null;
            ApplicationUser user = db.Users.Find(id);
            if (user != null)
            {
                userModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Gender = user.Gender,
                    HomeTown = user.HomeTown
                };
                userModel.PostModels = db.Posts.Where(p => p.SenderId == user.Id).OrderByDescending(p => p.Id).Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    PublicationTime = p.PublicationTime.ToString(),
                    Body = p.Body,
                    SenderId = user.Id,
                    SenderFullName = user.FullName,
                    Likes = db.Likes.Where(l => l.PostId == p.Id)
                });
                return View(userModel);
            }
            return RedirectToAction("Login", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}