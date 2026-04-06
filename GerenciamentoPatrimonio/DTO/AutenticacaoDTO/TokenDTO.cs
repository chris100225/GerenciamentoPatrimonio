namespace GerenciamentoPatrimonio.DTO.AutenticacaoDTO
{
    public class TokenDTO
    {
        public string Token { get; set; } = string.Empty;

        public bool PrimeiroAcesso { get; set; }

        public string TipoUsuario { get; set; } = string.Empty;
    }
}
