using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Repositories;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();

        SolicitacaoTransferencia BuscarPorID(Guid TransferenciaID);

        bool ExisteSolicitacaoPendente(Guid patrimonioID);

        bool UsuarioResponsavelDaLocalizacao(Guid usuarioID, Guid localizacaoID);

        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);

        void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);

        bool LocalizacaoExiste(Guid localizacaoID);
        Patrimonio BuscarPatrimonioPorID(Guid patrimonioID);
    }
}
