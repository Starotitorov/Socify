using social_network.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace social_network.Controllers
{
    public class PhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Show(int postId)
        {
            var photo = await db.Photos.FirstOrDefaultAsync(p => p.PostId == postId);
            if (photo != null)
            {
                return File(Server.MapPath(photo.Path), photo.ContentType);
            }
            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}