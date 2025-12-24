using System;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class Santa : BaseEffect
{
	// Token: 0x06002129 RID: 8489 RVA: 0x0011B158 File Offset: 0x00119358
	public void spawnOn(WorldTile pTile)
	{
		this.alive = true;
		this.current_height = Randy.randomFloat(30f, 50f);
		this.current_position.Set((float)pTile.x, (float)pTile.y - this.current_height);
		this.current_material = LibraryMaterials.instance.mat_world_object;
		this._timer_bomb = 2f + Randy.randomFloat(0f, 2f);
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x0011B1CC File Offset: 0x001193CC
	private void updateSanta(float pElapsed)
	{
		if (this.current_position.x > (float)(MapBox.width * 2))
		{
			this.kill();
			return;
		}
		if (this.alive)
		{
			if (!World.world.isPaused())
			{
				this.updateSantaMovement();
				this.updateBombDropTimer(pElapsed);
				return;
			}
		}
		else
		{
			this.updateSantaDeadFall();
			if (this.current_height == 0f)
			{
				this.fallDeathEvent();
				return;
			}
		}
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x0011B231 File Offset: 0x00119431
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateSanta(pElapsed);
		this.updatePosition();
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x0011B248 File Offset: 0x00119448
	public void updatePosition()
	{
		Vector3 tVec = new Vector3(this.current_position.x, this.current_position.y + this.current_height, this.current_height);
		base.transform.localPosition = tVec;
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x0011B28B File Offset: 0x0011948B
	private void updateBombDropTimer(float pElapsed)
	{
		if (this._timer_bomb > 0f)
		{
			this._timer_bomb -= pElapsed;
			return;
		}
		this._timer_bomb = 2f + Randy.randomFloat(0f, 2f);
		this.dropSantaBomb();
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x0011B2CC File Offset: 0x001194CC
	private void fallDeathEvent()
	{
		this.kill();
		EffectsLibrary.spawnAt("fx_land_explosion_old", base.transform.localPosition, 0.6f);
		WorldTile tTile = World.world.GetTile((int)this.current_position.x, (int)this.current_position.y);
		if (tTile == null)
		{
			return;
		}
		MapAction.damageWorld(tTile, 5, AssetManager.terraform.get("grenade"), null);
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x0011B338 File Offset: 0x00119538
	private void updateSantaDeadFall()
	{
		if (this._timer_smoke > 0f)
		{
			this._timer_smoke -= World.world.elapsed;
		}
		else
		{
			this._timer_smoke = 0.1f;
			EffectsLibrary.spawnAt("fx_fire_smoke", base.transform.position, 0.6f);
		}
		this.current_position += new Vector2(4f, Randy.randomFloat(-1f, 1f)) * World.world.elapsed;
		this.current_height -= 20f * World.world.elapsed;
		if (this.current_height < 0f)
		{
			this.current_height = 0f;
		}
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x0011B3FF File Offset: 0x001195FF
	private void updateSantaMovement()
	{
		this.current_position += new Vector2(5f, Randy.randomFloat(-1f, 1f)) * World.world.elapsed;
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x0011B43C File Offset: 0x0011963C
	private void dropSantaBomb()
	{
		WorldTile tTile = World.world.GetTile((int)this.current_position.x, (int)this.current_position.y);
		if (tTile == null)
		{
			return;
		}
		World.world.drop_manager.spawn(tTile, "santa_bomb", this.current_height, -1f, -1L).soundOn = true;
		if (Randy.randomBool())
		{
			MusicBox.playSound("event:/SFX/OTHER/RoboSanta/RoboSantaVoice", this.current_position.x, this.current_position.y - this.current_height, false, false);
		}
	}

	// Token: 0x04001896 RID: 6294
	private float _timer_bomb = 1f;

	// Token: 0x04001897 RID: 6295
	private float _timer_smoke;

	// Token: 0x04001898 RID: 6296
	internal bool alive = true;

	// Token: 0x04001899 RID: 6297
	internal Material current_material;

	// Token: 0x0400189A RID: 6298
	private float current_height;
}
