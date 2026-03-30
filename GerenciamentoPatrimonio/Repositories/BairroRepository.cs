using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;
using GerenciamentoPatrimonio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPatrimonio.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public BairroRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<Bairro> Listar()
        {
            return _context.Bairro.OrderBy(bairro=>bairro.NomeBairro).ToList();
        }

        public Bairro BuscarPorID(Guid bairroId)
    }
}



public Localizacao BuscarPorId(Guid localizacaoId)
{
    return _context.Localizacao.Find(localizacaoId);
}

public void Adicionar(Localizacao localizacao)
{
    _context.Localizacao.Add(localizacao);
    _context.SaveChanges();
}

public bool AreaExiste(Guid areaId)
{
    return _context.Area.Any(area => area.AreaID == areaId);
}

public void Atualizar(Localizacao localizacao)
{
    if (localizacao == null)
    {
        return;
    }

    Localizacao localBanco = _context.Localizacao.Find(localizacao.LocalizacaoID);

    if (localizacao == null)
    {
        return;
    }

    localBanco.NomeLocal = localizacao.NomeLocal;
    localBanco.LocalSAP = localizacao.LocalSAP;
    localBanco.DescricaoSAP = localizacao.DescricaoSAP;
    localBanco.AreaID = localizacao.AreaID;
    _context.SaveChanges();
}

public Localizacao BuscarPorNome(string nomeLocal, Guid areaId)
{
    return _context.Localizacao.FirstOrDefault(local => local.NomeLocal.ToLower() == nomeLocal.ToLower() && local.AreaID == areaId);
}