namespace msAlerta.Dto
{
    public class AlertaDto
    {
        public int Id_licenca { get; set; }
        public DateTime Data_alerta { get; set; }
        public string Mensagem { get; set; }
        public char Enviado { get; set; }
    }
}
