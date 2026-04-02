using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.DTO.PatrimonioDTO
{
    public class CriarPatrimonioDTO
    {
        public string Denominacao { get; set; } = string.Empty;

        public string? NumeroPatrimonio { get; set; }

        public decimal? Valor { get; set; }

        public string? Imagem { get; set; }

        public bool? Ativo { get; set; }

        public Guid LocalizacaoID { get; set; }

        public Guid TipoPatrimonioID { get; set; }

        public Guid StatusPatrimonioID { get; set; }
    }
}
