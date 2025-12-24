using System;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class BuildingTower : BaseBuildingComponent
{
	// Token: 0x0600160E RID: 5646 RVA: 0x000E1AAB File Offset: 0x000DFCAB
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.building.isUnderConstruction())
		{
			return;
		}
		this.updateTestShooting();
		this.updateTower(pElapsed);
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x000E1AD0 File Offset: 0x000DFCD0
	protected void updateTestShooting()
	{
		if (this._test_shooting && Input.GetMouseButtonDown(2))
		{
			Vector3 tStart = new Vector3(this.building.current_tile.posV3.x, this.building.current_tile.posV3.y);
			tStart.y += this.building.asset.tower_projectile_offset;
			World.world.projectiles.spawn(this.building, null, this.building.asset.tower_projectile, tStart, World.world.getMouseTilePos().posV3, 0f, 0.25f, null, null);
		}
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x000E1B82 File Offset: 0x000DFD82
	protected virtual void updateTower(float pElapsed)
	{
		if (this._shooting_active)
		{
			this.shootAtTarget();
			return;
		}
		this.updateCheckTargets(pElapsed);
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x000E1B9A File Offset: 0x000DFD9A
	protected virtual void updateCheckTargets(float pElapsed)
	{
		if (this._check_targets_timeout > 0f)
		{
			this._check_targets_timeout -= pElapsed;
			return;
		}
		this.checkTargets();
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x000E1BBE File Offset: 0x000DFDBE
	protected virtual void resetTimeout()
	{
		this._check_targets_timeout = this.building.asset.tower_projectile_reload + Randy.randomFloat(0f, this.building.asset.tower_projectile_reload);
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x000E1BF4 File Offset: 0x000DFDF4
	protected virtual void shootAtTarget()
	{
		if (this._shooting_target == null || !this._shooting_target.isAlive())
		{
			this._shooting_active = false;
			return;
		}
		this._shooting_amount--;
		if (this._shooting_amount <= 0)
		{
			this._shooting_active = false;
		}
		Vector3 tStart = new Vector3(this.building.current_tile.posV3.x, this.building.current_tile.posV3.y);
		tStart.y += this.building.asset.tower_projectile_offset;
		Vector3 tTargetPos = this._shooting_target.current_tile.posV3;
		tTargetPos.x += Randy.randomFloat(-(this._shooting_target.stats["size"] + 1f), this._shooting_target.stats["size"] + 1f);
		tTargetPos.y += Randy.randomFloat(-this._shooting_target.stats["size"], this._shooting_target.stats["size"]);
		float tZ = 0f;
		if (this._shooting_target.isInAir())
		{
			tZ = this._shooting_target.getHeight();
		}
		this.projectileStarted();
		World.world.projectiles.spawn(this.building, this._shooting_target, this.building.asset.tower_projectile, tStart, tTargetPos, tZ, 0.25f, null, null);
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x000E1D76 File Offset: 0x000DFF76
	protected virtual void projectileStarted()
	{
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x000E1D78 File Offset: 0x000DFF78
	protected virtual void checkTargets()
	{
		this.resetTimeout();
		this._shooting_target = null;
		this._shooting_active = false;
		this._shooting_amount = 0;
		BaseSimObject tNewTarget = this.findTarget();
		if (tNewTarget == null)
		{
			return;
		}
		this._shooting_active = true;
		this._shooting_target = tNewTarget;
		this._shooting_amount = this.building.asset.tower_projectile_amount;
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x000E1DCF File Offset: 0x000DFFCF
	protected virtual BaseSimObject findTarget()
	{
		return this.building.findEnemyObjectTarget(this.building.asset.tower_attack_buildings);
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x000E1DEC File Offset: 0x000DFFEC
	public override void Dispose()
	{
		this._shooting_target = null;
		base.Dispose();
	}

	// Token: 0x0400127D RID: 4733
	protected float _check_targets_timeout = 1f;

	// Token: 0x0400127E RID: 4734
	private bool _test_shooting;

	// Token: 0x0400127F RID: 4735
	protected int _shooting_amount;

	// Token: 0x04001280 RID: 4736
	private bool _shooting_active;

	// Token: 0x04001281 RID: 4737
	protected BaseSimObject _shooting_target;
}
