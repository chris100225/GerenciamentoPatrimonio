namespace GerenciamentoPatrimonio.DTO.BairroDTO
{
    public class ListarBairroDTO
    {
        public Guid BairroID { get; set; }

        public string NomeBairro { get; set; } = string.Empty;

        public Guid CidadeID { get; set; }
    }
}
