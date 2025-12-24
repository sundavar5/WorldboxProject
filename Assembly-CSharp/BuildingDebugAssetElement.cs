using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000555 RID: 1365
public class BuildingDebugAssetElement : BaseDebugAssetElement<BuildingAsset>
{
	// Token: 0x06002C7F RID: 11391 RVA: 0x0015D60A File Offset: 0x0015B80A
	public override void setData(BuildingAsset pAsset)
	{
		this.asset = pAsset;
		this.title.text = this.asset.id;
		this.initAnimations();
		this.initStats();
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x0015D638 File Offset: 0x0015B838
	protected override void initAnimations()
	{
		BuildingSprites tSprites = this.asset.building_sprites;
		this.spawn.setData(this.asset);
		this.main.setData(this.asset);
		this.disabled.setData(this.asset);
		this.ruin.setData(this.asset);
		this.special.setData(this.asset);
		List<DebugAnimatedVariation> tSpawnVariations = new List<DebugAnimatedVariation>();
		List<DebugAnimatedVariation> tMainVariations = new List<DebugAnimatedVariation>();
		List<DebugAnimatedVariation> tDisabledVariations = new List<DebugAnimatedVariation>();
		List<DebugAnimatedVariation> tRuinVariations = new List<DebugAnimatedVariation>();
		List<DebugAnimatedVariation> tSpecialVariations = new List<DebugAnimatedVariation>();
		foreach (BuildingAnimationData tAnimation in this.asset.building_sprites.animation_data)
		{
			tSpawnVariations.Add(new DebugAnimatedVariation(this.getBuildingColoredSprites(tAnimation.spawn), tAnimation.animated));
			tMainVariations.Add(new DebugAnimatedVariation(this.getBuildingColoredSprites(tAnimation.main), tAnimation.animated));
			tDisabledVariations.Add(new DebugAnimatedVariation(this.getBuildingColoredSprites(tAnimation.main_disabled), tAnimation.animated));
			tRuinVariations.Add(new DebugAnimatedVariation(this.getBuildingColoredSprites(tAnimation.ruins), tAnimation.animated));
			tSpecialVariations.Add(new DebugAnimatedVariation(this.getBuildingColoredSprites(tAnimation.special), tAnimation.animated));
		}
		this.spawn.setFrames(tSpawnVariations, this.asset.has_sprites_spawn);
		this.main.setFrames(tMainVariations, this.asset.has_sprites_main);
		this.disabled.setFrames(tDisabledVariations, this.asset.has_sprites_main_disabled);
		this.ruin.setFrames(tRuinVariations, this.asset.has_sprites_ruin);
		this.special.setFrames(tSpecialVariations, this.asset.has_sprites_special);
		if (tSprites.construction != null)
		{
			this.construction.sprite = tSprites.construction;
		}
		else if (this.asset.has_sprite_construction)
		{
			this.construction.sprite = this.no_animation;
		}
		else
		{
			this.construction.color = Color.clear;
		}
		this.mini.sprite = this.loadMini();
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x0015D88C File Offset: 0x0015BA8C
	private Sprite loadMini()
	{
		string tPath = this.asset.sprite_path;
		if (string.IsNullOrEmpty(tPath))
		{
			tPath = this.asset.main_path + this.asset.id;
		}
		tPath += "/mini_0";
		Sprite tSprite = SpriteTextureLoader.getSprite(tPath);
		if (tSprite == null)
		{
			Debug.LogError("Not found mini sprite for building: " + this.asset.id);
			return tSprite;
		}
		KingdomAsset tKingdomAsset = AssetManager.kingdoms.get("mad");
		if (!this.asset.has_kingdom_color)
		{
			return tSprite;
		}
		ColorAsset tKingdomColor = tKingdomAsset.debug_color_asset;
		Texture2D tTexture = new Texture2D(tSprite.texture.width, tSprite.texture.height);
		tTexture.filterMode = tSprite.texture.filterMode;
		for (int x = 0; x < tTexture.width; x++)
		{
			for (int y = 0; y < tTexture.height; y++)
			{
				Color tOrigColor = tSprite.texture.GetPixel(x, y);
				Color tColor = this.getColor(tOrigColor, tKingdomColor);
				tTexture.SetPixel(x, y, tColor);
			}
		}
		tTexture.Apply();
		return Sprite.Create(tTexture, new Rect(Vector2.zero, new Vector2((float)tTexture.width, (float)tTexture.height)), new Vector2(0.5f, 0.5f), 1f);
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x0015D9F4 File Offset: 0x0015BBF4
	private Color32 getColor(Color pOrigColor, ColorAsset pKingdomColor)
	{
		if (Toolbox.areColorsEqual(pOrigColor, Toolbox.color_magenta_0))
		{
			pOrigColor = pKingdomColor.k_color_0;
		}
		else if (Toolbox.areColorsEqual(pOrigColor, Toolbox.color_magenta_1))
		{
			pOrigColor = pKingdomColor.k_color_1;
		}
		else if (Toolbox.areColorsEqual(pOrigColor, Toolbox.color_magenta_2))
		{
			pOrigColor = pKingdomColor.k_color_2;
		}
		else if (Toolbox.areColorsEqual(pOrigColor, Toolbox.color_magenta_3))
		{
			pOrigColor = pKingdomColor.k_color_3;
		}
		else if (Toolbox.areColorsEqual(pOrigColor, Toolbox.color_magenta_4))
		{
			pOrigColor = pKingdomColor.k_color_4;
		}
		return pOrigColor;
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x0015DAB0 File Offset: 0x0015BCB0
	public override void update()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		this.spawn.update();
		this.main.update();
		this.disabled.update();
		this.ruin.update();
		this.special.update();
	}

	// Token: 0x06002C84 RID: 11396 RVA: 0x0015DB02 File Offset: 0x0015BD02
	public override void stopAnimations()
	{
		this.spawn.stopAnimations();
		this.main.stopAnimations();
		this.disabled.stopAnimations();
		this.ruin.stopAnimations();
		this.special.stopAnimations();
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x0015DB3B File Offset: 0x0015BD3B
	public override void startAnimations()
	{
		this.spawn.startAnimations();
		this.main.startAnimations();
		this.disabled.startAnimations();
		this.ruin.startAnimations();
		this.special.startAnimations();
	}

	// Token: 0x06002C86 RID: 11398 RVA: 0x0015DB74 File Offset: 0x0015BD74
	private Sprite[] getBuildingColoredSprites(Sprite[] pSprites)
	{
		if (pSprites == null)
		{
			return new Sprite[0];
		}
		Sprite[] tResult = new Sprite[pSprites.Length];
		for (int i = 0; i < pSprites.Length; i++)
		{
			tResult[i] = this.getBuildingColoredSprite(pSprites[i]);
		}
		return tResult;
	}

	// Token: 0x06002C87 RID: 11399 RVA: 0x0015DBB0 File Offset: 0x0015BDB0
	private Sprite getBuildingColoredSprite(Sprite pMainSprite)
	{
		ColorAsset tKingdomColor = null;
		if (this.asset.has_kingdom_color)
		{
			tKingdomColor = AssetManager.kingdoms.get("mad").debug_color_asset;
		}
		return DynamicSprites.getRecoloredBuilding(pMainSprite, tKingdomColor, this.asset.atlas_asset);
	}

	// Token: 0x06002C88 RID: 11400 RVA: 0x0015DBF4 File Offset: 0x0015BDF4
	protected override void initStats()
	{
		base.initStats();
		base.showStat("health", this.asset.base_stats["health"]);
		base.showStat("damage", this.asset.base_stats["damage"]);
		base.showStat("targets", this.asset.base_stats["targets"]);
		base.showStat("area_of_effect", this.asset.base_stats["area_of_effect"]);
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x0015DC9B File Offset: 0x0015BE9B
	protected override void showAssetWindow()
	{
		base.showAssetWindow();
		ScrollWindow.showWindow("building_asset");
	}

	// Token: 0x0400221F RID: 8735
	public BuildingDebugAnimationElement spawn;

	// Token: 0x04002220 RID: 8736
	public BuildingDebugAnimationElement main;

	// Token: 0x04002221 RID: 8737
	public BuildingDebugAnimationElement disabled;

	// Token: 0x04002222 RID: 8738
	public BuildingDebugAnimationElement ruin;

	// Token: 0x04002223 RID: 8739
	public BuildingDebugAnimationElement special;

	// Token: 0x04002224 RID: 8740
	public Image construction;

	// Token: 0x04002225 RID: 8741
	public Image mini;
}
