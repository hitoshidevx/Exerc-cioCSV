using System;
using System.Collections.Generic;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Produto prod = new Produto();
            prod.Codigo = 6;
            prod.Nome = "Switch";
            prod.Preco = 2900f;

            prod.Remover("6");

            prod.InserirProduto(prod);

            List<Produto> lista = prod.Ler();

            foreach (Produto item in lista)
            {
                Console.WriteLine($"R${item.Preco} - {item.Nome}");
            }




        }
    }
}
