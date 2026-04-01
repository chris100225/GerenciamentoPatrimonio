using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.DTO.EnderecoDTO
{
    public class ListarEnderecoDTO
    {
        public Guid EnderecoID { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public int? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? CEP { get; set; }
        public Guid BairroID { get; set; }
    }
}
