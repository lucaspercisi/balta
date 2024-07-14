using System.Text.Json.Serialization;

namespace FullStack.Core.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configuration.DefaultPageSize)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(TData? data, int code = Configuration.DefaultStatusCode, string message = null!) : base(data, code, message) { }

        public int CurrentPage { get; set; }
        public int TotalPage => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
        public int TotalCount { get; set; }
    }
}
