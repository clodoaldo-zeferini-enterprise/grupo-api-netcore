using Service.Grupo.Application.Base;
using Service.Grupo.Application.Models.Enum;
using System;

namespace Service.Grupo.Application.Models.Request.Grupo
{
    public class GetGrupoRequest : RequestBase
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


        private void Validate()
        {
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid isSysUsuSessionIdValido);
            var IsRequestIdValido = Guid.TryParse(RequestId.ToString(), out Guid isRequestIdValido);
            var IsGrupoIdValido = Guid.TryParse(GrupoId.ToString(), out Guid isGrupoIdValido);
            
            var IsEmpresaIdValido = (EmpresaId == null) ? true : Guid.TryParse(EmpresaId.ToString(), out Guid isEmpresaIdValido);


            DateTime dataAuxiliar;

            if ((FiltraDataInsert) && (DataInicial > DataFinal))
            {
                dataAuxiliar = DataInicial.Value;
                DataInicial = DataFinal.Value;
                DataFinal = dataAuxiliar;
            }

            if (PageSize > 50) PageSize = 50;

            ValidadorDeRegra.Novo()
                .Quando(!IsSysUsuSessionIdValido, Base.Resource.SysUsuSessionIdInvalido)
                .Quando(!IsRequestIdValido, Base.Resource.RequestIdInvalido)
                .Quando(!IsGrupoIdValido, Base.Resource.GrupoIdInvalido)
                .Quando((FiltraNome && (FiltroNome == null || FiltroNome.Length == 0 || FiltroNome.Length > 100)), Resource.FiltroNomeInvalido)
                .Quando(((!FiltraNome && (FiltroNome != null && FiltroNome.Length != 0))), Resource.FiltroNomeInvalido)
                .Quando((FiltraDataInsert && (DataInicial == null)), Resource.DataInicialInvalida)
                .Quando((FiltraDataInsert && (DataFinal == null)), Resource.DataFinalInvalida)
                .DispararExcecaoSeExistir();            
        }

        private GetGrupoRequest()
        {
        }

        public GetGrupoRequest(Guid empresaId)
        {
            EmpresaId = empresaId;
        }

        public GetGrupoRequest(
            ushort pageNumber, ushort pageSize, 
            bool filtraNome, string filtroNome, 
            bool filtraDataInsert, DateTime? dataInicial, DateTime? dataFinal, 
            bool filtraStatus, EStatus status,
            Guid grupoId, Guid? empresaId
        )
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            FiltraNome = filtraNome;
            FiltroNome = filtroNome;
            FiltraDataInsert = filtraDataInsert;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            FiltraStatus = filtraStatus;
            GrupoId = grupoId;
            EmpresaId = empresaId;
        }
    }
}
