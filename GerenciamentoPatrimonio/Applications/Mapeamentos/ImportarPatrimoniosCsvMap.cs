using CsvHelper.Configuration;
using GerenciamentoPatrimonio.DTO.PatrimonioDTO;

namespace GerenciamentoPatrimonio.Applications.Mapeamentos
{
    //ClassMap le o CSV que precisamos para importar
    public class ImportarPatrimoniosCsvMap : ClassMap<ImportarPatrimonioCsvDTO>
    {
        public ImportarPatrimoniosCsvMap()
        {
            Map(m => m.NumeroPatrimonio).Name("Nº invent.");
            Map(m => m.Denominacao).Name("Denominação imobilizado");
            Map(m => m.DataIncorporacao).Name("Dt. incorp.");
            Map(m => m.ValorAquisicao).Name("ValAlquis.");

        }
    }
}
