using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Grupo.Application.Interfaces
{
    public interface IUseCaseReq<in TRequest>
    {
        void Execute(TRequest request);
    }

    public interface IUseCase<in TRequest, out TResponse>
    {
        TResponse Execute(TRequest request);
    }

    public interface IUseCaseResp<out TResponse>
    {
        TResponse Execute();
    }

    public interface IUseCaseReqAsync<in TRequest>
    {
        Task ExecuteAsync(TRequest request);
    }

    public interface IUseCaseAsync<in TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }

    public interface IUseCaseAsyncParameter<in TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request, int parameter);
    }

    public interface IUseCaseRespAsync<TResponse>
    {
        Task<TResponse> ExecuteAsync();
    }
}
