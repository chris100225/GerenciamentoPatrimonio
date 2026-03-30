using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDTO;
using GerenciamentoPatrimonio.DTO.CidadeDTO;
using GerenciamentoPatrimonio.DTO.LocalizacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCidadeDTO> Listar()
        {
            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDTO> cidadeDTO = cidades.Select(cidade => new ListarCidadeDTO
            {
                CidadeID = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado,
            }).ToList();

            return cidadeDTO;
        }

        public ListarCidadeDTO BuscarPorId(Guid cidadeID)
        {
            Cidade cidade = _repository.BuscarPorId(cidadeID);

            if (cidade == null)
            {
                throw new DomainException("Cidade não encontrada");
            }

            ListarCidadeDTO cidadeDTO = new ListarCidadeDTO
            {
                CidadeID = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado,
            };

            return cidadeDTO;
        }

        public void Adicionar(CriarCidadeDTO criarDTO)
        {

            Validar.ValidarNome(criarDTO.NomeCidade);

            Cidade cidadeExiste = _repository.BuscarPorNomeEEstado(criarDTO.NomeCidade, criarDTO.Estado);

            if (cidadeExiste != null)
            {
                throw new DomainException("Esta cidade já está cadastrada nesse estado");
            }

            Cidade cidade = new Cidade
            {
                NomeCidade = criarDTO.NomeCidade,
                Estado = criarDTO.Estado
            };

            _repository.Adicionar(cidade);
        }

        public void Atualizar(Guid cidadeID, CriarCidadeDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeCidade);

            Cidade cidadeBanco = _repository.BuscarPorId(cidadeID);

            if (cidadeBanco == null)
            {
                throw new DomainException("Cidade não encontrada");
            }

            Cidade cidadeExiste = _repository.BuscarPorNomeEEstado(attDTO.NomeCidade, attDTO.Estado);

            if (cidadeExiste != null)
            {
                throw new DomainException("Esta cidade já está cadastrada nesse estado");
            }

            cidadeBanco.NomeCidade = attDTO.NomeCidade;

            _repository.Atualizar(cidadeBanco);
        }
    }
}