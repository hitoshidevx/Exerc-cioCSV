using System.IO;
namespace Aula27_28_29_30
{
    public class Produto
    {
        
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }

        private const string PATH = "Database/Produto.csv";

        public Produto()
        {

            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }

        }

        public void InserirProduto(Produto prod)
        {
            var linha = new string [] { prod.PrepararLinhaCSV(prod) };
            File.AppendAllLines(PATH, linha);
        }

        public string PrepararLinhaCSV(Produto p)
        {
            return $"código={p.Codigo};nome={p.Nome};preço={p.Preco};";
        }

    }
}