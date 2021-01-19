using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Website.Models.MyAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaLearning.Website.Components
{
    public class ProfileStatisticViewComponent : BaseViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public ProfileStatisticViewComponent(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
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
    }
}
