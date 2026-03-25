using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class AreaService
    {
        private readonly IAreaRepository _repository;

        public AreaService(IAreaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarAreaDTO> Listar()
        {
            List<Area> areas = _repository.Listar();
            List<ListarAreaDTO> areasDto = areas.Select(area => new ListarAreaDTO
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea,
            }).ToList();

            return areasDto;
        }

        public ListarAreaDTO BuscarPorID(Guid areaID)
        {
            Area area = _repository.BuscarPorID(areaID);

            if (area == null)
            {
                throw new DomainException("Área não encontrada");
            }

            ListarAreaDTO areaDTO = new ListarAreaDTO
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea,
            };

            return areaDTO;
        }

        public void Adicionar(CriarAreaDTO dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area areaExiste = _repository.BuscarPorNome(dto.NomeArea);

            if (areaExiste != null)
            {
                throw new DomainException("Já existe uma área cadastrada com esse nome");
            }

            Area area = new Area
            {
                NomeArea = dto.NomeArea
            };

            _repository.Adicionar(area);
        }

        public void Atualizar(Guid areaID, CriarAreaDTO dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area areaBanco = _repository.BuscarPorID(areaID);
            if (areaBanco == null)
            {
                throw new DomainException("Área não encontrada");
            }

            Area areaExistente = _repository.BuscarPorNome(dto.NomeArea);

            if (areaExistente != null)
            {
                throw new DomainException("Já existe um área cadastrada com esse nome");
            }

            areaBanco.NomeArea = dto.NomeArea;

            _repository.Atualizar(areaBanco);

        }
    }
}