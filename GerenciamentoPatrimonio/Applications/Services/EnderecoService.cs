using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.EnderecoDTO;
using GerenciamentoPatrimonio.DTO.LocalizacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class EnderecoService
    {

        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDTO> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();

            List<ListarEnderecoDTO> enderecosDTO = enderecos.Select(endereco => new ListarEnderecoDTO
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                BairroID = endereco.BairroID,

            }).ToList();

            return enderecosDTO;
        }

        public ListarEnderecoDTO BuscarPorID(Guid enderecoID)
        {
            Endereco endereco = _repository.BuscarPorID(enderecoID);

            if (endereco == null)
            {
                throw new DomainException("Endereco não encontrado");
            }

            ListarEnderecoDTO enderecoDTO = new ListarEnderecoDTO
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                BairroID = endereco.BairroID,
            };
            return enderecoDTO;
        }

        public void Adicionar(CriarEnderecoDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.Logradouro);

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(criarDTO.Logradouro, criarDTO.Numero, criarDTO.BairroID);

            if (enderecoExistente != null)
            {
                throw new DomainException("Este endereço já está cadastrado");
            }

            if (!_repository.BairroExiste(criarDTO.BairroID))
            {
                throw new DomainException("Bairro informado não existe");
            }

            Endereco endereco = new Endereco
            {
                Logradouro = criarDTO.Logradouro,
                Numero = criarDTO.Numero,
                Complemento = criarDTO.Complemento,
                CEP = criarDTO.CEP,
                BairroID = criarDTO.BairroID,
            };

            _repository.Adicionar(endereco);

        }
    }
}
