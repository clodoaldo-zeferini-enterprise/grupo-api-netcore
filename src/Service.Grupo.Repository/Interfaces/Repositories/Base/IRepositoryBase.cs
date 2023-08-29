using Service.Grupo.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Service.Grupo.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultiple<T1, T2>(DapperQuery dapperQuery, object parameters,
            Func<GridReader, IEnumerable<T1>> func1, Func<GridReader, IEnumerable<T2>> func2);

        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultiple<T1, T2, T3>(DapperQuery dapperQuery, object parameters,
            Func<GridReader, IEnumerable<T1>> func1,
            Func<GridReader, IEnumerable<T2>> func2,
            Func<GridReader, IEnumerable<T3>> func3);

        Task<IEnumerable<TEntity>> Get(DapperQuery dapperQuery);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task<bool> InsertCommand(DapperQuery dapperQuery);
        Task<bool> Insert(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<int> ExecuteCommand(DapperQuery dapperQuery);
        IEnumerable<TEntity> GetSync(DapperQuery dapperQuery);
        IEnumerable<TEntity> GetAllSync();
        TEntity GetByIdSync(Guid id);
        bool UpdateSync(TEntity entity);

    }

}
