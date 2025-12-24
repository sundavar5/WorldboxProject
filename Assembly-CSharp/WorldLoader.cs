using System;
using System.Collections.Generic;
using RSG;
using UnityEngine;

// Token: 0x02000822 RID: 2082
internal class WorldLoader : MonoBehaviour
{
	// Token: 0x04002F96 RID: 12182
	public static WorldLoader instance;

	// Token: 0x04002F97 RID: 12183
	public static Dictionary<string, Map> mapCache = new Dictionary<string, Map>();

	// Token: 0x04002F98 RID: 12184
	public static Dictionary<string, Promise<Dictionary<string, Map>>> listCache = new Dictionary<string, Promise<Dictionary<string, Map>>>();
}
