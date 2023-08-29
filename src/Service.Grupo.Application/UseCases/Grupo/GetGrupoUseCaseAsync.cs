
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

namespace Service.Grupo.Application.UseCases.Grupo
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
            
            _grupoRepository = null;
            _output = null;
            _configuration = null;
            
        }

        ~GetGrupoUseCaseAsync()
        {
            Dispose(false);
        }
        #endregion

        private IConfiguration _configuration;
        private IGrupoRepository _grupoRepository;
        private IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse> _getGetAuthorizationUseCaseAsync;
        private IUseCaseAsync<LogRequest, LogOutResponse> _sendLogUseCaseAsync;
        private GrupoOutResponse _output;

        private GrupoResponse grupoResponse;
        private AuthorizationOutResponse authorizationOutResponse;
        private AuthorizationResponse authorizationResponse;


        public GetGrupoUseCaseAsync(
              IConfiguration configuration
            , IGrupoRepository grupoRepository
            , IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse> getGetAuthorizationUseCaseAsync
            , IUseCaseAsync<LogRequest, LogOutResponse> sendLogUseCaseAsync
            )
        {
            _configuration = configuration;
            _grupoRepository = grupoRepository;
            _getGetAuthorizationUseCaseAsync = getGetAuthorizationUseCaseAsync;
            _sendLogUseCaseAsync = sendLogUseCaseAsync;

            _output = new()
            {
                Resultado = false,
                Mensagem = "Dados Fornecidos são inválidos!"
            };
        }

        public async Task< GrupoOutResponse> ExecuteAsync(GetGrupoRequest request)
        {
            try
            {
                authorizationOutResponse = await _getGetAuthorizationUseCaseAsync.ExecuteAsync(new AuthorizationRequest(request.SysUsuSessionId));

                if (!authorizationOutResponse.Resultado)
                {
                    _output.Resultado = false;
                    _output.Mensagem = "Ocorreu uma falha na Autorização!";
                    _output.Data = null;

                    return _output;
                }


                if (request.Id != null)
                {
                    Service.Grupo.Domain.Entities.Grupo grupo = await _grupoRepository.GetById(request.Id.Value);

                    _output.Resultado = true;
                    _output.Mensagem = "Dado recuperado com Sucesso!";
                    _output.Data = grupo;
                }
                else
                {
                    /*Montando Filtro*/
                    string _query = String.Empty;
                    string _where = String.Empty;

                    string tabela = $@"Grupo";
                    string select = $@" SELECT [Id],[Nome],[Status],[DataInsert],[DataUpdate],[SysUsuSessionId] FROM [Exemplo].[dbo].[{tabela}] ";

                    if (request.PageNumber <= 0) { request.PageNumber = 1; }
                    if (request.PageSize   <= 0) { request.PageSize   = 1; }

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

                        if (request.DataInicial > request.DataFinal)
                        {
                            aux = request.DataInicial.Value;
                            request.DataInicial = request.DataFinal;
                            request.DataFinal = aux;
                        }

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
                                                        SET @RecordCount = (SELECT COUNT(*)
                                                                            FROM
                                                                            {tabela} WITH (NOLOCK) ', @WHERE, ' );

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
                                                           , @WHERE, '  ORDER BY ID ', ' OFFSET ', @PageSize * (@PageNumber - 1)
                                                           , ' ROWS FETCH NEXT ', @PageSize, ' ROWS ONLY ; ', ''
                                                         );

                        EXEC sp_executesql @SelectTabelaDesejada;

                        /*SELECT @SelectTabelaDesejada;*/
                      ";

                    var navigatorNovosCasosLog = _grupoRepository.GetMultiple(new DapperQuery(request.SysUsuSessionId, _query), new { param = "" },
                                gr => gr.Read<Domain.Entities.Navigator>()
                              , gr => gr.Read<Service.Grupo.Domain.Entities.Grupo>()

                              );

                    var navigators = navigatorNovosCasosLog.Item1;
                    var Grupos = navigatorNovosCasosLog.Item2;

                    GrupoResponse GrupoResponse = new();


                    List<Models.Response.Navigator> responseNavigators =  new  List<Models.Response.Navigator>();
                    foreach (Domain.Entities.Navigator navigator in navigators)
                    {
                        responseNavigators.Add(new Models.Response.Navigator(navigator.RecordCount, navigator.PageNumber, navigator.PageSize, navigator.PageCount));
                    }

                    List<Service.Grupo.Application.Models.Grupo> responseGrupos = new List<Service.Grupo.Application.Models.Grupo>();
                    foreach (Domain.Entities.Grupo Grupo in Grupos)
                    {
                        responseGrupos.Add(new Service.Grupo.Application.Models.Grupo(Grupo.Id, Grupo.Nome, Grupo.Status, Grupo.SysUsuSessionId, Grupo.DataInsert, Grupo.DataUpdate ));
                    }

                    if (responseNavigators.Any() && responseGrupos.Any())
                    {
                        GrupoResponse.Navigators = responseNavigators;
                        GrupoResponse.Grupos = responseGrupos;

                        _output.Resultado = true;
                        _output.Mensagem = "Dados retornados com sucesso!";
                        _output.Data = GrupoResponse;
                    }
                    else
                    {
                        _output.Resultado = true;
                        _output.Mensagem = "Nenhum dado encontrado!";
                    }

                    return _output;
                }
            }            
            catch (Exception ex)
            {
                _output.Mensagem = "Ocorreram Exceções durante a execução";
                _output.AddExceptions(ex);
                Models.Response.Errors.ErrorResponse errorResponse = new Models.Response.Errors.ErrorResponse("id", "parameter", JsonConvert.SerializeObject(ex, Formatting.Indented));
                System.Collections.Generic.List<Models.Response.Errors.ErrorResponse> errorResponses = new System.Collections.Generic.List<Models.Response.Errors.ErrorResponse>();
                errorResponses.Add(errorResponse);
                _output.ErrorsResponse = new Models.Response.Errors.ErrorsResponse(errorResponses);
            }
            finally
            {
                _output.Request = JsonConvert.SerializeObject(request, Formatting.Indented);
                _sendLogUseCaseAsync.ExecuteAsync(new LogRequest(request.SysUsuSessionId));
            }

            return _output;
        }
    }
}
