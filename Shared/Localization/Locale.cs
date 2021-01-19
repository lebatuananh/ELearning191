namespace Shared.Localization
{
    public enum Locale
    {
        English = 1,
        Vietnamese,
    }

    public static class CultureName
    {
        public const string Vietnamese = "vi";
        public const string English = "en";

        public static string Default = Vietnamese;
    }
}
