using System;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Produto prod = new Produto();
            prod.Codigo = 1;
            prod.Nome = "Razer Deathadder V2";
            prod.Preco = 319.90f;

            prod.InserirProduto(prod);

        }
    }
}
