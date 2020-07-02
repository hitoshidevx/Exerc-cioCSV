using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula27_28_29_30
{
    public class Produto : IProduto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }

        private const string PATH = "Database/produto.csv";

        public Produto()
        {
            // ------------------------------------------------
            // Solução do desafio proposto pelo professor (método para criar pasta caso não exista)
            string pasta = PATH.Split('/')[0];

            if(!Directory.Exists(pasta)){
                Directory.CreateDirectory(pasta);
            }
            // ------------------------------------------------

            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        /// <summary>
        /// Cadastra um produto
        /// </summary>
        /// <param name="prod">Objeto Produto</param>
        public void Cadastrar(Produto prod)
        {
            var linha = new string[] { PrepararLinha(prod) };
            File.AppendAllLines(PATH, linha);
        }

        /// <summary>
        /// Lê o csv 
        /// </summary>
        /// <returns>Lista de produtos</returns>
        public List<Produto> Ler()
        {
            // Criei uma lista que servirá como nosso retorno
            List<Produto> produtos = new List<Produto>();

            // Li o arquivo e transformamos em um array de linhas
            
            string[] linhas = File.ReadAllLines(PATH);

            foreach(string linha in linhas){
                
                // Separei os dados de cada linha com Split
                // [0] = codigo=2
                // [1] = nome=Xbox one
                // [2] = preco=1900
                string[] dado = linha.Split(";");

                // Criei instâncias de produtos para serem colocados na lista
                Produto p   = new Produto();
                p.Codigo    = Int32.Parse( Separar(dado[0]) );
                p.Nome      = Separar(dado[1]);
                p.Preco     = float.Parse( Separar(dado[2]) );

                produtos.Add(p);
            }

            produtos = produtos.OrderBy(y => y.Nome).ToList();
            return produtos; 
        }

        /// <summary>
        /// Remove uma ou mais linhas que contenham o termo
        /// </summary>
        /// <param name="_termo">termo para ser buscado</param>
        public void Remover(string _termo){

            // Criei uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas = new List<string>();

            // Utilizei a bliblioteca StreamReader para ler o .csv
            using(StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            // Removi as linhas que tiverem o termo passado como argumento
            linhas.RemoveAll(l => l.Contains(_termo));

            // Reescrevi o csv do zero
            ReescreverCSV(linhas);
        }
        
        /// <summary>
        /// Altera um produto
        /// </summary>
        /// <param name="_produtoAlterado">Objeto de Produto</param>
        public void Alterar(Produto _produtoAlterado){

            // Criei uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas = new List<string>();

            // Utilizei a biblioteca StreamReader para ler o .csv
            using(StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }
            // codigo=2;nome=Xbox one;preco=1900
            // linhas.RemoveAll(y => y.Split(";")[0].Contains(_produtoAlterado.Codigo.ToString()));
            
            // codigo= 2; nome=Ibanez;preco=7500
            linhas.RemoveAll(y => y.Split(";")[0].Split("=")[1] == _produtoAlterado.Codigo.ToString());

            // Adicionei a linha alterada na lista de backup
            linhas.Add( PrepararLinha(_produtoAlterado) );

            // Reescrevi o csv do zero
            ReescreverCSV(linhas);         
        }


        private void ReescreverCSV(List<string> lines){
            // Reescrevi o csv do zero
            using(StreamWriter output = new StreamWriter(PATH))
            {
                foreach(string ln in lines)
                {
                    output.Write(ln + "\n");
                }
            }   
        }

        public List<Produto> Filtrar(string _nome)
        {
            return Ler().FindAll(x => x.Nome == _nome);
        }

        private string Separar(string _coluna)
        {
            return _coluna.Split("=")[1];
        }

        private string PrepararLinha(Produto p)
        {
            return $"codigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }

    }
}