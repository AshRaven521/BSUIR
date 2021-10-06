using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_953506_KONDRASHOV.Entities;

namespace WEB_953506_KONDRASHOV.Controllers
{
    public class ImageController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IWebHostEnvironment env;

        public ImageController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.env = env;
        }

        public async Task<FileResult> GetAvatar()
        {
            var user = await userManager.GetUserAsync(User);

            if(user.AvatarImage != null)
            {
                return File(user.AvatarImage, "image/...");
            }
            else
            {
                var avatarPath = "/Images/anonymous.png";

                return File(env.WebRootFileProvider.GetFileInfo(avatarPath).CreateReadStream(), "image/...");
            }
        }
    }
}
