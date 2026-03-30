using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.BairroDTO;
using GerenciamentoPatrimonio.DTO.CidadeDTO;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class BairroService
    {
        private readonly IBairroRepository _repository;

        public BairroService(IBairroRepository repository)
        {
            _repository = repository;
        }

        public List<ListarBairroDTO> Listar()
        {
            List<Bairro> bairros = _repository.Listar();

            List<ListarBairroDTO> bairroDTO = bairros.Select(bairro=>new ListarBairroDTO
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                Cidade = bairro.Cidade,
            }).ToList();
        }
    }
}

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