using System.ComponentModel;

namespace Restaurante.Data.DTOs.Enum
{
    public enum TagsFood : byte
    {
        [Description("Almoço")]
        Almoco =1,

        [Description("Hamburguer")]
        Hamburguer =2,

        [Description("Pizza")]
        Pizza =3,

        [Description("Massa")]
        Massa =4,

        [Description("Sorvete")]
        Sorvete =5,

        [Description("Cafe")]
        Cafe = 6,

        [Description("Sushi")]
        Sushi = 7,

        [Description("Hotdog")]
        Hotdog = 8,
    }

    public static class TagsFoodExtension
    {
        public static string GetDescription(this TagsFood value) => TagsFoodExtension.GetDescription(value);
        public static string GetValue(this TagsFood value) => TagsFoodExtension.GetValue(value);
    }
}
