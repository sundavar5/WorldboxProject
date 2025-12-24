using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000553 RID: 1363
public class BuildingDebugAnimationElement : BaseDebugAnimationElement<BuildingAsset>
{
	// Token: 0x06002C72 RID: 11378 RVA: 0x0015D0AC File Offset: 0x0015B2AC
	public override void update()
	{
		if (!this.is_playing)
		{
			return;
		}
		foreach (BuildingDebugAnimationVariation buildingDebugAnimationVariation in this._variations)
		{
			buildingDebugAnimationVariation.update(Time.deltaTime);
		}
		this.frame_number_text.text = this._variations[0].sprite_animation.currentFrameIndex.ToString();
	}

	// Token: 0x06002C73 RID: 11379 RVA: 0x0015D130 File Offset: 0x0015B330
	public override void setData(BuildingAsset pAsset)
	{
		base.setData(pAsset);
		this._variations = new List<BuildingDebugAnimationVariation>();
		for (int i = 0; i < pAsset.building_sprites.animation_data.Count; i++)
		{
			BuildingDebugAnimationVariation tVariation = Object.Instantiate<BuildingDebugAnimationVariation>(this.variation_prefab, this.variations_transform);
			this.setAnimationSettings(tVariation.sprite_animation, tVariation.image);
			this.setAnimationSettings(tVariation.shadow_animation, tVariation.shadow);
			this._variations.Add(tVariation);
		}
	}

	// Token: 0x06002C74 RID: 11380 RVA: 0x0015D1AC File Offset: 0x0015B3AC
	private void setAnimationSettings(SpriteAnimation pAnimation, Image pImage)
	{
		pAnimation.create();
		pAnimation.useOnSpriteRenderer = false;
		pAnimation.image = pImage;
		pAnimation.timeBetweenFrames = 1f / this.asset.animation_speed;
	}

	// Token: 0x06002C75 RID: 11381 RVA: 0x0015D1DC File Offset: 0x0015B3DC
	public void setFrames(List<DebugAnimatedVariation> pVariations, bool pShouldHaveSprites)
	{
		if (pVariations.Count != this._variations.Count)
		{
			throw new ArgumentOutOfRangeException();
		}
		bool tHasAnimation = false;
		for (int i = 0; i < pVariations.Count; i++)
		{
			BuildingDebugAnimationVariation tVariation = this._variations[i];
			if (!pShouldHaveSprites)
			{
				tVariation.image.color = Color.clear;
				tVariation.shadow.color = Color.clear;
			}
			else
			{
				DebugAnimatedVariation tAnimatedVariation = pVariations[i];
				Sprite[] tFrames = tAnimatedVariation.frames;
				if (tFrames == null || tFrames.Length == 0)
				{
					tVariation.image.sprite = this.no_animation_sprite;
					tVariation.shadow.color = Color.clear;
					tVariation.enabled = false;
					Debug.LogError("Missing sprites for Building asset " + this.asset.id);
				}
				else if (!tAnimatedVariation.animated)
				{
					Sprite tFirst = tFrames[0];
					tVariation.image.sprite = tFirst;
					if (this.asset.shadow)
					{
						DynamicSpriteCreator.createBuildingShadow(this.asset, tFirst, false);
						tVariation.shadow.sprite = DynamicSprites.getShadowBuilding(this.asset, tFirst);
					}
					else
					{
						tVariation.shadow.color = Color.clear;
					}
					tVariation.enabled = false;
				}
				else
				{
					tVariation.sprite_animation.setFrames(tFrames);
					Sprite[] tShadowFrames = new Sprite[tFrames.Length];
					for (int j = 0; j < tFrames.Length; j++)
					{
						Sprite tFrame = tFrames[j];
						if (this.asset.shadow)
						{
							DynamicSpriteCreator.createBuildingShadow(this.asset, tFrame, false);
							tShadowFrames[j] = DynamicSprites.getShadowBuilding(this.asset, tFrame);
						}
						else
						{
							tVariation.shadow.color = Color.clear;
						}
					}
					tVariation.shadow_animation.setFrames(tShadowFrames);
					tHasAnimation = true;
				}
			}
		}
		if (tHasAnimation)
		{
			this.startAnimations();
		}
	}

	// Token: 0x06002C76 RID: 11382 RVA: 0x0015D3A8 File Offset: 0x0015B5A8
	protected override void clear()
	{
		foreach (object obj in this.variations_transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x06002C77 RID: 11383 RVA: 0x0015D404 File Offset: 0x0015B604
	public override void stopAnimations()
	{
		base.stopAnimations();
		foreach (BuildingDebugAnimationVariation buildingDebugAnimationVariation in this._variations)
		{
			buildingDebugAnimationVariation.toggleAnimation(false);
		}
		this.frame_number_text.text = this._variations[0].sprite_animation.currentFrameIndex.ToString();
	}

	// Token: 0x06002C78 RID: 11384 RVA: 0x0015D484 File Offset: 0x0015B684
	public override void startAnimations()
	{
		base.startAnimations();
		foreach (BuildingDebugAnimationVariation buildingDebugAnimationVariation in this._variations)
		{
			buildingDebugAnimationVariation.toggleAnimation(true);
		}
	}

	// Token: 0x06002C79 RID: 11385 RVA: 0x0015D4DC File Offset: 0x0015B6DC
	protected override void clickNextFrame()
	{
		if (this.is_playing)
		{
			return;
		}
		SpriteAnimation sprite_animation = this._variations[0].sprite_animation;
		int tFramesCount = sprite_animation.frames.Length;
		int currentFrameIndex = sprite_animation.currentFrameIndex;
		sprite_animation.currentFrameIndex = currentFrameIndex + 1;
		int tFrameIndex = currentFrameIndex;
		if (tFrameIndex > tFramesCount - 1)
		{
			tFrameIndex = 0;
		}
		this.frame_number_text.text = tFrameIndex.ToString();
		foreach (BuildingDebugAnimationVariation buildingDebugAnimationVariation in this._variations)
		{
			buildingDebugAnimationVariation.setFrame(tFrameIndex);
		}
	}

	// Token: 0x04002218 RID: 8728
	public BuildingDebugAnimationVariation variation_prefab;

	// Token: 0x04002219 RID: 8729
	public Sprite no_animation_sprite;

	// Token: 0x0400221A RID: 8730
	public Transform variations_transform;

	// Token: 0x0400221B RID: 8731
	private List<BuildingDebugAnimationVariation> _variations;

	// Token: 0x0400221C RID: 8732
	private bool _has_baby;
}
