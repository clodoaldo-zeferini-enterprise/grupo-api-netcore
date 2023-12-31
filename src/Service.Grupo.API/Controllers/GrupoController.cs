﻿using Microsoft.AspNetCore.Mvc;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Response;

namespace Service.Grupo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GrupoController : ApiController
    {
        private readonly IUseCaseAsync<DeleteGrupoRequest, GrupoOutResponse> _deleteGrupoUseCaseAsync;
        private readonly IUseCaseAsync<GetGrupoRequest, GrupoOutResponse>    _getGrupoUseCaseAsync;
        private readonly IUseCaseAsync<InsertGrupoRequest, GrupoOutResponse> _insertGrupoUseCaseAsync;
        private readonly IUseCaseAsync<UpdateGrupoRequest, GrupoOutResponse> _updateGrupoUseCaseAsync;
        private readonly IConfiguration _configuration;

        private readonly ILogger<GrupoController> _logger;
        public GrupoController(
            IConfiguration configuration,
            ILogger<GrupoController> logger,
            IUseCaseAsync<DeleteGrupoRequest, GrupoOutResponse> deleteGrupoUseCaseAsync,
            IUseCaseAsync<GetGrupoRequest, GrupoOutResponse> getGrupoUseCaseAsync,
            IUseCaseAsync<InsertGrupoRequest, GrupoOutResponse> insertGrupoUseCaseAsync,
            IUseCaseAsync<UpdateGrupoRequest, GrupoOutResponse> updateGrupoUseCaseAsync
            )
        {
            _configuration = configuration;
            _logger = logger;   
            _deleteGrupoUseCaseAsync = deleteGrupoUseCaseAsync;
            _getGrupoUseCaseAsync = getGrupoUseCaseAsync;
            _insertGrupoUseCaseAsync = insertGrupoUseCaseAsync;
            _updateGrupoUseCaseAsync = updateGrupoUseCaseAsync;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] GetGrupoRequest request)
        {
            try
            {
                _logger.LogInformation("HTTP GET: Called get method of WeatherForecast contorller");
                using GrupoOutResponse GrupoOutResponse = await _getGrupoUseCaseAsync.ExecuteAsync(request);
                return Ok(GrupoOutResponse);
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

                using GrupoOutResponse GrupoOutResponse = await _insertGrupoUseCaseAsync.ExecuteAsync(request);
                return Ok(GrupoOutResponse);
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

                using GrupoOutResponse GrupoOutResponse = await _updateGrupoUseCaseAsync.ExecuteAsync(request);
                return Ok(GrupoOutResponse);
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

                using GrupoOutResponse GrupoOutResponse = await _deleteGrupoUseCaseAsync.ExecuteAsync(request);
                return Ok(GrupoOutResponse);
            }
            catch (Exception)
            {
                /*Pegar erro do Context*/
                return StatusCode(500, ControllerContext.ModelState);
            }
        }
    }
}
