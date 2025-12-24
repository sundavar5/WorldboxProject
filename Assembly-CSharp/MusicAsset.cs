using System;

// Token: 0x0200042B RID: 1067
[Serializable]
public class MusicAsset : Asset
{
	// Token: 0x06002539 RID: 9529 RVA: 0x001339A1 File Offset: 0x00131BA1
	public void setTileTypes(params string[] pTileTypes)
	{
		this.tile_type_strings = pTileTypes;
	}

	// Token: 0x04001C34 RID: 7220
	public int need_amount;

	// Token: 0x04001C35 RID: 7221
	public bool marker_same = true;

	// Token: 0x04001C36 RID: 7222
	public string marker_different = string.Empty;

	// Token: 0x04001C37 RID: 7223
	public bool disable_param_after_start = true;

	// Token: 0x04001C38 RID: 7224
	public float min_zoom = 70f;

	// Token: 0x04001C39 RID: 7225
	public int min_tiles_to_play;

	// Token: 0x04001C3A RID: 7226
	public bool is_environment;

	// Token: 0x04001C3B RID: 7227
	public bool is_param;

	// Token: 0x04001C3C RID: 7228
	public bool is_faction;

	// Token: 0x04001C3D RID: 7229
	public bool is_unit_param;

	// Token: 0x04001C3E RID: 7230
	public MusicAssetDelegate special_delegate_units;

	// Token: 0x04001C3F RID: 7231
	public string fmod_path;

	// Token: 0x04001C40 RID: 7232
	public bool mini_map_only;

	// Token: 0x04001C41 RID: 7233
	public bool civilization;

	// Token: 0x04001C42 RID: 7234
	[NonSerialized]
	public MusicBoxContainerTiles container_tiles;

	// Token: 0x04001C43 RID: 7235
	[NonSerialized]
	public TileTypeBase[] tile_types;

	// Token: 0x04001C44 RID: 7236
	public string[] tile_type_strings;

	// Token: 0x04001C45 RID: 7237
	public FmodAction action;

	// Token: 0x04001C46 RID: 7238
	public MusicLayerPriority priority;
}
