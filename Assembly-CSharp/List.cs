using System;
using System.Collections.Generic;

// Token: 0x0200046C RID: 1132
public static class List
{
	// Token: 0x060026A7 RID: 9895 RVA: 0x0013A978 File Offset: 0x00138B78
	public static List<T> Of<T>(params T[] pArgs)
	{
		return new List<T>(pArgs);
	}
}
