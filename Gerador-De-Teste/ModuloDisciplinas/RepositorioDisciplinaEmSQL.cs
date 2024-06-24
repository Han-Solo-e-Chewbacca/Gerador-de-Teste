﻿using Microsoft.Data.SqlClient;

namespace Gerador_De_Teste.ModuloDisciplinas
{
    public class RepositorioDisciplinaEmSql : IRepositorioDisciplina
    {
        private string enderecoBanco;

        public RepositorioDisciplinaEmSql()
        {
            enderecoBanco = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GeradorDeTestesDb;Integrated Security=True;Pooling=False";
        }

        public void Cadastrar(Disciplina novaDisciplina)
        {
            string sqlInserir =
                @"INSERT INTO [TBDisciplina]
                    (
                        [NOME]
                   
                    )
                    VALUES
                    (
                        @NOME
                    ); SELECT SCOPE_IDENTITY();";

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosContato(novaDisciplina, comandoInsercao);

            conexaoComBanco.Open();

            object id = comandoInsercao.ExecuteScalar();

            novaDisciplina.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }

        public bool Editar(int id, Disciplina disciplinaEditada)
        {
            string sqlEditar =
                @"UPDATE [TBDisciplina]	
		            SET
			            [NOME] = @NOME,			           
		            WHERE
			            [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            disciplinaEditada.Id = id;

            ConfigurarParametrosContato(disciplinaEditada, comandoEdicao);

            conexaoComBanco.Open();

            int numeroRegistrosAfetados = comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            if (numeroRegistrosAfetados < 1)
                return false;

            return true;
        }

        public bool Excluir(int id)
        {
            string sqlExcluir =
                @"DELETE FROM [TBDisciplina]
		            WHERE
			            [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

            if (numeroRegistrosExcluidos < 1)
                return false;

            return true;
        }

        public Disciplina SelecionarPorId(int idSelecionado)
        {
            string sqlSelecionarPorId =
                @"SELECT 
		            [ID], 
		            [NOME]		          
	            FROM 
		            [TBDisciplina]
                WHERE
                    [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", idSelecionado);

            conexaoComBanco.Open();

            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Disciplina disciplina = null;

            if (leitor.Read())
                disciplina = ConverterParaDisciplina(leitor);

            conexaoComBanco.Close();

            return disciplina;
        }

        public List<Disciplina> SelecionarTodos()
        {
            string sqlSelecionarTodos =
                @"SELECT 
		            [ID], 
		            [NOME] 		       
	            FROM 
		            [TBDisciplina]";

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            List<Disciplina> disciplinas = new List<Disciplina>();

            while (leitorDisciplina.Read())
            {
                Disciplina disciplina = ConverterParaDisciplina(leitorDisciplina);

                disciplinas.Add(disciplina);
            }

            conexaoComBanco.Close();

            return disciplinas;
        }

        private Disciplina ConverterParaDisciplina(SqlDataReader leitor)
        {
            Disciplina disciplina = new Disciplina()
            {
                Id = Convert.ToInt32(leitor["ID"]),
                Nome = Convert.ToString(leitor["NOME"]),
                
            };

            return disciplina;
        }

        private void ConfigurarParametrosContato(Disciplina disciplina, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", disciplina.Id);
            comando.Parameters.AddWithValue("NOME", disciplina.Nome);
        }

        

        
    }
}
