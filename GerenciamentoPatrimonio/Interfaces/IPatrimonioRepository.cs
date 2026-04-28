using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorID(Guid patrimonioID);

        // fazer esse com AsQueryable igual foi feito no endereço
        bool BuscarPorNumeroPatrimonio(string numeroPatrimonio);

        bool LocalizacaoExiste(Guid localizacaoID);
        bool StatusPatrimonioExiste(Guid statusPatrimonioID);

        void Adicionar(Patrimonio patrimonio);
        void Atualizar(Patrimonio patrimonio);
        void AtualizarStatus(Patrimonio patrimonio);
        void AdicionarLog(LogPatrimonio logPatrimonio);

        Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao);

        StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);

        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
    }
}
