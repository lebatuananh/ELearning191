using Microsoft.AspNetCore.Mvc;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Website.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaLearning.Website.Components
{
    public abstract class BaseViewComponent : ViewComponent
    {
        public User CurrentUser => HttpContext.User.CurrentUser();
    }
}
