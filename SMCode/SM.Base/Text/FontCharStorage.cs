namespace SM.Data.Fonts
{
    /// <summary>
    /// Contains default char sets.
    /// </summary>
    public class FontCharStorage
    {
        /// <summary>
        /// Contains the english alphabet and the common special character.
        /// </summary>
        public static readonly char[] SimpleUTF8 = new char[]
        {
            '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6',
            '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
            'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_', '`',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~'
        };
        /// <summary>
        /// Contains only numbers.
        /// </summary>
        public static readonly char[] Numbers = new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

    }
}