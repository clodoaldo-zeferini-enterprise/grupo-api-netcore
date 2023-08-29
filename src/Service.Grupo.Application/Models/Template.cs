using Service.Grupo.Application.Base;
using Service.Grupo.Domain.Enum;
using System;

namespace Service.Grupo.Application.Models
{
    public class Grupo : Base.Base 
    {
        public string Nome { get; set; }

        private void Validate()
        {
            var IsIdValido = Guid.TryParse(Id.ToString(), out Guid idValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsIdValido, Resource.IdInvalido)
                .Quando((string.IsNullOrEmpty(Nome) || Nome.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
        }
        private Grupo()
        {
        }

        public Grupo(Guid id)
        {
            Id = id;

            var IsIdValido = Guid.TryParse(Id.ToString(), out Guid idValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsIdValido, Resource.IdInvalido)
                .DispararExcecaoSeExistir();

        }

        public Grupo(Guid id, string nome, EStatus status, Guid sysUsuSessionId,  DateTime? dataInsert, DateTime? dataUpdate)
        {
            Id = id;
            Nome = nome;
            Status = status;
            DataInsert = dataInsert;
            DataUpdate = dataUpdate;
            SysUsuSessionId = sysUsuSessionId;

            var IsIdValido = Guid.TryParse(Id.ToString(), out Guid idValido);
            var IsSysUsuSessionIdValido = Guid.TryParse(sysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsIdValido, Resource.IdInvalido)
                .Quando(!IsSysUsuSessionIdValido, Resource.SysUsuSessionIdInvalido)
                .Quando((string.IsNullOrEmpty(Nome) || Nome.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();

        }
    }
}
