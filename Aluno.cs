namespace GerenciamentoAlunosAPI
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public double NotaPrimeiroSemestre { get; set; }
        public double NotaSegundoSemestre { get; set; }
        public string NomeProfessor { get; set; }
        public int NumeroSala { get; set; }
    }
}