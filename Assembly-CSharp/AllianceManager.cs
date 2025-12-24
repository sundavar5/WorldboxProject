using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000210 RID: 528
public class AllianceManager : MetaSystemManager<Alliance, AllianceData>
{
	// Token: 0x06001300 RID: 4864 RVA: 0x000D6043 File Offset: 0x000D4243
	public AllianceManager()
	{
		this.type_id = "alliance";
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x000D6064 File Offset: 0x000D4264
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		foreach (Alliance tAlliance in this)
		{
			tAlliance.clearCursorOver();
			if (!tAlliance.checkActive())
			{
				this._to_dissolve.Add(tAlliance);
			}
			else
			{
				tAlliance.update();
			}
		}
		foreach (Alliance tAlliance2 in this._to_dissolve)
		{
			this.dissolveAlliance(tAlliance2);
		}
		this._to_dissolve.Clear();
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x000D611C File Offset: 0x000D431C
	public void dissolveAlliance(Alliance pAlliance)
	{
		World.world.game_stats.data.alliancesDissolved += 1L;
		World.world.map_stats.alliancesDissolved += 1L;
		WorldLog.logAllianceDisolved(pAlliance);
		pAlliance.dissolve();
		this.removeObject(pAlliance);
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x000D6171 File Offset: 0x000D4371
	private void addTest()
	{
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x000D6174 File Offset: 0x000D4374
	public bool forceAlliance(Kingdom pKingdom1, Kingdom pKingdom2)
	{
		Alliance tCurAlliance = pKingdom1.getAlliance();
		if (tCurAlliance == null)
		{
			tCurAlliance = pKingdom2.getAlliance();
		}
		bool tNew = false;
		if (tCurAlliance == null)
		{
			tCurAlliance = this.newAlliance(pKingdom1, pKingdom2);
			tNew = true;
		}
		else
		{
			tCurAlliance.join(pKingdom1, true, true);
			tCurAlliance.join(pKingdom2, true, true);
		}
		tCurAlliance.setType(AllianceType.Forced);
		return tNew;
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x000D61C0 File Offset: 0x000D43C0
	public void useDiscordPower(Alliance pAlliance, City pCity)
	{
		Kingdom tKingdom = pCity.kingdom;
		pAlliance.leave(tKingdom, true);
		EffectsLibrary.highlightKingdomZones(tKingdom, Color.white, 0.3f);
		if (pAlliance.kingdoms_hashset.Count == 0)
		{
			this.dissolveAlliance(pAlliance);
		}
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x000D6200 File Offset: 0x000D4400
	public Alliance newAlliance(Kingdom pKingdom, Kingdom pKingdom2)
	{
		World.world.game_stats.data.alliancesMade += 1L;
		World.world.map_stats.alliancesMade += 1L;
		Alliance alliance = base.newObject();
		alliance.createNewAlliance();
		alliance.addFounders(pKingdom, pKingdom2);
		WorldLog.logAllianceCreated(alliance);
		return alliance;
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x000D625C File Offset: 0x000D445C
	public Sprite[] getBackgroundsList()
	{
		if (this._cached_banner_backgrounds == null)
		{
			this._cached_banner_backgrounds = SpriteTextureLoader.getSpriteList("alliances/backgrounds/", false);
		}
		return this._cached_banner_backgrounds;
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x000D627D File Offset: 0x000D447D
	public Sprite[] getIconsList()
	{
		if (this._cached_banner_icons == null)
		{
			this._cached_banner_icons = SpriteTextureLoader.getSpriteList("alliances/icons/", false);
		}
		return this._cached_banner_icons;
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x000D629E File Offset: 0x000D449E
	public bool anyAlliances()
	{
		return this.Count > 0;
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x000D62A9 File Offset: 0x000D44A9
	public override void clear()
	{
		base.clear();
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000D62B1 File Offset: 0x000D44B1
	protected override void updateDirtyUnits()
	{
	}

	// Token: 0x04001156 RID: 4438
	public Sprite[] _cached_banner_backgrounds;

	// Token: 0x04001157 RID: 4439
	public Sprite[] _cached_banner_icons;

	// Token: 0x04001158 RID: 4440
	private List<Alliance> _to_dissolve = new List<Alliance>();
}
