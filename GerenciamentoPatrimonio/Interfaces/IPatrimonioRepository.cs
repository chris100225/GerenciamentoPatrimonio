using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorID(Guid patrimonioID);

        // fazer esse com AsQueryable igual foi feito no endereço
        Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioID = null);

        bool LocalizacaoExiste(Guid localizacaoID);
        bool TipoPatrimonioExiste(Guid tipoPatrimonioID);
        bool StatusPatrimonioExiste(Guid statusPatrimonioID);

        void Adicionar(Patrimonio patrimonio);
        void Atualizar(Patrimonio patrimonio);
        void AtualizarStatus(Patrimonio patrimonio);
    }
}
