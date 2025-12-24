using System;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public class CrabLeg : MonoBehaviour
{
	// Token: 0x06002303 RID: 8963 RVA: 0x00123F24 File Offset: 0x00122124
	internal void create()
	{
		this._target_position = this.limbPoint.transform.position;
		this._target_position.z = 0f;
		this._current_position = this._target_position;
		base.transform.position = new Vector3(this._target_position.x, this._target_position.y, 0f);
		base.GetComponent<SpriteRenderer>().enabled = false;
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x00123F9C File Offset: 0x0012219C
	internal void update(float pElapsed)
	{
		float tDistanceTarget = Toolbox.DistVec3(this._current_position, this._target_position);
		this._current_position = Vector3.MoveTowards(this._current_position, this._target_position, 1.5f + tDistanceTarget / 5f);
		base.transform.position = new Vector3(this._current_position.x, this._current_position.y, 0f);
		this._target_pos = this.limbPoint.transform.position + this._random_pos;
		if (!this.legJoint.isAngleOk(-20f, 30f))
		{
			this.moveLeg();
		}
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x00124048 File Offset: 0x00122248
	public void moveLeg()
	{
		this._target_pos = this.limbPoint.transform.position + this._random_pos;
		this._target_pos.z = 0f;
		this._target_position = this._target_pos;
		this._random_pos.x = Randy.randomFloat(-1f, 1f);
		this._random_pos.y = Randy.randomFloat(-1f, 1f);
		Vector2 tMovementVector = ControllableUnit.getMovementVector();
		if (!ControllableUnit.isMovementActionActive())
		{
			tMovementVector = Vector2.zero;
		}
		if (tMovementVector.x != 0f)
		{
			if (tMovementVector.x > 0f)
			{
				this._random_pos.x = this._random_pos.x + 2f;
			}
			else
			{
				this._random_pos.x = this._random_pos.x - 2f;
			}
		}
		if (tMovementVector.y != 0f)
		{
			if (tMovementVector.y > 0f)
			{
				this._random_pos.y = this._random_pos.y + 2f;
			}
			else
			{
				this._random_pos.y = this._random_pos.y - 2f;
			}
		}
		this.crabzilla.legMoved();
		WorldTile tTile = World.world.GetTile((int)this._target_pos.x, (int)this._target_pos.y);
		if (tTile == null)
		{
			return;
		}
		MapAction.damageWorld(tTile, 3, AssetManager.terraform.get("crab_step"), null);
		MusicBox.playSound("event:/SFX/UNIQUE/Crabzilla/CrabzillaFootsteps", tTile, false, false);
	}

	// Token: 0x04001916 RID: 6422
	public CrabLegLimbPoint limbPoint;

	// Token: 0x04001917 RID: 6423
	internal Crabzilla crabzilla;

	// Token: 0x04001918 RID: 6424
	private Vector3 _current_position;

	// Token: 0x04001919 RID: 6425
	private Vector3 _target_position;

	// Token: 0x0400191A RID: 6426
	private Vector3 _random_pos = Vector3.zero;

	// Token: 0x0400191B RID: 6427
	public CrabLegJoint legJoint;

	// Token: 0x0400191C RID: 6428
	private Vector3 _target_pos;
}
