using Service.Grupo.Application.Base;
using Service.Grupo.Domain.Enum;
using System;

namespace Service.Grupo.Application.Models.Request.Grupo
{
    public class GetGrupoRequest : RequestBase
    {
        public UInt16 PageNumber { get; set; }
        public UInt16 PageSize { get; set; }
        public Guid? GrupoId { get; set; }
        public bool FiltraNome { get; set; }
        public string FiltroNome { get; set; }
        public bool FiltraDataInsert { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public bool FiltraStatus { get; set; }
        public EStatus Status { get; set; }


        private void Validate()
        {
            var IsGrupoIdValido = Guid.TryParse(GrupoId.ToString(), out Guid idValido);
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsGrupoIdValido, Resource.IdInvalido)
                .Quando(!IsSysUsuSessionIdValido, Resource.SysUsuSessionIdInvalido)
                .Quando((FiltraNome && (FiltroNome == null || FiltroNome.Length == 0 || FiltroNome.Length > 100)), Resource.FiltroNomeInvalido)
                .Quando(((!FiltraNome && (FiltroNome != null && FiltroNome.Length != 0))), Resource.FiltroNomeInvalido)
                .Quando((FiltraDataInsert && (DataInicial == null)), Resource.DataInicialInvalida)
                .Quando((FiltraDataInsert && (DataFinal == null)), Resource.DataFinalInvalida)
                .DispararExcecaoSeExistir();

            DateTime dataAuxiliar;

            if ((FiltraDataInsert) && (DataInicial > DataFinal))
            {
                dataAuxiliar = DataInicial.Value;
                DataInicial = DataFinal.Value;
                DataFinal = dataAuxiliar;
            }
        }


        public GetGrupoRequest()
        {
        }

        public GetGrupoRequest(Guid grupoId)
        {
            GrupoId = grupoId;
        }

        public GetGrupoRequest(UInt16 pageNumber, UInt16 pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
