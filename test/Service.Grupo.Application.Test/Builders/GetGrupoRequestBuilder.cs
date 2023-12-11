using Bogus;
using Service.Grupo.Application.Models.Enum;
using Service.Grupo.Application.Models.Request.Grupo;

namespace Service.Grupo.Application.Test.Builders
{
    public class GetGrupoRequestBuilder : Service.Grupo.Application.Base.Base
    {
        public UInt16 PageNumber { get; private set; }
        public UInt16 PageSize { get; private set; }
        public bool FiltraNome { get; private set; }
        public string FiltroNome { get; private set; }
        public bool FiltraDataInsert { get; private set; }
        public DateTime? DataInicial { get; private set; }
        public DateTime? DataFinal { get; private set; }
        public bool FiltraStatus { get; private set; }

        public Guid GrupoId { get; private set; }
        public Guid? EmpresaId { get; private set; }


        private GetGrupoRequest getEmpresaRequest;

        public static GetGrupoRequestBuilder Novo()
        {
            var eStatus = new Application.Models.Enum.EStatus[] { Application.Models.Enum.EStatus.ATIVO, Application.Models.Enum.EStatus.INATIVO, Application.Models.Enum.EStatus.EXCLUIDO };
            var getEmpresaBuilderFaker = new Faker<GetGrupoRequestBuilder>("pt_BR")                
                .RuleFor(u => u.SysUsuSessionId, f => Guid.NewGuid())
                .RuleFor(u => u.RequestId, f => Guid.NewGuid())
                .RuleFor(u => u.GrupoId, f => Guid.NewGuid())
                .RuleFor(u => u.EmpresaId, f => Guid.NewGuid())
                .RuleFor(u => u.FiltraStatus, f => f.Random.Bool())
                .RuleFor(u => u.Status, f => f.PickRandom(eStatus))
                .RuleFor(u => u.PageNumber, f => f.Random.UInt())
                .RuleFor(u => u.PageSize, f => f.Random.UInt())
                .RuleFor(u => u.FiltraNome, f => f.Random.Bool())
                .RuleFor(u => u.FiltroNome, f => f.Company.CompanyName())
                .RuleFor(u => u.FiltraDataInsert, f => f.Random.Bool())
                .RuleFor(u => u.DataInicial, f => f.Date.Soon())
                .RuleFor(u => u.DataFinal  , f => f.Date.Soon())
                ;

            var getEmpresaBuilders = getEmpresaBuilderFaker.Generate(1);

            return getEmpresaBuilders[0];
        }        
        
        public GetGrupoRequestBuilder ComGrupoId(Guid grupoId)
        {
            this.GrupoId = grupoId;
            return this;
        }

        public GetGrupoRequestBuilder ComEmpresaId(Guid empresaId)
        {
            this.EmpresaId = empresaId;
            return this;
        }        

        public GetGrupoRequestBuilder ComPageNumber(UInt16 pageNumber)
        {
            PageNumber = pageNumber;
            return this;
        }

        public GetGrupoRequestBuilder ComPageSize(UInt16 pageSize)
        {
            PageSize = pageSize;
            return this;
        }
        public GetGrupoRequestBuilder ComFiltraNome(bool filtraNome)
        {
            FiltraNome = filtraNome;
            return this;
        }

        public GetGrupoRequestBuilder ComFiltraDataInsert(bool filtraDataInsert, DateTime dataInicial, DateTime dataFinal)
        {
            FiltraDataInsert = filtraDataInsert;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            return this;
        }

        public GetGrupoRequestBuilder ComFiltraStatus(bool filtraStatus, EStatus status)
        {
            FiltraStatus = filtraStatus;
            Status = status;
            return this;
        }

        public Service.Grupo.Application.Models.Request.Grupo.GetGrupoRequest Build()
        {
            getEmpresaRequest = new GetGrupoRequest(PageNumber, PageSize, FiltraNome, FiltroNome, FiltraDataInsert, DataInicial, DataFinal, FiltraStatus, Status, GrupoId, EmpresaId);

            return getEmpresaRequest;
        }
    }
}
