using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using msAlerta.Entity;
using msAlerta.Service;

namespace msAlerta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController : ControllerBase
    {
        public readonly IAlertaService _alertaService;
        public AlertaController(IAlertaService alertaService) 
        {
            _alertaService = alertaService;
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
        public async Task<ActionResult<Alerta>> CreateAlertAsync(Alerta alerta) 
        {
            var newAlerta = await _alertaService.CreateAlert(alerta);
            return CreatedAtAction(nameof(GetAlertAsync), new { id = newAlerta.Id }, newAlerta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlertAsync(int id, Alerta alerta)
        {
            var updatedAlert = await _alertaService.UpdateAlert(id, alerta);

            if (updatedAlert == null)
            {
                return NotFound();
            }

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
