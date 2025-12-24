using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033B RID: 827
public class EffectInfinityCoin : BaseEffect
{
	// Token: 0x06001FF6 RID: 8182 RVA: 0x001130D7 File Offset: 0x001112D7
	internal override void create()
	{
		base.create();
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x001130DF File Offset: 0x001112DF
	internal override void spawnOnTile(WorldTile pTile)
	{
		this.prepare(new Vector3(pTile.posV3.x, pTile.posV3.y - 1f), 0.25f);
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x00113114 File Offset: 0x00111314
	internal override void prepare(Vector2 pVector, float pScale = 1f)
	{
		base.prepare(pVector, pScale);
		Vector3 tV = base.transform.localPosition;
		tV.z = -2f;
		this.current_position = tV;
		base.transform.localPosition = tV;
		this.used = false;
		World.world.startShake(0.1f, 0.02f, 3f, false, true);
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x0011317C File Offset: 0x0011137C
	private void Update()
	{
		if (this.sprite_animation.currentFrameIndex >= 32 && !this.used)
		{
			World.world.startShake(0.2f, 0.01f, 3f, false, true);
			this.used = true;
			Vector3 tVec = base.transform.localPosition;
			tVec.y += 2f;
			BaseEffect tEffect = EffectsLibrary.spawnAt("fx_boulder_impact", tVec, base.transform.localScale.x);
			if (tEffect != null)
			{
				tVec = tEffect.transform.localPosition;
				tVec.z = -1f;
				tEffect.transform.localPosition = tVec;
			}
			EffectsLibrary.spawnExplosionWave(tVec, 5f, 1f);
			this.doAction();
		}
	}

	// Token: 0x06001FFA RID: 8186 RVA: 0x00113244 File Offset: 0x00111444
	private void doAction()
	{
		int tTotal = 0;
		List<Actor> tActorList = World.world.units.getSimpleList();
		for (int i = 0; i < tActorList.Count; i++)
		{
			Actor tActor = tActorList[i];
			if (tActor.isAlive() && !tActor.isFavorite() && !tActor.asset.ignored_by_infinity_coin)
			{
				tTotal++;
			}
		}
		int tToRemove;
		if (tTotal % 2 == 0)
		{
			tToRemove = tTotal / 2;
		}
		else
		{
			tToRemove = tTotal / 2 + 1;
		}
		int tKilled = 0;
		EffectInfinityCoin._temp_list.AddRange(World.world.units);
		for (int j = 0; j < EffectInfinityCoin._temp_list.Count; j++)
		{
			EffectInfinityCoin._temp_list.ShuffleOne(j);
			Actor tAc = EffectInfinityCoin._temp_list[j];
			if (tToRemove == 0)
			{
				break;
			}
			if (tAc.isAlive() && !tAc.isFavorite() && !tAc.asset.ignored_by_infinity_coin && !tAc.is_invincible)
			{
				tKilled++;
				tToRemove--;
				tAc.getHitFullHealth(AttackType.Divine);
			}
		}
		WorldTip.addWordReplacement("$removed$", tKilled.ToString());
		WorldTip.showNow("infinity_coin_used", true, "top", 3f, "#F3961F");
		EffectInfinityCoin._temp_list.Clear();
	}

	// Token: 0x0400174A RID: 5962
	private static List<Actor> _temp_list = new List<Actor>();

	// Token: 0x0400174B RID: 5963
	private bool used;
}
