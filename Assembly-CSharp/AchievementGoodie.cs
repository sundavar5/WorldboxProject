using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000014 RID: 20
public class AchievementGoodie : MonoBehaviour
{
	// Token: 0x060000F5 RID: 245 RVA: 0x000099B1 File Offset: 0x00007BB1
	public void load(BaseUnlockableAsset pAsset, bool pUnlocked)
	{
		if (pUnlocked)
		{
			this.loadUnlocked(pAsset);
			return;
		}
		this.loadLocked(pAsset);
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x000099C5 File Offset: 0x00007BC5
	private void loadLocked(BaseUnlockableAsset pAsset)
	{
		this._icon.sprite = pAsset.getSprite();
		this._icon.color = Toolbox.color_black;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x000099E8 File Offset: 0x00007BE8
	private void loadUnlocked(BaseUnlockableAsset pAssets)
	{
		this._icon.sprite = pAssets.getSprite();
		this._name.GetComponent<LocalizedText>().setKeyAndUpdate(pAssets.getLocaleID());
		ActorAsset tActorAsset = pAssets as ActorAsset;
		if (tActorAsset != null)
		{
			KingdomAsset tKingdomAsset = AssetManager.kingdoms.get(tActorAsset.kingdom_id_wild);
			this._name.color = tKingdomAsset.default_kingdom_color.getColorText();
			return;
		}
		BaseAugmentationAsset tAugmentation = pAssets as BaseAugmentationAsset;
		if (tAugmentation == null)
		{
			this._name.color = Toolbox.color_white;
			return;
		}
		BaseCategoryAsset tCategory = tAugmentation.getGroup();
		this._name.color = ((tCategory != null) ? tCategory.getColor() : Toolbox.color_white);
	}

	// Token: 0x040000B4 RID: 180
	[SerializeField]
	private Image _icon;

	// Token: 0x040000B5 RID: 181
	[SerializeField]
	private Text _name;
}
