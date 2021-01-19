using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Application.Queries;
using PeaLearning.Common.Utils;
using PeaLearning.Website.Models.Common;
using static PeaLearning.Common.Constants;

namespace PeaLearning.Website.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("tin-tuc/p{pageIndex}")]
        [Route("tin-tuc")]
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            int pageSize = 6;
            int skip = (pageIndex - 1) * pageSize;
            var blogs = await _mediator.Send(new ListBlogsQuery(skip, pageSize));
            #region paging
            if (blogs.Count > 0)
            {
                PaginationModel pageModel = new PaginationModel
                {
                    PageIndex = pageIndex,
                    Count = blogs.Count,
                    LinkSite = CoreUtils.GetCurrentURL(Request.Path.ToString(), pageIndex),
                    PageSize = pageSize
                };
                ViewBag.PagingInfo = pageModel;
            }
            #endregion

            ViewBag.TitlePage = SEO.AddTitle(Meta.BlogTitle);
            ViewBag.MetaDescription = SEO.AddMeta("description", Meta.BlogDesc);
            return View(blogs.Items);
        }

        [Route("tin-tuc/{name}-aid{blogId}")]
        public async Task<IActionResult> Detail(Guid blogId)
        {
            var blog = await _mediator.Send(new GetBlogQuery(blogId));          
            return View(blog);
        }
    }
}
