using PeaLearning.Domain.AggregateModels.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PeaLearning.Website.Extensions;

namespace PeaLearning.Website.Controllers
{
    public class BaseController : Controller
    {
        private ISession _session;

        public User CurrentUser => HttpContext.User.CurrentUser();

        public ISession Session
        {
            get
            {
                if (_session == null)
                    _session = HttpContext.Session;
                return _session;
            }
            set => _session = value;
        }
    }
}
