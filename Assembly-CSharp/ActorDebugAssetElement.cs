using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000549 RID: 1353
public class ActorDebugAssetElement : BaseDebugAssetElement<ActorAsset>
{
	// Token: 0x06002C1C RID: 11292 RVA: 0x0015B808 File Offset: 0x00159A08
	public override void setData(ActorAsset pAsset)
	{
		this.asset = pAsset;
		Sprite tIcon = this.asset.getSpriteIcon();
		this.icon_left.sprite = tIcon;
		this.icon_right.sprite = tIcon;
		this.title.text = this.asset.id;
		this.egg.gameObject.SetActive(true);
		this.initAnimations();
		this.initStats();
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x0015B874 File Offset: 0x00159A74
	protected override void initAnimations()
	{
		if (this.asset.hasDefaultEggForm())
		{
			this.egg.gameObject.SetActive(true);
			SubspeciesTrait tEggAsset = AssetManager.subspecies_traits.get(this.asset.getDefaultEggID());
			this.egg.sprite = SpriteTextureLoader.getSprite(tEggAsset.sprite_path);
		}
		else
		{
			this.egg.gameObject.SetActive(false);
		}
		this.animation_idle.setData(this.asset);
		this.animation_walk.setData(this.asset);
		this.animation_swim.setData(this.asset);
		if (this.asset.special)
		{
			Sprite tIcon = this.icon_left.sprite;
			this.animation_idle.adult.image.sprite = tIcon;
			this.animation_walk.adult.image.sprite = tIcon;
			this.animation_swim.adult.image.sprite = tIcon;
			this.egg.gameObject.SetActive(false);
			this.stopAnimations();
			return;
		}
		if (this.asset.use_phenotypes)
		{
			this._phenotype_index = AssetManager.phenotype_library.get(this.asset.debug_phenotype_colors).phenotype_index;
			this._phenotype_shade_id = Actor.getRandomPhenotypeShade();
		}
		else
		{
			this._phenotype_index = 0;
			this._phenotype_shade_id = 0;
		}
		if (this.asset.is_boat)
		{
			AnimationDataBoat tBoatAnim = ActorAnimationLoader.loadAnimationBoat(this.asset.id);
			this.setAnimation(DynamicActorSpriteCreatorUI.getBoatAnimation(tBoatAnim), this.animation_idle, this.asset.animation_idle_speed, true, true, true);
			this.setAnimation(tBoatAnim.normal, this.animation_walk, this.asset.animation_walk_speed, true, true, true);
			this.setAnimation(tBoatAnim.broken, this.animation_swim, this.asset.animation_swim_speed, true, true, true);
			return;
		}
		string[] array = this.asset.animation_idle;
		bool tShouldIdle = array != null && array.Length != 0;
		string[] array2 = this.asset.animation_walk;
		bool tShouldWalk = array2 != null && array2.Length != 0;
		string[] array3 = this.asset.animation_swim;
		bool tShouldSwim = array3 != null && array3.Length != 0;
		this._animation_container_adult = DynamicActorSpriteCreatorUI.getContainerForUI(this.asset, true, this.asset.texture_asset, null, false, null, null);
		this.setAnimation(this._animation_container_adult.idle, this.animation_idle, this.asset.animation_idle_speed, true, this._animation_container_adult.has_idle, tShouldIdle);
		this.setAnimation(this._animation_container_adult.walking, this.animation_walk, this.asset.animation_walk_speed, true, this._animation_container_adult.has_walking, tShouldWalk);
		List<string> default_subspecies_traits = this.asset.default_subspecies_traits;
		if (default_subspecies_traits != null && !default_subspecies_traits.Contains("hovering") && !this.asset.flying)
		{
			this.setAnimation(this._animation_container_adult.swimming, this.animation_swim, this.asset.animation_swim_speed, true, this._animation_container_adult.has_swimming, tShouldSwim);
		}
		else
		{
			this.animation_swim.adult.image.color = Color.clear;
			this.animation_swim.adult.enabled = false;
		}
		if (this.asset.has_baby_form)
		{
			this._animation_container_baby = DynamicActorSpriteCreatorUI.getContainerForUI(this.asset, false, this.asset.texture_asset, null, false, null, null);
			this.setAnimation(this._animation_container_baby.idle, this.animation_idle, this.asset.animation_idle_speed, false, this._animation_container_baby.has_idle, tShouldIdle);
			this.setAnimation(this._animation_container_baby.walking, this.animation_walk, this.asset.animation_walk_speed, false, this._animation_container_baby.has_walking, tShouldWalk);
			if (!this.asset.default_subspecies_traits.Contains("hovering") && !this.asset.flying)
			{
				this.setAnimation(this._animation_container_baby.swimming, this.animation_swim, this.asset.animation_swim_speed, false, this._animation_container_baby.has_swimming, tShouldSwim);
				return;
			}
			this.animation_swim.baby.image.color = Color.clear;
			this.animation_swim.baby.enabled = false;
		}
	}

	// Token: 0x06002C1E RID: 11294 RVA: 0x0015BCAF File Offset: 0x00159EAF
	public override void update()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		this.animation_idle.update();
		this.animation_walk.update();
		this.animation_swim.update();
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x0015BCE0 File Offset: 0x00159EE0
	public override void stopAnimations()
	{
		this.animation_idle.stopAnimations();
		this.animation_walk.stopAnimations();
		this.animation_swim.stopAnimations();
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x0015BD03 File Offset: 0x00159F03
	public override void startAnimations()
	{
		this.animation_idle.startAnimations();
		this.animation_walk.startAnimations();
		this.animation_swim.startAnimations();
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x0015BD28 File Offset: 0x00159F28
	private void setAnimation(ActorAnimation pAnimation, ActorDebugAnimationElement pElement, float pAnimationSpeed, bool pIsAdult, bool pHasAnimation, bool pShouldHaveAnimation)
	{
		SpriteAnimation tSpriteAnimation = pIsAdult ? pElement.adult : pElement.baby;
		if (!pShouldHaveAnimation)
		{
			tSpriteAnimation.image.color = Color.clear;
			tSpriteAnimation.image.sprite = null;
			tSpriteAnimation.enabled = false;
			return;
		}
		if (!pHasAnimation)
		{
			tSpriteAnimation.image.color = Color.white;
			tSpriteAnimation.image.sprite = (pIsAdult ? this.no_animation : this.no_animation_baby);
			tSpriteAnimation.enabled = false;
			return;
		}
		AnimationContainerUnit tContainer = pIsAdult ? this._animation_container_adult : this._animation_container_baby;
		Sprite[] tReadyFrames = new Sprite[pAnimation.frames.Length];
		ColorAsset tKingdomColor = AssetManager.kingdoms.get(this.asset.kingdom_id_wild).debug_color_asset;
		for (int i = 0; i < pAnimation.frames.Length; i++)
		{
			tReadyFrames[i] = DynamicActorSpriteCreatorUI.getUnitSpriteForUI(this.asset, pAnimation.frames[i], tContainer, pIsAdult, AssetsDebugManager.actors_sex, this._phenotype_index, this._phenotype_shade_id, tKingdomColor, 0L, 0, false, false, false, false);
		}
		tSpriteAnimation.enabled = true;
		tSpriteAnimation.setFrames(tReadyFrames);
		tSpriteAnimation.timeBetweenFrames = 1f / pAnimationSpeed;
		pElement.startAnimations();
	}

	// Token: 0x06002C22 RID: 11298 RVA: 0x0015BE54 File Offset: 0x0015A054
	protected override void initStats()
	{
		base.initStats();
		BaseStats tOverviewStats = this.asset.getStatsForOverview();
		base.showStat("health", tOverviewStats["health"]);
		base.showStat("damage", tOverviewStats["damage"]);
		base.showStat("speed", tOverviewStats["speed"]);
		base.showStat("lifespan", tOverviewStats["lifespan"]);
	}

	// Token: 0x06002C23 RID: 11299 RVA: 0x0015BEDF File Offset: 0x0015A0DF
	protected override void showAssetWindow()
	{
		base.showAssetWindow();
		ScrollWindow.showWindow("actor_asset");
	}

	// Token: 0x040021D9 RID: 8665
	public Image icon_left;

	// Token: 0x040021DA RID: 8666
	public Image icon_right;

	// Token: 0x040021DB RID: 8667
	public ActorDebugAnimationElement animation_idle;

	// Token: 0x040021DC RID: 8668
	public ActorDebugAnimationElement animation_walk;

	// Token: 0x040021DD RID: 8669
	public ActorDebugAnimationElement animation_swim;

	// Token: 0x040021DE RID: 8670
	public Image egg;

	// Token: 0x040021DF RID: 8671
	public Sprite no_animation_baby;

	// Token: 0x040021E0 RID: 8672
	private AnimationContainerUnit _animation_container_adult;

	// Token: 0x040021E1 RID: 8673
	private AnimationContainerUnit _animation_container_baby;

	// Token: 0x040021E2 RID: 8674
	private int _phenotype_index;

	// Token: 0x040021E3 RID: 8675
	private int _phenotype_shade_id;
}
