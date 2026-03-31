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
            return _context.Bairro.OrderBy(bairro => bairro.NomeBairro).ToList();
        }

        public Bairro BuscarPorID(Guid bairroId)
        {
            return _context.Bairro.Find(bairroId);
        }

        public void Adicionar(Bairro bairro)
        {
            _context.Bairro.Add(bairro);
            _context.SaveChanges();
        }

        public void Atualizar(Bairro bairro)
        {
            if (bairro == null)
            {
                return;
            }

            Bairro bairroBanco = _context.Bairro.Find(bairro.BairroID);

            if (bairro == null)
            {
                return;
            }

            bairroBanco.NomeBairro = bairro.NomeBairro;
            bairroBanco.Cidade = bairro.Cidade;

            _context.SaveChanges();
        }

        public Bairro BuscarPorNome(string nomeBairro, Guid cidadeID)
        {
            return _context.Bairro.FirstOrDefault(bairroBanco => bairroBanco.CidadeID == cidadeID && bairroBanco.NomeBairro == nomeBairro);
        }

        public bool CidadeExiste(Guid cidadeId)
        {
            return _context.Cidade.Any(cidade => cidade.CidadeID == cidadeId);
        }
    }
}