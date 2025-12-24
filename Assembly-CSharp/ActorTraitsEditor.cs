using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006C9 RID: 1737
public class ActorTraitsEditor : TraitsEditor<ActorTrait, ActorTraitButton, ActorTraitEditorButton, ActorTraitGroupAsset, ActorTraitGroupElement>
{
	// Token: 0x17000312 RID: 786
	// (get) Token: 0x0600379A RID: 14234 RVA: 0x0019135E File Offset: 0x0018F55E
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Unit;
		}
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x0600379B RID: 14235 RVA: 0x00191362 File Offset: 0x0018F562
	protected override List<ActorTraitGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.trait_groups.list;
		}
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x0600379C RID: 14236 RVA: 0x0019136E File Offset: 0x0018F56E
	protected override ActorTrait edited_marker_augmentation
	{
		get
		{
			return AssetManager.traits.get("scar_of_divinity");
		}
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x0600379D RID: 14237 RVA: 0x0019137F File Offset: 0x0018F57F
	protected override List<ActorTrait> all_augmentations_list
	{
		get
		{
			return AssetManager.traits.list;
		}
	}

	// Token: 0x0600379E RID: 14238 RVA: 0x0019138C File Offset: 0x0018F58C
	protected override void onEnableRain()
	{
		TraitRainAsset tAsset = AssetManager.trait_rains.get(Config.selected_trait_editor);
		this.augmentations_list_link = tAsset.get_list();
		this.rain_editor_state = tAsset.get_state();
		this.rain_state_toggle_action = delegate()
		{
			this.toggleRainState(tAsset);
		};
		this.art.sprite = ((this.rain_editor_state == RainState.Add) ? tAsset.getSpriteArt() : tAsset.getSpriteArtVoid());
		this.validateRainData();
		this.rain_state_switcher.toggleState(this.rain_editor_state == RainState.Remove);
		this.augmentations_hashset.Clear();
		this.augmentations_hashset.UnionWith(this.augmentations_list_link);
		base.saveRainValues();
		this.loadEditorSelectedAugmentations();
		GodPower tPower = AssetManager.powers.get(Config.selected_trait_editor);
		this.window_title_text.key = tPower.getLocaleID();
		this.window_title_text.updateText(true);
		this.power_icon.sprite = tPower.getIconSprite();
		for (int i = 0; i < this.powers_icons.childCount; i++)
		{
			this.powers_icons.transform.GetChild(i).GetComponent<Image>().sprite = tPower.getIconSprite();
		}
	}

	// Token: 0x0600379F RID: 14239 RVA: 0x001914DB File Offset: 0x0018F6DB
	protected override bool addTrait(ActorTrait pTrait)
	{
		return base.addTrait(pTrait);
	}

	// Token: 0x060037A0 RID: 14240 RVA: 0x001914E4 File Offset: 0x0018F6E4
	protected override void onNanoWasModified()
	{
		base.onNanoWasModified();
		Actor actor = (Actor)this.getTraitsOwner();
		actor.makeStunnedFromUI();
		actor.updateStats();
	}

	// Token: 0x060037A1 RID: 14241 RVA: 0x00191502 File Offset: 0x0018F702
	protected override void loadEditorSelectedButton(ActorTraitButton pButton, string pAugmentationId)
	{
		base.loadEditorSelectedButton(pButton, pAugmentationId);
		pButton.load(pAugmentationId);
	}

	// Token: 0x060037A2 RID: 14242 RVA: 0x00191513 File Offset: 0x0018F713
	protected override bool isAugmentationExists(string pId)
	{
		return AssetManager.traits.has(pId);
	}

	// Token: 0x060037A3 RID: 14243 RVA: 0x00191520 File Offset: 0x0018F720
	public override void scrollToGroupStarter(GameObject pButton, bool pIgnoreTooltipCheck)
	{
		if (!this.rain_editor && !base.getActorAsset().can_edit_traits)
		{
			return;
		}
		base.scrollToGroupStarter(pButton, pIgnoreTooltipCheck);
	}

	// Token: 0x060037A4 RID: 14244 RVA: 0x00191540 File Offset: 0x0018F740
	protected void toggleRainState(TraitRainAsset pAsset)
	{
		RainState tState;
		if (pAsset.get_state() == RainState.Add)
		{
			tState = RainState.Remove;
			this.rain_state_switcher.toggleState(true);
			this.art.sprite = pAsset.getSpriteArtVoid();
		}
		else
		{
			tState = RainState.Add;
			this.rain_state_switcher.toggleState(false);
			this.art.sprite = pAsset.getSpriteArt();
		}
		IllustrationFadeIn tFadeInEffect = this.art.GetComponent<IllustrationFadeIn>();
		if (tFadeInEffect != null)
		{
			tFadeInEffect.startTween();
		}
		pAsset.set_state(tState);
		this.rain_editor_state = tState;
		this.reloadButtons();
	}
}
