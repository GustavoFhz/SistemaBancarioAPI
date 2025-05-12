using SistemaBancario.Enum;

namespace SistemaBancario.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public TipoTransacaoEnum Tipo { get; set; }
        public double Valor { get; set; }
        public DateTime DataTransacao { get; set; } = DateTime.UtcNow;

        public int ContaOrigemId { get; set; }
        public ContaBancaria ContaOrigem { get; set; }

        public int ContaDestinoId { get; set; }
        public ContaBancaria ContaDestino { get; set; }

        public string? Descricao { get; set; }
    }
}
