using LoginApp.Models;
using LoginApp.Models.Constante;
using LoginApp.Repositorio.Contrato;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using X.PagedList;
using X.PagedList.Extensions;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace LoginApp.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly string _conexaoMySQL;

        public ClienteRepositorio(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public Cliente Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbCliente where Email = @Email and Senha = @Senha", conexao);
                //MySqlCommand cmd = new MySqlCommand ("Call proc_login(_email, _senha"),conexao)

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    cliente.Id = Convert.ToInt32(dr["Id"]);
                    cliente.Nome = Convert.ToString(dr["Nome"]);
                    cliente.Nascimento = Convert.ToDateTime(dr["Nascimento"]);

                    cliente.Sexo = Convert.ToString(dr["Sexo"]);
                    cliente.CPF = Convert.ToString(dr["CPF"]);
                    cliente.Telefone = Convert.ToString(dr["Telefone"]);
                    cliente.Situacao = Convert.ToString(dr["Situacao"]);

                    cliente.Email = Convert.ToString(dr["Email"]);
                    cliente.Senha = Convert.ToString(dr["Senha"]);
                }
                return cliente;
            }
        }
        public void Ativar(int Id)
        {
            string Situacao = SituacaoConstantes.Ativo;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Cliente set Situacao=@Situacao WHERE Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = Id;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Atualizar(Cliente cliente)
        {
            try
            {
                string Situacao = SituacaoConstantes.Ativo;
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("update Cliente set Nome=@Nome, Nascimento=@Nascimento, Sexo=@Sexo,  CPF=@CPF," +
                        " Telefone=@Telefone, Email=@Email, Senha=@Senha, Situacao=@Situacao WHERE Id=@Id ", conexao);

                    cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = cliente.Id;
                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                    cmd.Parameters.Add("@Nascimento", MySqlDbType.DateTime).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                    cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                    cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                    cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                    cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                    cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                    cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                    cmd.ExecuteNonQuery();
                    conexao.Close();
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro no banco ao atualizar cliente" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na aplicação");
            }
        }




        public void Cadastrar(Cliente cliente)
        {
            string Situacao = SituacaoConstantes.Ativo;
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();

                    MySqlCommand cmd = new MySqlCommand("insert into Cliente(Nome, Nascimento, Sexo,  CPF, Telefone, Email, Senha, Situacao) " +
                    " values (@Nome, @Nascimento, @Sexo, @CPF, @Telefone, @Email, @Senha, @Situacao)", conexao); // @: PARAMETRO


                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = cliente.Nome;
                    cmd.Parameters.Add("@Nascimento", MySqlDbType.DateTime).Value = cliente.Nascimento.ToString("yyyy/MM/dd");
                    cmd.Parameters.Add("@Sexo", MySqlDbType.VarChar).Value = cliente.Sexo;
                    cmd.Parameters.Add("@CPF", MySqlDbType.VarChar).Value = cliente.CPF;
                    cmd.Parameters.Add("@Telefone", MySqlDbType.VarChar).Value = cliente.Telefone;
                    cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = cliente.Email;
                    cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = cliente.Senha;
                    cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;

                    cmd.ExecuteNonQuery();
                    conexao.Close();


                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro no banco em cadastro cliente" + ex.Message);
            }
            catch (Exception ex)
            {
            }
        }

        public void Desativar(int Id)
        {
            string Situacao = SituacaoConstantes.Desativado;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Cliente set Situacao=@Situacao WHERE Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = Id;
                cmd.Parameters.Add("@Situacao", MySqlDbType.VarChar).Value = Situacao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Cliente WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }



        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> cliList = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM TbCLIENTE", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    cliList.Add(
                        new Cliente
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Nascimento = Convert.ToDateTime(dr["Nascimento"]),
                            Sexo = Convert.ToString(dr["Sexo"]),
                            CPF = Convert.ToString(dr["CPF"]),
                            Telefone = Convert.ToString(dr["Telefone"]),
                            Email = Convert.ToString(dr["Email"]),
                            Senha = Convert.ToString(dr["Senha"]),
                            Situacao = Convert.ToString(dr["Situacao"])
                        });
                }
            }
            return cliList;
        }


        public Cliente ObterClienteId(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Cliente WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Id = (Int32)(dr["Id"]);
                    cliente.Nome = (string)(dr["Nome"]);
                    cliente.Nascimento = (DateTime)(dr["Nascimento"]);
                    cliente.Sexo = (string)(dr["Sexo"]);
                    cliente.CPF = (string)(dr["CPF"]);
                    cliente.Telefone = (string)(dr["Telefone"]);
                    cliente.Email = (string)(dr["Email"]);
                    cliente.Senha = (string)(dr["Senha"]);
                    cliente.Situacao = (string)(dr["Situacao"]);
                }
                return cliente;
            }
        }
        public Cliente ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Cliente WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Id = (Int32)(dr["Id"]);
                    cliente.Nome = (string)(dr["Nome"]);
                    cliente.Nascimento = (DateTime)(dr["Nascimento"]);
                    cliente.Sexo = (string)(dr["Sexo"]);
                    cliente.CPF = (string)(dr["CPF"]);
                    cliente.Telefone = (string)(dr["Telefone"]);
                    cliente.Email = (string)(dr["Email"]);
                    cliente.Senha = (string)(dr["Senha"]);
                    cliente.Situacao = (string)(dr["Situacao"]);

                }
                return cliente;
            }
        }

        public Cliente BuscaCpfCliente(string CPF)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select CPF from Cliente WHERE CPF=@CPF ", conexao);
                cmd.Parameters.AddWithValue("@CPF", CPF);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.CPF = (string)(dr["CPF"]);

                }
                return cliente;
            }
        }
        public Cliente BuscaEmailCliente(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select Email from Cliente WHERE Email=@Email ", conexao);
                cmd.Parameters.AddWithValue("@Email", email);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.Email = (string)(dr["Email"]);

                }
                return cliente;
            }
        }


        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _config.GetValue<int>("RegistroPorPagina");

            int NumeroPagina = pagina ?? 1;

            var clientePesquisadoEmail = BuscaEmailCliente(pesquisa);

            //if (!string.IsNullOrEmpty(pesquisa))
            //{
            //    clientePesquisadoEmail = clientePesquisadoEmail.Where(a => a.Email == pesquisa);
            //}           

            List<Cliente> cliList = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM CLIENTE", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    cliList.Add(
                        new Cliente
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Nascimento = Convert.ToDateTime(dr["Nascimento"]),
                            Sexo = Convert.ToString(dr["Sexo"]),
                            CPF = Convert.ToString(dr["CPF"]),
                            Telefone = Convert.ToString(dr["Telefone"]),
                            Email = Convert.ToString(dr["Email"]),
                            Senha = Convert.ToString(dr["Senha"]),
                            Situacao = Convert.ToString(dr["Situacao"])
                        });
                }
                ;
                return cliList.ToPagedList<Cliente>(NumeroPagina, RegistroPorPagina);
            }
        }

    }
}
