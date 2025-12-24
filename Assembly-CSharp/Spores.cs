using System;
using UnityEngine;

// Token: 0x0200034E RID: 846
public class Spores : BaseEffect
{
	// Token: 0x0600206E RID: 8302 RVA: 0x00116048 File Offset: 0x00114248
	public void setActorParent(Actor pActor)
	{
		this._actor_parent_id = pActor.getID();
		PhenotypeAsset tPhenotypeAssetFromSubspecies = pActor.subspecies.getRandomPhenotypeAsset();
		if (tPhenotypeAssetFromSubspecies != null)
		{
			this.sprite_animation.phenotype = tPhenotypeAssetFromSubspecies;
		}
		else
		{
			PhenotypeAsset tPhenotype = PhenotypeLibrary.default_green;
			this.sprite_animation.phenotype = tPhenotype;
		}
		this.sprite_animation.forceUpdateFrame();
		this.current_position = pActor.current_tile.posV3;
		this.prepare(pActor.current_position, pActor.actor_scale);
		float tSpeciesSpeed = pActor.subspecies.base_stats["speed"] / 2f;
		float tSpeciesLifetime = pActor.subspecies.base_stats["lifespan"];
		float tSpeed = Mathf.Clamp(Randy.randomFloat(0f, tSpeciesSpeed), 0f, 10f);
		this._speed_x = Randy.randomFloat(-tSpeed, tSpeed);
		this._speed_y = Randy.randomFloat(-tSpeed, tSpeed);
		this._life_time = Mathf.Clamp(Randy.randomFloat(1f, tSpeciesLifetime), 1f, 120f);
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x0011614E File Offset: 0x0011434E
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (World.world.isPaused())
		{
			return;
		}
		this.updateMovement(pElapsed);
		this.updatePosition();
		this.updateLifetime(pElapsed);
		if (this._life_time <= 0f)
		{
			this.kill();
		}
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x0011618B File Offset: 0x0011438B
	private void updateLifetime(float pElapsed)
	{
		this._life_time -= pElapsed;
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x0011619C File Offset: 0x0011439C
	public override void kill()
	{
		base.kill();
		Actor tActor = World.world.units.get(this._actor_parent_id);
		if (tActor == null)
		{
			return;
		}
		BabyMaker.spawnBabyFromSpore(tActor, this.current_position);
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x001161DA File Offset: 0x001143DA
	private void updatePosition()
	{
		base.transform.localPosition = new Vector3(this.current_position.x, this.current_position.y, 0f);
	}

	// Token: 0x06002073 RID: 8307 RVA: 0x00116208 File Offset: 0x00114408
	private void updateMovement(float pElapsed)
	{
		float tChangeX = this._speed_x * pElapsed;
		float tChangeY = this._speed_y * pElapsed;
		Vector3 tVec = new Vector3(this.current_position.x + this._speed_x * 0.5f, this.current_position.y + this._speed_y * 0.5f, 0f);
		if (this.isBlockedByTile(tVec))
		{
			this._life_time = 0f;
			return;
		}
		this.current_position.x = this.current_position.x + tChangeX;
		this.current_position.y = this.current_position.y + tChangeY;
		tVec.x = this.current_position.x;
		tVec.y = this.current_position.y;
		base.transform.localPosition = tVec;
	}

	// Token: 0x06002074 RID: 8308 RVA: 0x001162D0 File Offset: 0x001144D0
	private bool isBlockedByTile(Vector2 pPos)
	{
		WorldTile tTile = World.world.GetTile((int)pPos.x, (int)pPos.y);
		return tTile != null && tTile.Type.block;
	}

	// Token: 0x04001799 RID: 6041
	private const float WALL_CHECKER_MOD_DISTANCE = 0.5f;

	// Token: 0x0400179A RID: 6042
	private float _speed_x;

	// Token: 0x0400179B RID: 6043
	private float _speed_y;

	// Token: 0x0400179C RID: 6044
	private float _life_time;

	// Token: 0x0400179D RID: 6045
	private long _actor_parent_id;
}
