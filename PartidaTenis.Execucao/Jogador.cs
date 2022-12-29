using System.Collections.Generic;

namespace PartidaTenis.Execucao
{
    public class Jogador
    {

        #region Attributes

        private int _indicePontos = 0;

        private static readonly IList<PontosTenis> _pontosPossiveis
            = new List<PontosTenis>() {
                PontosTenis.P0,
                PontosTenis.P15,
                PontosTenis.P30,
                PontosTenis.P40
            };

        #endregion

        #region Properties

        public int Numero { get; private set; }
        public int Sets { get; private set; } = 0;
        public int Games { get; private set; } = 0;
        public string Nome { get; private set; }
        public PontosTenis Pontos { get; private set; } = _pontosPossiveis[0];
        public bool EndGame { get; private set; } = false;
        public bool ShowSets { get; set; } = false;

        #endregion

        public Jogador(int numero, string nome)
        {
            Numero = numero;
            Nome = nome;
        }

        #region Public Methods


        public PontosTenis MarcarPonto(Jogador outroJogador)
        {
            if (Pontos == PontosTenis.P30 && outroJogador.Pontos == PontosTenis.P40
                || Pontos == PontosTenis.P40 && outroJogador.Pontos == PontosTenis.ADVANTAGE)
            {
                Deuce(outroJogador);
            }
            else if (Pontos == PontosTenis.DEUCE && outroJogador.Pontos == PontosTenis.DEUCE)
            {
                Advantage(outroJogador);
            }
            else if (Pontos == PontosTenis.ADVANTAGE ||
                (Pontos == PontosTenis.P40 && (outroJogador.Pontos == PontosTenis.P0 || outroJogador.Pontos == PontosTenis.P15 || outroJogador.Pontos == PontosTenis.P30)))
            {
                Pontos = PontosTenis.WIN;

                SetEndGame(outroJogador);
            }
            else
            {
                Pontos = _pontosPossiveis[++_indicePontos];
            }

            return Pontos;
        }

        private void SetEndGame(Jogador outroJogador)
        {
            Games++;
            EndGame = true;

            if (Games >= 6 && Games - outroJogador.Games >= 2)
            {
                Sets++;
                ShowSets = true;
            }
            else
            {
                ShowSets = false;
            }
        }

        public void ZerarPontuacaoGame()
        {
            _indicePontos = 0;
            Pontos = PontosTenis.P0;
        }

        private void Advantage(Jogador outroJogador)
        {
            Pontos = PontosTenis.ADVANTAGE;
            outroJogador.OpponentAdvantage();
        }

        private void OpponentAdvantage()
        {
            Pontos = PontosTenis.P40;
            _indicePontos = 3;
        }

        public void Deuce(Jogador outroJogador)
        {
            Pontos = PontosTenis.DEUCE;
            outroJogador.Pontos = PontosTenis.DEUCE;
        }

        public void Restart()
        {
            _indicePontos = 0;
            EndGame = false;
            Pontos = _pontosPossiveis[0];
        }

        public void RestartGame()
        {
            Games = 0;
        }

        #endregion

    }
}
