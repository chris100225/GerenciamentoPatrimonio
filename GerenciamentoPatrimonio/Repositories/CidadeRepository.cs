using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public CidadeRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<Cidade> Listar()
        {
            return _context.Cidade.OrderBy(cidade=>cidade.NomeCidade).ToList();
        }

        public Cidade BuscarPorId(Guid cidadeId)
        {
            return _context.Cidade.Find(cidadeId);
        }


        //public void Adicionar(Localizacao localizacao)
        //{
        //    _context.Localizacao.Add(localizacao);
        //    _context.SaveChanges();
        //}

        //public bool AreaExiste(Guid areaId)
        //{
        //    return _context.Area.Any(area => area.AreaID == areaId);
        //}

        //public void Atualizar(Localizacao localizacao)
        //{
        //    if (localizacao == null)
        //    {
        //        return;
        //    }

        //    Localizacao localBanco = _context.Localizacao.Find(localizacao.LocalizacaoID);

        //    if (localizacao == null)
        //    {
        //        return;
        //    }

        //    localBanco.NomeLocal = localizacao.NomeLocal;
        //    localBanco.LocalSAP = localizacao.LocalSAP;
        //    localBanco.DescricaoSAP = localizacao.DescricaoSAP;
        //    localBanco.AreaID = localizacao.AreaID;
        //    _context.SaveChanges();
        //}

        //public Localizacao BuscarPorNome(string nomeLocal, Guid areaId)
        //{
        //    return _context.Localizacao.FirstOrDefault(local => local.NomeLocal.ToLower() == nomeLocal.ToLower() && local.AreaID == areaId);
        //}
    }
}
