namespace Service.Grupo.Application.Models.Response
{
    public class Navigator
    {
        public int RecordCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

        public Navigator()
        {
        }

        public Navigator(int recordCount, int pageNumber, int pageSize, int pageCount)
        {
            RecordCount = recordCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            PageCount = pageCount;
        }
    }
}
