using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

// Token: 0x02000447 RID: 1095
public static class ArrayExtensions
{
	// Token: 0x17000205 RID: 517
	// (get) Token: 0x060025EE RID: 9710 RVA: 0x001379E1 File Offset: 0x00135BE1
	private static Random rnd
	{
		get
		{
			return Randy.rnd;
		}
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x001379E8 File Offset: 0x00135BE8
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T First<T>(this T[] pArray)
	{
		return pArray[0];
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x001379F1 File Offset: 0x00135BF1
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Last<T>(this T[] pArray)
	{
		return pArray[pArray.Length - 1];
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x001379FE File Offset: 0x00135BFE
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int IndexOf<T>(this T[] pArray, T pValue)
	{
		return Array.IndexOf<T>(pArray, pValue);
	}

	// Token: 0x060025F2 RID: 9714 RVA: 0x00137A07 File Offset: 0x00135C07
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Contains<T>(this T[] pArray, T pValue)
	{
		return Array.IndexOf<T>(pArray, pValue) > -1;
	}

	// Token: 0x060025F3 RID: 9715 RVA: 0x00137A13 File Offset: 0x00135C13
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int FreeIndex<T>(this T[] pArray)
	{
		return Array.IndexOf(pArray, null);
	}

	// Token: 0x060025F4 RID: 9716 RVA: 0x00137A1C File Offset: 0x00135C1C
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetRandom<T>(this T[] pArray)
	{
		return pArray[ArrayExtensions.rnd.Next(0, pArray.Length)];
	}

	// Token: 0x060025F5 RID: 9717 RVA: 0x00137A32 File Offset: 0x00135C32
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetRandom<T>(this T[] pArray, int pLength)
	{
		return pArray[ArrayExtensions.rnd.Next(0, pLength)];
	}

	// Token: 0x060025F6 RID: 9718 RVA: 0x00137A48 File Offset: 0x00135C48
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Swap<T>(this T[] pArray, int pIndex1, int pIndex2)
	{
		T temp = pArray[pIndex1];
		pArray[pIndex1] = pArray[pIndex2];
		pArray[pIndex2] = temp;
	}

	// Token: 0x060025F7 RID: 9719 RVA: 0x00137A74 File Offset: 0x00135C74
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Shuffle<T>(this T[] pArray)
	{
		if (pArray.Length < 2)
		{
			return;
		}
		int tCount = pArray.Length;
		for (int i = 0; i < tCount; i++)
		{
			pArray.Swap(i, ArrayExtensions.rnd.Next(i, tCount));
		}
	}

	// Token: 0x060025F8 RID: 9720 RVA: 0x00137AAC File Offset: 0x00135CAC
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Shuffle<T>(this T[] pArray, int pCount)
	{
		if (pCount < 2)
		{
			return;
		}
		for (int i = 0; i < pCount; i++)
		{
			pArray.Swap(i, ArrayExtensions.rnd.Next(i, pCount));
		}
	}

	// Token: 0x060025F9 RID: 9721 RVA: 0x00137ADD File Offset: 0x00135CDD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ShuffleOne<T>(this T[] pArray)
	{
		if (pArray.Length < 2)
		{
			return;
		}
		pArray.Swap(0, ArrayExtensions.rnd.Next(0, pArray.Length));
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x00137AFB File Offset: 0x00135CFB
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ShuffleOne<T>(this T[] pArray, int pItem)
	{
		if (pArray.Length < 2)
		{
			return;
		}
		if (pArray.Length < pItem + 1)
		{
			return;
		}
		pArray.Swap(pItem, ArrayExtensions.rnd.Next(pItem, pArray.Length));
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x00137B22 File Offset: 0x00135D22
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ShuffleOne<T>(this T[] pArray, int pItem, int pCount)
	{
		if (pCount < 2)
		{
			return;
		}
		if (pCount < pItem + 1)
		{
			return;
		}
		pArray.Swap(pItem, ArrayExtensions.rnd.Next(pItem, pCount));
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x00137B43 File Offset: 0x00135D43
	public static void Clear<T>(this T[] pArray)
	{
		Array.Clear(pArray, 0, pArray.Length);
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x00137B4F File Offset: 0x00135D4F
	public static void Clear<T>(this T[] pArray, int pCount)
	{
		Array.Clear(pArray, 0, pCount);
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x00137B5C File Offset: 0x00135D5C
	[Pure]
	public static bool AnyTrue(this bool[] pArray)
	{
		for (int i = 0; i < pArray.Length; i++)
		{
			if (pArray[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060025FF RID: 9727 RVA: 0x00137B84 File Offset: 0x00135D84
	[Pure]
	public static bool AnyFalse(this bool[] pArray)
	{
		for (int i = 0; i < pArray.Length; i++)
		{
			if (!pArray[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002600 RID: 9728 RVA: 0x00137BAC File Offset: 0x00135DAC
	public static string AsString<T>(this T[] pArray)
	{
		if (pArray == null)
		{
			return "";
		}
		string result;
		using (ListPool<string> tStringToPrint = new ListPool<string>(pArray.Length))
		{
			foreach (T tObject in pArray)
			{
				tStringToPrint.Add(((tObject != null) ? tObject.ToString() : null) ?? "null");
			}
			result = string.Join(", ", tStringToPrint.ToArray<string>());
		}
		return result;
	}

	// Token: 0x06002601 RID: 9729 RVA: 0x00137C3C File Offset: 0x00135E3C
	public static void PrintToConsole<T>(this T[] pArray, string pMessage = null)
	{
		if (pArray == null)
		{
			return;
		}
		string tStringToPrint = "";
		foreach (T tObject in pArray)
		{
			tStringToPrint = tStringToPrint + tObject.ToString() + ",";
		}
		if (tStringToPrint.Length > 0)
		{
			tStringToPrint = tStringToPrint.TrimEnd(',');
		}
		if (pMessage != null)
		{
			Debug.Log(pMessage + ": [" + tStringToPrint + "]");
			return;
		}
		Debug.Log(tStringToPrint);
	}

	// Token: 0x06002602 RID: 9730 RVA: 0x00137CB6 File Offset: 0x00135EB6
	public static bool AllTrue(this bool[] pArray)
	{
		return !pArray.AnyFalse();
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x00137CC1 File Offset: 0x00135EC1
	public static bool AllFalse(this bool[] pArray)
	{
		return !pArray.AnyTrue();
	}
}
