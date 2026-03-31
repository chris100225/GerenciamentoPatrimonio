using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.BairroDTO;
using GerenciamentoPatrimonio.DTO.CidadeDTO;
using GerenciamentoPatrimonio.DTO.LocalizacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
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

            List<ListarBairroDTO> bairroDTO = bairros.Select(bairro => new ListarBairroDTO
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                CidadeID = bairro.CidadeID,
            }).ToList();

            return bairroDTO;
        }

        public ListarBairroDTO BuscarPorID(Guid bairroID)
        {
            Bairro bairro = _repository.BuscarPorID(bairroID);

            if (bairro == null)
            {
                throw new DomainException("Bairro não encotrado");
            }

            ListarBairroDTO bairroDTO = new ListarBairroDTO
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                CidadeID = bairro.CidadeID,
            };
            return bairroDTO;
        }

        public void Adicionar(CriarBairroDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.NomeBairro);

            Bairro bairroExistente = _repository.BuscarPorNome(criarDTO.NomeBairro, criarDTO.CidadeID);

            if (bairroExistente != null)
            {
                throw new DomainException("Já existe um bairro com esse nome na Cidade");
            }

            if (!_repository.CidadeExiste(criarDTO.CidadeID))
            {
                throw new DomainException("Cidade informada não existe");
            }

            Bairro bairro = new Bairro
            {
                NomeBairro = criarDTO.NomeBairro,
                CidadeID = criarDTO.CidadeID,
            };

            _repository.Adicionar(bairro);
        }

        public void Atualizar(Guid bairroID, CriarBairroDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeBairro);

            Bairro bairroExiste = _repository.BuscarPorNome(attDTO.NomeBairro, attDTO.CidadeID);

            if (bairroExiste!=null)
            {
                throw new DomainException("Já existe um bairro cadastrado nessa cidade");
            }

            Bairro bairroBanco = _repository.BuscarPorID(bairroID);

            if (bairroBanco == null)
            {
                throw new DomainException("Bairro não encontrado");
            }

            if (!_repository.CidadeExiste(attDTO.CidadeID))
            {
                throw new DomainException("Cidade informada não existe");
            }

            bairroBanco.NomeBairro = attDTO.NomeBairro;
            bairroBanco.CidadeID = attDTO.CidadeID;

            _repository.Atualizar(bairroBanco);
        }
    }
}
