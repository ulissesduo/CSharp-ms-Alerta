using Microsoft.AspNetCore.Http.HttpResults;
using msAlerta.Entity;
using msAlerta.Repository;

namespace msAlerta.Service
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _alertaRepository;
        public AlertaService(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository;
        }
        public async Task<Alerta> CreateAlert(Alerta alerta)
        {
            return await _alertaRepository.CreateAlerta(alerta);
        }

        public async Task<bool> DeleteAlert(int id)
        {
            return await _alertaRepository.DeleteAlerta(id);
        }

        public async Task<Alerta> GetAlertaById(int id)
        {
            return await _alertaRepository.AlertaById(id);
        }

        public async Task<List<Alerta>> GetAllAlertas()
        {
            
            return await _alertaRepository.ListaAlertas();
        }

        public async Task<Alerta> UpdateAlert(int id, Alerta alerta)
        {
            var existingAlert = await _alertaRepository.AlertaById(id);
            if (existingAlert != null) 
            {
                return await _alertaRepository.UpdateAlerta(id, alerta);
            }
            return null;
        }
    }
}
