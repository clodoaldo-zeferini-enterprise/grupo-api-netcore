using Microsoft.AspNetCore.Mvc;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Response;

namespace Service.Grupo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GrupoController : ApiController
    {
        private readonly IUseCaseAsync<DeleteGrupoRequest, EmpresaOutResponse> _deleteEmpresaUseCaseAsync;
        private readonly IUseCaseAsync<GetGrupoRequest, EmpresaOutResponse>    _getEmpresaUseCaseAsync;
        private readonly IUseCaseAsync<InsertGrupoRequest, EmpresaOutResponse> _insertEmpresaUseCaseAsync;
        private readonly IUseCaseAsync<UpdateGrupoRequest, EmpresaOutResponse> _updateEmpresaUseCaseAsync;
        private readonly IConfiguration _configuration;

        private readonly ILogger<GrupoController> _logger;
        public GrupoController(
            IConfiguration configuration,
            ILogger<GrupoController> logger,
            IUseCaseAsync<DeleteGrupoRequest, EmpresaOutResponse> deleteEmpresaUseCaseAsync,
            IUseCaseAsync<GetGrupoRequest, EmpresaOutResponse> getEmpresaUseCaseAsync,
            IUseCaseAsync<InsertGrupoRequest, EmpresaOutResponse> insertEmpresaUseCaseAsync,
            IUseCaseAsync<UpdateGrupoRequest, EmpresaOutResponse> updateEmpresaUseCaseAsync
            )
        {
            _configuration = configuration;
            _logger = logger;   
            _deleteEmpresaUseCaseAsync = deleteEmpresaUseCaseAsync;
            _getEmpresaUseCaseAsync = getEmpresaUseCaseAsync;
            _insertEmpresaUseCaseAsync = insertEmpresaUseCaseAsync;
            _updateEmpresaUseCaseAsync = updateEmpresaUseCaseAsync;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] GetGrupoRequest request)
        {
            try
            {
                _logger.LogInformation("HTTP GET: Called get method of WeatherForecast contorller");
                using EmpresaOutResponse EmpresaOutResponse = await _getEmpresaUseCaseAsync.ExecuteAsync(request);
                return Ok(EmpresaOutResponse);
            }
            catch (Exception)
            {
                /*Pegar erro do Context*/
                return StatusCode(500, ControllerContext.ModelState);
            }
        }
         
        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] InsertGrupoRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest();

                using EmpresaOutResponse EmpresaOutResponse = await _insertEmpresaUseCaseAsync.ExecuteAsync(request);
                return Ok(EmpresaOutResponse);
            }
            catch (Exception)
            {
                /*Pegar erro do Context*/
                return StatusCode(500, ControllerContext.ModelState);
            }
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateGrupoRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest();

                using EmpresaOutResponse EmpresaOutResponse = await _updateEmpresaUseCaseAsync.ExecuteAsync(request);
                return Ok(EmpresaOutResponse);
            }
            catch (Exception)
            {
                /*Pegar erro do Context*/
                return StatusCode(500, ControllerContext.ModelState);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteGrupoRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest();

                using EmpresaOutResponse EmpresaOutResponse = await _deleteEmpresaUseCaseAsync.ExecuteAsync(request);
                return Ok(EmpresaOutResponse);
            }
            catch (Exception)
            {
                /*Pegar erro do Context*/
                return StatusCode(500, ControllerContext.ModelState);
            }
        }
    }
}
