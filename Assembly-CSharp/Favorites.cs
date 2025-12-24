using System;
using System.Collections.Generic;
using RSG;

// Token: 0x020005BC RID: 1468
[Serializable]
public class Favorites
{
	// Token: 0x0400249B RID: 9371
	public static Dictionary<string, bool> favorites = new Dictionary<string, bool>();

	// Token: 0x0400249C RID: 9372
	public static Promise promise;
}
