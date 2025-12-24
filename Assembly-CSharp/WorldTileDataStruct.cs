using System;

// Token: 0x0200050C RID: 1292
public readonly struct WorldTileDataStruct : IEquatable<WorldTileDataStruct>
{
	// Token: 0x06002A9F RID: 10911 RVA: 0x00153E44 File Offset: 0x00152044
	public WorldTileDataStruct(string pType, int pHeight, ConwayType pConwayType, bool pFire, double pFireTimestamp, bool pFrozen, int pTileID, int pX, int pY)
	{
		this.type = pType;
		this.height = pHeight;
		this.conwayType = pConwayType;
		this.fire_timestamp = pFireTimestamp;
		this.frozen = pFrozen;
		this.tile_id = pTileID;
		this.x = pX;
		this.y = pY;
	}

	// Token: 0x06002AA0 RID: 10912 RVA: 0x00153E84 File Offset: 0x00152084
	public WorldTileDataStruct(WorldTile pTile, int pTileID)
	{
		WorldTileData pData = pTile.data;
		this.type = pData.type;
		this.height = pData.height;
		this.conwayType = pData.conwayType;
		this.fire_timestamp = pData.fire_timestamp;
		this.frozen = pData.frozen;
		this.tile_id = pTileID;
		this.x = pTile.x;
		this.y = pTile.y;
	}

	// Token: 0x06002AA1 RID: 10913 RVA: 0x00153EF3 File Offset: 0x001520F3
	public bool Equals(WorldTileDataStruct pOther)
	{
		return this.tile_id == pOther.tile_id;
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x00153F04 File Offset: 0x00152104
	public override bool Equals(object pObject)
	{
		if (pObject is WorldTileDataStruct)
		{
			WorldTileDataStruct tOther = (WorldTileDataStruct)pObject;
			return this.Equals(tOther);
		}
		return false;
	}

	// Token: 0x06002AA3 RID: 10915 RVA: 0x00153F29 File Offset: 0x00152129
	public override int GetHashCode()
	{
		return this.tile_id;
	}

	// Token: 0x04001FEC RID: 8172
	public readonly string type;

	// Token: 0x04001FED RID: 8173
	public readonly int height;

	// Token: 0x04001FEE RID: 8174
	public readonly ConwayType conwayType;

	// Token: 0x04001FEF RID: 8175
	public readonly double fire_timestamp;

	// Token: 0x04001FF0 RID: 8176
	public readonly bool frozen;

	// Token: 0x04001FF1 RID: 8177
	public readonly int tile_id;

	// Token: 0x04001FF2 RID: 8178
	public readonly int x;

	// Token: 0x04001FF3 RID: 8179
	public readonly int y;
}
