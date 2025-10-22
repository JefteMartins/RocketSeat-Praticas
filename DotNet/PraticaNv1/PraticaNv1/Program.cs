using System.Globalization;

bool continuar = true;

while (continuar)
{
    Console.Clear();
    Console.WriteLine("=== MENU DE EXERCÍCIOS PRÁTICOS ===\n");
    Console.WriteLine("1. Mensagem de boas-vindas personalizada");
    Console.WriteLine("2. Concatenar nome e sobrenome");
    Console.WriteLine("3. Operações matemáticas com dois números");
    Console.WriteLine("4. Contador de caracteres");
    Console.WriteLine("5. Validador de placa de veículo");
    Console.WriteLine("6. Exibir data atual em diferentes formatos");
    Console.WriteLine("0. Sair");
    Console.Write("\nEscolha um exercício (0-6): ");

    string? opcao = Console.ReadLine();
    Console.WriteLine();

    switch (opcao)
    {
        case "1":
            Exercicio1();
            break;
        case "2":
            Exercicio2();
            break;
        case "3":
            Exercicio3();
            break;
        case "4":
            Exercicio4();
            break;
        case "5":
            Exercicio5();
            break;
        case "6":
            Exercicio6();
            break;
        case "0":
            continuar = false;
            Console.WriteLine("Encerrando o programa. Até logo!");
            break;
        default:
            Console.WriteLine("Opção inválida! Tente novamente.");
            break;
    }

    if (continuar && opcao != "0")
    {
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
}

// Exercício 1: Mensagem de boas-vindas personalizada
static void Exercicio1()
{
    Console.WriteLine("--- EXERCÍCIO 1: MENSAGEM DE BOAS-VINDAS ---\n");
    Console.Write("Digite seu nome: ");
    string? nome = Console.ReadLine();
    Console.WriteLine($"Olá, {nome}! Seja muito bem-vindo!");
}

// Exercício 2: Concatenar nome e sobrenome
static void Exercicio2()
{
    Console.WriteLine("--- EXERCÍCIO 2: CONCATENAR NOME E SOBRENOME ---\n");
    Console.Write("Digite seu nome: ");
    string? nome = Console.ReadLine();
    Console.Write("Digite seu sobrenome: ");
    string? sobrenome = Console.ReadLine();
    string nomeCompleto = nome + " " + sobrenome;
    Console.WriteLine($"Nome completo: {nomeCompleto}");
}

// Exercício 3: Operações matemáticas
static void Exercicio3()
{
    Console.WriteLine("--- EXERCÍCIO 3: OPERAÇÕES MATEMÁTICAS ---\n");
    Console.Write("Digite o primeiro número: ");
    double numero1 = Convert.ToDouble(Console.ReadLine());
    Console.Write("Digite o segundo número: ");
    double numero2 = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine($"\nResultados:");
    Console.WriteLine($"Soma: {numero1} + {numero2} = {numero1 + numero2}");
    Console.WriteLine($"Subtração: {numero1} - {numero2} = {numero1 - numero2}");
    Console.WriteLine($"Multiplicação: {numero1} × {numero2} = {numero1 * numero2}");

    if (numero2 != 0)
    {
        Console.WriteLine($"Divisão: {numero1} ÷ {numero2} = {numero1 / numero2}");
    }
    else
    {
        Console.WriteLine("Divisão: Não é possível dividir por zero!");
    }

    Console.WriteLine($"Média: ({numero1} + {numero2}) ÷ 2 = {(numero1 + numero2) / 2}");
}

// Exercício 4: Contador de caracteres
static void Exercicio4()
{
    Console.WriteLine("--- EXERCÍCIO 4: CONTADOR DE CARACTERES ---\n");
    Console.Write("Digite uma ou mais palavras: ");
    string? texto = Console.ReadLine();

    if (texto != null)
    {
        int totalCaracteres = texto.Length;
        int caracteressSemEspaco = 0;

        foreach (char c in texto)
        {
            if (c != ' ')
            {
                caracteressSemEspaco++;
            }
        }

        Console.WriteLine($"\nTotal de caracteres (com espaços): {totalCaracteres}");
        Console.WriteLine($"Total de caracteres (sem espaços): {caracteressSemEspaco}");
    }
}

// Exercício 5: Validador de placa
static void Exercicio5()
{
    Console.WriteLine("--- EXERCÍCIO 5: VALIDADOR DE PLACA DE VEÍCULO ---\n");
    Console.WriteLine("Padrão brasileiro válido até 2018: ABC1234");
    Console.Write("Digite a placa do veículo: ");
    string? placa = Console.ReadLine();

    bool placaValida = false;

    if (placa != null && placa.Length == 7)
    {
        bool letrasPrimeiras = char.IsLetter(placa[0]) && char.IsLetter(placa[1]) && char.IsLetter(placa[2]);
        bool numerosUltimos = char.IsDigit(placa[3]) && char.IsDigit(placa[4]) && char.IsDigit(placa[5]) && char.IsDigit(placa[6]);

        placaValida = letrasPrimeiras && numerosUltimos;
    }

    Console.WriteLine($"\nResultado: {placaValida}");
}

// Exercício 6: Data em diferentes formatos
static void Exercicio6()
{
    Console.WriteLine("--- EXERCÍCIO 6: DATA ATUAL EM DIFERENTES FORMATOS ---\n");

    DateTime dataAtual = DateTime.Now;
    CultureInfo culturaBR = new CultureInfo("pt-BR");

    Console.WriteLine("1. Formato completo:");
    Console.WriteLine($"   {dataAtual.ToString("dddd, dd 'de' MMMM 'de' yyyy, HH:mm:ss", culturaBR)}");

    Console.WriteLine("\n2. Apenas a data (dd/MM/yyyy):");
    Console.WriteLine($" {dataAtual.ToString("dd/MM/yyyy")}");

    Console.WriteLine("\n3. Apenas a hora (formato 24h):");
    Console.WriteLine($"   {dataAtual.ToString("HH:mm:ss")}");

    Console.WriteLine("\n4. Data com mês por extenso:");
    Console.WriteLine($"   {dataAtual.ToString("dd 'de' MMMM 'de' yyyy", culturaBR)}");
}