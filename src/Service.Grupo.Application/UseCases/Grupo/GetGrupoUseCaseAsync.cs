
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Request.STS;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.Models.STS;
using Service.Grupo.Domain.Base;
using Service.Grupo.Repository.Interfaces.Repositories.DB;
using Service.Grupo.Application.Models.Request.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Grupo.Application.UseCases.Empresa
{
    public class GetGrupoUseCaseAsync : IUseCaseAsync<GetGrupoRequest,  GrupoOutResponse>, IDisposable
    {
        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _configuration = null;
            _empresaRepository = null;
            _getGetAuthorizationUseCaseAsync = null;
            _sendLogUseCaseAsync = null;

            grupoResponse = null;
            authorizationOutResponse = null;
            authorizationResponse = null;

        }

        ~GetGrupoUseCaseAsync()
        {
            Dispose(false);
        }
        #endregion

        private IConfiguration _configuration;
        private IGrupoRepository _empresaRepository;
        private IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse> _getGetAuthorizationUseCaseAsync;
        private IUseCaseAsync<LogRequest, LogOutResponse> _sendLogUseCaseAsync;
        private GrupoOutResponse output;

        private GrupoResponse grupoResponse;
        private AuthorizationOutResponse authorizationOutResponse;
        private AuthorizationResponse authorizationResponse;


        public GetGrupoUseCaseAsync(
              IConfiguration configuration
            , IGrupoRepository empresaRepository
            , IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse> getGetAuthorizationUseCaseAsync
            , IUseCaseAsync<LogRequest, LogOutResponse> sendLogUseCaseAsync
            )
        {
            _configuration = configuration;
            _empresaRepository = empresaRepository;
            _getGetAuthorizationUseCaseAsync = getGetAuthorizationUseCaseAsync;
            _sendLogUseCaseAsync = sendLogUseCaseAsync;

            output = new();
        }

        public async Task<GrupoOutResponse> ExecuteAsync(GetGrupoRequest request)
        {
            try
            {
                authorizationOutResponse = await _getGetAuthorizationUseCaseAsync.ExecuteAsync(new AuthorizationRequest(request.SysUsuSessionId));

                if (!authorizationOutResponse.Resultado)
                {
                    output.SetResultado(false);
                    output.AddMensagem("Ocorreu uma falha na Autorização!");
                    output.SetData(authorizationOutResponse.Data);

                    return output;
                }

                if (request.EmpresaId != null)
                {
                    Service.Grupo.Domain.Entities.Grupo grupo = await _empresaRepository.GetById(request.EmpresaId.Value);
                    Service.Grupo.Application.Models.Grupo grupoModel = new Models.Grupo(grupo.SysUsuSessionId, request.RequestId, grupo.GrupoId, grupo.EmpresaId, grupo.NomeDoGrupo, (Models.Enum.EStatus)grupo.Status, grupo.DataInsert, grupo.DataUpdate);
                    grupoResponse = new GrupoResponse(grupoModel);

                    output.SetResultado(true);
                    output.AddMensagem("Dado recuperado com Sucesso!");
                    output.SetData(grupo);
                }
                else
                {
                    /*Montando Filtro*/
                    string _query = String.Empty;
                    string _where = String.Empty;

                    string tabela = $@"Sysmega.Empresa";
                    string select = $@" SELECT [GrupoId] ,[Status],[NomeDoGrupo],[DataInsert],[DataUpdate],[SysUsuSessionId] FROM {tabela} ";                    

                    _where = "' Status <> -1 '";

                    string whereNome = String.Empty;
                    if (request.FiltraNome && ((request.FiltroNome != null) && (request.FiltroNome.Trim().Length > 0)))
                    {
                        whereNome = $@"' Nome LIKE ' , '''', '%' , '{request.FiltroNome}' , '%', ''''";
                    }

                    string whereDataInsert = String.Empty;
                    if ((request.FiltraDataInsert) && (request.DataInicial != null && request.DataFinal != null))
                    {
                        DateTime aux;                       

                        whereDataInsert = $@"' DataInsert BETWEEN ', '''', '{request.DataInicial.Value:yyyy-MM-dd}{$@" 00:00:00.000"}' , '''', ' AND ' , '''', '{request.DataFinal.Value:yyyy-MM-dd}{$@" 23:59:59.999"}', ''''";

                    }
                    
                    _where = $@" SET @WHERE = CONCAT(' WHERE ' , 
                                                        {_where} 
                             
                                                        {((whereNome.Trim().Length > 0)           ? ($@" AND CONCAT({whereNome}));") : ($@""))}

                                                        {((whereDataInsert.Trim().Length > 0) ? ($@" AND CONCAT({whereDataInsert}));") : ($@""))}
                                                    );
                    ";                   

                    _query = $@"
                         DECLARE 
                              @PageNumber            int = {request.PageNumber}
                            , @PageSize              int = {request.PageSize}

                            , @WHERE                 NVARCHAR(MAX) 
                            , @SelectNavigator       NVARCHAR(MAX)
                            , @SelectTabelaDesejada  NVARCHAR(MAX)
                        ;

                        {_where}
                        
                        SET @SelectNavigator = CONCAT(
                                                        ' DECLARE
                                                          @RecordCount     INT
                                                        , @PageCount       INT
                                                        , @Resto           INT
                                                        , @PageSize        INT
                                                        , @PageNumber      INT;',
                                                        '
                                                        SET @PageSize   = ', @PageSize, ';',
                                                        '
                                                        SET @PageNumber = ', @PageNumber, ';',
                                                        '
                                                        SET @RecordCount = (SELECT COUNT(*) FROM {tabela} WITH (NOLOCK) ', @WHERE, ' );

                                                        SET @Resto = (@RecordCount % @PageSize);

                                                        SET @PageCount = (SELECT IIF(@Resto = 0, (@RecordCount / @PageSize), ((@RecordCount / @PageSize) + 1)));

                                                        SELECT 
                                                          @RecordCount RecordCount
                                                        , @PageNumber  PageNumber
                                                        , @PageSize    PageSize
                                                        , @PageCount   PageCount; 

                                                        SET @Resto = (@RecordCount % @PageSize);
                                                        SET @PageCount = (SELECT IIF(@Resto = 0, (@RecordCount / @PageSize), ((@RecordCount / @PageSize) + 1)));
                                                                   

                                                        '
                                                        );

                        EXEC sp_executesql @SelectNavigator;

                        SET @SelectTabelaDesejada = CONCAT( ' {select} '
                                                           , @WHERE, '  ORDER BY DataInsert ', ' OFFSET ', @PageSize * (@PageNumber - 1)
                                                           , ' ROWS FETCH NEXT ', @PageSize, ' ROWS ONLY ; ', ''
                                                         );

                        EXEC sp_executesql @SelectTabelaDesejada;

                        /*SELECT @SelectTabelaDesejada;*/
                      ";

                    var navigatorNovosCasosLog = _empresaRepository.GetMultiple(new DapperQuery(request.SysUsuSessionId, _query), new { param = "" },
                                gr => gr.Read<Domain.Entities.Navigator>()
                              , gr => gr.Read<Service.Grupo.Domain.Entities.Grupo>()

                              );

                    var navigators = navigatorNovosCasosLog.Item1;
                    var empresas = navigatorNovosCasosLog.Item2;

                    List<Models.Response.Navigator> responseNavigators =  new  List<Models.Response.Navigator>();
                    foreach (Domain.Entities.Navigator navigator in navigators)
                    {
                        responseNavigators.Add(new Models.Response.Navigator(navigator.RecordCount, navigator.PageNumber, navigator.PageSize, navigator.PageCount));
                    }

                    List<Service.Grupo.Application.Models.Grupo> responseEmpresas = new List<Service.Grupo.Application.Models.Grupo>();
                    foreach (Domain.Entities.Grupo empresa in empresas)
                    {
                        responseEmpresas.Add(new Service.Grupo.Application.Models.Grupo(empresa.SysUsuSessionId, request.RequestId, empresa.GrupoId, empresa.EmpresaId, empresa.NomeDoGrupo, (Application.Models.Enum.EStatus)empresa.Status, empresa.DataInsert, empresa.DataUpdate));
                    }

                    if (responseNavigators.Any() && responseEmpresas.Any())
                    {
                        GrupoResponse grupoResponse = new GrupoResponse(responseNavigators, responseEmpresas);

                        output.SetResultado(true);
                        output.AddMensagem("Dados retornados com sucesso!");
                        output.SetData(grupoResponse);
                    }
                    else
                    {
                        output.SetResultado(true);
                        output.AddMensagem("Nenhum dado encontrado!");
                    }

                    return output;
                }
            }            
            catch (Exception ex)
            {
                output.AddMensagem("Ocorreram Exceções durante a execução");
                output.AddExceptions(ex);
                Models.Response.Errors.ErrorResponse errorResponse = new Models.Response.Errors.ErrorResponse("id", "parameter", JsonConvert.SerializeObject(ex, Formatting.Indented));
                System.Collections.Generic.List<Models.Response.Errors.ErrorResponse> errorResponses = new System.Collections.Generic.List<Models.Response.Errors.ErrorResponse>();
                errorResponses.Add(errorResponse);
                output.SetErrorsResponse(new Models.Response.Errors.ErrorsResponse(errorResponses));
            }
            finally
            {
                output.SetRequest(JsonConvert.SerializeObject(request, Formatting.Indented));
                _sendLogUseCaseAsync.ExecuteAsync(new LogRequest(request.SysUsuSessionId));
            }

            return output;
        }
    }
}
