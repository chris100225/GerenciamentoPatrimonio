using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;
using GerenciamentoPatrimonio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPatrimonio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GerenciamentoPatrimonioContext _context;

        public EnderecoRepository(GerenciamentoPatrimonioContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(endereco => endereco.Logradouro).ToList();
        }

        public Endereco BuscarPorID(Guid enderecoId)
        {
            return _context.Endereco.Find(enderecoId);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId)

        {
            return _context.Endereco.FirstOrDefault(endereco => endereco.Logradouro.ToLower() == logradouro.ToLower() && endereco.Numero == numero && endereco.BairroID == bairroId);
        }

        public bool BairroExiste(Guid bairroId)
        {
            return _context.Bairro.Any(bairro => bairro.BairroID == bairroId);
        }
    }
}
//void Atualizar(Endereco endereco);
//bool BairroExiste(Guid bairroId);


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
