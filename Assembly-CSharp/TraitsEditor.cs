using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000797 RID: 1943
public abstract class TraitsEditor<TTrait, TTraitButton, TTraitEditorButton, TTraitGroupAsset, TTraitGroup> : AugmentationsEditor<TTrait, TTraitButton, TTraitEditorButton, TTraitGroupAsset, TTraitGroup, ITraitWindow<TTrait, TTraitButton>, ITraitsEditor<TTrait>>, ITraitsEditor<TTrait>, IAugmentationsEditor where TTrait : BaseTrait<TTrait> where TTraitButton : TraitButton<TTrait> where TTraitEditorButton : TraitEditorButton<TTraitButton, TTrait> where TTraitGroupAsset : BaseTraitGroupAsset where TTraitGroup : TraitGroupElement<TTrait, TTraitButton, TTraitEditorButton>
{
	// Token: 0x1700039E RID: 926
	// (get) Token: 0x06003D96 RID: 15766 RVA: 0x001AE7D7 File Offset: 0x001AC9D7
	protected virtual MetaType meta_type
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x06003D97 RID: 15767 RVA: 0x001AE7DE File Offset: 0x001AC9DE
	protected MetaTypeAsset meta_type_asset
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(this.meta_type);
		}
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x06003D98 RID: 15768 RVA: 0x001AE7F0 File Offset: 0x001AC9F0
	protected virtual List<string> filter_traits
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06003D99 RID: 15769 RVA: 0x001AE7F3 File Offset: 0x001AC9F3
	protected virtual List<string> filter_trait_groups
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06003D9A RID: 15770 RVA: 0x001AE7F8 File Offset: 0x001AC9F8
	protected override void OnEnable()
	{
		if (!this.rain_editor && this.filter_traits != null)
		{
			foreach (TTraitEditorButton ttraitEditorButton in this.all_augmentation_buttons)
			{
				TTrait tAsset = ttraitEditorButton.augmentation_button.getElementAsset();
				bool tShowButton = !this.filter_traits.Contains(tAsset.id);
				ttraitEditorButton.gameObject.SetActive(tShowButton);
			}
		}
		base.OnEnable();
	}

	// Token: 0x06003D9B RID: 15771 RVA: 0x001AE89C File Offset: 0x001ACA9C
	protected override void metaAugmentationClick(TTraitEditorButton pButton)
	{
		TTraitButton tButton = pButton.augmentation_button;
		TTrait tTrait = pButton.augmentation_button.getElementAsset();
		bool tHaveTrait = this.hasAugmentation(tButton);
		bool tAvailable = tTrait.isAvailable();
		bool tWithAchievement = tTrait.unlocked_with_achievement;
		if (!tAvailable)
		{
			if (!tWithAchievement)
			{
				return;
			}
			if (tWithAchievement && !tHaveTrait)
			{
				return;
			}
		}
		if (tTrait.can_be_removed)
		{
			if (tHaveTrait)
			{
				this.removeAugmentation(tButton);
			}
			else
			{
				if (tTrait.hasOppositeTraits<TTrait>())
				{
					foreach (TTrait tOppositeTrait in tTrait.opposite_traits)
					{
						this.removeTrait(tOppositeTrait);
					}
				}
				this.addAugmentation(tButton);
			}
			this.onNanoWasModified();
		}
		base.metaAugmentationClick(pButton);
	}

	// Token: 0x06003D9C RID: 15772 RVA: 0x001AE984 File Offset: 0x001ACB84
	protected override void rainAugmentationClick(TTraitEditorButton pButton)
	{
		if (!base.isAugmentationAvailable(pButton.augmentation_button))
		{
			return;
		}
		TTrait tTrait = pButton.augmentation_button.getElementAsset();
		if (this.augmentations_hashset.Contains(tTrait.id))
		{
			this.augmentations_hashset.Remove(tTrait.id);
		}
		else
		{
			if (tTrait.hasOppositeTraits<TTrait>())
			{
				foreach (TTrait tOppositeTrait in tTrait.opposite_traits)
				{
					this.augmentations_hashset.Remove(tOppositeTrait.id);
				}
			}
			this.augmentations_hashset.Add(tTrait.id);
		}
		base.rainAugmentationClick(pButton);
	}

	// Token: 0x06003D9D RID: 15773 RVA: 0x001AEA70 File Offset: 0x001ACC70
	protected override void showActiveButtons()
	{
		this.augmentation_window.reloadTraits(false);
	}

	// Token: 0x06003D9E RID: 15774 RVA: 0x001AEA80 File Offset: 0x001ACC80
	protected override void createButton(TTrait pElement, TTraitGroup pGroup)
	{
		TTraitEditorButton tEditorButton = Object.Instantiate<TTraitEditorButton>(this.prefab_editor_augmentation, pGroup.augmentation_buttons_transform);
		tEditorButton.augmentation_button.load(pElement.id);
		tEditorButton.augmentation_button.is_editor_button = true;
		this.all_augmentation_buttons.Add(tEditorButton);
		pGroup.augmentation_buttons.Add(tEditorButton);
		tEditorButton.augmentation_button.button.onClick.AddListener(delegate()
		{
			this.editorButtonClick(tEditorButton);
		});
	}

	// Token: 0x06003D9F RID: 15775 RVA: 0x001AEB4D File Offset: 0x001ACD4D
	protected override void startSignal()
	{
		AchievementLibrary.traits_explorer_40.checkBySignal(null);
		AchievementLibrary.traits_explorer_60.checkBySignal(null);
		AchievementLibrary.traits_explorer_90.checkBySignal(null);
	}

	// Token: 0x06003DA0 RID: 15776 RVA: 0x001AEB70 File Offset: 0x001ACD70
	protected override void onNanoWasModified()
	{
		if (this.edited_marker_augmentation == null)
		{
			return;
		}
		this.addTrait(this.edited_marker_augmentation);
	}

	// Token: 0x06003DA1 RID: 15777 RVA: 0x001AEB90 File Offset: 0x001ACD90
	protected override void checkEnabledGroups()
	{
		foreach (KeyValuePair<string, TTraitGroup> tGroup in this.dict_groups)
		{
			string tAssetId = tGroup.Key;
			TTraitGroup tElement = tGroup.Value;
			if (this.filter_trait_groups != null && this.filter_trait_groups.Contains(tAssetId))
			{
				tElement.gameObject.SetActive(false);
			}
			else
			{
				bool tGroupState = tElement.countActiveButtons() > 0;
				tElement.gameObject.SetActive(tGroupState);
			}
		}
	}

	// Token: 0x06003DA2 RID: 15778 RVA: 0x001AEC38 File Offset: 0x001ACE38
	protected override bool hasAugmentation(TTraitButton pButton)
	{
		return this.hasTrait(pButton.getElementAsset());
	}

	// Token: 0x06003DA3 RID: 15779 RVA: 0x001AEC4B File Offset: 0x001ACE4B
	protected override bool addAugmentation(TTraitButton pButton)
	{
		return this.addTrait(pButton.getElementAsset());
	}

	// Token: 0x06003DA4 RID: 15780 RVA: 0x001AEC5E File Offset: 0x001ACE5E
	protected override bool removeAugmentation(TTraitButton pButton)
	{
		return this.removeTrait(pButton.getElementAsset());
	}

	// Token: 0x06003DA5 RID: 15781 RVA: 0x001AEC71 File Offset: 0x001ACE71
	public virtual ITraitsOwner<TTrait> getTraitsOwner()
	{
		return (ITraitsOwner<TTrait>)this.meta_type_asset.get_selected();
	}

	// Token: 0x06003DA6 RID: 15782 RVA: 0x001AEC88 File Offset: 0x001ACE88
	public ActorAsset getActorAsset()
	{
		return this.getTraitsOwner().getActorAsset();
	}

	// Token: 0x06003DA7 RID: 15783 RVA: 0x001AEC95 File Offset: 0x001ACE95
	protected virtual bool hasTrait(TTrait pTrait)
	{
		return this.getTraitsOwner().hasTrait(pTrait);
	}

	// Token: 0x06003DA8 RID: 15784 RVA: 0x001AECA3 File Offset: 0x001ACEA3
	protected virtual bool addTrait(TTrait pTrait)
	{
		this.getTraitsOwner().traitModifiedEvent();
		return this.getTraitsOwner().addTrait(pTrait, false);
	}

	// Token: 0x06003DA9 RID: 15785 RVA: 0x001AECBD File Offset: 0x001ACEBD
	protected virtual bool removeTrait(TTrait pTrait)
	{
		return this.getTraitsOwner().removeTrait(pTrait);
	}
}
