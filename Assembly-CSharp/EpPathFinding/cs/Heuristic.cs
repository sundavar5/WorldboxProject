using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace EpPathFinding.cs
{
	// Token: 0x02000870 RID: 2160
	public class Heuristic
	{
		// Token: 0x060043E7 RID: 17383 RVA: 0x001CBAEB File Offset: 0x001C9CEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Manhattan(int iDx, int iDy)
		{
			return (float)(iDx + iDy);
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x001CBAF4 File Offset: 0x001C9CF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Euclidean(int iDx, int iDy)
		{
			float num = (float)iDx;
			float tFdy = (float)iDy;
			return (float)Math.Sqrt((double)(num * num + tFdy * tFdy));
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x001CBB13 File Offset: 0x001C9D13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Chebyshev(int iDx, int iDy)
		{
			return (float)Mathf.Max(iDx, iDy);
		}
	}
}
