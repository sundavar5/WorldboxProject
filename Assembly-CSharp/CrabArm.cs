using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class CrabArm : MonoBehaviour
{
	// Token: 0x060022FD RID: 8957 RVA: 0x00123BED File Offset: 0x00121DED
	private void Start()
	{
		this.laser.enabled = false;
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x00123BFC File Offset: 0x00121DFC
	internal void update(float pElapsed)
	{
		Vector3 tArmPos = World.world.camera.WorldToScreenPoint(this.crabzilla.armTarget.transform.position);
		tArmPos.z = 5.23f;
		Vector3 tJointPos = World.world.camera.WorldToScreenPoint(this.joint.transform.position);
		tArmPos.x -= tJointPos.x;
		tArmPos.y -= tJointPos.y;
		float tAngle = Mathf.Atan2(tArmPos.y, tArmPos.x) * 57.29578f + 90f;
		if (this.mirrored)
		{
			tAngle += 180f;
		}
		this.joint.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, tAngle));
		this.updateLaser(pElapsed);
		if (this.crabzilla.isBeamEnabled())
		{
			float tX = this.laserPoint.transform.position.x;
			float tY = this.laserPoint.transform.position.y;
			MusicBox.inst.playDrawingSound("event:/SFX/UNIQUE/Crabzilla/CrabzillaLazer", tX, tY);
			World.world.stack_effects.light_blobs.Add(new LightBlobData
			{
				position = new Vector2(this.laser.transform.position.x, this.laser.transform.position.y),
				radius = 1.5f
			});
			if (this._laser_frame_index > 6 && this._laser_frame_index < 10)
			{
				this.damageWorld();
			}
		}
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x00123DA4 File Offset: 0x00121FA4
	private void damageWorld()
	{
		float tX = this.laserPoint.transform.position.x;
		float tY = this.laserPoint.transform.position.y;
		WorldTile tTile = World.world.GetTile((int)tX, (int)tY);
		if (tTile != null)
		{
			MapAction.damageWorld(tTile, 4, AssetManager.terraform.get("crab_laser"), null);
		}
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x00123E08 File Offset: 0x00122008
	private void updateLaser(float pTime)
	{
		this._laser_timer -= pTime;
		if (this.crabzilla.isBeamEnabled())
		{
			if (this._laser_timer <= 0f)
			{
				this._laser_frame_index++;
				if (this._laser_frame_index >= 10)
				{
					this._laser_frame_index = 6;
				}
			}
		}
		else if (this._laser_frame_index != 0)
		{
			this._laser_frame_index++;
			if (this._laser_frame_index > 13)
			{
				this._laser_frame_index = 0;
			}
		}
		if (this._laser_timer <= 0f)
		{
			this._laser_timer = 0.07f;
		}
		if (this.laser.sprite.name != this.laserSprites[this._laser_frame_index].name)
		{
			this.laser.sprite = this.laserSprites[this._laser_frame_index];
		}
		this.laser.enabled = (this._laser_frame_index != 0 || this.crabzilla.isBeamEnabled());
	}

	// Token: 0x0400190D RID: 6413
	internal Crabzilla crabzilla;

	// Token: 0x0400190E RID: 6414
	public SpriteRenderer laser;

	// Token: 0x0400190F RID: 6415
	public Transform laserPoint;

	// Token: 0x04001910 RID: 6416
	public GameObject joint;

	// Token: 0x04001911 RID: 6417
	public List<Sprite> laserSprites;

	// Token: 0x04001912 RID: 6418
	public bool mirrored;

	// Token: 0x04001913 RID: 6419
	private const float LASER_INTERVAL = 0.07f;

	// Token: 0x04001914 RID: 6420
	private float _laser_timer = 0.07f;

	// Token: 0x04001915 RID: 6421
	private int _laser_frame_index;
}
