using System;
using UnityEngine;

// Token: 0x020001BB RID: 443
public readonly struct AttackData
{
	// Token: 0x06000CDE RID: 3294 RVA: 0x000BA0F4 File Offset: 0x000B82F4
	public AttackData(BaseSimObject pInitiator, WorldTile pHitTile, Vector3 pHitPosition, Vector3 pInitiatorPosition, BaseSimObject pTarget, Kingdom pKingdom, AttackType pAttackType = AttackType.Other, bool pMetallicWeapon = false, bool pSkipShake = true, bool pProjectile = false, string pProjectileID = "", Action pKillAction = null, float pBonusAreOfEffect = 0f)
	{
		bool tCriticalDamage = false;
		float tKnockback = 0f;
		int tTargets = 1;
		float tAreaOfEffect = 0.1f;
		int tDamage = 1;
		float tDamageRange = 1f;
		float tCriticalDamageMultiplier = 1f;
		if (pInitiator != null)
		{
			tCriticalDamage = Randy.randomChance(pInitiator.stats["critical_chance"]);
			tKnockback = pInitiator.stats["knockback"];
			tTargets = (int)pInitiator.stats["targets"];
			tAreaOfEffect = pInitiator.stats["area_of_effect"];
			tDamage = (int)pInitiator.stats["damage"];
			tDamageRange = pInitiator.stats["damage_range"];
			tCriticalDamageMultiplier = pInitiator.stats["critical_damage_multiplier"];
		}
		tAreaOfEffect += pBonusAreOfEffect;
		this.kill_action = pKillAction;
		this.initiator = pInitiator;
		this.kingdom = pKingdom;
		this.hit_tile = pHitTile;
		this.initiator_position = pInitiatorPosition;
		this.hit_position = pHitPosition;
		this.target = pTarget;
		this.attack_type = pAttackType;
		this.metallic_weapon = pMetallicWeapon;
		this.skip_shake = pSkipShake;
		this.is_projectile = pProjectile;
		this.projectile_id = pProjectileID;
		this.targets = tTargets;
		this.critical = tCriticalDamage;
		this.knockback = tKnockback;
		this.area_of_effect = tAreaOfEffect;
		this.damage = tDamage;
		this.damage_range = tDamageRange;
		this.critical_damage_multiplier = (int)tCriticalDamageMultiplier;
	}

	// Token: 0x04000C79 RID: 3193
	public readonly BaseSimObject initiator;

	// Token: 0x04000C7A RID: 3194
	public readonly Action kill_action;

	// Token: 0x04000C7B RID: 3195
	public readonly Kingdom kingdom;

	// Token: 0x04000C7C RID: 3196
	public readonly WorldTile hit_tile;

	// Token: 0x04000C7D RID: 3197
	public readonly Vector3 hit_position;

	// Token: 0x04000C7E RID: 3198
	public readonly Vector3 initiator_position;

	// Token: 0x04000C7F RID: 3199
	public readonly BaseSimObject target;

	// Token: 0x04000C80 RID: 3200
	public readonly AttackType attack_type;

	// Token: 0x04000C81 RID: 3201
	public readonly bool skip_shake;

	// Token: 0x04000C82 RID: 3202
	public readonly bool metallic_weapon;

	// Token: 0x04000C83 RID: 3203
	public readonly bool critical;

	// Token: 0x04000C84 RID: 3204
	public readonly int targets;

	// Token: 0x04000C85 RID: 3205
	public readonly int critical_damage_multiplier;

	// Token: 0x04000C86 RID: 3206
	public readonly float area_of_effect;

	// Token: 0x04000C87 RID: 3207
	public readonly int damage;

	// Token: 0x04000C88 RID: 3208
	public readonly float damage_range;

	// Token: 0x04000C89 RID: 3209
	public readonly bool is_projectile;

	// Token: 0x04000C8A RID: 3210
	public readonly string projectile_id;

	// Token: 0x04000C8B RID: 3211
	public readonly float knockback;
}
