using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Token: 0x02000205 RID: 517
[Serializable]
public class ActorBag
{
	// Token: 0x0400110B RID: 4363
	[JsonProperty]
	internal Dictionary<string, ResourceContainer> dict;

	// Token: 0x0400110C RID: 4364
	[JsonProperty]
	internal string last_item_to_render;
}
