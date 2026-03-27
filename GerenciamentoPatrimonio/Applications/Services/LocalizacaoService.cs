using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDTO;
using GerenciamentoPatrimonio.DTO.LocalizacaoDTO;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLocalizacaoDTO> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();

            List<ListarLocalizacaoDTO> localizacoesDTO = localizacoes.Select(localizacao => new ListarLocalizacaoDTO
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID,
            }).ToList();

            return localizacoesDTO;
        }

        public ListarLocalizacaoDTO BuscarPorId(Guid localizacaoId)
        {
            Localizacao localizacao = _repository.BuscarPorId(localizacaoId);

            if (localizacao == null)
            {
                throw new DomainException("Localização não encontrada");
            }
            ListarLocalizacaoDTO localizacaoDTO = new ListarLocalizacaoDTO
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID,
            };

            return localizacaoDTO;
        }

        public void Adicionar(CriarLocalizacaoDTO criarDTO)
        {
            Validar.ValidarNome(criarDTO.NomeLocal);

            Localizacao localExistente = _repository.BuscarPorNome(criarDTO.NomeLocal, criarDTO.AreaID);

            if (localExistente!=null)
            {
                throw new DomainException("Já existe um local cadastrado com esse nome na área");
            }

            if (!_repository.AreaExiste(criarDTO.AreaID))
            {
                throw new DomainException("Área informada não existe");
            }



            Localizacao localizacao = new Localizacao
            {
                NomeLocal = criarDTO.NomeLocal,
                LocalSAP = criarDTO.LocalSAP,
                DescricaoSAP = criarDTO.DescricaoSAP,
                AreaID = criarDTO.AreaID,
            };
            _repository.Adicionar(localizacao);

        }

        public void Atualizar(Guid localID, CriarLocalizacaoDTO attDTO)
        {
            Validar.ValidarNome(attDTO.NomeLocal);


            Localizacao localExistente = _repository.BuscarPorNome(attDTO.NomeLocal, attDTO.AreaID);

            if (localExistente != null)
            {
                throw new DomainException("Já existe um local cadastrado com esse nome na área");
            }

            Localizacao localBanco = _repository.BuscarPorId(localID);

            if (localBanco == null)
            {
                throw new DomainException("Localização não encontrada");
            }

            if (!_repository.AreaExiste(attDTO.AreaID))
            {
                throw new DomainException("Área informada não existe");
            }

            localBanco.NomeLocal = attDTO.NomeLocal;
            localBanco.LocalSAP = attDTO.LocalSAP;
            localBanco.DescricaoSAP = attDTO.DescricaoSAP;
            localBanco.AreaID = attDTO.AreaID;

            _repository.Atualizar(localBanco);

        }
    }
}
