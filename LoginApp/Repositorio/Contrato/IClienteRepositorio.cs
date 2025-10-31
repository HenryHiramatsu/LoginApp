using LoginApp.Models;
using X.PagedList;

namespace LoginApp.Repositorio.Contrato
{
    public interface IClienteRepositorio
    {
        //Dentro do contrato temos ASSINATURAS DE MÉTODOS

        //Login Cliente
        Cliente Login(string Email, string Senha);
    
        //VOID

        void Cadastrar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Ativar(int id);
        void Desativar(int id);
        void Excluir(int id);
        Cliente ObterClienteId(int Id);
        Cliente BuscaEmailCliente(string Email);
        Cliente BuscaCpfCliente(string CPF);

        IEnumerable<Cliente> ObterTodosClientes();
        IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa);
    }
}
