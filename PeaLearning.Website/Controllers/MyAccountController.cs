using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Common.Enumerations;
using PeaLearning.Common.Utils;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Website.Extensions;
using PeaLearning.Website.Models;
using PeaLearning.Website.Models.MyAccount;
using Shared.Constants;

namespace PeaLearning.Website.Controllers
{
    [Authorize]
    public class MyAccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public MyAccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("profile")]
        public async Task<IActionResult> Profile()
        {
            ViewBag.TitlePage = SEO.AddTitle("Thông tin cá nhân");
            var userModel = await _userManager.FindByIdAsync(CurrentUser.Id.ToString());
            UserStatisticModel userStatisticModel = new UserStatisticModel()
            {
                Email = userModel.Email,
                Phone = userModel.PhoneNumber,
                Score = 0,
                UpdatedDate = DateTime.Now,
                DisplayName = userModel.DisplayName
            };
            return View(userStatisticModel);
        }

        [Route("change-profile")]
        public async Task<IActionResult> ChangeProfile()
        {
            ViewBag.TitlePage = SEO.AddTitle("Cập nhật thông tin cá nhân");
            var userModel = await _userManager.FindByIdAsync(CurrentUser.Id.ToString());
            UserProfileModel userProfileModel = new UserProfileModel()
            {
                Address = userModel.Address,
                Email = userModel.Email,
                Id = userModel.Id,
                PhoneNumber = userModel.PhoneNumber,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName
            };
            return View(userProfileModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserProfileModel model)
        {
            var msg = new CommonResultModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            user.Address = model.Address;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                msg.NextAction = (int)NextAction.Message;
                msg.Title = "Cập nhật thất bại";
                msg.Error = true;
            }
            else
            {
                msg.NextAction = (int)NextAction.Reload;
                msg.Title = "Cập nhật thành công";
            }
            return Json(msg);
        }
    }
}
