using System;

namespace HackfestBotBase.Settings
{
    /// <summary>
    /// A helper class to provide string extensions
    /// </summary>
    public static class SettingsHelper
    {
        /// <summary>
        /// Converts the string representation of a number to an integer.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int")]
        public static int ToInt(this string item)
        {
            int number;
            if (int.TryParse(item, out number))
            {
                return number;
            }
            string error = $"The configuration setting '{item}' cannot be converted to an int";
            throw new InvalidOperationException(error);
        }

        /// <summary>
        /// Converts the string representation of a number to a double.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int")]
        public static double ToDouble(this string item)
        {
            double number;
            if (double.TryParse(item, out number))
            {
                return number;
            }
            string error = $"The configuration setting '{item}' cannot be converted to a double";
            throw new InvalidOperationException(error);
        }

        /// <summary>
        /// To the bool.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bool")]
        public static bool ToBool(this string item)
        {
            bool result;
            if (bool.TryParse(item, out result))
            {
                return result;
            }

            string error = $"The configuration setting '{item}' cannot be converted to boolean item";
            throw new InvalidOperationException(error);
        }
    }
}