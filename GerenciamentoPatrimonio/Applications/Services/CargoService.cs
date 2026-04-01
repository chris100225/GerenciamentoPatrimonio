using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.CargoDTO;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class CargoService
    {

        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }
        public List<ListarCargoDTO> Listar()
        {
            List<Cargo> cargos = _repository.Listar();

            List<ListarCargoDTO> cargosDTO = cargos.Select(cargos => new ListarCargoDTO
            {
                CargoID = cargos.CargoID,
                NomeCargo = cargos.NomeCargo,

            }).ToList();

            return cargosDTO;
        }

 
        public ListarCargoDTO BuscarPorID(Guid cargoID)
        {
            Cargo cargo = _repository.BuscarPorID(cargoID);

            if (cargo == null)
            {
                throw new DomainException("Tipo de usuário não encontrado");
            }

            ListarCargoDTO cargoDTO = new ListarCargoDTO
            {
                CargoID = cargo.CargoID,
                NomeCargo = cargo.NomeCargo,
            };
            return cargoDTO;
        }


        public void Adicionar(CriarCargoDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.NomeCargo);

            Cargo cargoExiste = _repository.BuscarPorNome(criarDTO.NomeCargo);

            if (cargoExiste != null)
            {
                throw new DomainException("Esse cargo já está no sistema");
            }

            Cargo cargo = new Cargo
            {
                NomeCargo = criarDTO.NomeCargo
            };

            _repository.Adicionar(cargo);
        }

        public void Atualizar(Guid id, CriarCargoDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeCargo);

            Cargo cargoBanco = _repository.BuscarPorID(id);

            if (cargoBanco == null)
            {
                throw new DomainException("Cargo não encontrado");
            }

            Cargo cargoExiste = _repository.BuscarPorNome(attDTO.NomeCargo);

            if (cargoExiste != null)
            {
                throw new DomainException("Esse cargo já está cadastrado");
            }

            cargoBanco.NomeCargo = attDTO.NomeCargo;

            _repository.Atualizar(cargoBanco);
        }

    }
}
