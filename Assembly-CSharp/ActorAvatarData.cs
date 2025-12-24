using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C5 RID: 1733
public class ActorAvatarData
{
	// Token: 0x0600378E RID: 14222 RVA: 0x00191084 File Offset: 0x0018F284
	public void setData(Actor pActor)
	{
		ActorAsset tAsset = pActor.getActorAsset();
		ActorData tActorData = pActor.data;
		ActorAsset pAsset = tAsset;
		Subspecies subspecies = pActor.subspecies;
		SubspeciesTrait pMutation = (subspecies != null) ? subspecies.mutation_skin_asset : null;
		ActorSex pSex = tActorData.sex;
		long id = tActorData.id;
		int head = tActorData.head;
		Sprite cached_sprite_head = pActor.cached_sprite_head;
		int pPhenotypeIndex = tActorData.phenotype_index;
		int phenotype_shade = tActorData.phenotype_shade;
		ColorAsset color = pActor.kingdom.getColor();
		bool pIsEgg = pActor.isEgg();
		bool pIsKing = pActor.isKing();
		bool pIsWarrior = pActor.isWarrior() && !pActor.equipment.helmet.isEmpty();
		bool pIsWise = pActor.hasTrait("wise");
		Subspecies subspecies2 = pActor.subspecies;
		this.setData(pAsset, pMutation, pSex, id, head, cached_sprite_head, pPhenotypeIndex, phenotype_shade, color, pIsEgg, pIsKing, pIsWarrior, pIsWise, (subspecies2 != null) ? subspecies2.egg_asset : null, pActor.isAdult(), !tAsset.prevent_unconscious_rotation && pActor.isLying(), pActor.isTouchingLiquid(), pActor.is_inside_boat, pActor.isHovering(), pActor.isImmovable(), !tAsset.prevent_unconscious_rotation && pActor.is_unconscious, pActor.hasStopIdleAnimation(), pActor.getHandRendererAsset(), pActor.GetHashCode(), pActor.getStatusesIds(), pActor.getStatusesDict());
	}

	// Token: 0x0600378F RID: 14223 RVA: 0x0019118C File Offset: 0x0018F38C
	public void setData(ActorAsset pAsset, SubspeciesTrait pMutation, ActorSex pSex, long pActorId, int pHeadId, Sprite pSpriteHead, int pPhenotypeIndex, int pPhenotypeSkinShade, ColorAsset pKingdomColor, bool pIsEgg, bool pIsKing, bool pIsWarrior, bool pIsWise, SubspeciesTrait pEggAsset, bool pIsAdult, bool pIsLying, bool pIsTouchingLiquid, bool pIsInsideBoat, bool pIsHovering, bool pIsImmovable, bool pIsUnconscious, bool pIsStopIdleAnimation, IHandRenderer pItemPath, int pActorHash, IEnumerable<string> pStatuses, IReadOnlyDictionary<string, Status> pGameplayStatuses)
	{
		this.asset = pAsset;
		this.mutation_skin_asset = pMutation;
		this.sex = pSex;
		this.sprite_head = pSpriteHead;
		this.actor_id = pActorId;
		this.head_id = pHeadId;
		this.phenotype_index = pPhenotypeIndex;
		this.phenotype_skin_shade = pPhenotypeSkinShade;
		this.kingdom_color = pKingdomColor;
		this.is_egg = pIsEgg;
		this.is_king = pIsKing;
		this.is_warrior = pIsWarrior;
		this.is_wise = pIsWise;
		this.egg_asset = pEggAsset;
		this.is_adult = pIsAdult;
		this.is_lying = pIsLying;
		this.is_touching_liquid = pIsTouchingLiquid;
		this.is_inside_boat = pIsInsideBoat;
		this.is_hovering = pIsHovering;
		this.is_immovable = pIsImmovable;
		this.is_unconscious = pIsUnconscious;
		this.is_stop_idle_animation = pIsStopIdleAnimation;
		this.item_renderer = pItemPath;
		this.actor_hash = pActorHash;
		this.statuses = pStatuses;
		this.statuses_gameplay = pGameplayStatuses;
	}

	// Token: 0x06003790 RID: 14224 RVA: 0x00191268 File Offset: 0x0018F468
	public ActorTextureSubAsset getTextureAsset()
	{
		ActorTextureSubAsset tTextureAsset;
		if (this.mutation_skin_asset != null)
		{
			tTextureAsset = this.mutation_skin_asset.texture_asset;
		}
		else
		{
			tTextureAsset = this.asset.texture_asset;
		}
		return tTextureAsset;
	}

	// Token: 0x06003791 RID: 14225 RVA: 0x00191298 File Offset: 0x0018F498
	public Sprite getColoredSprite(Sprite pSprite, AnimationContainerUnit pContainer)
	{
		return DynamicActorSpriteCreatorUI.getUnitSpriteForUI(this.asset, pSprite, pContainer, this.is_adult, this.sex, this.phenotype_index, this.phenotype_skin_shade, this.kingdom_color, this.actor_id, this.head_id, this.is_egg, this.is_king, this.is_warrior, this.is_wise);
	}

	// Token: 0x06003792 RID: 14226 RVA: 0x001912F4 File Offset: 0x0018F4F4
	public bool hasRenderedSpriteHead()
	{
		return this.sprite_head != null;
	}

	// Token: 0x04002934 RID: 10548
	public ActorAsset asset;

	// Token: 0x04002935 RID: 10549
	public SubspeciesTrait mutation_skin_asset;

	// Token: 0x04002936 RID: 10550
	public ActorSex sex;

	// Token: 0x04002937 RID: 10551
	public Sprite sprite_head;

	// Token: 0x04002938 RID: 10552
	public int head_id;

	// Token: 0x04002939 RID: 10553
	public long actor_id;

	// Token: 0x0400293A RID: 10554
	public int phenotype_index;

	// Token: 0x0400293B RID: 10555
	public int phenotype_skin_shade;

	// Token: 0x0400293C RID: 10556
	public ColorAsset kingdom_color;

	// Token: 0x0400293D RID: 10557
	public bool is_egg;

	// Token: 0x0400293E RID: 10558
	public bool is_king;

	// Token: 0x0400293F RID: 10559
	public bool is_warrior;

	// Token: 0x04002940 RID: 10560
	public bool is_wise;

	// Token: 0x04002941 RID: 10561
	public SubspeciesTrait egg_asset;

	// Token: 0x04002942 RID: 10562
	public bool is_adult;

	// Token: 0x04002943 RID: 10563
	public bool is_lying;

	// Token: 0x04002944 RID: 10564
	public bool is_touching_liquid;

	// Token: 0x04002945 RID: 10565
	public bool is_inside_boat;

	// Token: 0x04002946 RID: 10566
	public bool is_hovering;

	// Token: 0x04002947 RID: 10567
	public bool is_immovable;

	// Token: 0x04002948 RID: 10568
	public bool is_unconscious;

	// Token: 0x04002949 RID: 10569
	public bool is_stop_idle_animation;

	// Token: 0x0400294A RID: 10570
	public IHandRenderer item_renderer;

	// Token: 0x0400294B RID: 10571
	public int actor_hash;

	// Token: 0x0400294C RID: 10572
	public IEnumerable<string> statuses;

	// Token: 0x0400294D RID: 10573
	public IReadOnlyDictionary<string, Status> statuses_gameplay;
}
