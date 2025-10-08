using System.ComponentModel.DataAnnotations;

namespace LoginApp.Models
{
    public class Cliente
    {
        [Display(Name = "Código", Description = "Código")]
        public int Id { get; set; }
        [Display(Name = "Nome completo", Description = "Nome e sobrenome")]
        public string Nome{ get; set; }
        [Display(Name = "Data de Nascimento", Description = "Data no qual o cliente nasceu")]
        public DateOnly DataNasc { get; set; }
        [Display(Name = "Sexo", Description = "Sexo")]
        public string Sexo{ get; set; }
        [Display(Name = "CPF", Description ="CPF do Cliente")]
        public decimal CPF { get; set; }
        [Display(Name = "Senha", Description="Senha do cliente")]
        public string Senha { get; set; }
        [Display(Name ="Situação", Description="Situação do cliente")]
        public string Situacao { get; set; }
         

    }
}
