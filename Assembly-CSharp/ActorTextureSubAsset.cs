using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x0200035E RID: 862
[Serializable]
public class ActorTextureSubAsset : ICloneable
{
	// Token: 0x060020C1 RID: 8385 RVA: 0x00118094 File Offset: 0x00116294
	public ActorTextureSubAsset(string pBasePath, bool pHasAdvancedTextures)
	{
		ActorTextureSubAsset._total++;
		this.has_advanced_textures = pHasAdvancedTextures;
		this._base_path = pBasePath;
		this.texture_path_base = pBasePath;
		this.texture_path_base_male = pBasePath + "male_1";
		this.texture_path_base_female = pBasePath + "female_1";
		if (string.IsNullOrEmpty(this.texture_head_warrior))
		{
			this.texture_head_warrior = pBasePath + "head_warrior";
		}
		if (string.IsNullOrEmpty(this.texture_head_king))
		{
			this.texture_head_king = pBasePath + "head_king";
		}
		if (string.IsNullOrEmpty(this.texture_heads_old_female))
		{
			this.texture_heads_old_female = pBasePath + "head_old_female";
		}
		if (string.IsNullOrEmpty(this.texture_heads_old_male))
		{
			this.texture_heads_old_male = pBasePath + "head_old_male";
		}
		if (string.IsNullOrEmpty(this.texture_heads_male))
		{
			this.texture_heads_male = pBasePath + "heads_male";
		}
		if (string.IsNullOrEmpty(this.texture_heads_female))
		{
			this.texture_heads_female = pBasePath + "heads_female";
		}
		this.texture_path_main = pBasePath + "main";
		if (!this.hasSpriteInResources(this.texture_path_main))
		{
			this.texture_path_main = this.texture_path_base_male;
		}
		if (string.IsNullOrEmpty(this.texture_path_king))
		{
			this.texture_path_king = pBasePath + "king";
		}
		if (string.IsNullOrEmpty(this.texture_path_leader))
		{
			this.texture_path_leader = pBasePath + "leader";
		}
		if (string.IsNullOrEmpty(this.texture_path_warrior))
		{
			this.texture_path_warrior = pBasePath + "warrior_1";
		}
		if (string.IsNullOrEmpty(this.texture_path_zombie))
		{
			this.texture_path_zombie = pBasePath + "zombie";
		}
		if (string.IsNullOrEmpty(this.texture_heads))
		{
			this.texture_heads = pBasePath + "heads";
			if (!this.hasSpriteInResources(this.texture_path_main))
			{
				this.texture_path_main = this.texture_heads_male;
			}
		}
		if (this.hasSpriteInResources(this.texture_heads_old_male))
		{
			this.has_old_heads = true;
		}
		this.texture_path_baby = pBasePath + "child";
	}

	// Token: 0x060020C2 RID: 8386 RVA: 0x0011831F File Offset: 0x0011651F
	private void logAssetError(string pMessage, string pPath)
	{
		BaseAssetLibrary.logAssetError(pMessage, pPath);
	}

	// Token: 0x060020C3 RID: 8387 RVA: 0x00118328 File Offset: 0x00116528
	public string getUnitTexturePath(Actor pActor)
	{
		Subspecies tSubspecies = pActor.subspecies;
		if (pActor.isEgg())
		{
			return tSubspecies.egg_sprite_path;
		}
		if (pActor.isBaby())
		{
			return this.texture_path_baby;
		}
		if (pActor.hasSubspecies() && pActor.subspecies.has_mutation_reskin && pActor.asset.unit_zombie)
		{
			return this.texture_path_zombie;
		}
		string tResult = this.texture_path_main;
		ProfessionAsset tProfessionAsset = pActor.profession_asset;
		if (tProfessionAsset == null || tProfessionAsset.profession_id == UnitProfession.Nothing)
		{
			return tResult;
		}
		if (!this.has_advanced_textures)
		{
			return tResult;
		}
		switch (tProfessionAsset.profession_id)
		{
		case UnitProfession.King:
			return this.texture_path_king;
		case UnitProfession.Leader:
			return this.texture_path_leader;
		case UnitProfession.Warrior:
		{
			string tWarriorId = this.texture_path_warrior;
			if (pActor.hasSubspecies())
			{
				tWarriorId = pActor.subspecies.getSkinWarrior();
			}
			if (tSubspecies.has_mutation_reskin)
			{
				List<string> tWarriorSkins = tSubspecies.mutation_skin_asset.skin_warrior;
				int tNextIndex = Toolbox.loopIndex(pActor.asset.skin_warrior.IndexOf(tWarriorId), tWarriorSkins.Count);
				tWarriorId = tWarriorSkins[tNextIndex];
			}
			return this.texture_path_base + tWarriorId;
		}
		}
		return this.getTextureSkinBasedOnSex(pActor);
	}

	// Token: 0x060020C4 RID: 8388 RVA: 0x00118450 File Offset: 0x00116650
	private string getTextureSkinBasedOnSex(Actor pActor)
	{
		string tResult;
		if (pActor.isSexFemale())
		{
			if (pActor.hasSubspecies())
			{
				tResult = this.texture_path_base + pActor.subspecies.getSkinFemale();
			}
			else
			{
				tResult = this.texture_path_base_female;
			}
		}
		else if (pActor.hasSubspecies())
		{
			tResult = this.texture_path_base + pActor.subspecies.getSkinMale();
		}
		else
		{
			tResult = this.texture_path_base_male;
		}
		return tResult;
	}

	// Token: 0x060020C5 RID: 8389 RVA: 0x001184B8 File Offset: 0x001166B8
	public string getUnitTexturePathForUI(ActorAsset pAsset)
	{
		string tResult = this.texture_path_main;
		if (!pAsset.civ)
		{
			return tResult;
		}
		if (AssetsDebugManager.actors_sex == ActorSex.Male)
		{
			tResult = this.texture_path_base + pAsset.skin_citizen_male[0];
		}
		else
		{
			tResult = this.texture_path_base + pAsset.skin_citizen_female[0];
		}
		return tResult;
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x00118508 File Offset: 0x00116708
	private bool hasSpriteInResources(string pPath)
	{
		Sprite[] tSprites = SpriteTextureLoader.getSpriteList(pPath, true);
		if (tSprites == null)
		{
			return false;
		}
		ActorTextureSubAsset.all_preloaded_sprites_units.AddRange(tSprites);
		return tSprites.Length != 0;
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x00118532 File Offset: 0x00116732
	public object Clone()
	{
		return new ActorTextureSubAsset(this._base_path, this.has_advanced_textures);
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x00118548 File Offset: 0x00116748
	public void preloadSprites(bool pCivTextures, bool pHasBabyForm, IAnimationFrames pAnimationAsset)
	{
		if (!pCivTextures)
		{
			this.preloadSpritePath("texture_path_main", this.texture_path_main, pAnimationAsset, false, true, false);
		}
		if (pHasBabyForm)
		{
			this.preloadSpritePath("texture_path_baby", this.texture_path_baby, pAnimationAsset, false, true, false);
		}
		if (this.has_advanced_textures)
		{
			this.preloadSpritePath("texture_path_base_male", this.texture_path_base_male, pAnimationAsset, false, true, false);
			this.preloadSpritePath("texture_path_base_female", this.texture_path_base_female, pAnimationAsset, false, true, false);
			this.preloadSpritePath("texture_path_king", this.texture_path_king, pAnimationAsset, false, true, false);
			this.preloadSpritePath("texture_path_leader", this.texture_path_leader, pAnimationAsset, false, true, false);
			this.preloadSpritePath("texture_path_warrior", this.texture_path_warrior, pAnimationAsset, false, true, false);
			this.preloadSpritePath("texture_head_king", this.texture_head_king, pAnimationAsset, true, true, true);
			this.preloadSpritePath("texture_head_warrior", this.texture_head_warrior, pAnimationAsset, true, true, true);
			this.preloadSpritePath("texture_heads_male", this.texture_heads_male, pAnimationAsset, true, true, false);
			this.preloadSpritePath("texture_heads_female", this.texture_heads_female, pAnimationAsset, true, true, false);
			return;
		}
		this.preloadSpritePath("texture_heads", this.texture_heads, pAnimationAsset, true, false, false);
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x00118670 File Offset: 0x00116870
	private bool preloadSpritePath(string pType, string pPath, IAnimationFrames pAnimationAsset, bool pLoadHeads = false, bool pThrowError = true, bool pSpecialHead = false)
	{
		if (string.IsNullOrEmpty(pPath))
		{
			return false;
		}
		if (this.dict_mains.ContainsKey(pPath))
		{
			return false;
		}
		Sprite[] tSprites = SpriteTextureLoader.getSpriteList(pPath, false);
		if (!pLoadHeads)
		{
			this.dict_mains.Add(pPath, tSprites);
		}
		ActorTextureSubAsset.all_preloaded_sprites_units.AddRange(tSprites);
		if (tSprites.Length == 0)
		{
			if (pThrowError)
			{
				this.logAssetError(string.Concat(new string[]
				{
					"ActorAssetLibrary: <e>",
					pType,
					"</e> doesn't exist for <e>",
					((Asset)pAnimationAsset).id,
					"</e> at "
				}), pPath);
			}
			return false;
		}
		if (pLoadHeads)
		{
			if (this.has_advanced_textures)
			{
				for (int i = 0; i < tSprites.Length; i++)
				{
					if (pSpecialHead)
					{
						ActorAnimationLoader.getHeadSpecial(pPath);
					}
					else
					{
						ActorAnimationLoader.getHead(pPath, i);
					}
				}
			}
			this.checkHeads(tSprites, pPath, pAnimationAsset);
		}
		else
		{
			this.checkAnimations(tSprites, pPath, (Asset)pAnimationAsset, pAnimationAsset);
		}
		return true;
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x0011874C File Offset: 0x0011694C
	internal void loadShadow()
	{
		Sprite tShadow = this.getShadowSprite(this.shadow_texture);
		this.shadow_sprite = tShadow;
		this.shadow_size = tShadow.rect.size;
		Sprite tShadowEgg = this.getShadowSprite(this.shadow_texture_egg);
		this.shadow_sprite_egg = tShadowEgg;
		this.shadow_size_egg = tShadowEgg.rect.size;
		Sprite tShadowBaby = this.getShadowSprite(this.shadow_texture_baby);
		this.shadow_sprite_baby = tShadowBaby;
		this.shadow_size_baby = tShadowBaby.rect.size;
	}

	// Token: 0x060020CB RID: 8395 RVA: 0x001187D4 File Offset: 0x001169D4
	private Sprite getShadowSprite(string pTexturePath)
	{
		if (!ActorTextureSubAsset._shadow_sprites.ContainsKey(pTexturePath))
		{
			Sprite tSprite = SpriteTextureLoader.getSprite("shadows/" + pTexturePath);
			if (tSprite == null)
			{
				Debug.LogError("Shadow not found " + pTexturePath);
			}
			ActorTextureSubAsset._shadow_sprites.Add(pTexturePath, tSprite);
		}
		Sprite sprite = ActorTextureSubAsset._shadow_sprites[pTexturePath];
		return DynamicSprites.getShadowUnit(sprite, sprite.GetHashCode());
	}

	// Token: 0x060020CC RID: 8396 RVA: 0x0011883C File Offset: 0x00116A3C
	private void checkHeads(Sprite[] pSprites, string pPath, IAnimationFrames pAnimationAsset)
	{
		using (ListPool<string> tSpriteNames = new ListPool<string>())
		{
			for (int i = 0; i < pSprites.Length; i++)
			{
				string tName = pSprites[i].name;
				if (tName.Any(new Func<char, bool>(char.IsDigit)))
				{
					if (tSpriteNames.Contains(tName))
					{
						Debug.LogError("Duplicate head " + tName);
					}
					tSpriteNames.Add(tName);
				}
			}
			tSpriteNames.Sort(new Comparison<string>(this.headSorter));
			string tLastHead = "";
			int tLastId = -1;
			foreach (string ptr in tSpriteNames)
			{
				string[] array = ptr.Split("_", StringSplitOptions.None);
				string tHead = array[0];
				int tIndex;
				if (int.TryParse(array[1], out tIndex))
				{
					if (tHead != tLastHead)
					{
						tLastHead = tHead;
						if (tIndex != 0)
						{
							this.logAssetError(string.Concat(new string[]
							{
								"ActorAssetLibrary: <e>",
								((Asset)pAnimationAsset).id,
								"</e> missing head: <e>",
								tHead,
								"_0</e> at "
							}), pPath);
						}
					}
					else if (tIndex != tLastId + 1)
					{
						this.logAssetError(string.Format("ActorAssetLibrary: <e>{0}</e> missing head: <e>{1}_{2}</e> at ", ((Asset)pAnimationAsset).id, tHead, tLastId + 1), pPath);
					}
					tLastId = tIndex;
				}
			}
		}
	}

	// Token: 0x060020CD RID: 8397 RVA: 0x001189D0 File Offset: 0x00116BD0
	private int headSorter(string x, string y)
	{
		Match xRegexResult = ActorTextureSubAsset._regex_heads_sorter.Match(x);
		Match yRegexResult = ActorTextureSubAsset._regex_heads_sorter.Match(y);
		int tIntX;
		int tIntY;
		if (xRegexResult.Success && yRegexResult.Success && xRegexResult.Groups[1].Value == yRegexResult.Groups[1].Value && int.TryParse(xRegexResult.Groups[2].Value, out tIntX) && int.TryParse(yRegexResult.Groups[2].Value, out tIntY))
		{
			return tIntX.CompareTo(tIntY);
		}
		return x.CompareTo(y);
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x00118A74 File Offset: 0x00116C74
	private void checkAnimations(Sprite[] pSprites, string pPath, Asset pAsset, IAnimationFrames pAnimationFrames)
	{
		using (ListPool<string> tSpriteNames = new ListPool<string>())
		{
			foreach (Sprite tSprite in pSprites)
			{
				tSpriteNames.Add(tSprite.name);
			}
			using (ListPool<string> tMissing = new ListPool<string>())
			{
				string[] walk = pAnimationFrames.getWalk();
				if (walk != null && walk.Length != 0)
				{
					tMissing.Clear();
					bool tFound = false;
					foreach (string tAnimation in pAnimationFrames.getWalk())
					{
						if (!tSpriteNames.Contains(tAnimation))
						{
							tMissing.Add(tAnimation);
						}
						else
						{
							tFound = true;
						}
					}
					if (!tFound)
					{
						this.logAssetError(string.Concat(new string[]
						{
							"ActorAssetLibrary: <e>",
							pAsset.id,
							"</e> missing all animation_walk sprites: <e>",
							string.Join(", ", tMissing),
							"</e> at "
						}), pPath);
					}
				}
				string[] swim = pAnimationFrames.getSwim();
				if (swim != null && swim.Length != 0)
				{
					tMissing.Clear();
					bool tFound2 = false;
					foreach (string tAnimation2 in pAnimationFrames.getSwim())
					{
						if (!tSpriteNames.Contains(tAnimation2))
						{
							tMissing.Add(tAnimation2);
						}
						else
						{
							tFound2 = true;
						}
					}
					if (!tFound2)
					{
						this.logAssetError(string.Concat(new string[]
						{
							"ActorAssetLibrary: <e>",
							pAsset.id,
							"</e> missing all animation_swim sprites: <e>",
							string.Join(", ", tMissing),
							"</e> at "
						}), pPath);
					}
				}
				string[] idle = pAnimationFrames.getIdle();
				if (idle != null && idle.Length != 0)
				{
					tMissing.Clear();
					bool tFound3 = false;
					foreach (string tAnimation3 in pAnimationFrames.getIdle())
					{
						if (!tSpriteNames.Contains(tAnimation3))
						{
							tMissing.Add(tAnimation3);
						}
						else
						{
							tFound3 = true;
						}
					}
					if (!tFound3)
					{
						this.logAssetError(string.Concat(new string[]
						{
							"ActorAssetLibrary: <e>",
							pAsset.id,
							"</e> missing all animation_idle sprites: <e>",
							string.Join(", ", tMissing),
							"</e> at "
						}), pPath);
					}
				}
			}
		}
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x00118CC8 File Offset: 0x00116EC8
	public static int getTotal()
	{
		return ActorTextureSubAsset._total;
	}

	// Token: 0x040017F9 RID: 6137
	[DefaultValue("male_1")]
	public const string skin_civ_default_male = "male_1";

	// Token: 0x040017FA RID: 6138
	[DefaultValue("female_1")]
	public const string skin_civ_default_female = "female_1";

	// Token: 0x040017FB RID: 6139
	public static List<Sprite> all_preloaded_sprites_units = new List<Sprite>();

	// Token: 0x040017FC RID: 6140
	[NonSerialized]
	public readonly Dictionary<string, Sprite[]> dict_mains = new Dictionary<string, Sprite[]>();

	// Token: 0x040017FD RID: 6141
	private static Dictionary<string, Sprite> _shadow_sprites = new Dictionary<string, Sprite>();

	// Token: 0x040017FE RID: 6142
	public string texture_path_base;

	// Token: 0x040017FF RID: 6143
	public string texture_path_base_male;

	// Token: 0x04001800 RID: 6144
	public string texture_path_base_female;

	// Token: 0x04001801 RID: 6145
	public string texture_path_main;

	// Token: 0x04001802 RID: 6146
	public string texture_path_baby;

	// Token: 0x04001803 RID: 6147
	public string texture_path_king;

	// Token: 0x04001804 RID: 6148
	public string texture_path_leader;

	// Token: 0x04001805 RID: 6149
	public string texture_path_warrior;

	// Token: 0x04001806 RID: 6150
	public string texture_path_zombie;

	// Token: 0x04001807 RID: 6151
	public bool has_advanced_textures;

	// Token: 0x04001808 RID: 6152
	public bool has_old_heads;

	// Token: 0x04001809 RID: 6153
	[DefaultValue("")]
	public string texture_heads = string.Empty;

	// Token: 0x0400180A RID: 6154
	[DefaultValue("")]
	public string texture_head_king = string.Empty;

	// Token: 0x0400180B RID: 6155
	[DefaultValue("")]
	public string texture_head_warrior = string.Empty;

	// Token: 0x0400180C RID: 6156
	[DefaultValue("")]
	public string texture_heads_old_male = string.Empty;

	// Token: 0x0400180D RID: 6157
	[DefaultValue("")]
	public string texture_heads_old_female = string.Empty;

	// Token: 0x0400180E RID: 6158
	[DefaultValue("")]
	public string texture_heads_male = string.Empty;

	// Token: 0x0400180F RID: 6159
	[DefaultValue("")]
	public string texture_heads_female = string.Empty;

	// Token: 0x04001810 RID: 6160
	public bool render_heads_for_children;

	// Token: 0x04001811 RID: 6161
	public bool prevent_unconscious_rotation;

	// Token: 0x04001812 RID: 6162
	[DefaultValue(true)]
	public bool shadow = true;

	// Token: 0x04001813 RID: 6163
	[DefaultValue("unitShadow_5")]
	public string shadow_texture = "unitShadow_5";

	// Token: 0x04001814 RID: 6164
	[NonSerialized]
	internal Sprite shadow_sprite;

	// Token: 0x04001815 RID: 6165
	[NonSerialized]
	internal Vector2 shadow_size;

	// Token: 0x04001816 RID: 6166
	[DefaultValue("unitShadow_2")]
	public string shadow_texture_egg = "unitShadow_2";

	// Token: 0x04001817 RID: 6167
	[NonSerialized]
	internal Sprite shadow_sprite_egg;

	// Token: 0x04001818 RID: 6168
	[NonSerialized]
	internal Vector2 shadow_size_egg;

	// Token: 0x04001819 RID: 6169
	[DefaultValue("unitShadow_4")]
	public string shadow_texture_baby = "unitShadow_4";

	// Token: 0x0400181A RID: 6170
	[NonSerialized]
	internal Sprite shadow_sprite_baby;

	// Token: 0x0400181B RID: 6171
	[NonSerialized]
	internal Vector2 shadow_size_baby;

	// Token: 0x0400181C RID: 6172
	private string _base_path;

	// Token: 0x0400181D RID: 6173
	private static int _total;

	// Token: 0x0400181E RID: 6174
	private static readonly Regex _regex_heads_sorter = new Regex("(\\D*)(\\d+)");
}
