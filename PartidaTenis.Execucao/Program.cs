using System;

namespace PartidaTenis.Execucao
{
    internal class Program
    {

        #region Main Method
        
        static void Main(string[] args)
        {
            while ("S".Equals(Continuar()))
            {
                Console.WriteLine("");
                Jogar();
            }

            Despedida();
        }

        #endregion

        #region Private Methods

        private static string Continuar()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Começar mais um jogo? (S/N): ");
            return Console.ReadLine().ToUpper();
        }

        private static void Despedida()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Tchau...");
            Console.ReadKey();
            //System.Threading.Thread.Sleep(1500);
        }

        private static void Jogar()
        {
            var jogador1 = new Jogador(1, "Nadal");
            var jogador2 = new Jogador(2, "Djokovic");
            var endMatch = false;
            int jogadorDaVez;
            var random = new Random();

            while (!endMatch)
            {
                bool endGame = false;

                while (!endGame)
                {
                    System.Threading.Thread.Sleep(500);

                    jogadorDaVez = random.Next(1, 100000);

                    if (jogadorDaVez % 2 == 0)
                    {
                        jogador1.MarcarPonto(jogador2);
                    }
                    else
                    {
                        jogador2.MarcarPonto(jogador1);
                    }

                    if (jogador1.Pontos == PontosTenis.DEUCE)
                    {
                        EscreverDeuce(jogador1);
                    }
                    else if (jogador1.Pontos != PontosTenis.WIN && jogador2.Pontos != PontosTenis.WIN)
                    {
                        EscreverPlacarNormal(jogador1, jogador2);
                    }

                    endGame = jogador1.Pontos == PontosTenis.WIN || jogador2.Pontos == PontosTenis.WIN;
                }

                EscreverGame(jogador1, jogador2);

                if (jogador1.ShowSets || jogador2.ShowSets)
                {
                    EscreverSets(jogador1, jogador2);
                }

                endMatch = jogador1.Sets == 3 || jogador2.Sets == 3;
                RestartGame(jogador1, jogador2);
            }
        }

        private static void EscreverSets(Jogador jogador1, Jogador jogador2)
        {
            jogador1.ShowSets = false;
            jogador2.ShowSets = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Sets: {jogador1.Nome} {jogador1.Sets} x {jogador2.Sets} {jogador2.Nome}\n");
            jogador1.RestartGame();
            jogador2.RestartGame();
        }

        private static void EscreverGame(Jogador jogador1, Jogador jogador2)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nGame: {jogador1.Nome} {jogador1.Games} x {jogador2.Games} {jogador2.Nome}\n");
        }

        private static void EscreverPlacarNormal(Jogador jogador1, Jogador jogador2)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Placar: {jogador1.Nome} {jogador1.Pontos.GetDescription()} x {jogador2.Pontos.GetDescription()} {jogador2.Nome}");
        }

        private static void EscreverDeuce(Jogador jogador1)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Placar: {jogador1.Pontos.GetDescription()}");
        }

        private static void RestartGame(Jogador jogador1, Jogador jogador2)
        {
            jogador1.Restart();
            jogador2.Restart();
        }

        #endregion

    }
}

/* 
Partida de Tênis (https://dojopuzzles.com/problems/partida-de-tenis/)

Este problema foi utilizado em 291 Dojo(s).

Neste problema você deverá implementar as regras de um jogo de tênis simples (apenas dois jogadores).

As regras de um jogo de tênis tem diversos detalhes, mas para simplificar o problema, 
você deve implementar apenas as regras de um game:

    Em uma game cada jogador pode ter a seguinte pontuação: 0, 15, 30, ou 40;
    Os jogadores sempre começam com 0 pontos;
    Se o jogador possui 40 pontos e ganha a disputa, ele vence o game;
    Se ambos jogadores atingem 40 pontos, ocorre um empate (deuce);
    Estando em empate, o jogador que ganhar a bola seguinte está em vantagem (advantage);
    Se um jogador em vantagem ganha novamente a bola, ele vence o game;
    Se um jogador estiver em vantagem e o outro ganhar a bola, volta a ocorrer o empta (deuce).

Caso tenha tempo e vontade de melhorar o seu código, você pode implementar mais regras do tênis 
(serviço, sets, tie-break, etc). Mais informações sobre as regras em http://pt.wikipedia.org/wiki/T%C3%A9nis

Adaptado de: http://codingdojo.org/cgi-bin/wiki.pl?KataTennis
*/