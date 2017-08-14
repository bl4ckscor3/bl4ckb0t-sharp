using System;
using System.Linq;

namespace bl4ckb0t.Util
{
	public class Colors
	{
		public static readonly string NORMAL = "\u000f";
		public static readonly string BOLD = "\u0002";
		public static readonly string UNDERLINE = "\u001f";
		public static readonly string ITALICS = "\u001d";
		public static readonly string REVERSE = "\u0016";
		public static readonly string WHITE = "\u000300";
		public static readonly string BLACK = "\u000301";
		public static readonly string DARK_BLUE = "\u000302";
		public static readonly string DARK_GREEN = "\u000303";
		public static readonly string RED = "\u000304";
		public static readonly string BROWN = "\u000305";
		public static readonly string PURPLE = "\u000306";
		public static readonly string OLIVE = "\u000307";
		public static readonly string YELLOW = "\u000308";
		public static readonly string GREEN = "\u000309";
		public static readonly string TEAL = "\u000310";
		public static readonly string CYAN = "\u000311";
		public static readonly string BLUE = "\u000312";
		public static readonly string MAGENTA = "\u000313";
		public static readonly string DARK_GRAY = "\u000314";
		public static readonly string LIGHT_GRAY = "\u000315";

		/// <summary>
		/// Sets this text's fore- and background color
		/// </summary>
		/// <param name="fg">The text's actual color</param>
		/// <param name="bg">The text's background color</param>
		/// <returns></returns>
		public static string WithBackground(string background, string foreground)
		{
			if(!background.StartsWith("\u0003") || !foreground.StartsWith("\u0003"))
				return "";
			return foreground + "," + background.Substring(background.Length - 2);
		}

		/// <summary>
		/// Removes all colors from a piece of text (method from PircBotX)
		/// </summary>
		/// <param name="line">The text to remove all colors from</param>
		/// <returns>The text with all colors removed</returns>
		public static string RemoveColors(string line)
		{
			int length = line.Length;
			int i = 0;
			string result = "";

			while(i < length)
			{
				char c = line.ElementAt(i);

				if(c == '\u0003')
				{
					i++;

					if(i < length)
					{
						c = line.ElementAt(i);

						if(Char.IsDigit(c))
						{
							i++;

							if(i < length)
							{
								c = line.ElementAt(i);

								if(Char.IsDigit(c))
									i++;
							}

							if(i < length)
							{
								c = line.ElementAt(i);

								if(c == ',')
								{
									i++;

									if(i < length)
									{
										c = line.ElementAt(i);

										if(Char.IsDigit(c))
										{
											i++;

											if(i < length)
											{
												c = line.ElementAt(i);

												if(Char.IsDigit(c))
													i++;
											}
										}
										else
											i--;
									}
									else
										i--;
								}
							}
						}
					}
				}
				else if(c == '\u000f')
					i++;
				else
				{
					result += c;
					i++;
				}
			}

			return result;
		}

		/// <summary>
		/// Removes all formatting from a piece of text (method from PircBotX)
		/// </summary>
		/// <param name="line">The text to remove all formatting from</param>
		/// <returns>The text with all formatting removed</returns>
		public static string RemoveFormatting(string line)
		{
			string result = "";

			for(int i = 0; i < line.Length; i++)
			{
				char c = line.ElementAt(i);

				if(c != '\u000f' && c != '\u0002' && c != '\u001f' && c != '\u001d' && c != '\u0016')
					result += c;
			}

			return result;
		}

		/// <summary>
		/// Removes all colors and formatting from a piece of text
		/// </summary>
		/// <param name="line">The text to remove all colors and formatting from</param>
		/// <returns>The text with all colors and formatting removed</returns>
		public static string RemoveColorsAndFormatting(string line)
		{
			return RemoveFormatting(RemoveColors(line));
		}
	}
}
