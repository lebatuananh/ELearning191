using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeaLearning.Application.Queries;
using PeaLearning.Common;
using PeaLearning.Common.Utils;
using PeaLearning.Website.Extensions;
using PeaLearning.Website.Models.Common;
using PeaLearning.Website.Models.Payments;
using System;
using System.Threading.Tasks;
using static PeaLearning.Common.Constants;

namespace PeaLearning.Website.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ILogger<CourseController> _logger;
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator, ILogger<CourseController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Route("khoa-hoc/p/{pageIndex}")]
        [Route("khoa-hoc")]
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            int pageSize = 6;
            int skip = (pageIndex - 1) * pageSize;
            var courses = await _mediator.Send(new ListCoursesQuery(skip, pageSize));
            #region paging
            if (courses.Count > 0)
            {
                PaginationModel pageModel = new PaginationModel
                {
                    PageIndex = pageIndex,
                    Count = courses.Count,
                    LinkSite = CoreUtils.GetCurrentURL(Request.Path.ToString(), pageIndex),
                    PageSize = pageSize
                };
                ViewBag.PagingInfo = pageModel;
            }
            #endregion

            ViewBag.TitlePage = SEO.AddTitle(Meta.CourseTitle);
            ViewBag.MetaDescription = SEO.AddMeta("description", Meta.CourseDesc);
            return View(courses.Items);
        }

        [Route("khoa-hoc/{title}-{code}")]
        public async Task<IActionResult> Detail(int code)
        {
            var lstLesson = await _mediator.Send(new ListLessonByCourseQuery(code));
            ViewBag.TitlePage = SEO.AddTitle(Meta.CourseListTitle);
            ViewBag.MetaDescription = SEO.AddMeta("description", Meta.CourseListDesc);
            return View(lstLesson);
        }
        [Authorize]
        [Route("mua-khoa-hoc/{title}/{code}")]
        public async Task<IActionResult> BuyCourse(string code)
        {
            var id = Guid.Parse(code);
            var courses = await _mediator.Send(new GetCourseQuery(id));
            ViewBag.TitlePage = SEO.AddTitle(courses.Title);
            ViewBag.MetaDescription = SEO.AddMeta("description", courses.Description);
            return View(courses);
        }


        [Authorize]
        [Route("checkout/{code}")]
        public async Task<IActionResult> Checkout(string code)
        {
            var id = Guid.Parse(code);
            var courses = await _mediator.Send(new GetCourseQuery(id));


            //request params need to request to MoMo system
            string endpoint = StaticVariable.MomoConfigs.RequestUrl;
            string partnerCode = StaticVariable.MomoConfigs.PartnerCode;
            string accessKey = StaticVariable.MomoConfigs.AccessKey;
            string serectkey = StaticVariable.MomoConfigs.SecretKey;
            string orderInfo = courses.Title;

            string returnUrl = string.Format("{0}/notify", StaticVariable.Domain);
            string notifyurl = string.Format("{0}/return", StaticVariable.Domain);

            string amount = courses.Price.ToString();
            string orderid = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            MomoRequestModel message = new MomoRequestModel
            {
                partnerCode = partnerCode,
                accessKey = accessKey,
                requestId = requestId,
                amount = amount,
                orderId = orderid,
                orderInfo = orderInfo,
                returnUrl = returnUrl,
                notifyUrl = notifyurl,
                extraData = extraData,
                requestType = "captureMoMoWallet",
                signature = signature
            };
            _logger.LogInformation("Return from MoMo: " + message.ToString());

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, JsonConvert.SerializeObject(message));
            _logger.LogInformation("Return from MoMo: " + responseFromMomo);
            try
            {
                var jmessage = JsonConvert.DeserializeObject<MomoResponseModel>(responseFromMomo);
                _logger.LogInformation("Return from MoMo: " + jmessage.ToString());
                if (jmessage != null && !string.IsNullOrEmpty(jmessage.payUrl))
                    return Redirect(jmessage.payUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Json(courses);
        }

        [Route("notify")]
        public async Task<IActionResult> Notify()
        {
            string partnerCode = HttpContext.Request.Query["partnerCode"].ToString();
            Console.WriteLine(partnerCode);
            return View();
        }
    }
}
