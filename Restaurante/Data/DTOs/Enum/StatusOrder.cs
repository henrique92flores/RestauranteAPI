using System.ComponentModel;

namespace Restaurante.Data.DTOs.Enum
{
    public enum StatusOrder : int
    {
        [Description("Criado")]
        Criado = 1,

        [Description("Pago")]
        Pago = 2,
    }

    public static class StatusOrderExtension
    {
        public static string GetDescription(this StatusOrder value) => GetDescription(value);
    }
}
