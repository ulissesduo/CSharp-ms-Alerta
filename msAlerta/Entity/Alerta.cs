using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace msAlerta.Entity
{
    public class Alerta
    {
        public int Id { get; set; }
        public int Id_licenca { get; set; }
        public DateTime Data_alerta { get; set; }
        public string Mensagem { get; set; }
        public char Enviado { get; set; }

        public Alerta()
        {
            
        }

        public Alerta(int id, int id_licenca, DateTime data_alerta, string mensagem, char enviado)
        {
            this.Id = id;
            this.Data_alerta = data_alerta;
            this.Enviado = enviado;
            this.Id_licenca = id_licenca;
        }
    }
}
