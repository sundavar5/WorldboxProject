using System;
using UnityEngine;

// Token: 0x020002D4 RID: 724
[Serializable]
public class PrintTemplate
{
	// Token: 0x040014CD RID: 5325
	public string name;

	// Token: 0x040014CE RID: 5326
	public Texture2D graphics;

	// Token: 0x040014CF RID: 5327
	internal PrintStep[] steps;

	// Token: 0x040014D0 RID: 5328
	internal int steps_per_tick = 1;
}
