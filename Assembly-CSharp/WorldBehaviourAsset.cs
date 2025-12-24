using System;
using System.ComponentModel;

// Token: 0x02000097 RID: 151
[Serializable]
public class WorldBehaviourAsset : Asset
{
	// Token: 0x040004F8 RID: 1272
	[DefaultValue(true)]
	public bool enabled = true;

	// Token: 0x040004F9 RID: 1273
	[DefaultValue(true)]
	public bool enabled_on_minimap = true;

	// Token: 0x040004FA RID: 1274
	[DefaultValue(1f)]
	public float interval = 1f;

	// Token: 0x040004FB RID: 1275
	[DefaultValue(1f)]
	public float interval_random = 1f;

	// Token: 0x040004FC RID: 1276
	public WorldBehaviourAction action;

	// Token: 0x040004FD RID: 1277
	public WorldBehaviourAction action_world_clear;

	// Token: 0x040004FE RID: 1278
	[DefaultValue(true)]
	public bool stop_when_world_on_pause = true;

	// Token: 0x040004FF RID: 1279
	[NonSerialized]
	public WorldBehaviour manager;
}
