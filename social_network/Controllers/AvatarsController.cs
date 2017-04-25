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
    public class AvatarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
 
        public async Task<ActionResult> Show(string userId)
        {
            var avatar = await db.Avatars.FirstOrDefaultAsync(a => a.UserId == userId);
            if (avatar != null)
            {
                return File(Server.MapPath(avatar.Path), avatar.ContentType);
            }
            return File(Server.MapPath("~/Content/Images/w3images/avatar2.png"), "image/png");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}