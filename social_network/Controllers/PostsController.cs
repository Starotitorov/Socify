using Microsoft.AspNet.Identity;
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
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const int perPage = 2;

        [HttpPost]
        public ActionResult Create(NewPostViewModel newPostModel)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (newPostModel.Photo != null)
            {
                var post = new Post
                {
                    Body = newPostModel.Body,
                    SenderId = user.Id,
                    PublicationTime = DateTime.Now,
                };
                var photo = new Photo()
                {
                    Path = SaveFile(newPostModel.Photo, user.UserName),
                    ContentType = newPostModel.Photo.ContentType,
                    PostId = post.Id
                };
                db.Posts.Add(post);
                db.Photos.Add(photo);
                db.SaveChanges();

                var photoModel = new PostViewModel()
                {
                    Id = post.Id,
                    Body = post.Body,
                    PublicationTime = post.PublicationTime.ToString(),
                    SenderFullName = db.Users.Find(user.Id).FullName,
                    SenderId = post.SenderId,
                    Likes = db.Likes.Where(l => l.PostId == post.Id)
                };
                return Json(photoModel);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var postViewModels = db.Posts.OrderByDescending(p => p.Id).Take(perPage).ToList().Select(p => new PostViewModel()
            {
                Id = p.Id,
                Body = p.Body,
                SenderFullName = p.Sender.FullName,
                PublicationTime = p.PublicationTime.ToString(),
                Likes = db.Likes.Where(l => l.PostId == p.Id)
            });
            if (postViewModels.Count() < perPage)
            {
                ViewBag.lastId = -1;
            }
            else
            {
                ViewBag.lastId = postViewModels.Last().Id;
            }
            return View(postViewModels);
        }

        [HttpPost]
        public ActionResult More(int lastId)
        {
            var postViewModels = db.Posts.OrderByDescending(p => p.Id).Where(p => p.Id < lastId).Take(perPage).ToList().Select(p => new PostViewModel()
            {
                Id = p.Id,
                Body = p.Body,
                SenderId = p.SenderId,
                SenderFullName = p.Sender.FullName,
                PublicationTime = p.PublicationTime.ToString(),
                Likes = db.Likes.Where(l => l.PostId == p.Id)
            });
            if (postViewModels.Count() < perPage)
            {
                lastId = -1;
            }
            else
            {
                lastId = postViewModels.Last().Id;
            }
            return Json(new { lastId = lastId, posts = postViewModels }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult Destroy(int postId)
        {
            var post = db.Posts.Find(postId);
            if (post != null)
            {
                if (User.Identity.GetUserId() != post.SenderId)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.Posts.Remove(post);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            else {
                return HttpNotFound();
            }
        }

        private string SaveFile(HttpPostedFileBase file, string userName)
        {
            if (file != null)
            {
                string photoName = System.IO.Path.GetFileName(file.FileName);
                string filename = Path.GetRandomFileName();
                string path = "~/PostedImages/" + userName + "/" + filename;
                string physicalPath = Server.MapPath(path);

                new FileInfo(Server.MapPath("~/PostedImages/" + userName + "/")).Directory.Create();
                file.SaveAs(physicalPath);

                return path;
            }
            return "";
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}