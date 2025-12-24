using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class BuildingAnimationData
{
	// Token: 0x040005F2 RID: 1522
	public bool animated;

	// Token: 0x040005F3 RID: 1523
	public ListPool<Sprite> list_spawn;

	// Token: 0x040005F4 RID: 1524
	public ListPool<Sprite> list_main;

	// Token: 0x040005F5 RID: 1525
	public ListPool<Sprite> list_main_disabled;

	// Token: 0x040005F6 RID: 1526
	public ListPool<Sprite> list_ruins;

	// Token: 0x040005F7 RID: 1527
	public ListPool<Sprite> list_special;

	// Token: 0x040005F8 RID: 1528
	public Sprite[] main;

	// Token: 0x040005F9 RID: 1529
	public Sprite[] main_disabled;

	// Token: 0x040005FA RID: 1530
	public Sprite[] spawn;

	// Token: 0x040005FB RID: 1531
	public Sprite[] ruins;

	// Token: 0x040005FC RID: 1532
	public Sprite[] special;
}
