using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

// Token: 0x0200022A RID: 554
public static class NanoObjectExtensions
{
	// Token: 0x060014C9 RID: 5321 RVA: 0x000DC0DF File Offset: 0x000DA2DF
	[ContractAnnotation("null => true")]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isRekt(this NanoObject pObject)
	{
		return pObject == null || !pObject.isAlive();
	}
}
