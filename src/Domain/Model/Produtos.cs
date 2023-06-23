namespace Domain.Model
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; } 
        public double Valor { get; set; }
        public bool Vendido { get; set; }

        public Produtos(string nome, double valor, bool vendido)
        {
            Nome = nome;
            Valor = valor;
            Vendido = vendido;
        }

        public Produtos() 
        { 
        }
    }
}
