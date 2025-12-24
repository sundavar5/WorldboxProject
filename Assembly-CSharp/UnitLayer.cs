using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000411 RID: 1041
public class UnitLayer : MapLayer
{
	// Token: 0x060023F9 RID: 9209 RVA: 0x0012C2FE File Offset: 0x0012A4FE
	internal override void create()
	{
		this.dead = Toolbox.makeColor("#393939");
		this.prevTiles = new List<WorldTile>();
		base.create();
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x0012C326 File Offset: 0x0012A526
	internal override void clear()
	{
		this.prevTiles.Clear();
		base.clear();
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x0012C33C File Offset: 0x0012A53C
	protected override void UpdateDirty(float pElapsed)
	{
		if (MapBox.isRenderGameplay())
		{
			this.timer = 0f;
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.timer = this.interval;
		for (int i = 0; i < this.prevTiles.Count; i++)
		{
			WorldTile tTile = this.prevTiles[i];
			this.pixels[tTile.data.tile_id] = this._color_clear;
		}
		this.prevTiles.Clear();
		bool tBoatsEnabled = PlayerConfig.optionBoolEnabled("marks_boats");
		bool tDrawCultures = Zones.showCultureZones(false);
		if (World.world.isAnyPowerSelected() && !Zones.isPowerForceMapMode(MetaType.None))
		{
			tDrawCultures = false;
		}
		bool tDrawClans = Zones.showClanZones(false);
		bool tDrawAlliances = Zones.showAllianceZones(false);
		List<Actor> tActorList = World.world.units.getSimpleList();
		for (int j = 0; j < tActorList.Count; j++)
		{
			Actor tActor = tActorList[j];
			if (!tActor.asset.visible_on_minimap && tActor.asset.color != null && !tActor.is_inside_building)
			{
				this.prevTiles.Add(tActor.current_tile);
				if (!tActor.isAlive())
				{
					this.pixels[tActor.current_tile.data.tile_id] = this.dead;
				}
				else if (tDrawCultures)
				{
					if (tActor.hasCulture())
					{
						this.pixels[tActor.current_tile.data.tile_id] = tActor.culture.getColor().getColorUnit32();
					}
				}
				else if (tDrawClans)
				{
					if (tActor.hasClan())
					{
						this.pixels[tActor.current_tile.data.tile_id] = tActor.clan.getColor().getColorUnit32();
					}
				}
				else
				{
					if (tDrawAlliances)
					{
						Alliance tAlliance = World.world.alliances.get(tActor.kingdom.data.allianceID);
						if (tAlliance != null)
						{
							this.pixels[tActor.current_tile.data.tile_id] = tAlliance.getColor().getColorUnit32();
							goto IL_2C9;
						}
					}
					if ((tActor.asset.is_boat || tActor.isSapient()) && tActor.hasKingdom() && tActor.isKingdomCiv())
					{
						if (!tBoatsEnabled || !tActor.asset.draw_boat_mark)
						{
							this.pixels[tActor.current_tile.data.tile_id] = tActor.kingdom.getColor().getColorUnit32();
						}
					}
					else
					{
						this.pixels[tActor.current_tile.data.tile_id] = tActor.asset.color.Value;
					}
				}
			}
			IL_2C9:;
		}
		base.updatePixels();
	}

	// Token: 0x040019E9 RID: 6633
	private List<WorldTile> prevTiles;

	// Token: 0x040019EA RID: 6634
	private float interval = 0.1f;

	// Token: 0x040019EB RID: 6635
	private Color32 dead = new Color(0f, 0f, 0f, 0.5f);

	// Token: 0x040019EC RID: 6636
	private Color32 _color_clear = Color.clear;
}
