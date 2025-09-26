namespace Portal_dos_Agentes.Server.Services.Utilities
{
    public static class MesHelper
    {
        public static string MounthtoString(int mounth)
        {
            return mounth switch
            {
                1 => "Janeiro",
                2 => "Fevereiro",
                3 => "Março",
                4 => "Abril",
                5 => "Maio",
                6 => "Junho",
                7 => "Julho",
                8 => "Agosto",
                9 => "Setembro",
                10 => "Outubro",
                11 => "Novembro",
                12 => "Dezembro",
                _ => throw new ArgumentOutOfRangeException(nameof(mounth), "Mês inválido")
            };
        }
    }
}
