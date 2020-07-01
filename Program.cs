using System;
using System.Collections.Generic;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Produto prod = new Produto();
            prod.Codigo = 3;
            prod.Nome = "Ps4";
            prod.Preco = 1899.90f;

            prod.InserirProduto(prod);
            prod.Remover("Ps4");

            List<Produto> lista = prod.Filtrar("Razer");
            lista = prod.Ler();

            foreach(Produto item in lista)
            {
                Console.WriteLine($"{prod.Nome} - R${prod.Preco}");
            }



        }
    }
}
