using System;
using System.Globalization;

namespace System.Text
{
	// Token: 0x02000860 RID: 2144
	public static class StringBuilderExtensions
	{
		// Token: 0x06004306 RID: 17158 RVA: 0x001C7D2C File Offset: 0x001C5F2C
		public static StringBuilder Remove(this StringBuilder sb, params char[] removeChars)
		{
			int i = 0;
			while (i < sb.Length)
			{
				if (removeChars.IndexOf(sb[i]) > -1)
				{
					sb.Remove(i, 1);
				}
				else
				{
					i++;
				}
			}
			return sb;
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x001C7D66 File Offset: 0x001C5F66
		public static StringBuilder Remove(this StringBuilder sb, int startIndex)
		{
			if (startIndex >= sb.Length)
			{
				return sb;
			}
			return sb.Remove(startIndex, sb.Length - startIndex);
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x001C7D82 File Offset: 0x001C5F82
		private static bool IsBOMWhitespace(char c)
		{
			return false;
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x001C7D88 File Offset: 0x001C5F88
		private static StringBuilder TrimHelper(this StringBuilder sb, int trimType)
		{
			int end = sb.Length - 1;
			int start = 0;
			if (trimType != 1)
			{
				start = 0;
				while (start < sb.Length && (char.IsWhiteSpace(sb[start]) || StringBuilderExtensions.IsBOMWhitespace(sb[start])))
				{
					start++;
				}
			}
			if (trimType != 0)
			{
				end = sb.Length - 1;
				while (end >= start && (char.IsWhiteSpace(sb[end]) || StringBuilderExtensions.IsBOMWhitespace(sb[start])))
				{
					end--;
				}
			}
			return sb.CreateTrimmedString(start, end);
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x001C7E0C File Offset: 0x001C600C
		internal static StringBuilder CreateTrimmedString(this StringBuilder sb, int start, int end)
		{
			int length = end - start + 1;
			if (length == sb.Length)
			{
				return sb;
			}
			if (length == 0)
			{
				sb.Length = 0;
				return sb;
			}
			return sb.InternalSubstring(start, end);
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x001C7E3E File Offset: 0x001C603E
		private static StringBuilder InternalSubstring(this StringBuilder sb, int startIndex, int end)
		{
			sb.Length = end + 1;
			if (startIndex > 0)
			{
				sb.Remove(0, startIndex);
			}
			return sb;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x001C7E58 File Offset: 0x001C6058
		private static StringBuilder TrimHelper(this StringBuilder sb, char[] trimChars, int trimType)
		{
			int end = sb.Length - 1;
			int start = 0;
			if (trimType != 1)
			{
				for (start = 0; start < sb.Length; start++)
				{
					int index = 0;
					char ch = sb[start];
					while (index < trimChars.Length && trimChars[index] != ch)
					{
						index++;
					}
					if (index == trimChars.Length)
					{
						break;
					}
				}
			}
			if (trimType != 0)
			{
				for (end = sb.Length - 1; end >= start; end--)
				{
					int num4 = 0;
					char ch2 = sb[end];
					while (num4 < trimChars.Length && trimChars[num4] != ch2)
					{
						num4++;
					}
					if (num4 == trimChars.Length)
					{
						break;
					}
				}
			}
			return sb.CreateTrimmedString(start, end);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x001C7EEF File Offset: 0x001C60EF
		public static StringBuilder TrimStart(this StringBuilder sb, params char[] trimChars)
		{
			if (trimChars != null && trimChars.Length != 0)
			{
				return sb.TrimHelper(trimChars, 0);
			}
			return sb.TrimHelper(0);
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x001C7F08 File Offset: 0x001C6108
		public static StringBuilder TrimEnd(this StringBuilder sb, params char[] trimChars)
		{
			if (trimChars != null && trimChars.Length != 0)
			{
				return sb.TrimHelper(trimChars, 1);
			}
			return sb.TrimHelper(1);
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x001C7F21 File Offset: 0x001C6121
		public static StringBuilder Trim(this StringBuilder sb)
		{
			return sb.TrimHelper(2);
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x001C7F2A File Offset: 0x001C612A
		public static StringBuilder Trim(this StringBuilder sb, params char[] trimChars)
		{
			if (trimChars != null && trimChars.Length != 0)
			{
				return sb.TrimHelper(trimChars, 2);
			}
			return sb.TrimHelper(2);
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x001C7F43 File Offset: 0x001C6143
		public static int IndexOf(this StringBuilder sb, char value)
		{
			return sb.IndexOf(value, 0, sb.Length);
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x001C7F53 File Offset: 0x001C6153
		public static int IndexOf(this StringBuilder sb, char value, int startIndex)
		{
			return sb.IndexOf(value, startIndex, sb.Length - startIndex);
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x001C7F68 File Offset: 0x001C6168
		public static int IndexOf(this StringBuilder sb, char value, int startIndex, int count)
		{
			if (sb.Length == 0 || count == 0)
			{
				return -1;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (sb[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x001C7F9D File Offset: 0x001C619D
		public static int IndexOf(this StringBuilder sb, string value, bool ignoreCase = false)
		{
			if (value == string.Empty)
			{
				return 0;
			}
			return StringBuilderExtensions.IndexOfInternal(sb, value, 0, sb.Length, ignoreCase);
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x001C7FBD File Offset: 0x001C61BD
		public static int IndexOf(this StringBuilder sb, string value, int startIndex, bool ignoreCase = false)
		{
			return StringBuilderExtensions.IndexOfInternal(sb, value, startIndex, sb.Length - startIndex, ignoreCase);
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x001C7FD0 File Offset: 0x001C61D0
		public static int IndexOf(this StringBuilder sb, string value, int startIndex, int count, bool ignoreCase = false)
		{
			return StringBuilderExtensions.IndexOfInternal(sb, value, startIndex, count, ignoreCase);
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x001C7FE0 File Offset: 0x001C61E0
		private static int IndexOfInternal(StringBuilder sb, string value, int startIndex, int count, bool ignoreCase)
		{
			if (value == string.Empty)
			{
				return startIndex;
			}
			if (sb.Length == 0 || count == 0 || startIndex + 1 + value.Length > sb.Length)
			{
				return -1;
			}
			int length = value.Length;
			int num2 = startIndex + count - value.Length;
			if (!ignoreCase)
			{
				for (int i = startIndex; i <= num2; i++)
				{
					if (sb[i] == value[0])
					{
						int num3 = 1;
						while (num3 < length && sb[i + num3] == value[num3])
						{
							num3++;
						}
						if (num3 == length)
						{
							return i;
						}
					}
				}
			}
			else
			{
				for (int j = startIndex; j <= num2; j++)
				{
					if (char.ToLower(sb[j]) == char.ToLower(value[0]))
					{
						int num3 = 1;
						while (num3 < length && char.ToLower(sb[j + num3]) == char.ToLower(value[num3]))
						{
							num3++;
						}
						if (num3 == length)
						{
							return j;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x001C80CF File Offset: 0x001C62CF
		public static int IndexOfAny(this StringBuilder sb, char[] anyOf)
		{
			return sb.IndexOfAny(anyOf, 0, sb.Length);
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x001C80DF File Offset: 0x001C62DF
		public static int IndexOfAny(this StringBuilder sb, char[] anyOf, int startIndex)
		{
			return sb.IndexOfAny(anyOf, startIndex, sb.Length - startIndex);
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x001C80F4 File Offset: 0x001C62F4
		public static int IndexOfAny(this StringBuilder sb, char[] anyOf, int startIndex, int count)
		{
			if (sb.Length == 0 || count == 0)
			{
				return -1;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (anyOf.IndexOf(sb[i]) > -1)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x001C812F File Offset: 0x001C632F
		public static int LastIndexOf(this StringBuilder sb, char value)
		{
			return sb.LastIndexOf(value, sb.Length - 1, sb.Length);
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x001C8146 File Offset: 0x001C6346
		public static int LastIndexOf(this StringBuilder sb, char value, int startIndex)
		{
			return sb.LastIndexOf(value, startIndex, startIndex + 1);
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x001C8154 File Offset: 0x001C6354
		public static int LastIndexOf(this StringBuilder sb, char value, int startIndex, int count)
		{
			if (sb.Length == 0 || count == 0)
			{
				return -1;
			}
			for (int i = startIndex; i > startIndex - count; i--)
			{
				if (sb[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x001C818C File Offset: 0x001C638C
		public static int LastIndexOf(this StringBuilder sb, string value, bool ignoreCase = false)
		{
			if (value == string.Empty)
			{
				if (sb.Length == 0)
				{
					return 0;
				}
				return sb.Length - 1;
			}
			else
			{
				if (sb.Length == 0)
				{
					return -1;
				}
				return StringBuilderExtensions.LastIndexOfInternal(sb, value, sb.Length - 1, sb.Length, ignoreCase);
			}
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x001C81D9 File Offset: 0x001C63D9
		public static int LastIndexOf(this StringBuilder sb, string value, int startIndex, bool ignoreCase = false)
		{
			return StringBuilderExtensions.LastIndexOfInternal(sb, value, startIndex, startIndex + 1, ignoreCase);
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x001C81E7 File Offset: 0x001C63E7
		public static int LastIndexOf(this StringBuilder sb, string value, int startIndex, int count, bool ignoreCase = false)
		{
			return StringBuilderExtensions.LastIndexOfInternal(sb, value, startIndex, count, ignoreCase);
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x001C81F4 File Offset: 0x001C63F4
		private static int LastIndexOfInternal(StringBuilder sb, string value, int startIndex, int count, bool ignoreCase)
		{
			if (value == string.Empty)
			{
				return startIndex;
			}
			if (sb.Length == 0 || count == 0 || startIndex + 1 - count + value.Length > sb.Length)
			{
				return -1;
			}
			int length = value.Length;
			int maxValueIndex = length - 1;
			int num2 = startIndex - count + value.Length;
			if (!ignoreCase)
			{
				for (int i = startIndex; i >= num2; i--)
				{
					if (sb[i] == value[maxValueIndex])
					{
						int num3 = 1;
						while (num3 < length && sb[i - num3] == value[maxValueIndex - num3])
						{
							num3++;
						}
						if (num3 == length)
						{
							return i - num3 + 1;
						}
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num2; j--)
				{
					if (char.ToLower(sb[j]) == char.ToLower(value[maxValueIndex]))
					{
						int num3 = 1;
						while (num3 < length && char.ToLower(sb[j - num3]) == char.ToLower(value[maxValueIndex - num3]))
						{
							num3++;
						}
						if (num3 == length)
						{
							return j - num3 + 1;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x001C82FC File Offset: 0x001C64FC
		public static int LastIndexOfAny(this StringBuilder sb, char[] anyOf)
		{
			return sb.LastIndexOfAny(anyOf, sb.Length - 1, sb.Length);
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x001C8313 File Offset: 0x001C6513
		public static int LastIndexOfAny(this StringBuilder sb, char[] anyOf, int startIndex)
		{
			return sb.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x001C8320 File Offset: 0x001C6520
		public static int LastIndexOfAny(this StringBuilder sb, char[] anyOf, int startIndex, int count)
		{
			if (sb.Length == 0 || count == 0)
			{
				return -1;
			}
			for (int i = startIndex; i > startIndex - count; i--)
			{
				if (anyOf.IndexOf(sb[i]) > -1)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x001C835C File Offset: 0x001C655C
		public static bool StartsWith(this StringBuilder sb, string value, bool ignoreCase = false)
		{
			int length = value.Length;
			if (length > sb.Length)
			{
				return false;
			}
			if (!ignoreCase)
			{
				for (int i = 0; i < length; i++)
				{
					if (sb[i] != value[i])
					{
						return false;
					}
				}
			}
			else
			{
				for (int j = 0; j < length; j++)
				{
					if (char.ToLower(sb[j]) != char.ToLower(value[j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x001C83C8 File Offset: 0x001C65C8
		public static bool EndsWith(this StringBuilder sb, string value, bool ignoreCase = false)
		{
			int length = value.Length;
			int maxSBIndex = sb.Length - 1;
			int maxValueIndex = length - 1;
			if (length > sb.Length)
			{
				return false;
			}
			if (!ignoreCase)
			{
				for (int i = 0; i < length; i++)
				{
					if (sb[maxSBIndex - i] != value[maxValueIndex - i])
					{
						return false;
					}
				}
			}
			else
			{
				for (int j = length - 1; j >= 0; j--)
				{
					if (char.ToLower(sb[maxSBIndex - j]) != char.ToLower(value[maxValueIndex - j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x001C8450 File Offset: 0x001C6650
		public static StringBuilder ToLower(this StringBuilder sb)
		{
			for (int i = 0; i < sb.Length; i++)
			{
				sb[i] = char.ToLower(sb[i]);
			}
			return sb;
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x001C8484 File Offset: 0x001C6684
		public static StringBuilder Reverse(this StringBuilder sb)
		{
			int length = sb.Length;
			for (int i = 0; i < length / 2; i++)
			{
				char ch = sb[i];
				sb[i] = sb[length - i - 1];
				sb[length - i - 1] = ch;
			}
			return sb;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x001C84D0 File Offset: 0x001C66D0
		public static StringBuilder ToLower(this StringBuilder sb, CultureInfo culture)
		{
			for (int i = 0; i < sb.Length; i++)
			{
				sb[i] = char.ToLower(sb[i], culture);
			}
			return sb;
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x001C8503 File Offset: 0x001C6703
		public static StringBuilder ToLowerInvariant(this StringBuilder sb)
		{
			return sb.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x001C8510 File Offset: 0x001C6710
		public static StringBuilder ToUpper(this StringBuilder sb)
		{
			for (int i = 0; i < sb.Length; i++)
			{
				sb[i] = char.ToUpper(sb[i]);
			}
			return sb;
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x001C8544 File Offset: 0x001C6744
		public static StringBuilder ToUpper(this StringBuilder sb, CultureInfo culture)
		{
			for (int i = 0; i < sb.Length; i++)
			{
				sb[i] = char.ToUpper(sb[i], culture);
			}
			return sb;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x001C8577 File Offset: 0x001C6777
		public static StringBuilder ToUpperInvariant(this StringBuilder sb)
		{
			return sb.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x001C8584 File Offset: 0x001C6784
		public static StringBuilder ToTitleCase(this StringBuilder sb)
		{
			return sb.ToTitleCase(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x001C8594 File Offset: 0x001C6794
		public static StringBuilder ToTitleCase(this StringBuilder sb, CultureInfo culture)
		{
			bool newWord = true;
			for (int i = 0; i < sb.Length; i++)
			{
				if (newWord)
				{
					sb[i] = char.ToUpper(sb[i], culture);
					newWord = false;
				}
				else
				{
					sb[i] = char.ToLower(sb[i], culture);
				}
				if (char.IsWhiteSpace(sb[i]) || char.IsPunctuation(sb[i]) || sb[i] == '\'')
				{
					newWord = true;
				}
			}
			return sb;
		}
	}
}
