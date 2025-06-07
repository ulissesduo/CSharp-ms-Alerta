using msAlerta.Entity;

namespace msAlerta.Repository
{
    public interface IAlertaRepository
    {
        Task <List<Alerta>> ListaAlertas();
        Task<Alerta> AlertaById(int id);
        Task<Alerta> CreateAlerta(Alerta alerta);
        Task<Alerta> UpdateAlerta(int id, Alerta alerta);
        Task<bool> DeleteAlerta(int id);

    }
}
