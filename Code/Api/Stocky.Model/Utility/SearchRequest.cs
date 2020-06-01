using System;

namespace Stocky.Model.Utility
{
    [Serializable]
    public class SearchRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string SearchTerm { get; set; }
        public string OrderBy { get; set; }

        public SearchRequest()
        {
            Skip = 0;
            Take = 0;
            SearchTerm = "";
            OrderBy = "asc";
        }
    }
}
