using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class NukeFlash : BaseEffect
{
	// Token: 0x06002059 RID: 8281 RVA: 0x00115893 File Offset: 0x00113A93
	internal void spawnFlash(WorldTile pTile, string pBomb)
	{
		this.tile = pTile;
		this._bomb_spawned = false;
		this._terraform_asset = AssetManager.terraform.get(pBomb);
		this._killing = false;
		this.prepare(pTile, 0.1f);
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x001158C8 File Offset: 0x00113AC8
	private void Update()
	{
		Vector3 tVec = this.m_transform.localScale;
		if (!this._killing && tVec.x < 1f)
		{
			Config.grey_goo_damaged = false;
			tVec.x += World.world.elapsed * 2.5f;
			if (tVec.x >= 0.8f && !this._bomb_spawned)
			{
				this._bomb_spawned = true;
				WorldAction bomb_action = this._terraform_asset.bomb_action;
				if (bomb_action != null)
				{
					bomb_action(null, this.tile);
				}
			}
			if (Config.grey_goo_damaged && !AchievementLibrary.isUnlocked(AchievementLibrary.final_resolution))
			{
				AchievementLibrary.final_resolution.check(null);
			}
			if (tVec.x >= 1f)
			{
				tVec.x = 1f;
				this._killing = true;
			}
			tVec.y = tVec.x;
			this.m_transform.localScale = tVec;
			return;
		}
		if (this._killing)
		{
			tVec.x -= World.world.elapsed * 2.5f;
			tVec.y = tVec.x;
			if (tVec.x <= 0f)
			{
				tVec.x = 0f;
				this.kill();
			}
			this.m_transform.localScale = tVec;
		}
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x00115A0B File Offset: 0x00113C0B
	internal static bool atomic_bomb_action(BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		EffectsLibrary.spawnAtTileRandomScale("fx_explosion_nuke_atomic", pTile, 0.8f, 0.9f);
		if (World.world.explosion_checker.checkNearby(pTile, 30))
		{
			return false;
		}
		MapAction.damageWorld(pTile, 30, TerraformLibrary.atomic_bomb, null);
		return true;
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x00115A48 File Offset: 0x00113C48
	internal static bool crabzilla_bomb_action(BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		EffectsLibrary.spawnAtTileRandomScale("fx_explosion_huge", pTile, 0.8f, 0.9f);
		if (World.world.explosion_checker.checkNearby(pTile, 30))
		{
			return false;
		}
		MapAction.damageWorld(pTile, 30, TerraformLibrary.crabzilla_bomb, null);
		int tRandomAmount = Randy.randomInt(1, 5);
		for (int i = 0; i < tRandomAmount; i++)
		{
			Actor actor = World.world.units.createNewUnit("crab", pTile, false, 0f, null, null, true, true, false, false);
			actor.addTrait("fire_blood", false);
			actor.addTrait("fire_proof", false);
			actor.addTrait("evil", false);
			actor.addTrait("tough", false);
		}
		return true;
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x00115AF8 File Offset: 0x00113CF8
	internal static bool czar_bomba_action(BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		EffectsLibrary.spawnAtTileRandomScale("fx_explosion_huge", pTile, 1.4f, 1.6f);
		if (World.world.explosion_checker.checkNearby(pTile, 70))
		{
			return false;
		}
		MapAction.damageWorld(pTile, 70, TerraformLibrary.czar_bomba, null);
		return true;
	}

	// Token: 0x04001783 RID: 6019
	private bool _killing;

	// Token: 0x04001784 RID: 6020
	private bool _bomb_spawned;

	// Token: 0x04001785 RID: 6021
	private TerraformOptions _terraform_asset;
}
