using GerenciamentoPatrimonio.Applications.Autenticacao;
using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AutenticacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;
using GerenciamentoPatrimonio.Repositories;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _tokenjwt;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt tokenjwt)
        {
            _repository = repository;
            _tokenjwt = tokenjwt;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            var hashDigitado = CriptografiaUsuario.CriptografarSenha(senhaDigitada);
            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDTO Login(LoginDTO loginDTO)
        {
            Usuario usuario = _repository.ObterPorNIFComTipoUser(loginDTO.NIF);

            if (usuario == null)
            {
                throw new DomainException("Os dados inseridos são inválidos");
            }

            if (usuario.Ativo == false)
            {
                throw new DomainException("Usuário está inativo");
            }

            if (!VerificarSenha(loginDTO.Senha, usuario.Senha))
            {
                throw new DomainException("Os dados inseridos são inválidos");
            }

            string token = _tokenjwt.GerarToken(usuario);

            TokenDTO novoToken = new TokenDTO
            {
                Token = token,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                TipoUsuario = usuario.TipoUsuario.NomeTipo
            };
            return novoToken;
        }
        public void TrocarPrimeiraSenha(Guid usuarioID, TrocarPrimeiraSenhaDTO dto)
        {
            Validar.ValidarSenha(dto.SenhaAtual);
            Validar.ValidarSenha(dto.NovaSenha);

            Usuario usuario = _repository.BuscarPorID(usuarioID);

            if (usuario == null) { throw new DomainException("Usuário não encontrado"); }

            if (!VerificarSenha(dto.SenhaAtual, usuario.Senha)) { throw new DomainException("Senha atual inválida"); }

            if (dto.SenhaAtual == dto.NovaSenha) { throw new DomainException("A senha deve ser diferente da senha atual"); }

            usuario.Senha = CriptografiaUsuario.CriptografarSenha(dto.NovaSenha);

            usuario.PrimeiroAcesso = false;

            _repository.AtualizarSenha(usuario);
            _repository.AtualizarPrimeiroAcesso(usuario);
        }
    }
}
