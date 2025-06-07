using Microsoft.EntityFrameworkCore;
using msAlerta.Data;
using msAlerta.Entity;

namespace msAlerta.Repository
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly DataContext _context;

        public AlertaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Alerta> AlertaById(int id)
        {
            return await _context.Alerta.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Alerta> CreateAlerta(Alerta alerta)
        {
            await _context.Alerta.AddAsync(alerta);
            await _context.SaveChangesAsync();
            return alerta;
        }

        public async Task<bool> DeleteAlerta(int id)
        {
            var existingAlert = await _context.Alerta.FindAsync(id);
            if (existingAlert == null) return false;
            _context.Alerta.Remove(existingAlert);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {

                throw;
            }
        }

        public async Task<List<Alerta>> ListaAlertas()
        {
            return await _context.Alerta.AsNoTracking().ToListAsync();

        }

        //add dto
        public async Task<Alerta> UpdateAlerta(int id, Alerta alerta)
        {
            var existingAlert = await _context.Alerta.FindAsync(id);
            if (existingAlert == null) return null;
            
            existingAlert.Enviado = alerta.Enviado;
            existingAlert.Id_licenca = alerta.Id_licenca;
            existingAlert.Mensagem = alerta.Mensagem;
            existingAlert.Data_alerta = alerta.Data_alerta;
            try {
                await _context.SaveChangesAsync();
                return existingAlert;

            }
            catch (DbUpdateException e) 
            {
                throw;
            }
            
        }
    }
}
