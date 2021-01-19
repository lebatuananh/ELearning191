using Microsoft.AspNetCore.Mvc;
using PeaLearning.Website.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaLearning.Website.Components
{
    public class PagingViewComponent : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PaginationModel pagingInfo)
        {
            int pageNum = (int)Math.Ceiling((double)pagingInfo.Count / pagingInfo.PageSize);
            if (pageNum * pagingInfo.PageSize < pagingInfo.Count)
            {
                pageNum++;
            }
            pagingInfo.LinkSite = pagingInfo.LinkSite.TrimEnd('/') + "/";
            const string buildLink = "<li class='page-item {2}'><a class='page-link' href='{0}{1}' >{3}</a> </li>";
            const string active = "active";
            const string prev_page = "class='prev-page'";
            const string next_page = "class='next-page'";
            const string strTrang = "p";
            string currentPage = pagingInfo.PageIndex.ToString();
            StringBuilder htmlPage = new StringBuilder();
            int iCurrentPage = 0;
            if (pagingInfo.PageIndex <= 0) iCurrentPage = 1;
            else iCurrentPage = pagingInfo.PageIndex;
            if (pageNum <= 3)
            {
                if (pageNum != 1)
                {
                    for (int i = 1; i <= pageNum; i++)
                    {
                        if (i == 1)
                        {
                            htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite.TrimEnd('/'), string.Empty, i == pagingInfo.PageIndex ? active : string.Empty, i);
                        }
                        else
                        {
                            htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite, strTrang + i, i == pagingInfo.PageIndex ? active : string.Empty, i);
                        }
                    }
                }
            }
            else
            {
                if (iCurrentPage == 1)
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        if (i == 1)
                        {
                            htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite.TrimEnd('/'), "", i == pagingInfo.PageIndex ? active : string.Empty, i);
                        }
                        else
                        {
                            htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite, strTrang + i, i == pagingInfo.PageIndex ? active : string.Empty, i);
                        }
                    }
                    htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite, strTrang + 2, next_page, "Next");
                }
                if (iCurrentPage > 1 && iCurrentPage < pageNum)
                {
                    if (iCurrentPage > 1)
                    {
                        if (iCurrentPage - 1 == 1)
                        {
                            htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite.TrimEnd('/'), string.Empty, prev_page, "Prev");
                        }
                        else
                        {
                            htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite, strTrang + (iCurrentPage - 1), prev_page, "Prev");
                        }
                    }
                    for (int i = (iCurrentPage - 1); i <= (iCurrentPage + 1 < pageNum ? iCurrentPage + 1 : pageNum); i++)
                    {
                        if (i > 0)
                        {
                            if (i == 1)
                            {
                                htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite.TrimEnd('/'), "", i == pagingInfo.PageIndex ? active : string.Empty, i);
                            }
                            else
                            {
                                htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite, strTrang + i, i == pagingInfo.PageIndex ? active : string.Empty, i);
                            }
                        }
                    }
                    if (iCurrentPage <= pageNum - 1)
                    {
                        htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite, strTrang + (iCurrentPage + 1), next_page, "Next");
                    }
                }
                int intCurrentPage = 0;
                int.TryParse(currentPage, out intCurrentPage);
                if (intCurrentPage == 0) intCurrentPage = 1;
                if (iCurrentPage == pageNum)
                {
                    htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite, strTrang + (iCurrentPage - 1), prev_page, "Prev");
                    int j = 2;
                    for (int i = pageNum; i >= pageNum - 2; i--)
                    {
                        htmlPage.AppendFormat(buildLink, pagingInfo.LinkSite, strTrang + (pageNum - j), j == 0 ? active : string.Empty, pageNum - j);
                        j--;
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ul class='pagination justify-content-center mb-0'>{0}</ul>", htmlPage);
            return View("Default",sb.ToString());
        }
    }
}
