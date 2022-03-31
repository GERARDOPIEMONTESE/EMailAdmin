namespace EMailAdmin.Utils
{
    public class TagUtils
    {
        #region Private Methods

        private static string GenerateTag(string parameter)
        {
            return "${" + parameter + " }$";
        }

        private static string GenerateTag(string type, string parameter)
        {
            return "${" + type + "," + parameter + " }$";
        }

        #endregion Private Methods

        #region Methods

        public static string GenerateImageTag(string name)
        {
            return GenerateTag("IMAGE", name);
        }

        public static string GenerateLinkTag(string name)
        {
            return GenerateTag("LINK", name);
        }

        public static string GenerateVariableTextTag(string name)
        {
            return GenerateTag(name);
        }

        public static string GenerateContactTag(string type)
        {
            return GenerateTag("CONTACT", type);
        }

        public static string GenerateSignatureTag(string type)
        {
            return GenerateTag("SIGNATURE", type);
        }

        public static string GenerateCountryVisibleTextTag(string type)
        {
            return GenerateTag("CTRYVTEXT", type);
        }

        public static string GenerateUpgradeVariableTextTag(string type)
        {
            return GenerateTag("UPGTEXT", type);
        }

        public static string GenerateTableVariableTextTag(string type)
        {
            return GenerateTag("TABLE", type);
        }
        #endregion Methods

        internal static string GenerateConditionVariableTextTag(string type)
        {
            return GenerateTag("CONDITION", type);
        }

        internal static string GeneratePixelTag(string type)
        {
            return GenerateTag("PIXEL", type);
        }

        internal static string GenerateClausuleTag(string type)
        {
            return GenerateTag("CLAUSETEXT", type);
        }
    }
}