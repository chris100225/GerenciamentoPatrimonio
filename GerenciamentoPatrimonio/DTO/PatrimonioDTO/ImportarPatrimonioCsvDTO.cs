namespace GerenciamentoPatrimonio.DTO.PatrimonioDTO
{
    public class ImportarPatrimonioCsvDTO
    {
        public string NumeroPatrimonio { get; set; } = string.Empty;

        public string Denominacao { get; set; } = string.Empty;

        public string? DataIncorporacao { get; set; }

        public string? ValorAquisicao { get; set; }
    }
}
