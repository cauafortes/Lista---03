﻿class Program
{
    struct Eletrodomestico
    {
        public string nome;
        public double potencia;
        public double tempoMedAtivo;
    }

    static void addEletro(List<Eletrodomestico> lista)
    {
        Eletrodomestico eletro = new Eletrodomestico();
        Console.WriteLine("Digite o nome do Eletrodoméstico: ");
        eletro.nome = Console.ReadLine();
        Console.WriteLine("Digite a Potência: ");
        eletro.potencia = double.Parse(Console.ReadLine());
        Console.WriteLine("Digite o Tempo Médio Ativo por dia: ");
        eletro.tempoMedAtivo = double.Parse(Console.ReadLine());
        lista.Add(eletro);
    }

    static void listarEletro(List<Eletrodomestico> lista)
    {
        int qtd = lista.Count();
        Console.WriteLine("Lista de Eletrodomésticos: ");
        for (int i = 0; i < qtd; i++)
        {
            Console.WriteLine($"Nome do Eletrodoméstico: {lista[i].nome}");
            Console.WriteLine($"Potência: {lista[i].potencia}");
            Console.WriteLine($"Tempo Médio Ativo por dia: {lista[i].tempoMedAtivo}\n");
        }
    }

    static void buscarNome(List<Eletrodomestico> lista, string nome)
    {
        int qtd = lista.Count();

        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Equals(nome.ToUpper()))
            {
                Console.WriteLine("Dados do Eletrodoméstico: ");
                Console.WriteLine($"Nome do Eletrodoméstico: {lista[i].nome}");
                Console.WriteLine($"Potência: {lista[i].potencia}");
                Console.WriteLine($"Tempo Médio Ativo por dia: {lista[i].tempoMedAtivo}\n");
            }
        }
    }

    static void gastoMaior(List<Eletrodomestico> lista, double valor)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].potencia > valor)
            {
                Console.WriteLine("Dados do Eletrodoméstico: ");
                Console.WriteLine("Nome: " + lista[i].nome);
                Console.WriteLine("Potência: " + lista[i].potencia);
                Console.WriteLine("Tempo Médio Ativo por dia: " + lista[i].tempoMedAtivo);
            }
        }
    }

    static void calcularGasto(List<Eletrodomestico> lista, string nomeBusca, double valorKw)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].nome.ToUpper().Equals(nomeBusca.ToUpper()))
            {
                double consumoDia = lista[i].tempoMedAtivo * lista[i].potencia;
                double valorGastoDia = consumoDia * valorKw;

                Console.WriteLine($"Consumo por dia: " +
                    $"{Math.Round(consumoDia, 2)}, por mês: {Math.Round(consumoDia, 2) * 30}"
                    + $"\nGasto por dia: " +
                    $"R${valorGastoDia:F2}, por mês: R${valorGastoDia * 30:F2}");
            }
        }
    }


    static void salvarDados(List<Eletrodomestico> lista, string nomeArquivo)
    {

        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (Eletrodomestico eletro in lista)
            {
                writer.WriteLine($"{eletro.nome},{eletro.potencia},{eletro.tempoMedAtivo}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");
    }


    static void carregarDados(List<Eletrodomestico> lista, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(',');
                Eletrodomestico eletro = new Eletrodomestico
                {
                    nome = campos[0],
                    potencia = double.Parse(campos[1]),
                    tempoMedAtivo = double.Parse(campos[2]),

                };
                lista.Add(eletro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
    static int menu()
    {
        Console.WriteLine("Menu Principal");
        Console.WriteLine("1 - Adicionar Eletrodoméstico: ");
        Console.WriteLine("2 - Listar Eletrodomésticos: ");
        Console.WriteLine("3 - Buscar pelo Nome: ");
        Console.WriteLine("4 - Buscar por gasto maior que determinado valor: ");
        Console.WriteLine("5 - Calcular Consumo Diário e Mensal: ");
        Console.WriteLine("0 - Sair: ");
        int op = int.Parse(Console.ReadLine());
        return op;

    }
    static void Main()
    {
        List<Eletrodomestico> listaEletro = new List<Eletrodomestico>();

        int op;

        do
        {
            op = menu();

            switch (op)
            {
                case 1:
                    addEletro(listaEletro);
                    break;
                case 2:
                    listarEletro(listaEletro);
                    break;
                case 3:
                    Console.WriteLine("Digite o nome que deseja procurar: ");
                    string nomeBusca = Console.ReadLine();
                    buscarNome(listaEletro, nomeBusca);
                    break;
                case 4:
                    Console.WriteLine("Digite valor da potência: ");
                    double valor = double.Parse(Console.ReadLine());
                    gastoMaior(listaEletro, valor);
                    break;
                case 5:
                    Console.WriteLine("Digite o eletrodoméstico que deseja calcular: ");
                    nomeBusca = Console.ReadLine();
                    Console.WriteLine("Digite o valor do KW/h");
                    double valorKw = double.Parse(Console.ReadLine());
                    calcularGasto(listaEletro, nomeBusca, valorKw);

                    break;
                case 0:
                    Console.WriteLine("Saindo");
                    salvarDados(listaEletro, "dados.txt");
                    break;
                default:
                    Console.WriteLine("ERRO: Opção Inválida.");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (op != 0);
    }
}