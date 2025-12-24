using System;
using System.ComponentModel;

// Token: 0x020001F5 RID: 501
[Serializable]
public class WorldTileData
{
	// Token: 0x06000EF2 RID: 3826 RVA: 0x000C3CC4 File Offset: 0x000C1EC4
	public WorldTileData(int pTileID)
	{
		this.tile_id = pTileID;
		this.clear();
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x000C3CE0 File Offset: 0x000C1EE0
	internal void clear()
	{
		this.type = null;
		this.height = 0;
		this.conwayType = ConwayType.None;
		this.fire_timestamp = 0.0;
		this.frozen = false;
	}

	// Token: 0x04000EFF RID: 3839
	public string type;

	// Token: 0x04000F00 RID: 3840
	public int height;

	// Token: 0x04000F01 RID: 3841
	[DefaultValue(ConwayType.None)]
	public ConwayType conwayType = ConwayType.None;

	// Token: 0x04000F02 RID: 3842
	[NonSerialized]
	public double fire_timestamp;

	// Token: 0x04000F03 RID: 3843
	[NonSerialized]
	public bool frozen;

	// Token: 0x04000F04 RID: 3844
	public readonly int tile_id;
}
