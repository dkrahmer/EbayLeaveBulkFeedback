using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EbayLeaveBulkFeedback
{
	internal class Helpers
	{
		/// <summary>
		/// Extracts all numbers from the string.
		/// </summary>
		/// <param name="source">The string containing numbers.</param>
		/// <returns>An array of the numbers as strings.</returns>
		public static string[] ExtractStringsByRegex(string source, string regEx)
		{
			if (source == null)
			{
				return new string[0];
			}

			// Match the digits
			MatchCollection matches = Regex.Matches(source, regEx, RegexOptions.Multiline);

			// Move to a string array
			var strings = new List<string>();

			for (int index = 0; index < matches.Count; index++)
			{
				Match match = matches[index];

				if (match.Success)
				{
					var target = match.Groups["target"];
					if (target != null && target.Success)
					{
						strings.Add(target.Value);
					}
					else
					{
						strings.Add(match.Value);
					}
				}
			}

			return strings.ToArray();
		}
	}
}
