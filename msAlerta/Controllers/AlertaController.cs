using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using msAlerta.Dto;
using msAlerta.Entity;
using msAlerta.Service;

namespace msAlerta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController : ControllerBase
    {
        public readonly IAlertaService _alertaService;
        private readonly IMapper _mapper;
        public AlertaController(IAlertaService alertaService, IMapper mapper) 
        {
            _alertaService = alertaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Alerta>>> GetAll() 
        {
            var alerta = await _alertaService.GetAllAlertas();
            return Ok(alerta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alerta>> GetAlertAsync(int id) 
        {
            var alerta = await _alertaService.GetAlertaById(id);
            if (alerta == null) return NotFound();

            return Ok(alerta);
        }

        [HttpPost]
        public async Task<ActionResult<Alerta>> CreateAlertAsync([FromBody] AlertaDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid alert data");

            var alerta = new Alerta
            {
                Id_licenca = dto.Id_licenca,
                Data_alerta = dto.Data_alerta,
                Mensagem = dto.Mensagem,
                Enviado = dto.Enviado
            };

            var newAlerta = await _alertaService.CreateAlert(alerta);
            return Created("", newAlerta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlertAsync(int id, [FromBody] AlertaDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid alert data");

            var alerta = new Alerta
            {
                Id = id, // Make sure ID is passed
                Id_licenca = dto.Id_licenca,
                Data_alerta = dto.Data_alerta,
                Mensagem = dto.Mensagem,
                Enviado = dto.Enviado
            };

            var updatedAlert = await _alertaService.UpdateAlert(id, alerta);
            if (updatedAlert == null)
                return NotFound();

            return Ok(updatedAlert);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertAsync(int id)
        {
            var success = await _alertaService.DeleteAlert(id);
            if (!success) return NotFound();

            return NoContent();
        }


    }
}
