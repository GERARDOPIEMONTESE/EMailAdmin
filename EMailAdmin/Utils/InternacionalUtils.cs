namespace EMailAdmin.Utils
{
    public class InternacionalUtils
    {
        public static string GetCulture(int idIdioma)
        {
            switch (idIdioma)
            {
                case 1:
                    return "es";
                case 2:
                    return "en";
                case 3:
                    return "pt";
                default://RESTO
                    return "es";
            }
        }
    }
}