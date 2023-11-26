using System.ComponentModel;

namespace Restaurante.Data.DTOs.Enum
{
    public enum TipoUser : byte
    {
        [Description("Cliente")]
        Cliente = 1,

        [Description("Administrador")]
        Admin = 2,
    }

    public static class TipoUserExtension
    {
        public static string GetDescription(this TipoUser value) => GetDescription(value);
    }
}
