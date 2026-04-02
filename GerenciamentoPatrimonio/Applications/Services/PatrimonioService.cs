using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.EnderecoDTO;
using GerenciamentoPatrimonio.DTO.PatrimonioDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;

        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarPatrimonioDTO> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            List<ListarPatrimonioDTO> patrimonioDTO = patrimonios.Select(patrimonio => new ListarPatrimonioDTO
            {
                PatrimonioID = patrimonio.PatrimonioID,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoID = patrimonio.LocalizacaoID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID,
            }).ToList();

            return patrimonioDTO;
        }

        public ListarPatrimonioDTO BuscarPorID(Guid patrimonioID)
        {
            Patrimonio patrimonio = _repository.BuscarPorID(patrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimonio não encontrado");
            }

            return new ListarPatrimonioDTO
            {
                PatrimonioID = patrimonio.PatrimonioID,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoID = patrimonio.LocalizacaoID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            };
        }


        public void Adicionar(CriarPatrimonioDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.Denominacao);

            if (!_repository.LocalizacaoExiste(criarDTO.LocalizacaoID))
            {
                throw new DomainException("Local informado não existe.");
            }

            if (!_repository.TipoPatrimonioExiste(criarDTO.TipoPatrimonioID))
            {
                throw new DomainException("Tipo Patrimonio informado não existe.");
            }

            if (!_repository.StatusPatrimonioExiste(criarDTO.StatusPatrimonioID))
            {
                throw new DomainException("Status de Patrimônio informado não existe.");
            }

            Patrimonio patrimonioExiste = _repository.BuscarPorNumeroPatrimonio(
                criarDTO.NumeroPatrimonio
            );

            if (patrimonioExiste != null)
            {
                throw new DomainException("Já existe um patrimonio com esses dados.");
            }

            Patrimonio patrimonio = new Patrimonio
            {
                Denominacao = criarDTO.Denominacao,
                NumeroPatrimonio = criarDTO.NumeroPatrimonio,
                Valor = criarDTO.Valor,
                Imagem = criarDTO.Imagem,
                LocalizacaoID = criarDTO.LocalizacaoID,
                TipoPatrimonioID = criarDTO.TipoPatrimonioID,
                StatusPatrimonioID = criarDTO.StatusPatrimonioID
            };

        }

        public void Atualizar(Guid patrimonioID, CriarPatrimonioDTO attDTO)
        {
            Validar.ValidarNome(attDTO.Denominacao);

            Patrimonio patrimonioBanco = _repository.BuscarPorID(patrimonioID);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.LocalizacaoExiste(attDTO.LocalizacaoID))
            {
                throw new DomainException("Local informado não existe.");
            }

            if (!_repository.TipoPatrimonioExiste(attDTO.TipoPatrimonioID))
            {
                throw new DomainException("Tipo Patrimonio informado não existe.");
            }

            if (!_repository.StatusPatrimonioExiste(attDTO.StatusPatrimonioID))
            {
                throw new DomainException("Status de Patrimônio informado não existe.");
            }

            Patrimonio patrimonioExiste = _repository.BuscarPorID(
                patrimonioID
            );

            if (patrimonioExiste != null)
            {
                throw new DomainException("Já existe um parimônio com esses dados.");
            }
            patrimonioBanco.Denominacao = attDTO.Denominacao;
            patrimonioBanco.NumeroPatrimonio = attDTO.NumeroPatrimonio;
            patrimonioBanco.Valor = attDTO.Valor;
            patrimonioBanco.Imagem = attDTO.Imagem;
            patrimonioBanco.LocalizacaoID = attDTO.LocalizacaoID;
            patrimonioBanco.TipoPatrimonioID = attDTO.TipoPatrimonioID;
            patrimonioBanco.StatusPatrimonioID = attDTO.StatusPatrimonioID;
            _repository.Atualizar(patrimonioBanco);
        }

        public void AtualizarStatus(Guid patrimonioID, CriarPatrimonioDTO attDTO)
        {
            Validar.ValidarNome(attDTO.Denominacao);
            Patrimonio patrimonioBanco = _repository.BuscarPorID(patrimonioID);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.StatusPatrimonioExiste(attDTO.StatusPatrimonioID))
            {
                throw new DomainException("Status de Patrimônio informado não existe.");
            }

            Patrimonio patrimonioExiste = _repository.BuscarPorID(
                patrimonioID
            );

            if (patrimonioExiste != null)
            {
                throw new DomainException("Já existe um parimônio com esses dados.");
            }

            patrimonioBanco.StatusPatrimonioID = attDTO.StatusPatrimonioID;
            _repository.AtualizarStatus(patrimonioBanco);
        }
    }
}
