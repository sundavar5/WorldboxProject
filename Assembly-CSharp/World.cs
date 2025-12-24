using System;
using System.Runtime.CompilerServices;

// Token: 0x02000013 RID: 19
public static class World
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000F3 RID: 243 RVA: 0x00009999 File Offset: 0x00007B99
	public static MapBox world
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return MapBox.instance;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060000F4 RID: 244 RVA: 0x000099A0 File Offset: 0x00007BA0
	public static WorldAgeAsset world_era
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return MapBox.instance.era_manager.getCurrentAge();
		}
	}
}
