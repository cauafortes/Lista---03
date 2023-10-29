﻿class Program
{
    struct DadosLivros
    {
        public string titulo;
        public string autor;
        public int ano;
        public int prateleira;
    }
    static void addLivro(List<DadosLivros> lista)
    {
        DadosLivros dadosLivros = new DadosLivros();
        Console.WriteLine("Digite o Título do Livro: ");
        dadosLivros.titulo = Console.ReadLine();
        Console.WriteLine("Digite o nome do Autor: ");
        dadosLivros.autor = Console.ReadLine();
        Console.WriteLine("Digite o Ano do Livro: ");
        dadosLivros.ano = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Digite a Prateleira do Livro: ");
        dadosLivros.prateleira = Convert.ToInt32(Console.ReadLine());
        lista.Add(dadosLivros);
    }

    static void buscarTitulo(List<DadosLivros> lista, string titulo)
    {
        int qtd = lista.Count();
        bool flag = false;

        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].titulo.ToUpper().Contains(titulo.ToUpper()))
            {
                flag = true;
                Console.WriteLine($"Título do Livro: {lista[i].titulo}");
                Console.WriteLine($"Prateleira do Livro: {lista[i].prateleira}");
            }
        }
        if (!flag)
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    static void listarLivros(List<DadosLivros> lista)
    {
        int qtd = lista.Count();

        for (int i = 0; i < qtd; i++)
        {
            Console.WriteLine($"Título do Livro: {lista[i].titulo}");
            Console.WriteLine($"Autor do Livro: {lista[i].autor}");
            Console.WriteLine($"Ano do Livro: {lista[i].ano}");
            Console.WriteLine($"Prateleira do Livro: {lista[i].prateleira}");
            Console.WriteLine("\n");
        }
    }

    static void listarAno(List<DadosLivros> lista, int ano)
    {
        int qtd = lista.Count();
        Console.WriteLine($"Livros mais novos que {ano}: ");
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].ano > ano)
            {
                Console.WriteLine($"Título do Livro: {lista[i].titulo}");
                Console.WriteLine($"Ano do Livro: {lista[i].ano}");
            }
            Console.WriteLine("\n");
        }
    }
    static int menu()
    {
        Console.WriteLine("**Menu**");
        Console.WriteLine("1 - Adicionar Livro: ");
        Console.WriteLine("2 - Buscar Livro: ");
        Console.WriteLine("3 - Listar Livros: ");
        Console.WriteLine("4 - Procurar por livros mais novos: ");
        Console.WriteLine("0 - Sair: ");
        int op = int.Parse(Console.ReadLine());
        return op;
    }

    static void salvarDados(List<DadosLivros> lista, string nomeArquivo)
    {

        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (DadosLivros livros in lista)
            {
                writer.WriteLine($"{livros.titulo},{livros.autor},{livros.ano},{livros.prateleira}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");


    }

    static void carregarDados(List<DadosLivros> lista, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(',');
                DadosLivros livros = new DadosLivros
                {
                    titulo = campos[0],
                    autor = campos[1],
                    ano = int.Parse(campos[2]),
                    prateleira = int.Parse(campos[3])
                };
                lista.Add(livros);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
    static void Main()
    {
        List<DadosLivros> listaDadosLivros = new List<DadosLivros>();

        int op;

        do
        {
            op = menu();

            switch (op)
            {
                case 1:
                    addLivro(listaDadosLivros);
                    break;
                case 2:
                    Console.WriteLine("Digite o Título do Livro que deseja buscar: ");
                    string titulo = Console.ReadLine();
                    buscarTitulo(listaDadosLivros, titulo);
                    break;
                case 3:
                    listarLivros(listaDadosLivros);
                    break;
                case 4:
                    Console.WriteLine("Digite o ano para filtrar: ");
                    int ano = int.Parse(Console.ReadLine());
                    listarAno(listaDadosLivros, ano);
                    break;
                case 0:
                    salvarDados(listaDadosLivros, "dados.txt");
                    break;
                default:
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (op != 0);


    }
}