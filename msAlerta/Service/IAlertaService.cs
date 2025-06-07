using msAlerta.Entity;

namespace msAlerta.Service
{
    public interface IAlertaService
    {
        Task<List<Alerta>> GetAllAlertas();
        Task<Alerta> GetAlertaById(int id);
        Task<Alerta> CreateAlert(Alerta alerta);
        Task<Alerta> UpdateAlert(int id, Alerta alerta);
        Task<bool> DeleteAlert(int id);
    }
}
