using Microsoft.Extensions.Configuration;
using Service.Grupo.Domain.Enum;
using Service.Grupo.Repository.Interfaces.Repositories.DB;

namespace Service.Grupo.Infrastructure.Repositories.DB
{
    public class GrupoRepository : Repositories.Base.Repository<Service.Grupo.Domain.Entities.Grupo>, IGrupoRepository
    {
        public GrupoRepository(IConfiguration configuration) : base(EDataBaseConnection.ID_DB_Grupo, configuration)
        {

        }
    }
}
