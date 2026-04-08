using GerenciamentoPatrimonio.Applications.Autenticacao;
using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.UsuarioDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarUsuarioDTO> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<ListarUsuarioDTO> usuariosDTO = usuarios.Select(usuarios => new ListarUsuarioDTO
            {
                UsuarioID = usuarios.UsuarioID,
                NIF = usuarios.NIF,
                Nome = usuarios.Nome,
                RG = usuarios.RG,
                CPF = usuarios.CPF,
                CarteiraTrabalho = usuarios.CarteiraTrabalho,
                Email = usuarios.Email,
                Ativo = usuarios.Ativo,
                PrimeiroAcesso = usuarios.PrimeiroAcesso,
                EnderecoID = usuarios.EnderecoID,
                CargoID = usuarios.CargoID,
                TipoUsuarioID = usuarios.TipoUsuarioID,
            }).ToList();

            return usuariosDTO;
        }
        public ListarUsuarioDTO BuscarPorID(Guid usuarioID)
        {
            Usuario usuarios = _repository.BuscarPorID(usuarioID);

            if (usuarios == null)
            {
                throw new DomainException("Usuário não encontrado");
            }

            ListarUsuarioDTO usuariosDTO = new ListarUsuarioDTO
            {
                UsuarioID = usuarios.UsuarioID,
                NIF = usuarios.NIF,
                Nome = usuarios.Nome,
                RG = usuarios.RG,
                CPF = usuarios.CPF,
                CarteiraTrabalho = usuarios.CarteiraTrabalho,
                Email = usuarios.Email,
                Ativo = usuarios.Ativo,
                PrimeiroAcesso = usuarios.PrimeiroAcesso,
                EnderecoID = usuarios.EnderecoID,
                CargoID = usuarios.CargoID,
                TipoUsuarioID = usuarios.TipoUsuarioID,
            };

            return usuariosDTO;
        }

        public void Adicionar(CriarUsuarioDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.Nome);
            Validar.ValidarNIF(criarDTO.NIF);
            Validar.ValidarCPF(criarDTO.CPF);
            Validar.ValidarEmail(criarDTO.Email);

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(criarDTO.NIF, criarDTO.CPF, criarDTO.Email);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == criarDTO.NIF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse NIF");
                }
                if (usuarioDuplicado.CPF == criarDTO.CPF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse CPF");
                }
                if (usuarioDuplicado.Email.ToLower() == criarDTO.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse E-mail");
                }
            }

            if (!_repository.EnderecoExiste(criarDTO.EnderecoID))
            {
                throw new DomainException("Endereco informado não existe");
            }

            if (!_repository.CargoExiste(criarDTO.CargoID))
            {
                throw new DomainException("Cargo informado não existe");
            }

            if (!_repository.TipoUsuarioExiste(criarDTO.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuário informado não existe");
            }

            Usuario usuario = new Usuario
            {
                NIF = criarDTO.NIF,
                Nome = criarDTO.Nome,
                RG = criarDTO.RG,
                CPF = criarDTO.CPF,
                CarteiraTrabalho = criarDTO.CarteiraTrabalho,
                Senha = CriptografiaUsuario.CriptografarSenha(criarDTO.NIF),
                Email = criarDTO.Email,
                Ativo = true,
                PrimeiroAcesso = true,
                EnderecoID = criarDTO.EnderecoID,
                CargoID = criarDTO.CargoID,
                TipoUsuarioID = criarDTO.TipoUsuarioID
            };

            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid usuarioID, CriarUsuarioDTO attDTO)
        {
            Validar.ValidarNome(attDTO.Nome);
            Validar.ValidarNIF(attDTO.NIF);
            Validar.ValidarCPF(attDTO.CPF);
            Validar.ValidarEmail(attDTO.Email);

            Usuario usuarioBanco = _repository.BuscarPorID(usuarioID);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não foi encontrado");
            }

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(attDTO.NIF, attDTO.CPF, attDTO.Email, usuarioID);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == attDTO.NIF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse NIF");
                }
                if (usuarioDuplicado.CPF == attDTO.CPF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse CPF");
                }
                if (usuarioDuplicado.Email.ToLower() == attDTO.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse E-mail");
                }
            }

            if (!_repository.EnderecoExiste(attDTO.EnderecoID))
            {
                throw new DomainException("Endereco informado não existe");
            }

            if (!_repository.CargoExiste(attDTO.CargoID))
            {
                throw new DomainException("Cargo informado não existe");
            }

            if (!_repository.TipoUsuarioExiste(attDTO.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuário informado não existe");
            }

            usuarioBanco.NIF = attDTO.NIF;
            usuarioBanco.Nome = attDTO.Nome;
            usuarioBanco.RG = attDTO.RG;
            usuarioBanco.CPF = attDTO.CPF;
            usuarioBanco.CarteiraTrabalho = attDTO.CarteiraTrabalho;
            usuarioBanco.Email = attDTO.Email;
            usuarioBanco.EnderecoID = attDTO.EnderecoID;
            usuarioBanco.CargoID = attDTO.CargoID;
            usuarioBanco.TipoUsuarioID = attDTO.TipoUsuarioID;

            _repository.Atualizar(usuarioBanco);

        }

        public void AtualizarStatus(Guid usuarioID, AtualizarStatusUsuarioDTO attStatusDTO)
        {
            Usuario usuarioBanco = _repository.BuscarPorID(usuarioID);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuario não encontrado");
            }

            usuarioBanco.Ativo = attStatusDTO.Ativo;
            _repository.AtualizarStatus(usuarioBanco);
        }
    }
}
