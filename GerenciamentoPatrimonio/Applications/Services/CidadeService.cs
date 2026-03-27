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
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID,
            }).ToList();

            return localizacoesDTO;
        }

        //    List<ListarAreaDTO> areasDto = areas.Select(area => new ListarAreaDTO
        //    {
        //        AreaID = area.AreaID,
        //        NomeArea = area.NomeArea,
        //    }).ToList();

        //    return areasDto;
        //}

        //public ListarAreaDTO BuscarPorID(Guid areaID)
        //{
        //    Area area = _repository.BuscarPorID(areaID);

        //    if (area == null)
        //    {
        //        throw new DomainException("Área não encontrada");
        //    }

        //    ListarAreaDTO areaDTO = new ListarAreaDTO
        //    {
        //        AreaID = area.AreaID,
        //        NomeArea = area.NomeArea,
        //    };

        //    return areaDTO;
        //}
    }
}
