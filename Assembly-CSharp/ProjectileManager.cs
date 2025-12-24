using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class ProjectileManager : CoreSystemManager<Projectile, ProjectileData>
{
	// Token: 0x06001AF4 RID: 6900 RVA: 0x000FB1FD File Offset: 0x000F93FD
	public ProjectileManager()
	{
		this.type_id = "projectile";
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x000FB21B File Offset: 0x000F941B
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateProjectiles(pElapsed);
		this.checkCollision();
		this.checkDead();
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x000FB238 File Offset: 0x000F9438
	private void checkCollision()
	{
		if (this.list.Count < 2)
		{
			return;
		}
		for (int i = 0; i < this.list.Count - 1; i++)
		{
			Projectile tProjectile = this.list[i];
			if (tProjectile.canBeCollided())
			{
				for (int i2 = i + 1; i2 < this.list.Count; i2++)
				{
					Projectile tProjectile2 = this.list[i2];
					if (tProjectile2.canBeCollided() && (tProjectile.kingdom != tProjectile2.kingdom || tProjectile.kingdom.asset.always_attack_each_other))
					{
						Vector3 tPos = tProjectile.getTransformedPositionWithHeight();
						Vector3 tPos2 = tProjectile2.getTransformedPositionWithHeight();
						if (Vector3.Distance(tPos, tPos2) < 0.25f)
						{
							Vector3 tNewPos = (tPos + tPos2) / 2f;
							tNewPos.y += tNewPos.z;
							tNewPos.z = 0f;
							EffectsLibrary.spawnAt("fx_hit", tNewPos, 0.1f);
							tProjectile2.getCollided(tPos);
							tProjectile.getCollided(tPos2);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x000FB360 File Offset: 0x000F9560
	private void updateProjectiles(float pElapsed)
	{
		this._kingdoms.Clear();
		List<Projectile> tList = this.list;
		for (int i = 0; i < tList.Count; i++)
		{
			Projectile tObj = tList[i];
			if (!tObj.isTargetReached())
			{
				this._kingdoms[tObj.kingdom] = true;
			}
			tObj.update(pElapsed);
		}
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x000FB3B9 File Offset: 0x000F95B9
	internal bool hasActiveProjectiles(Kingdom pKingdom)
	{
		return this._kingdoms.ContainsKey(pKingdom);
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x000FB3C8 File Offset: 0x000F95C8
	private void checkDead()
	{
		for (int i = this.list.Count - 1; i >= 0; i--)
		{
			Projectile tProjectile = this.list[i];
			if (tProjectile.isFinished())
			{
				this.removeObject(tProjectile);
			}
		}
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x000FB40C File Offset: 0x000F960C
	public Projectile spawn(BaseSimObject pInitiator, BaseSimObject pTargetObject, string pAssetID, Vector3 pLaunchPosition, Vector3 pTargetPosition, float pTargetZ = 0f, float pStartPosZ = 0.25f, Action pKillAction = null, Kingdom pForcedKingdom = null)
	{
		Projectile tProjectile = base.newObject();
		if (tProjectile == null)
		{
			return null;
		}
		tProjectile.start(pInitiator, pTargetObject, pLaunchPosition, pTargetPosition, pAssetID, pTargetZ, pStartPosZ, 0f, pKillAction, pForcedKingdom);
		return tProjectile;
	}

	// Token: 0x040014EB RID: 5355
	private const float COLLISION_DISTANCE = 0.25f;

	// Token: 0x040014EC RID: 5356
	private readonly Dictionary<Kingdom, bool> _kingdoms = new Dictionary<Kingdom, bool>();
}
