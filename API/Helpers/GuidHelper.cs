using System.Text.RegularExpressions;

namespace API.Helpers
{
    public static class GuidHelper
    {
        // This RegEx enforces uppercase letters and numbers, with a length of exactly 32.
        private static Regex _regex = new Regex(@"^[A-Z0-9]{32}$");
        public static string GenerateNewGuid()
        {
            // Formatting to "N" removes any hyphens or braces, resulting in 32 char length string with just digits & letters
            return Guid.NewGuid().ToString("N").ToUpperInvariant();
        }

        public static bool IsValidGuid(string guid)
        {
            var match = _regex.Match(guid);
            return match.Success;
        }
    }
}
