using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class StatusTransferencia
{
    public Guid StatusTransferenciaID { get; set; }

    public string NomeStatus { get; set; } = null!;
}
