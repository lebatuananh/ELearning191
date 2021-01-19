using System;

namespace PeaLearning.Website.Models.Common
{
    [Serializable]
    public class PaginationModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string LinkSite { get; set; }
        public long Count { get; set; }
    }

    [Serializable]
    public class PagingEntity
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int NumPage { get; set; }
    }
}
