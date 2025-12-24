using System;
using UnityEngine;

// Token: 0x020000EA RID: 234
public static class DynamicActorSpriteCreatorUI
{
	// Token: 0x060006DE RID: 1758 RVA: 0x00067968 File Offset: 0x00065B68
	public static AnimationContainerUnit getContainerForUI(ActorAsset pAsset, bool pAdult, ActorTextureSubAsset pTextureAsset, SubspeciesTrait pMutationAsset = null, bool pIsEgg = false, SubspeciesTrait pEggAsset = null, string pTexturePath = null)
	{
		string tPath;
		if (!string.IsNullOrEmpty(pTexturePath))
		{
			tPath = pTexturePath;
		}
		else if (pIsEgg)
		{
			tPath = pEggAsset.sprite_path;
		}
		else if (!pAdult && pAsset.has_baby_form)
		{
			tPath = pTextureAsset.texture_path_baby;
		}
		else
		{
			tPath = pTextureAsset.getUnitTexturePathForUI(pAsset);
		}
		return ActorAnimationLoader.getAnimationContainer(tPath, pAsset, pEggAsset, pMutationAsset);
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x000679B8 File Offset: 0x00065BB8
	public static AnimationContainerUnit getContainerForUI(Actor pActor)
	{
		Subspecies tSubspecies = pActor.subspecies;
		ActorAsset tAsset = pActor.asset;
		SubspeciesTrait tMutationAsset = null;
		ActorTextureSubAsset tTextureAsset;
		if (pActor.hasSubspecies() && tSubspecies.has_mutation_reskin)
		{
			tMutationAsset = tSubspecies.mutation_skin_asset;
			tTextureAsset = tMutationAsset.texture_asset;
		}
		else
		{
			tTextureAsset = tAsset.texture_asset;
		}
		return DynamicActorSpriteCreatorUI.getContainerForUI(tAsset, pActor.isAdult(), tTextureAsset, tMutationAsset, false, null, null);
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00067A10 File Offset: 0x00065C10
	public static Sprite getUnitSpriteForUI(ActorAsset pAsset, Sprite pMainSprite, AnimationContainerUnit pContainer, bool pAdult, ActorSex pSex, int pPhenotypeIndex, int pPhenotypeShade, ColorAsset pKingdomColor, long pActorId, int pHeadId, bool pEgg = false, bool pKing = false, bool pWarrior = false, bool pWise = false)
	{
		long t_kingdomID = 0L;
		long t_phenotypeIndex = (long)pPhenotypeIndex;
		long t_phenotypeShadeID = 0L;
		long t_headID = 0L;
		long t_bodyID = (long)DynamicSpriteCreator.getBodySpriteSmallID(pMainSprite);
		if (t_phenotypeIndex != 0L)
		{
			t_phenotypeShadeID = (long)(pPhenotypeShade + 1);
		}
		Sprite tHeadSprite = DynamicActorSpriteCreatorUI.getSpriteHeadForUI(pAsset, pSex, pContainer, pActorId, pHeadId, pAdult, pEgg, pKing, pWarrior, pWise, false);
		int tHeadId = 0;
		if (tHeadSprite != null)
		{
			ActorAnimationLoader.int_ids_heads.TryGetValue(tHeadSprite, out tHeadId);
			if (tHeadId == 0)
			{
				int tNewID = ActorAnimationLoader.int_ids_heads.Count + 1;
				ActorAnimationLoader.int_ids_heads.Add(tHeadSprite, tNewID);
				tHeadId = tNewID;
			}
			t_headID = (long)tHeadId;
		}
		if (pKingdomColor != null)
		{
			t_kingdomID = (long)(pKingdomColor.index_id + 1);
		}
		long tId = t_kingdomID * 10000000L + t_headID * 1000000L + t_bodyID * 1000L + t_phenotypeIndex * 10L + t_phenotypeShadeID;
		AnimationFrameData tFrameData = null;
		if (pContainer != null)
		{
			pContainer.dict_frame_data.TryGetValue(pMainSprite.name, out tFrameData);
		}
		DynamicSpritesAsset tAsset = DynamicSpritesLibrary.units;
		Sprite tResult = tAsset.getSprite(tId);
		if (tResult == null)
		{
			tResult = DynamicSpriteCreator.createNewSpriteUnit(tFrameData, pMainSprite, tHeadSprite, pKingdomColor, pAsset, pPhenotypeIndex, pPhenotypeShade, pAsset.texture_atlas);
			tAsset.addSprite(tId, tResult);
		}
		return tResult;
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00067B24 File Offset: 0x00065D24
	public static Sprite getUnitSpriteForUI(Actor pActor, Sprite pSprite)
	{
		ActorAsset tAsset = pActor.asset;
		AnimationContainerUnit tContainer = pActor.animation_container;
		Sprite tResult;
		if (tAsset.has_override_avatar_frames)
		{
			tResult = tAsset.get_override_avatar_frames(pActor)[0];
		}
		else
		{
			int tPhenotypeShade = pActor.data.phenotype_shade;
			int tPhenotypeIndex = pActor.data.phenotype_index;
			tResult = DynamicActorSpriteCreatorUI.getUnitSpriteForUI(pActor.asset, pSprite, tContainer, pActor.isAdult(), pActor.data.sex, tPhenotypeIndex, tPhenotypeShade, pActor.kingdom.getColor(), pActor.data.id, pActor.data.head, pActor.isEgg(), false, false, false);
		}
		return tResult;
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x00067BC0 File Offset: 0x00065DC0
	public static Sprite getSpriteHeadForUI(ActorAsset pAsset, ActorSex pSex, AnimationContainerUnit pContainer, long pActorId, int pHeadId, bool pAdult = true, bool pEgg = false, bool pKing = false, bool pWarrior = false, bool pWise = false, bool pRandom = false)
	{
		if (pEgg)
		{
			return null;
		}
		if (pAsset.is_boat)
		{
			return null;
		}
		if (!pAdult && !pContainer.render_heads_for_children)
		{
			return null;
		}
		string tHeadPath = "";
		bool tSpecial = false;
		ActorTextureSubAsset tTextureAsset = pAsset.texture_asset;
		if (!tTextureAsset.has_advanced_textures)
		{
			Sprite[] heads = pContainer.heads;
			if (heads == null || heads.Length == 0)
			{
				return null;
			}
			if (pRandom)
			{
				return pContainer.heads.GetRandom<Sprite>();
			}
			int tHeadIndex = AnimationHelper.getSpriteIndex(pActorId, pContainer.heads.Length);
			return DynamicActorSpriteCreatorUI.getSprite(tHeadIndex, pContainer.heads);
		}
		else
		{
			if (pKing)
			{
				tHeadPath = tTextureAsset.texture_head_king;
				tSpecial = true;
			}
			else if (pWarrior)
			{
				tHeadPath = tTextureAsset.texture_head_warrior;
				tSpecial = true;
			}
			else if (pWise && tTextureAsset.has_old_heads)
			{
				tSpecial = true;
				if (pSex == ActorSex.Male)
				{
					tHeadPath = tTextureAsset.texture_heads_old_male;
				}
				else
				{
					tHeadPath = tTextureAsset.texture_heads_old_female;
				}
			}
			if (tSpecial)
			{
				return ActorAnimationLoader.getHeadSpecial(tHeadPath);
			}
			if (pSex == ActorSex.Male)
			{
				if (pRandom)
				{
					return pContainer.heads_male.GetRandom<Sprite>();
				}
				int tHeadIndex;
				if (pHeadId == -1)
				{
					tHeadIndex = AnimationHelper.getSpriteIndex(pActorId, pContainer.heads_male.Length);
				}
				else
				{
					tHeadIndex = pHeadId;
				}
				return DynamicActorSpriteCreatorUI.getSprite(tHeadIndex, pContainer.heads_male);
			}
			else
			{
				if (pRandom)
				{
					return pContainer.heads_female.GetRandom<Sprite>();
				}
				int tHeadIndex;
				if (pHeadId == -1)
				{
					tHeadIndex = AnimationHelper.getSpriteIndex(pActorId, pContainer.heads_female.Length);
				}
				else
				{
					tHeadIndex = pHeadId;
				}
				return DynamicActorSpriteCreatorUI.getSprite(tHeadIndex, pContainer.heads_female);
			}
		}
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00067CFC File Offset: 0x00065EFC
	private static Sprite getSprite(int pIndex, Sprite[] pSprites)
	{
		return pSprites[pIndex];
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00067D04 File Offset: 0x00065F04
	public static ActorAnimation getBoatAnimation(AnimationDataBoat pBoatAnimation)
	{
		ActorAnimation tResult = new ActorAnimation();
		Sprite[] tSprites = new Sprite[DynamicActorSpriteCreatorUI._boat_angles.Length * 2];
		for (int i = 0; i < DynamicActorSpriteCreatorUI._boat_angles.Length; i++)
		{
			int tAngle = DynamicActorSpriteCreatorUI._boat_angles[i];
			ActorAnimation tAnim = pBoatAnimation.dict[tAngle];
			int tArrayIndex = i * 2;
			tSprites[tArrayIndex] = tAnim.frames[0];
			tSprites[tArrayIndex + 1] = tAnim.frames[1];
		}
		tResult.frames = tSprites;
		return tResult;
	}

	// Token: 0x04000783 RID: 1923
	private static int[] _boat_angles = new int[]
	{
		0,
		-45,
		-90,
		-135,
		180,
		135,
		90,
		45
	};
}
