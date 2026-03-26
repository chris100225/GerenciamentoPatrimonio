namespace GerenciamentoPatrimonio.DTO.LocalizacaoDTO
{
    public class ListarLocalizacaoDTO
    {
        public Guid LocalizacaoID { get; set; }
        public string NomeLocal {  get; set; } = string.Empty;

        public int? LocalSAP { get; set; }

        public string DescricaoSAP {  get; set; }

        public Guid AreaID { get; set; }
    }
}
