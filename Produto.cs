using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace Aula27_28_29_30
{
    public class Produto
    {
        
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }

        //Solução do desafio proposto.
        private const string PATH2 = "Database";
        DirectoryInfo folder = Directory.CreateDirectory(PATH2);
        private const string PATH = "Database/Produto.csv";

        public Produto()
        {

            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }

        }

        public void InserirProduto(Produto prod)
        {
            string[] linha = new string[] { PrepararLinhaCSV(prod) };
            File.AppendAllLines(PATH, linha);
        }


        /// <summary>
        /// Leitura da lista de produtos.
        /// </summary>
        /// <returns>
        /// Retorna a lista de produtos feita.
        /// </returns>
        public List<Produto> Ler()
        {
            //Criei a lista;
            List<Produto> produtos = new List<Produto>();

            //Transformei as linhas em um array de string;
            string[] linhas = File.ReadAllLines(PATH);

            //Varri o array em strings
            foreach(string linha in linhas)
            {
                //Separei os dados entre os ";"
                string[] dados = linha.Split(";");

                //Tratei os dados separando declarando um novo tipo de "produto";
                Produto p   = new Produto(); 
                p.Codigo    = Int32.Parse( Separar(dados[0]) );
                p.Nome      = Separar(dados[1]);
                p.Preco     = float.Parse( Separar(dados[2]) );

                //Adicionei o produto na lista antes do return;
                produtos.Add(p);
            }

            produtos = produtos.OrderBy(z => z.Nome).ToList();
            //Retornei o produto;
            return produtos;
        }

        
        public List<Produto> Filtrar(string _nome){

            return Ler().FindAll(x => x.Nome == _nome);

        }

        public void Remover(string _term)
        {
            //Criei uma lista de linhas dos produtos para fazer uma espécie de backup
            List<string> linhas = new List<string>();

            using(StreamReader file = new StreamReader(PATH))
            {
                string line;
                while((line = file.ReadLine()) != null){

                    linhas.Add(line);

                }
                linhas.RemoveAll(y => y.Contains(_term));
            }

            //Criei uma forma de reescrita
            using(StreamWriter output = new StreamWriter(PATH)){

                foreach(string ln in linhas)
                {
                    output.Write(ln+"\n");
                }

            }

        }


        /// <summary>
        /// Método para separar os dados e colocá-los em um array;
        /// Método utilizado = Split (serve para separar);
        /// </summary>
        /// <returns>
        /// Retorna o dado que foi separado em array escolhido; que no caso seria o dado (p.Codigo/p.Nome/p.Preco) de fato;
        /// </returns>
        public string Separar(string dados){
            //Separei os dados em dois
            //Antes: código = {p.Codigo};
            //Agora: código[0] | {p.Codigo}[1];
            return dados.Split("=")[1];

        }
        public string PrepararLinhaCSV(Produto p)
        {
            return $"código={p.Codigo};nome={p.Nome};preço={p.Preco};";
        }

    }
}