using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003FB RID: 1019
public class ConwayLife : MapLayer
{
	// Token: 0x06002330 RID: 9008 RVA: 0x00125A52 File Offset: 0x00123C52
	internal override void create()
	{
		base.create();
		ConwayLife.colorCreator = Toolbox.makeColor("#3BCC55");
		this.hashsetTiles = new HashSetWorldTile();
		this.newList = new HashSetWorldTile();
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x00125A84 File Offset: 0x00123C84
	protected override void UpdateDirty(float pElapsed)
	{
		this.UpdateVisual();
		if (World.world.isPaused())
		{
			return;
		}
		if (this.nextTickTimer > 0f)
		{
			this.nextTickTimer -= pElapsed;
			return;
		}
		this.nextTickTimer = this.nextTickInterval;
		for (int i = 0; i < Config.time_scale_asset.conway_ticks; i++)
		{
			this.updateTick();
		}
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x00125AE8 File Offset: 0x00123CE8
	private void UpdateVisual()
	{
		if (this.pixels_to_update.Count == 0)
		{
			return;
		}
		foreach (WorldTile tTile in this.pixels_to_update)
		{
			if (this.hashsetTiles.Contains(tTile))
			{
				if (tTile.data.conwayType == ConwayType.Eater)
				{
					this.pixels[tTile.data.tile_id] = ConwayLife.colorEater;
				}
				else if (tTile.data.conwayType == ConwayType.Creator)
				{
					this.pixels[tTile.data.tile_id] = ConwayLife.colorCreator;
				}
				else
				{
					this.pixels[tTile.data.tile_id] = Toolbox.clear;
				}
			}
			else
			{
				tTile.data.conwayType = ConwayType.None;
				this.pixels[tTile.data.tile_id] = Toolbox.clear;
			}
		}
		this.pixels_to_update.Clear();
		base.updatePixels();
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x00125C00 File Offset: 0x00123E00
	public void remove(WorldTile pTile)
	{
		if (this.hashsetTiles.Count == 0)
		{
			return;
		}
		this.hashsetTiles.Remove(pTile);
		this.pixels_to_update.Add(pTile);
		pTile.data.conwayType = ConwayType.None;
	}

	// Token: 0x06002334 RID: 9012 RVA: 0x00125C38 File Offset: 0x00123E38
	public void add(WorldTile pTile, string pType)
	{
		if (pType == "conway")
		{
			pTile.data.conwayType = ConwayType.Eater;
		}
		else
		{
			pTile.data.conwayType = ConwayType.Creator;
		}
		this.hashsetTiles.Add(pTile);
		this.pixels_to_update.Add(pTile);
	}

	// Token: 0x06002335 RID: 9013 RVA: 0x00125C88 File Offset: 0x00123E88
	private void updateTick()
	{
		int i = this.decreaseTick;
		this.decreaseTick = i - 1;
		if (i <= 0)
		{
			this.decreaseTick = 5;
		}
		if (this.hashsetTiles.Count <= 0 && this.newList.Count <= 0)
		{
			return;
		}
		this.newList.Clear();
		foreach (WorldTile tMainTile in this.hashsetTiles)
		{
			this.checkCell(tMainTile);
			foreach (WorldTile tTile in tMainTile.neighboursAll)
			{
				this.checkCell(tTile);
			}
		}
		HashSetWorldTile tTemp = this.hashsetTiles;
		this.hashsetTiles = this.newList;
		this.newList = tTemp;
		this.UpdateVisual();
	}

	// Token: 0x06002336 RID: 9014 RVA: 0x00125D64 File Offset: 0x00123F64
	private void makeAlive(WorldTile pCell)
	{
		if (this.decreaseTick == 5)
		{
			MusicBox.playSound("event:/SFX/UNIQUE/ConwayMove", pCell, false, false);
			if (pCell.data.conwayType == ConwayType.Eater)
			{
				MapAction.decreaseTile(pCell, true, "destroy_no_flash");
			}
			else
			{
				MapAction.increaseTile(pCell, true, "destroy_no_flash");
			}
		}
		this.newList.Add(pCell);
		if (this.makeFlash)
		{
			this.makeFlashh(pCell, 25);
		}
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x00125DCC File Offset: 0x00123FCC
	internal void makeFlashh(WorldTile pCell, int pAmount)
	{
		if (pCell.data.conwayType == ConwayType.None)
		{
			return;
		}
		ConwayType conwayType = pCell.data.conwayType;
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x00125DE9 File Offset: 0x00123FE9
	internal override void clear()
	{
		base.clear();
		this.newList.Clear();
		this.hashsetTiles.Clear();
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x00125E08 File Offset: 0x00124008
	private void checkCell(WorldTile pCell)
	{
		if (this.pixels_to_update.Contains(pCell))
		{
			return;
		}
		int count = 0;
		int eaters = 0;
		int creators = 0;
		this.pixels_to_update.Add(pCell);
		if (pCell.data.conwayType == ConwayType.Eater)
		{
			eaters++;
		}
		if (pCell.data.conwayType == ConwayType.Creator)
		{
			creators++;
		}
		if (!this.hashsetTiles.Contains(pCell))
		{
			foreach (WorldTile tTile in pCell.neighboursAll)
			{
				if (this.hashsetTiles.Contains(tTile))
				{
					count++;
				}
				if (tTile.data.conwayType == ConwayType.Eater)
				{
					eaters++;
				}
				if (tTile.data.conwayType == ConwayType.Creator)
				{
					creators++;
				}
			}
			if (count == 3)
			{
				if (pCell.data.conwayType == ConwayType.None && (eaters != 0 || creators != 0))
				{
					if (eaters >= creators)
					{
						pCell.data.conwayType = ConwayType.Eater;
					}
					else
					{
						pCell.data.conwayType = ConwayType.Creator;
					}
				}
				this.makeAlive(pCell);
			}
			return;
		}
		foreach (WorldTile tTile2 in pCell.neighboursAll)
		{
			if (this.hashsetTiles.Contains(tTile2))
			{
				count++;
				if (tTile2.data.conwayType == ConwayType.Creator)
				{
					creators++;
				}
				else if (tTile2.data.conwayType == ConwayType.Eater)
				{
					eaters++;
				}
			}
			if (count >= 4)
			{
				if (this.makeFlash)
				{
					this.makeFlashh(pCell, 15);
				}
				pCell.data.conwayType = ConwayType.None;
				return;
			}
		}
		if (count == 2 || count == 3)
		{
			if (pCell.data.conwayType == ConwayType.None && (eaters != 0 || creators != 0))
			{
				if (eaters >= creators)
				{
					pCell.data.conwayType = ConwayType.Eater;
				}
				else
				{
					pCell.data.conwayType = ConwayType.Creator;
				}
			}
			this.makeAlive(pCell);
			return;
		}
		pCell.data.conwayType = ConwayType.None;
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x00125FCC File Offset: 0x001241CC
	internal void checkKillRange(Vector2Int pPos, int pRad)
	{
		if (this.hashsetTiles.Count == 0)
		{
			return;
		}
		this.toRemove.Clear();
		foreach (WorldTile pCell in this.hashsetTiles)
		{
			if (Toolbox.DistVec2(pCell.pos, pPos) <= (float)pRad)
			{
				pCell.data.conwayType = ConwayType.None;
				this.toRemove.Add(pCell);
			}
		}
		foreach (WorldTile tTile in this.toRemove)
		{
			this.remove(tTile);
		}
	}

	// Token: 0x0400197F RID: 6527
	public static Color32 colorEater = new Color(1f, 0.2f, 1f);

	// Token: 0x04001980 RID: 6528
	public static Color32 colorCreator;

	// Token: 0x04001981 RID: 6529
	public bool makeFlash = true;

	// Token: 0x04001982 RID: 6530
	private HashSetWorldTile newList;

	// Token: 0x04001983 RID: 6531
	private float nextTickTimer;

	// Token: 0x04001984 RID: 6532
	private float nextTickInterval = 0.05f;

	// Token: 0x04001985 RID: 6533
	private int decreaseTick;

	// Token: 0x04001986 RID: 6534
	private List<WorldTile> toRemove = new List<WorldTile>();
}
