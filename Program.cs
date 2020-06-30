using System;
using System.Collections.Generic;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Produto prod = new Produto();
            prod.Codigo = 2;
            prod.Nome = "Deathadder V2";
            prod.Preco = 319.90f;

            prod.InserirProduto(prod);

            List<Produto> lista = new List<Produto>();
            lista = prod.Ler();

            foreach(Produto item in lista)
            {
                Console.WriteLine($"{prod.Nome} - R${prod.Preco}");
            }



        }
    }
}
