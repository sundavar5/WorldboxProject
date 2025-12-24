using System;
using System.Runtime.CompilerServices;

// Token: 0x02000469 RID: 1129
public static class IntExtensions
{
	// Token: 0x06002697 RID: 9879 RVA: 0x0013A537 File Offset: 0x00138737
	public static string ToText(this int pInt)
	{
		return pInt.ToString("##,0.#");
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x0013A545 File Offset: 0x00138745
	public static string ToText(this long pLong)
	{
		return pLong.ToString("##,0.#");
	}

	// Token: 0x06002699 RID: 9881 RVA: 0x0013A553 File Offset: 0x00138753
	public static string ToText(this float pFloat)
	{
		return pFloat.ToString("##,0.#");
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x0013A561 File Offset: 0x00138761
	public static string ToText(this double pDouble)
	{
		return pDouble.ToString("##,0.#");
	}

	// Token: 0x0600269B RID: 9883 RVA: 0x0013A56F File Offset: 0x0013876F
	public static string ToText(this int pInt, int pMaxLength)
	{
		return Toolbox.formatNumber((long)pInt, pMaxLength);
	}

	// Token: 0x0600269C RID: 9884 RVA: 0x0013A579 File Offset: 0x00138779
	public static string ToText(this long pLong, int pMaxLength)
	{
		return Toolbox.formatNumber(pLong, pMaxLength);
	}

	// Token: 0x0600269D RID: 9885 RVA: 0x0013A584 File Offset: 0x00138784
	public static string ToRoman(this int pNumber)
	{
		if (pNumber < 1)
		{
			return "N";
		}
		if (pNumber > 3999)
		{
			return "MMMM";
		}
		string result;
		using (StringBuilderPool tResult = new StringBuilderPool())
		{
			foreach (ValueTuple<int, string> valueTuple in IntExtensions._roman_number_map)
			{
				int tValue = valueTuple.Item1;
				string tSymbol = valueTuple.Item2;
				while (pNumber >= tValue)
				{
					tResult.Append(tSymbol);
					pNumber -= tValue;
				}
			}
			result = tResult.ToString();
		}
		return result;
	}

	// Token: 0x04001D0F RID: 7439
	[TupleElementNames(new string[]
	{
		"value",
		"symbol"
	})]
	private static readonly ValueTuple<int, string>[] _roman_number_map = new ValueTuple<int, string>[]
	{
		new ValueTuple<int, string>(1000, "M"),
		new ValueTuple<int, string>(900, "CM"),
		new ValueTuple<int, string>(500, "D"),
		new ValueTuple<int, string>(400, "CD"),
		new ValueTuple<int, string>(100, "C"),
		new ValueTuple<int, string>(90, "XC"),
		new ValueTuple<int, string>(50, "L"),
		new ValueTuple<int, string>(40, "XL"),
		new ValueTuple<int, string>(10, "X"),
		new ValueTuple<int, string>(9, "IX"),
		new ValueTuple<int, string>(5, "V"),
		new ValueTuple<int, string>(4, "IV"),
		new ValueTuple<int, string>(1, "I")
	};
}
