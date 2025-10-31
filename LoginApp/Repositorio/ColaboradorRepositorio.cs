using LoginApp.Models;
using LoginApp.Models.Constante;
using LoginApp.Repositorio.Contrato;
using MySql.Data.MySqlClient;
using System.Data;

namespace LoginApp.Repositorio
{
    public class ColaboradorRepositorio : IColaboradorRepositorio
    {
        // Propriedade Privada para injetar a conexão com o banco de dados ;
        private readonly string _conexaoMySQL;

        //Metodo construtor da classe ColaboradorRepository    
        public ColaboradorRepositorio(IConfiguration conf)
        {
            // Injeção de dependencia do banco de dados
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public Colaborador Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from Colaborador where Email = @Email and Senha = @Senha", conexao);

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Colaborador colaborador = new Colaborador();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    colaborador.Id = (Int32)(dr["Id"]);
                    colaborador.Nome = (string)(dr["Nome"]);
                    colaborador.Email = (string)(dr["Email"]);
                    colaborador.Senha = (string)(dr["Senha"]);
                    colaborador.TipoColaborador = (string)(dr["Tipo"]);
                }
                return colaborador;
            }
        }
        public void AtualizarSenha(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }
        public void Cadastrar(Colaborador colaborador)
        {
            string Comum = ColaboradorTipoConstante.Comum;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into Colaborador(Nome, Email, Senha, Tipo) " +
                                                     " values (@Nome, @Email, @Senha, @Tipo)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = colaborador.Nome;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = colaborador.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = colaborador.Senha;
                cmd.Parameters.Add("@Tipo", MySqlDbType.VarChar).Value = Comum;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public IEnumerable<Colaborador> ObterTodosColaboradores()
        {
            List<Colaborador> colabList = new List<Colaborador>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM COLABORADOR", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    colabList.Add(
                        new Colaborador
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Email = (string)(dr["Email"]),
                            Senha = (string)(dr["Senha"]),
                            TipoColaborador = (string)(dr["Tipo"])
                        });
                }
                return colabList;
            }
        }
        public Colaborador ObterColaborador(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Colaborador WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Colaborador colaborador = new Colaborador();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    colaborador.Id = (Int32)(dr["Id"]);
                    colaborador.Nome = (string)(dr["Nome"]);
                    colaborador.Email = (string)(dr["Email"]);
                    colaborador.Senha = (string)(dr["Senha"]);
                    colaborador.TipoColaborador = (string)(dr["Tipo"]);
                }
                return colaborador;
            }
        }
        public List<Colaborador> ObterColaboradorPorEmail(string email)
        {
            List<Colaborador> colabList = new List<Colaborador>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Colaborador WHERE email=@email ", conexao);
                cmd.Parameters.AddWithValue("@Id", email);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    colabList.Add(
                        new Colaborador
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = (string)(dr["Nome"]),
                            Senha = (string)(dr["Senha"]),
                            Email = (string)(dr["Email"]),
                            TipoColaborador = (string)(dr["Senha"])
                        });
                }
                return colabList;
            }
        }
        //Comentario
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Colaborador WHERE Id=@Id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Atualizar(Colaborador colaborador)
        {
            string Tipo = ColaboradorTipoConstante.Comum;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update Colaborador set Nome=@Nome, " +
                    " Email=@Email, Senha=@Senha, Tipo=@Tipo Where Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.VarChar).Value = colaborador.Id;
                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = colaborador.Nome;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = colaborador.Email;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = colaborador.Senha;
                cmd.Parameters.Add("@Tipo", MySqlDbType.VarChar).Value = Tipo;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
