using System.ComponentModel.DataAnnotations;

namespace GerenciamentoPatrimonio.DTO.AreaDTO
{
    public class CriarAreaDTO
    {
        [Required(ErrorMessage = "O nome da área é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome da área deve ter no máximo 50 caracteres")]
        public string NomeArea {  get; set; } = string.Empty;

        //.Empty é proibido ser null
        //string? pode ser null
        //null! "calma a xerequinha" nao vai ter null


    }
}
