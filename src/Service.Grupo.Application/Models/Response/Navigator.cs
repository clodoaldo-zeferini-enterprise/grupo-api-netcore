namespace Service.Grupo.Application.Models.Response
{
    public class Navigator
    {
        public int RecordCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int PageCount { get; private set; }

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
