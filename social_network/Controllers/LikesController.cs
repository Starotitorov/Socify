using Microsoft.AspNet.Identity;
using social_network.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace social_network.Controllers
{
    [Authorize]
    public class LikesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Create(int postId)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (db.Photos.Find(postId) == null)
            {
                return HttpNotFound();
            }
            if (db.Likes.Where(l => l.PostId == postId && l.VoterId == user.Id).Count() != 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var like = new Like
            {
                PostId = postId,
                VoterId = user.Id,
                VotingTime = DateTime.Now
            };
            db.Likes.Add(like);
            db.SaveChanges();
            return Json(db.Likes.Where(l => l.PostId == postId).Count());
        }

        [HttpDelete]
        public ActionResult Destroy(int postId)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var like = db.Likes.FirstOrDefault(l => l.PostId == postId && l.VoterId == user.Id);
            if (like == null)
            {
                return HttpNotFound();
            }
            db.Likes.Remove(like);
            db.SaveChanges();
            return Json(db.Likes.Where(l => l.PostId == postId).Count());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}