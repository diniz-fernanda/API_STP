using Domain.Model;
using System.Data.SqlClient;

namespace Infra.Data
{
    public class DALProdutos
    {
        private readonly SqlConnection _conexao;
        public DALProdutos(SqlConnection conexao)
        {
            _conexao = conexao;
        }

        public async Task<Produtos> AddAsync(Produtos model)
        {
            _conexao.Open();
            using SqlCommand command = _conexao.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "stp_ProdutosAdd";
            command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 50).Value = model.Nome;
            command.Parameters.Add("@Valor", System.Data.SqlDbType.Decimal).Value = model.Valor;
            command.Parameters.Add("@Vendido", System.Data.SqlDbType.Bit).Value = model.Vendido;
            using(SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if(reader is not null && reader.HasRows && reader.Read())
                {
                    model.Id = (int)reader["Id"];
                }
            }
            _conexao.Close();
            return model;
        }

        public async Task<bool> AtualizarAsync(Produtos model)
        {
            bool status = false;
            _conexao.Open();
            using SqlCommand command = _conexao.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "stp_ProdutosAtualizar";
            command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = model.Id;
            command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 50).Value = model.Nome;
            command.Parameters.Add("@Valor", System.Data.SqlDbType.Decimal).Value = model.Valor;
            command.Parameters.Add("Vendido", System.Data.SqlDbType.Bit).Value = model.Vendido;
            status = await command.ExecuteNonQueryAsync() > 0;
            _conexao.Close();
            return status;
        }

        public async Task<Produtos> BuscarPorIdAsync(int id)
        {
            Produtos? model = null;
            _conexao.Open();
            using SqlCommand command = _conexao.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "stp_ProdutosBuscarPorId";
            command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = model.Id;
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if(reader is not null && reader.HasRows)
                {
                    model = new Produtos();
                    while (await reader.ReadAsync())
                    {
                        model.Id = (int)reader["Id"];
                        model.Nome = reader["Nome"].ToString();
                        model.Valor = (double)(decimal)reader["Valor"];
                        model.Vendido = (bool)reader["Vendido"];
                    }
                }
            }
            _conexao.Close();
            return model;
        }

        public async IAsyncEnumerable<Produtos> BuscarTodosAsync()
        {
            _conexao.Open();
            using SqlCommand command = _conexao.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "stp_ProdutosBuscarTodos";
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if(reader is not null && reader.HasRows)
                {
                    while(await reader.ReadAsync())
                    {
                        yield return new Produtos()
                        {
                            Id = (int)reader["Id"],
                            Nome = reader["Nome"].ToString(),
                            Valor = (double)(decimal)reader["Valor"],
                            Vendido = (bool)reader["Vendido"]
                        };
                    }
                }
            }
            _conexao.Close();
        }

        public async Task<bool> DeletarAsync (int id)
        {
            bool status = false;
            _conexao.Open();
            using SqlCommand command = _conexao.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "stp_ProdutosDeletar";
            command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
            status = await command.ExecuteNonQueryAsync() > 0;
            _conexao.Close();
            return status;
        }
    }
}
