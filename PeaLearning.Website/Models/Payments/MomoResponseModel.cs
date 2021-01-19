using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeaLearning.Website.Models.Payments
{
    public class MomoRequestModel
    {
        public string accessKey { get; set; }
        public string partnerCode { get; set; }
        public string requestType { get; set; }
        public string notifyUrl { get; set; }
        public string returnUrl { get; set; }
        public string orderId { get; set; }
        public string amount { get; set; }
        public string orderInfo { get; set; }
        public string requestId { get; set; }
        public string extraData { get; set; }
        public string signature { get; set; }
    }
    public class MomoResponseModel
    {
        public string requestId { get; set; }
        public int errorCode { get; set; }
        public string orderId { get; set; }

        public string localMessage { get; set; }
        public string requestType { get; set; }
        public string payUrl { get; set; }
        public string signature { get; set; }
    }
    public class MomoResult
    {

    }
}
