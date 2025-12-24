using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006CE RID: 1742
public class UnitAvatarLoader : MonoBehaviour
{
	// Token: 0x060037D9 RID: 14297 RVA: 0x00191E84 File Offset: 0x00190084
	public void load(Actor pActor)
	{
		this._actor = pActor;
		this._same_actor_reloaded = (this._actor == pActor);
		if (this._data == null)
		{
			this._data = new ActorAvatarData();
		}
		if (!pActor.isAlive())
		{
			this.load(true);
			return;
		}
		ActorAvatarData data = this._data;
		SubspeciesTrait subspeciesTrait = (data != null) ? data.mutation_skin_asset : null;
		Subspecies subspecies = pActor.subspecies;
		this._same_skin_mutation_reloaded = (subspeciesTrait == ((subspecies != null) ? subspecies.mutation_skin_asset : null));
		this._data.setData(pActor);
		if (!this._data.asset.has_override_sprite)
		{
			this._animation_container = DynamicActorSpriteCreatorUI.getContainerForUI(this._data.asset, this._data.is_adult, this._data.getTextureAsset(), this._data.mutation_skin_asset, this._data.is_egg, this._data.egg_asset, this._actor.getUnitTexturePath());
		}
		this.load(false);
	}

	// Token: 0x060037DA RID: 14298 RVA: 0x00191F74 File Offset: 0x00190174
	public void load(ActorAvatarData pData, bool pSameActor = false)
	{
		this._died = false;
		this._same_actor_reloaded = pSameActor;
		ActorAvatarData data = this._data;
		this._same_skin_mutation_reloaded = (((data != null) ? data.mutation_skin_asset : null) == pData.mutation_skin_asset);
		this._data = pData;
		if (!this._data.asset.has_override_sprite)
		{
			this._animation_container = DynamicActorSpriteCreatorUI.getContainerForUI(this._data.asset, this._data.is_adult, this._data.getTextureAsset(), this._data.mutation_skin_asset, this._data.is_egg, this._data.egg_asset, null);
		}
		this.load(false);
	}

	// Token: 0x060037DB RID: 14299 RVA: 0x00192020 File Offset: 0x00190220
	private void load(bool pDied = false)
	{
		if (this._effects_pool_attached_below == null)
		{
			this._effects_pool_attached_below = new ObjectPoolGenericMono<AvatarEffect>(this._effect_prefab, this._effects_parent_attached_below);
		}
		if (this._effects_pool_attached_above == null)
		{
			this._effects_pool_attached_above = new ObjectPoolGenericMono<AvatarEffect>(this._effect_prefab, this._effects_parent_attached_above);
		}
		if (this._effects_pool_below == null)
		{
			this._effects_pool_below = new ObjectPoolGenericMono<AvatarEffect>(this._effect_prefab, this._effects_parent_below);
		}
		if (this._effects_pool_above == null)
		{
			this._effects_pool_above = new ObjectPoolGenericMono<AvatarEffect>(this._effect_prefab, this._effects_parent_above);
		}
		this.clear();
		this._died = pDied;
		if (this._died)
		{
			base.transform.localScale = new Vector3(1f * this.avatarSize, 1f * this.avatarSize, 0f);
			this.showDied();
			return;
		}
		base.transform.localScale = new Vector3(this._data.asset.inspect_avatar_scale * this.avatarSize, this._data.asset.inspect_avatar_scale * this.avatarSize, 0f);
		if (this._frame != null)
		{
			this._frame.localScale = new Vector3(2.5f / (this._data.asset.inspect_avatar_scale * this.avatarSize), 2.5f / (this._data.asset.inspect_avatar_scale * this.avatarSize), 0f);
		}
		this.loadItemSprites();
		AnimationContainerUnit animation_container = this._animation_container;
		bool tWalking = animation_container != null && animation_container.has_walking;
		if (this._data.asset.has_override_sprite && !this._data.asset.has_override_avatar_frames && !this._data.asset.is_boat)
		{
			this._animated = false;
		}
		else
		{
			this._animated = ((tWalking || this._data.asset.is_boat || this._data.asset.has_override_avatar_frames) && !this._data.is_egg && !this._data.is_lying && !this._data.is_stop_idle_animation);
		}
		if (!this._animated)
		{
			this.showStatic();
		}
		else
		{
			this.showAnimation();
		}
		this.checkRotationAndPivot();
		this.showStatusEffects();
	}

	// Token: 0x060037DC RID: 14300 RVA: 0x00192264 File Offset: 0x00190464
	private void loadItemSprites()
	{
		IHandRenderer tItemRenderer = this._data.item_renderer;
		this._show_item = (this._data.asset.use_items && tItemRenderer != null);
		if (!this._show_item)
		{
			return;
		}
		if (tItemRenderer.is_animated)
		{
			foreach (Sprite tSprite in tItemRenderer.getSprites())
			{
				Sprite tResult = this.getColoredItemSprite(tSprite, tItemRenderer);
				this._item_sprites.Add(tResult);
			}
			int tIndex = this.getActualSpriteIndexItem();
			this._item_image.sprite = this._item_sprites[tIndex];
			return;
		}
		Sprite tSprite2 = ItemRendering.getItemMainSpriteFrame(tItemRenderer);
		if (tSprite2 == null)
		{
			return;
		}
		this._item_image.sprite = this.getColoredItemSprite(tSprite2, tItemRenderer);
	}

	// Token: 0x060037DD RID: 14301 RVA: 0x00192324 File Offset: 0x00190524
	private void clear()
	{
		this._died = false;
		this._unit_sprites.Clear();
		this._item_sprites.Clear();
		this._item_positions.Clear();
		this._item_show_frames.Clear();
		this._effects.Clear();
		this._effects_pool_above.clear(true);
		this._effects_pool_below.clear(true);
		this._effects_pool_attached_above.clear(true);
		this._effects_pool_attached_below.clear(true);
	}

	// Token: 0x060037DE RID: 14302 RVA: 0x001923A0 File Offset: 0x001905A0
	private void Update()
	{
		if (this._died)
		{
			return;
		}
		this.updateEffects();
		this.updateItem();
		if (!this._animated)
		{
			return;
		}
		int tFrameIndex = this.getActualSpriteIndex();
		if (this._last_frame_index == tFrameIndex)
		{
			return;
		}
		this._last_frame_index = tFrameIndex;
		this._actor_image.sprite = this._unit_sprites[this._last_frame_index];
		this.syncItemWithUnit();
	}

	// Token: 0x060037DF RID: 14303 RVA: 0x00192408 File Offset: 0x00190608
	private void updateEffects()
	{
		foreach (AvatarEffect avatarEffect in this._effects)
		{
			avatarEffect.update(Time.deltaTime);
		}
	}

	// Token: 0x060037E0 RID: 14304 RVA: 0x00192460 File Offset: 0x00190660
	private void updateItem()
	{
		if (!this._show_item || !this._data.item_renderer.is_animated)
		{
			return;
		}
		int tFrameIndex = this.getActualSpriteIndexItem();
		if (this._last_frame_index_item == tFrameIndex)
		{
			return;
		}
		this._last_frame_index_item = tFrameIndex;
		this._item_image.sprite = this._item_sprites[this._last_frame_index_item];
	}

	// Token: 0x060037E1 RID: 14305 RVA: 0x001924BC File Offset: 0x001906BC
	private void checkRotationAndPivot()
	{
		this.checkRotation();
		this.checkPivot();
	}

	// Token: 0x060037E2 RID: 14306 RVA: 0x001924CC File Offset: 0x001906CC
	private float getRotation()
	{
		float tResult;
		if (this._data.is_lying && (!this._data.is_touching_liquid || !this._data.is_unconscious))
		{
			tResult = 90f;
		}
		else
		{
			tResult = 0f;
		}
		return tResult;
	}

	// Token: 0x060037E3 RID: 14307 RVA: 0x00192510 File Offset: 0x00190710
	private void checkRotation()
	{
		Quaternion tRotation = Quaternion.Euler(0f, 0f, this.getRotation());
		this._actor_and_item_container.rotation = tRotation;
	}

	// Token: 0x060037E4 RID: 14308 RVA: 0x00192540 File Offset: 0x00190740
	private void checkPivot()
	{
		Vector2 tPivot;
		if (this._data.is_lying && (!this._data.is_touching_liquid || !this._data.is_unconscious))
		{
			tPivot = new Vector2(0.75f, 0.25f);
		}
		else
		{
			tPivot = new Vector2(0.5f, 0.5f);
		}
		this._actor_and_item_container.pivot = tPivot;
	}

	// Token: 0x060037E5 RID: 14309 RVA: 0x001925A4 File Offset: 0x001907A4
	private void syncItemWithUnit()
	{
		if (!this._show_item)
		{
			return;
		}
		bool tShowItemOnFrame = this._item_show_frames[this._last_frame_index];
		this._item_image.enabled = tShowItemOnFrame;
		if (!tShowItemOnFrame)
		{
			return;
		}
		Vector3 tItemPosition = this._item_positions[this._last_frame_index];
		this.setImageParams(this._item_image, tItemPosition, 1f);
	}

	// Token: 0x060037E6 RID: 14310 RVA: 0x00192600 File Offset: 0x00190800
	private void showDied()
	{
		this._show_item = false;
		this._animated = false;
		this._is_swimming = false;
		this._actor_image.sprite = this._died_sprite;
		this.setImageParams(this._actor_image, Vector2.zero, 1f);
		this._item_image.enabled = false;
	}

	// Token: 0x060037E7 RID: 14311 RVA: 0x0019265C File Offset: 0x0019085C
	private void showStatic()
	{
		Vector3 tPosition = this.getAvatarPosition();
		Sprite tMainSprite;
		Sprite tColoredSprite;
		if (this._data.asset.has_override_sprite)
		{
			tMainSprite = null;
			tColoredSprite = this._data.asset.get_override_sprite(this._actor);
		}
		else
		{
			if (this._data.is_touching_liquid && this._animation_container.has_swimming && !this._data.is_inside_boat)
			{
				this._is_swimming = true;
				tMainSprite = this._animation_container.swimming.frames[0];
			}
			else
			{
				this._is_swimming = false;
				tMainSprite = this._animation_container.walking.frames[0];
			}
			tColoredSprite = this._data.getColoredSprite(tMainSprite, this._animation_container);
		}
		this._actor_image.sprite = tColoredSprite;
		this.setImageParams(this._actor_image, tPosition, 1f);
		if (this._show_item)
		{
			AnimationFrameData tFrameData = this._animation_container.dict_frame_data[tMainSprite.name];
			if (!tFrameData.show_item)
			{
				this._item_image.enabled = false;
				return;
			}
			this._item_image.enabled = true;
			tPosition = this.getAvatarPosition();
			tPosition.x += tFrameData.pos_item.x;
			tPosition.y += tFrameData.pos_item.y;
			this.setImageParams(this._item_image, tPosition, 1f);
		}
		else
		{
			this._item_image.enabled = false;
		}
		string tObjectNameSuffix;
		if (this._actor == null)
		{
			tObjectNameSuffix = string.Format("syntetic_{0}_{1}", this._data.asset.id, ++UnitAvatarLoader._syntetic_index);
		}
		else
		{
			tObjectNameSuffix = this._actor.data.id.ToString();
		}
		base.gameObject.name = "UnitAvatar_" + tObjectNameSuffix;
	}

	// Token: 0x060037E8 RID: 14312 RVA: 0x00192834 File Offset: 0x00190A34
	private void showAnimation()
	{
		this._item_image.enabled = this._show_item;
		ActorAsset tAsset = this._data.asset;
		Vector2 tAnchor = new Vector2(0.5f, 0f);
		if (this._data.is_hovering && !this._data.is_lying && !this._data.is_immovable)
		{
			tAnchor.y = 0.25f;
		}
		this._actor_image.rectTransform.anchorMax = tAnchor;
		this._actor_image.rectTransform.anchorMin = tAnchor;
		this._item_image.rectTransform.anchorMax = tAnchor;
		this._item_image.rectTransform.anchorMin = tAnchor;
		Vector2 tPosition;
		if (tAsset.has_override_avatar_frames)
		{
			tPosition = this.getAvatarPosition();
			Sprite[] tSprites = tAsset.get_override_avatar_frames(this._actor);
			this._unit_sprites.AddRange(tSprites);
			this._animation_speed = 8f;
		}
		else
		{
			tPosition = Vector2.zero;
			ActorAnimation tAnimation;
			if (tAsset.is_boat)
			{
				tAnimation = DynamicActorSpriteCreatorUI.getBoatAnimation(ActorAnimationLoader.loadAnimationBoat(tAsset.id));
				this._animation_speed = 8f;
			}
			else if (this._data.is_touching_liquid && this._animation_container.has_swimming && !this._data.is_inside_boat)
			{
				this._is_swimming = true;
				tAnimation = this._animation_container.swimming;
				this._animation_speed = tAsset.animation_swim_speed;
			}
			else
			{
				this._is_swimming = false;
				tAnimation = this._animation_container.walking;
				this._animation_speed = tAsset.animation_walk_speed;
			}
			foreach (Sprite tSprite in tAnimation.frames)
			{
				Sprite tNewSprite = this._data.getColoredSprite(tSprite, this._animation_container);
				this._unit_sprites.Add(tNewSprite);
				if (this._show_item)
				{
					AnimationFrameData tFrameData = this._animation_container.dict_frame_data[tSprite.name];
					float tFrameDataPositionX = 0f;
					float tFrameDataPositionY = 0f;
					if (tFrameData != null)
					{
						if (!tFrameData.show_item)
						{
							this._item_show_frames.Add(false);
							this._item_positions.Add(Vector3.zero);
							goto IL_26D;
						}
						tFrameDataPositionX = tFrameData.pos_item.x;
						tFrameDataPositionY = tFrameData.pos_item.y;
					}
					float tX = tAsset.inspect_avatar_offset_x + tFrameDataPositionX;
					float tY = tAsset.inspect_avatar_offset_y + tFrameDataPositionY;
					Vector3 tItemPosition = new Vector3(tX, tY, -0.01f);
					this._item_positions.Add(tItemPosition);
					this._item_show_frames.Add(true);
				}
				IL_26D:;
			}
		}
		if (!this._same_actor_reloaded || !this._same_skin_mutation_reloaded || this._last_frame_index >= this._unit_sprites.Count)
		{
			this._last_frame_index = 0;
		}
		this._actor_image.sprite = this._unit_sprites[this._last_frame_index];
		this.setImageParams(this._actor_image, tPosition, 1f);
		if (this._show_item)
		{
			this.setImageParams(this._item_image, this._item_positions[this._last_frame_index], 1f);
		}
	}

	// Token: 0x060037E9 RID: 14313 RVA: 0x00192B48 File Offset: 0x00190D48
	private void showStatusEffects()
	{
		if (this._data.statuses == null)
		{
			return;
		}
		foreach (string tStatusId in this._data.statuses)
		{
			if (this._data.statuses_gameplay == null || !this._data.statuses_gameplay[tStatusId].is_finished)
			{
				StatusAsset tStatusAsset = AssetManager.status.get(tStatusId);
				if (tStatusAsset.need_visual_render && tStatusAsset.render_check(this._data.asset) && (tStatusAsset.has_override_sprite || tStatusAsset.texture != null))
				{
					AvatarEffect tEffect = this.getEffectsPool(tStatusAsset).getNext();
					this._effects.Add(tEffect);
					Image tImage = tEffect.image;
					RectTransform rectTransform = tImage.rectTransform;
					Vector2 tAnchor = new Vector2(0.5f, 0f);
					if (this._data.is_hovering && !this._data.is_lying && !this._data.is_immovable)
					{
						tAnchor.y = 0.25f;
					}
					rectTransform.anchorMax = tAnchor;
					rectTransform.anchorMin = tAnchor;
					tEffect.load(tStatusAsset, this._actor, this);
					Rect tSpriteRect = tImage.sprite.rect;
					this.setImageParams(tImage, new Vector3
					{
						x = tStatusAsset.offset_x_ui * (tSpriteRect.width * tStatusAsset.scale),
						y = tStatusAsset.offset_y_ui * (tSpriteRect.height * tStatusAsset.scale)
					}, tStatusAsset.scale);
					tEffect.setInitialPosition(tImage.transform.localPosition);
				}
			}
		}
	}

	// Token: 0x060037EA RID: 14314 RVA: 0x00192D20 File Offset: 0x00190F20
	private void setImageParams(Image pImage, Vector3 pPosition, float pScale = 1f)
	{
		pImage.rectTransform.sizeDelta = new Vector2(pImage.sprite.rect.width * pScale, pImage.sprite.rect.height * pScale);
		float tPivX = pImage.sprite.pivot.x / pImage.sprite.rect.width;
		float tPivY = pImage.sprite.pivot.y / pImage.sprite.rect.height;
		pImage.rectTransform.pivot = new Vector2(tPivX, tPivY);
		pImage.rectTransform.anchoredPosition = pPosition;
	}

	// Token: 0x060037EB RID: 14315 RVA: 0x00192DD4 File Offset: 0x00190FD4
	private Sprite getColoredItemSprite(Sprite pSprite, IHandRenderer pIHandRenderer)
	{
		ColorAsset tColorAsset = this._data.kingdom_color;
		if (!pIHandRenderer.is_colored)
		{
			tColorAsset = null;
		}
		if (pIHandRenderer.is_colored && tColorAsset == null)
		{
			throw new InvalidOperationException("ItemRenderer is colored but no color asset found");
		}
		return DynamicSprites.getCachedAtlasItemSprite(DynamicSprites.getItemSpriteID(pSprite, tColorAsset), pSprite, tColorAsset);
	}

	// Token: 0x060037EC RID: 14316 RVA: 0x00192E1C File Offset: 0x0019101C
	public int getActualSpriteIndex()
	{
		int tIndex = 0;
		if (this._animated)
		{
			tIndex = AnimationHelper.getSpriteIndex((Time.time + (float)this.getAnimationHashCode()) * this._animation_speed, 0, this._unit_sprites.Count);
		}
		return tIndex;
	}

	// Token: 0x060037ED RID: 14317 RVA: 0x00192E5A File Offset: 0x0019105A
	private int getActualSpriteIndexItem()
	{
		return AnimationHelper.getSpriteIndex((Time.time + (float)this.getAnimationHashCode()) * 5f, 0, this._item_sprites.Count);
	}

	// Token: 0x060037EE RID: 14318 RVA: 0x00192E80 File Offset: 0x00191080
	private int getAnimationHashCode()
	{
		return this._data.actor_hash;
	}

	// Token: 0x060037EF RID: 14319 RVA: 0x00192E8D File Offset: 0x0019108D
	private Vector3 getAvatarPosition()
	{
		return new Vector3(this._data.asset.inspect_avatar_offset_x, this._data.asset.inspect_avatar_offset_y);
	}

	// Token: 0x060037F0 RID: 14320 RVA: 0x00192EB4 File Offset: 0x001910B4
	private ObjectPoolGenericMono<AvatarEffect> getEffectsPool(StatusAsset pAsset)
	{
		if (pAsset.use_parent_rotation)
		{
			if (pAsset.position_z >= 0f)
			{
				return this._effects_pool_attached_above;
			}
			return this._effects_pool_attached_below;
		}
		else
		{
			if (pAsset.position_z >= 0f)
			{
				return this._effects_pool_above;
			}
			return this._effects_pool_below;
		}
	}

	// Token: 0x060037F1 RID: 14321 RVA: 0x00192EF4 File Offset: 0x001910F4
	public bool actorStateChanged()
	{
		if (this._died)
		{
			return false;
		}
		bool flag = (!this._is_swimming && this._actor.isTouchingLiquid()) || (this._is_swimming && !this._actor.isTouchingLiquid());
		bool tItemStateChanged = this._data.item_renderer != this._actor.getHandRendererAsset();
		return flag || tItemStateChanged;
	}

	// Token: 0x060037F2 RID: 14322 RVA: 0x00192F5A File Offset: 0x0019115A
	public ActorAvatarData getData()
	{
		return this._data;
	}

	// Token: 0x060037F3 RID: 14323 RVA: 0x00192F62 File Offset: 0x00191162
	public AnimationContainerUnit getAnimationContainer()
	{
		return this._animation_container;
	}

	// Token: 0x04002962 RID: 10594
	private const float DEFAULT_ANIMATION_SPEED = 8f;

	// Token: 0x04002963 RID: 10595
	private const float ANIMATION_SPEED_ITEM = 5f;

	// Token: 0x04002964 RID: 10596
	private const float FLOATING_UNITS_ANCHOR = 0.25f;

	// Token: 0x04002965 RID: 10597
	private const float DEFAULT_AVATAR_SCALE = 2.5f;

	// Token: 0x04002966 RID: 10598
	private const float DIED_AVATAR_SCALE = 1f;

	// Token: 0x04002967 RID: 10599
	private static int _syntetic_index;

	// Token: 0x04002968 RID: 10600
	public float avatarSize = 1f;

	// Token: 0x04002969 RID: 10601
	[SerializeField]
	private Transform _frame;

	// Token: 0x0400296A RID: 10602
	[SerializeField]
	private RectTransform _actor_and_item_container;

	// Token: 0x0400296B RID: 10603
	[SerializeField]
	private Image _actor_image;

	// Token: 0x0400296C RID: 10604
	[SerializeField]
	private Image _item_image;

	// Token: 0x0400296D RID: 10605
	[SerializeField]
	private Sprite _died_sprite;

	// Token: 0x0400296E RID: 10606
	private Actor _actor;

	// Token: 0x0400296F RID: 10607
	private ActorAvatarData _data;

	// Token: 0x04002970 RID: 10608
	private AnimationContainerUnit _animation_container;

	// Token: 0x04002971 RID: 10609
	private readonly List<Sprite> _unit_sprites = new List<Sprite>();

	// Token: 0x04002972 RID: 10610
	private readonly List<Sprite> _item_sprites = new List<Sprite>();

	// Token: 0x04002973 RID: 10611
	private readonly List<Vector3> _item_positions = new List<Vector3>();

	// Token: 0x04002974 RID: 10612
	private readonly List<bool> _item_show_frames = new List<bool>();

	// Token: 0x04002975 RID: 10613
	private readonly List<AvatarEffect> _effects = new List<AvatarEffect>();

	// Token: 0x04002976 RID: 10614
	[SerializeField]
	private AvatarEffect _effect_prefab;

	// Token: 0x04002977 RID: 10615
	[SerializeField]
	private Transform _effects_parent_attached_below;

	// Token: 0x04002978 RID: 10616
	[SerializeField]
	private Transform _effects_parent_attached_above;

	// Token: 0x04002979 RID: 10617
	[SerializeField]
	private Transform _effects_parent_below;

	// Token: 0x0400297A RID: 10618
	[SerializeField]
	private Transform _effects_parent_above;

	// Token: 0x0400297B RID: 10619
	private ObjectPoolGenericMono<AvatarEffect> _effects_pool_attached_below;

	// Token: 0x0400297C RID: 10620
	private ObjectPoolGenericMono<AvatarEffect> _effects_pool_attached_above;

	// Token: 0x0400297D RID: 10621
	private ObjectPoolGenericMono<AvatarEffect> _effects_pool_below;

	// Token: 0x0400297E RID: 10622
	private ObjectPoolGenericMono<AvatarEffect> _effects_pool_above;

	// Token: 0x0400297F RID: 10623
	private bool _show_item;

	// Token: 0x04002980 RID: 10624
	private bool _animated;

	// Token: 0x04002981 RID: 10625
	private float _animation_speed = 8f;

	// Token: 0x04002982 RID: 10626
	private int _last_frame_index;

	// Token: 0x04002983 RID: 10627
	private int _last_frame_index_item;

	// Token: 0x04002984 RID: 10628
	private bool _same_actor_reloaded;

	// Token: 0x04002985 RID: 10629
	private bool _same_skin_mutation_reloaded;

	// Token: 0x04002986 RID: 10630
	private bool _is_swimming;

	// Token: 0x04002987 RID: 10631
	private bool _died;
}
