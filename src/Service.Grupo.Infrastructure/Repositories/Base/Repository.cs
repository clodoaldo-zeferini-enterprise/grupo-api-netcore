
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Service.Grupo.Domain.Enum;
using Service.Grupo.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static Service.Grupo.Infrastructure.Repositories.Base.DapperUtils;
using Service.Grupo.Domain.Base;

namespace Service.Grupo.Infrastructure.Repositories.Base
{

    public abstract class Repository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly string _connectionString;

        private static PropertyContainer GetPropertyContainer(TEntity entity)
        {
             var propertyContainer = ParsePropertiesInsert(entity);
            
            return propertyContainer;
        }

        protected Repository(EDataBaseConnection eDataBaseConnection, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(Convert.ToString((int)eDataBaseConnection));
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultiple<T1, T2>(DapperQuery dapperQuery, object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2)
        {
            var objs = ObterMultiple(dapperQuery, parameters, func1, func2);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultiple<T1, T2, T3>(DapperQuery dapperQuery, object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3)
        {
            var objs = ObterMultiple(dapperQuery, parameters, func1, func2, func3);
            return Tuple.Create(
                  objs[0] as IEnumerable<T1>
                , objs[1] as IEnumerable<T2>
                , objs[2] as IEnumerable<T3>);
        }

        private List<object> ObterMultiple(DapperQuery dapperQuery, object parameters, params Func<GridReader, object>[] readerFuncs)
        {
            var returnResults = new List<object>();
            using (var con = new SqlConnection(_connectionString))
            {
                try
                {
                    con.Open();
                    var gridReader = con.QueryMultiple(dapperQuery.Query, parameters);

                    foreach (var readerFunc in readerFuncs)
                    {
                        var obj = readerFunc(gridReader);
                        returnResults.Add(obj);
                    }
                }
                finally { con.Close(); con.Dispose(); }
            }

            return returnResults;
        }


        public virtual async Task<IEnumerable<TEntity>> Get(DapperQuery dapperQuery) 
        {
            await using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return await con.QueryAsync<TEntity>(dapperQuery.Query);
            }
            finally
            {
                con.Close();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return await con.GetAllAsync<TEntity>();
            }
            finally
            {
                con.Close();
            }
        }

        public virtual IEnumerable<TEntity> GetSync(DapperQuery dapperQuery)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return con.Query<TEntity>(dapperQuery.Query);
            }
            finally
            {
                con.Close();
            }
        }

        public virtual IEnumerable<TEntity> GetAllSync()
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return con.GetAll<TEntity>();
            }
            finally
            {
                con.Close();
            }
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            await using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return await con.GetAsync<TEntity>(id);
            }
            finally
            {
                con.Close();
            }
        }

        public TEntity GetByIdSync(Guid id)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return con.Get<TEntity>(id);
            }
            finally
            {
                con.Close();
            }
        }
        
        public virtual async Task<bool> Insert(TEntity entity)
        {
            PropertyContainer propertyContainer = GetPropertyContainer(entity);

            var sql = string.Format("INSERT INTO [{0}] ({1}) VALUES (@{2})",
            typeof(TEntity).Name,
            string.Join(", ", propertyContainer.ValueNames),
            string.Join(", @", propertyContainer.ValueNames));

            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var result = await con.ExecuteAsync(sql, propertyContainer.ValuePairs, commandType: System.Data.CommandType.Text);

                return (result > 0);
            }
            finally
            {
                con.Close();
            }
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            await using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return await con.UpdateAsync<TEntity>(entity);
            }
            finally
            {
                con.Close();
            }
        }

        public bool UpdateSync(TEntity entity)
        {
            using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return con.Update<TEntity>(entity);
            }
            finally
            {
                con.Close();
            }
        }

        public virtual async Task<bool> Delete(TEntity entity)
        {
            await using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                return await con.DeleteAsync<TEntity>(entity);
            }
            finally
            {
                con.Close();
            }
        }

        public virtual async Task<int> ExecuteCommand(DapperQuery dapperQuery)
        {
            await using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var resultado = (await con.ExecuteAsync(dapperQuery.Query));
                return resultado;
            }
            finally
            {
                con.Close();
            }
        }

        public virtual async Task<bool> InsertCommand(DapperQuery dapperQuery)
        {
            await using var con = new SqlConnection(_connectionString);
            try
            {
                con.Open();
                var rowsAffected = await con.ExecuteAsync(dapperQuery.Query);
                return (rowsAffected > 0);
            }
            finally
            {
                con.Close();
            }
        }
    }
}