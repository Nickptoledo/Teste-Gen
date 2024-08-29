    using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;

namespace GerenciamentoAlunosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly string stringConexao = "Server=localhost;Database=BancoAlunos;Trusted_Connection=True;";

        [HttpGet]
        public ActionResult<IEnumerable<Aluno>> Get()
        {
            List<Aluno> alunos = new List<Aluno>();

            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string sql = "SELECT * FROM Alunos";
                using (SqlCommand comando = new SqlCommand(sql, conexao))
                {
                    using (SqlDataReader leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            alunos.Add(new Aluno
                            {
                                Id = leitor.GetInt32(0),
                                Nome = leitor.GetString(1),
                                Idade = leitor.GetInt32(2),
                                NotaPrimeiroSemestre = leitor.GetDouble(3),
                                NotaSegundoSemestre = leitor.GetDouble(4),
                                NomeProfessor = leitor.GetString(5),
                                NumeroSala = leitor.GetInt32(6)
                            });
                        }
                    }
                }
            }

            return Ok(alunos);
        }

        [HttpPost]
        public ActionResult Post(Aluno aluno)
        {
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string sql = @"INSERT INTO Alunos (Nome, Idade, NotaPrimeiroSemestre, NotaSegundoSemestre, NomeProfessor, NumeroSala) 
                               VALUES (@Nome, @Idade, @NotaPrimeiroSemestre, @NotaSegundoSemestre, @NomeProfessor, @NumeroSala)";
                using (SqlCommand comando = new SqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", aluno.Nome);
                    comando.Parameters.AddWithValue("@Idade", aluno.Idade);
                    comando.Parameters.AddWithValue("@NotaPrimeiroSemestre", aluno.NotaPrimeiroSemestre);
                    comando.Parameters.AddWithValue("@NotaSegundoSemestre", aluno.NotaSegundoSemestre);
                    comando.Parameters.AddWithValue("@NomeProfessor", aluno.NomeProfessor);
                    comando.Parameters.AddWithValue("@NumeroSala", aluno.NumeroSala);
                    comando.ExecuteNonQuery();
                }
            }

            return Ok();
        }
    }
}