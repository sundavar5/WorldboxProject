using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000359 RID: 857
public static class ActorAnimationLoader
{
	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0011766D File Offset: 0x0011586D
	public static int count_units
	{
		get
		{
			return ActorAnimationLoader._dict_units.Count;
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x060020B1 RID: 8369 RVA: 0x00117679 File Offset: 0x00115879
	public static int count_boats
	{
		get
		{
			return ActorAnimationLoader._dict_boats.Count;
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00117685 File Offset: 0x00115885
	public static int count_heads
	{
		get
		{
			return ActorAnimationLoader._dict_civ_heads.Count;
		}
	}

	// Token: 0x060020B3 RID: 8371 RVA: 0x00117694 File Offset: 0x00115894
	public static Sprite getHeadSpecial(string pPath)
	{
		Sprite tResult;
		if (!ActorAnimationLoader._dict_civ_heads.TryGetValue(pPath, out tResult))
		{
			foreach (Sprite tSprite in SpriteTextureLoader.getSpriteList(pPath, false))
			{
				ActorAnimationLoader._dict_civ_heads.TryAdd(pPath, tSprite);
			}
			tResult = ActorAnimationLoader._dict_civ_heads[pPath];
		}
		return tResult;
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x001176EC File Offset: 0x001158EC
	public static Sprite getHead(string pPath, int pHeadIndex)
	{
		string tHeadID = string.Format("{0}_head_{1}", pPath, pHeadIndex);
		Sprite tResult;
		if (!ActorAnimationLoader._dict_civ_heads.TryGetValue(tHeadID, out tResult))
		{
			foreach (Sprite tSprite in SpriteTextureLoader.getSpriteList(pPath, false))
			{
				string tNewID = pPath + "_" + tSprite.name;
				ActorAnimationLoader._dict_civ_heads.TryAdd(tNewID, tSprite);
			}
			tResult = ActorAnimationLoader._dict_civ_heads[tHeadID];
		}
		return tResult;
	}

	// Token: 0x060020B5 RID: 8373 RVA: 0x00117768 File Offset: 0x00115968
	public static AnimationDataBoat loadAnimationBoat(string pTexturePath)
	{
		AnimationDataBoat tAnimationData;
		if (!ActorAnimationLoader._dict_boats.TryGetValue(pTexturePath, out tAnimationData))
		{
			Dictionary<string, Sprite> tDict = new Dictionary<string, Sprite>();
			Sprite[] sprites = SpriteTextureLoader.getSpriteList("actors/boats/" + pTexturePath, false);
			foreach (Sprite tSprite in sprites)
			{
				tDict.Add(tSprite.name, tSprite);
			}
			tAnimationData = new AnimationDataBoat();
			tAnimationData.broken = new ActorAnimation();
			tAnimationData.broken.frames = new Sprite[]
			{
				tDict["broken"]
			};
			tAnimationData.normal = new ActorAnimation();
			tAnimationData.normal.frames = new Sprite[]
			{
				tDict["normal"]
			};
			foreach (Sprite tSprite2 in sprites)
			{
				if (!tSprite2.name.Contains("@1") && tSprite2.name.Contains("@"))
				{
					ActorAnimationLoader.createBoatAnimationArray(tAnimationData, tDict, tSprite2.name, 0.2f);
				}
			}
			ActorAnimationLoader._dict_boats[pTexturePath] = tAnimationData;
		}
		return tAnimationData;
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x00117884 File Offset: 0x00115A84
	private static void createBoatAnimationArray(AnimationDataBoat pAnimationData, Dictionary<string, Sprite> pDict, string pID, float pTimeBetween = 0.2f)
	{
		int tID_main = int.Parse(pID.Split('@', StringSplitOptions.None)[0]);
		ActorAnimation tAnim = new ActorAnimation();
		tAnim.frames = new Sprite[2];
		tAnim.frames[0] = pDict[tID_main.ToString() + "@" + 0.ToString()];
		tAnim.frames[1] = pDict[tID_main.ToString() + "@" + 1.ToString()];
		pAnimationData.dict.Add(tID_main, tAnim);
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x00117914 File Offset: 0x00115B14
	public static AnimationContainerUnit getAnimationContainer(string pTexturePath, ActorAsset pAsset, SubspeciesTrait pEggAsset = null, SubspeciesTrait pMutationSkinAsset = null)
	{
		AnimationContainerUnit tAnimationContainer;
		if (!ActorAnimationLoader._dict_units.TryGetValue(pTexturePath, out tAnimationContainer))
		{
			tAnimationContainer = ActorAnimationLoader.createAnimationContainer(pTexturePath, pAsset, pEggAsset, pMutationSkinAsset);
		}
		return tAnimationContainer;
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x0011793C File Offset: 0x00115B3C
	private static AnimationContainerUnit createAnimationContainer(string pTexturePath, ActorAsset pAsset, SubspeciesTrait pEggAsset, SubspeciesTrait pMutationSkinAsset = null)
	{
		AnimationContainerUnit tAnim = new AnimationContainerUnit(pTexturePath);
		ActorAnimationLoader._dict_units.Add(pTexturePath, tAnim);
		string[] tAnimationWalk;
		string[] tAnimationSwim;
		string[] tAnimationIdle;
		if (pTexturePath.Contains("eggs/"))
		{
			tAnimationWalk = pEggAsset.animation_walk;
			tAnimationSwim = pEggAsset.animation_swim;
			tAnimationIdle = pEggAsset.animation_idle;
		}
		else if (pTexturePath.Contains("species/mutations"))
		{
			tAnimationWalk = pMutationSkinAsset.animation_walk;
			tAnimationSwim = pMutationSkinAsset.animation_swim;
			tAnimationIdle = pMutationSkinAsset.animation_idle;
		}
		else
		{
			tAnimationWalk = pAsset.animation_walk;
			tAnimationSwim = pAsset.animation_swim;
			tAnimationIdle = pAsset.animation_idle;
		}
		ActorAnimationLoader.generateFrameData(pTexturePath, tAnim, tAnim.sprites, tAnimationSwim);
		ActorAnimationLoader.generateFrameData(pTexturePath, tAnim, tAnim.sprites, tAnimationWalk);
		ActorAnimationLoader.generateFrameData(pTexturePath, tAnim, tAnim.sprites, tAnimationIdle);
		if (tAnimationSwim != null && tAnimationSwim.Length != 0)
		{
			tAnim.swimming = ActorAnimationLoader.createAnim(0, tAnim.sprites, tAnimationSwim);
			if (tAnim.swimming != null)
			{
				tAnim.has_swimming = true;
			}
		}
		if (tAnimationWalk != null && tAnimationWalk.Length != 0)
		{
			tAnim.walking = ActorAnimationLoader.createAnim(1, tAnim.sprites, tAnimationWalk);
			if (tAnim.walking != null)
			{
				tAnim.has_walking = true;
			}
		}
		if (tAnimationIdle != null && tAnimationIdle.Length != 0)
		{
			tAnim.idle = ActorAnimationLoader.createAnim(2, tAnim.sprites, tAnimationIdle);
			if (tAnim.idle != null)
			{
				tAnim.has_idle = true;
			}
		}
		if (pTexturePath.Contains("/child"))
		{
			tAnim.child = true;
		}
		ActorTextureSubAsset tTextureAsset;
		if (pMutationSkinAsset != null && pMutationSkinAsset.is_mutation_skin)
		{
			tTextureAsset = pMutationSkinAsset.texture_asset;
		}
		else
		{
			tTextureAsset = pAsset.texture_asset;
		}
		if (tTextureAsset.texture_heads != string.Empty)
		{
			tAnim.heads = SpriteTextureLoader.getSpriteList(tTextureAsset.texture_heads, false);
		}
		if (tTextureAsset.texture_heads_male != string.Empty)
		{
			tAnim.heads_male = SpriteTextureLoader.getSpriteList(tTextureAsset.texture_heads_male, false);
		}
		if (tTextureAsset.texture_heads_female != string.Empty)
		{
			tAnim.heads_female = SpriteTextureLoader.getSpriteList(tTextureAsset.texture_heads_female, false);
		}
		if (tAnim.heads == null || tAnim.heads.Length == 0)
		{
			tAnim.heads = tAnim.heads_male;
		}
		if (tTextureAsset.render_heads_for_children)
		{
			tAnim.render_heads_for_children = true;
		}
		return tAnim;
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x00117B30 File Offset: 0x00115D30
	private static void generateFrameData(string pFrameString, AnimationContainerUnit pAnimContainer, Dictionary<string, Sprite> pFrames, string[] pStringIDs)
	{
		if (string.IsNullOrEmpty(pFrameString))
		{
			return;
		}
		if (pStringIDs == null)
		{
			return;
		}
		foreach (string tID in pStringIDs)
		{
			if (!pAnimContainer.dict_frame_data.ContainsKey(tID) && pFrames.ContainsKey(tID))
			{
				AnimationFrameData tFrameData = new AnimationFrameData();
				tFrameData.id = tID;
				tFrameData.sheet_path = pFrameString;
				Sprite tBodySprite = pFrames[tID];
				tFrameData.size_unit = tBodySprite.rect.size;
				string tFrameHeadID = tID + "_head";
				Sprite tCoSprite;
				if (pFrames.TryGetValue(tFrameHeadID, out tCoSprite))
				{
					float tX = tCoSprite.rect.x - tBodySprite.rect.x;
					tX = tX - tBodySprite.pivot.x + tCoSprite.pivot.x;
					float tY = tCoSprite.rect.y - tBodySprite.rect.y;
					tY = tY - tBodySprite.pivot.y + tCoSprite.pivot.y;
					tFrameData.pos_head = new Vector2(tX, tY);
					float tNewX = tCoSprite.rect.x - tBodySprite.rect.x;
					float tNewY = tCoSprite.rect.y - tBodySprite.rect.y;
					tFrameData.pos_head_new = new Vector2(tNewX, tNewY);
					tFrameData.show_head = true;
				}
				string tFrameItemID = tID + "_item";
				Sprite tCoSprite2;
				if (pFrames.TryGetValue(tFrameItemID, out tCoSprite2))
				{
					float tX2 = tCoSprite2.rect.x - tBodySprite.rect.x;
					tX2 = tX2 - tBodySprite.pivot.x + tCoSprite2.pivot.x;
					float tY2 = tCoSprite2.rect.y - tBodySprite.rect.y;
					tY2 = tY2 - tBodySprite.pivot.y + tCoSprite2.pivot.y;
					tFrameData.pos_item = new Vector2(tX2, tY2);
					tFrameData.show_item = true;
				}
				pAnimContainer.dict_frame_data.Add(tID, tFrameData);
			}
		}
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x00117D80 File Offset: 0x00115F80
	private static ActorAnimation createAnim(int pID, Dictionary<string, Sprite> pDict, string[] pStringIDs)
	{
		Sprite[] tFrames = ActorAnimationLoader.createArray(pDict, pStringIDs);
		if (tFrames.Length == 0)
		{
			return null;
		}
		return new ActorAnimation
		{
			id = pID,
			frames = tFrames
		};
	}

	// Token: 0x060020BB RID: 8379 RVA: 0x00117DB0 File Offset: 0x00115FB0
	private static Sprite[] createArray(Dictionary<string, Sprite> pDict, string[] pStringIDs)
	{
		Sprite[] result;
		using (ListPool<Sprite> tSprites = new ListPool<Sprite>(pStringIDs.Length))
		{
			foreach (string tString in pStringIDs)
			{
				Sprite tSprite;
				if (!pDict.TryGetValue(tString, out tSprite))
				{
					break;
				}
				tSprites.Add(tSprite);
			}
			result = tSprites.ToArray<Sprite>();
		}
		return result;
	}

	// Token: 0x040017D1 RID: 6097
	public static readonly Dictionary<Sprite, int> int_ids_heads = new Dictionary<Sprite, int>();

	// Token: 0x040017D2 RID: 6098
	private static readonly Dictionary<string, AnimationContainerUnit> _dict_units = new Dictionary<string, AnimationContainerUnit>();

	// Token: 0x040017D3 RID: 6099
	private static readonly Dictionary<string, AnimationDataBoat> _dict_boats = new Dictionary<string, AnimationDataBoat>();

	// Token: 0x040017D4 RID: 6100
	private static readonly Dictionary<string, Sprite> _dict_civ_heads = new Dictionary<string, Sprite>();
}
