using System;

using System.Collections.Generic;

class Program

{

    static Dictionary<string, (int vitorias, int empates, int derrotas)> jogadores

        = new Dictionary<string, (int, int, int)>();

    static Random random = new Random();

    static void Main()

    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ExibirMensagemInicial();

        while (true)

        {

            string jogadorAtual = ObterOuCriarJogador();

            JogarPartidas(jogadorAtual);

            char opcaoFinal = MenuFinal();

            if (opcaoFinal == '0')

                break;

            if (opcaoFinal == '2')

                ExibirEstatisticas();

        }

        Console.WriteLine("\n👋 Até a próxima!");

    }

    // =========================

    // MÉTODOS PRINCIPAIS

    // =========================

    static void ExibirMensagemInicial()

    {

        Console.WriteLine("😀 Olá! Vamos jogar Jokempô?");

        Console.WriteLine("Pressione qualquer tecla para começar...");

        Console.ReadKey();

    }

    static string ObterOuCriarJogador()

    {

        Console.WriteLine("\nDigite o nome do jogador:");

        string nome = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(nome))

        {

            Console.WriteLine("Nome inválido. Digite novamente:");

            nome = Console.ReadLine();

        }

        if (!jogadores.ContainsKey(nome))

        {

            jogadores[nome] = (0, 0, 0);

        }

        return nome;

    }

    static void JogarPartidas(string jogador)

    {

        char continuar;

        do

        {

            int escolhaJogador = ObterEscolhaJogador();

            int escolhaPC = random.Next(0, 3);

            string resultado = DeterminarResultado(escolhaJogador, escolhaPC);

            AtualizarEstatisticas(jogador, resultado);

            Console.WriteLine($"\nResultado: {resultado}");

            Console.WriteLine("\nJogar novamente? 1 - Sim | 0 - Não");

            continuar = ValidarEntrada('0', '1');

        } while (continuar == '1');

    }

    static char MenuFinal()

    {

        Console.WriteLine("\nO que deseja fazer?");

        Console.WriteLine("1 - Trocar jogador");

        Console.WriteLine("2 - Ver estatísticas");

        Console.WriteLine("0 - Sair");

        return ValidarEntrada('0', '1', '2');

    }

    // =========================

    // LÓGICA DO JOGO

    // =========================

    static int ObterEscolhaJogador()

    {

        Console.WriteLine("\nEscolha:");

        Console.WriteLine("0 - Pedra ✊");

        Console.WriteLine("1 - Papel ✋");

        Console.WriteLine("2 - Tesoura ✌");

        char opcao = ValidarEntrada('0', '1', '2');

        return int.Parse(opcao.ToString());

    }

    static string DeterminarResultado(int jogador, int pc)

    {

        Console.WriteLine($"\nVocê escolheu {TraduzirOpcao(jogador)}");

        Console.WriteLine($"PC escolheu {TraduzirOpcao(pc)}");

        if (jogador == pc)

            return "Empate";

        if ((jogador == 0 && pc == 2) ||

            (jogador == 1 && pc == 0) ||

            (jogador == 2 && pc == 1))

            return "Vitória";

        return "Derrota";

    }

    static string TraduzirOpcao(int opcao)

    {

        return opcao switch

        {

            0 => "Pedra ✊",

            1 => "Papel ✋",

            2 => "Tesoura ✌",

            _ => ""

        };

    }

    static void AtualizarEstatisticas(string jogador, string resultado)

    {

        var stats = jogadores[jogador];

        switch (resultado)

        {

            case "Vitória":

                stats.vitorias++;

                break;

            case "Empate":

                stats.empates++;

                break;

            case "Derrota":

                stats.derrotas++;

                break;

        }

        jogadores[jogador] = stats;

    }

    // =========================

    // UTILITÁRIOS

    // =========================

    static char ValidarEntrada(params char[] opcoesValidas)

    {

        char entrada = Console.ReadKey().KeyChar;

        while (Array.IndexOf(opcoesValidas, entrada) == -1)

        {

            Console.WriteLine("\nOpção inválida. Tente novamente:");

            entrada = Console.ReadKey().KeyChar;

        }

        return entrada;

    }

    static void ExibirEstatisticas()

    {

        Console.WriteLine("\n===== ESTATÍSTICAS =====");

        foreach (var jogador in jogadores)

        {

            Console.WriteLine($"{jogador.Key}: " +

                $"{jogador.Value.vitorias} Vitórias | " +

                $"{jogador.Value.empates} Empates | " +

                $"{jogador.Value.derrotas} Derrotas");

        }

        Console.WriteLine("=========================");

    }

}
