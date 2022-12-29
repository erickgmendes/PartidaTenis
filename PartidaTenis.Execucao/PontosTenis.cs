using System.ComponentModel;

namespace PartidaTenis.Execucao
{
    public enum PontosTenis
    {
        [Description("00")]
        P0 = 0,

        [Description("15")]
        P15 = 15,
        
        [Description("30")]
        P30 = 30,
        
        [Description("40")]
        P40 = 40,
        
        [Description("DEUCE")]
        DEUCE = 100,
        
        [Description("AD")]
        ADVANTAGE = 200,
        
        [Description("WIN")]
        WIN = 1000,
    }
}