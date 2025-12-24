using System;

// Token: 0x020002C9 RID: 713
[Serializable]
public class NameplateAsset : Asset
{
	// Token: 0x04001446 RID: 5190
	public string path_sprite;

	// Token: 0x04001447 RID: 5191
	public NameplateAction action;

	// Token: 0x04001448 RID: 5192
	public NameplateBase action_main;

	// Token: 0x04001449 RID: 5193
	public MetaType map_mode;

	// Token: 0x0400144A RID: 5194
	public int padding_left = 12;

	// Token: 0x0400144B RID: 5195
	public int padding_top;

	// Token: 0x0400144C RID: 5196
	public int padding_right = 18;

	// Token: 0x0400144D RID: 5197
	public float banner_only_mode_scale = 2f;

	// Token: 0x0400144E RID: 5198
	public bool overlap_for_fluid_mode;

	// Token: 0x0400144F RID: 5199
	public int max_nameplate_count = 100;
}
