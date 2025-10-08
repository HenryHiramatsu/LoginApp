using System.ComponentModel.DataAnnotations;

namespace LoginApp.Models
{
    public class Colaborador
    {
        [Display(Name = "Código", Description = "Código")]
        public int Id { get; set; }
        [Display(Name = "Nome completo", Description = "Nome e sobrenome")]
        public string Nome { get; set; }
      
        [Display(Name = "Email", Description = "Email do colaborador")]
        public string Email{ get; set; }
        [Display(Name = "Senha", Description = "Senha do colaborador")]
        public string Senha { get; set; }
        [Display(Name = "Tipo", Description = "Tipo do colaborador")]
        public string TipoColaborador { get; set; }
    }
}
