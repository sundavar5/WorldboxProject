using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005FB RID: 1531
public class SelectedPowerTabTopButtons : MonoBehaviour
{
	// Token: 0x06003251 RID: 12881 RVA: 0x0017E594 File Offset: 0x0017C794
	private void Update()
	{
		if (SelectedUnit.isSet() || SelectedObjects.isNanoObjectSet())
		{
			bool tActive = Screen.width < Screen.height;
			this._button_open_window.SetActive(tActive);
			bool tHasHistory = SelectedTabsHistory.count() > 1;
			this._button_tab_back.setVisible(tHasHistory, false, null);
			if (tHasHistory)
			{
				this.updateTabBackButton();
				return;
			}
		}
		else
		{
			this._button_open_window.SetActive(false);
			this._button_tab_back.setVisible(false, false, null);
		}
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x0017E604 File Offset: 0x0017C804
	private void updateTabBackButton()
	{
		TabHistoryData? tData = SelectedTabsHistory.getPrevData();
		if (tData == null)
		{
			return;
		}
		MetaTypeAsset tMetaAsset = AssetManager.meta_type_library.getAsset(tData.Value.meta_type);
		NanoObject tObject = tData.Value.getNanoObject();
		Sprite tSprite;
		if (tObject.getMetaType() == MetaType.Unit)
		{
			tSprite = ((Actor)tObject).asset.getSpriteIcon();
		}
		else
		{
			tSprite = tMetaAsset.getIconSprite();
		}
		this._button_tab_back_icon.sprite = tSprite;
		this._button_tab_back_counter.text = (SelectedTabsHistory.count() - 1).ToString();
	}

	// Token: 0x06003253 RID: 12883 RVA: 0x0017E694 File Offset: 0x0017C894
	public void clickButtonFavorite()
	{
		if (!SelectedObjects.isNanoObjectSet())
		{
			return;
		}
		if (SelectedUnit.isSet())
		{
			this.clickFavoriteUnit();
			return;
		}
		this.clickFavoriteMeta();
	}

	// Token: 0x06003254 RID: 12884 RVA: 0x0017E6B2 File Offset: 0x0017C8B2
	public void clickLocate()
	{
	}

	// Token: 0x06003255 RID: 12885 RVA: 0x0017E6B4 File Offset: 0x0017C8B4
	public void clickOpenMain()
	{
		PowerTabAsset tAsset = this.getPowerTabAsset();
		tAsset.on_main_info_click(tAsset);
	}

	// Token: 0x06003256 RID: 12886 RVA: 0x0017E6D4 File Offset: 0x0017C8D4
	private void clickFavoriteMeta()
	{
		ICoreObject tCoreObject = this.getPowerTabAsset().meta_type.getAsset().get_selected() as ICoreObject;
		tCoreObject.switchFavorite();
		if (tCoreObject.isFavorite())
		{
			WorldTip.showNowTop("tip_favorite_icon", true);
		}
		this.updateFavoriteIcon(tCoreObject.isFavorite());
	}

	// Token: 0x06003257 RID: 12887 RVA: 0x0017E728 File Offset: 0x0017C928
	private void clickFavoriteUnit()
	{
		Actor tActor = (Actor)this.getPowerTabAsset().meta_type.getAsset().get_selected();
		tActor.switchFavorite();
		if (tActor.isFavorite())
		{
			WorldTip.showNowTop("tip_favorite_icon", true);
		}
		this.updateFavoriteIcon(tActor.isFavorite());
	}

	// Token: 0x06003258 RID: 12888 RVA: 0x0017E77A File Offset: 0x0017C97A
	private void updateFavoriteIcon(bool pStatus)
	{
		if (pStatus)
		{
			this._favorite_icon.color = ColorStyleLibrary.m.favorite_selected;
			return;
		}
		this._favorite_icon.color = ColorStyleLibrary.m.favorite_not_selected;
	}

	// Token: 0x06003259 RID: 12889 RVA: 0x0017E7AA File Offset: 0x0017C9AA
	private PowerTabAsset getPowerTabAsset()
	{
		return PowersTab.getActiveTab().getAsset();
	}

	// Token: 0x04002607 RID: 9735
	[SerializeField]
	private Image _favorite_icon;

	// Token: 0x04002608 RID: 9736
	[SerializeField]
	private GameObject _button_possession;

	// Token: 0x04002609 RID: 9737
	[SerializeField]
	private GameObject _button_spectate;

	// Token: 0x0400260A RID: 9738
	[SerializeField]
	private GameObject _button_open_window;

	// Token: 0x0400260B RID: 9739
	[SerializeField]
	private GameObject _button_favorite;

	// Token: 0x0400260C RID: 9740
	[SerializeField]
	private UiMover _button_tab_back;

	// Token: 0x0400260D RID: 9741
	[SerializeField]
	private Image _button_tab_back_icon;

	// Token: 0x0400260E RID: 9742
	[SerializeField]
	private Text _button_tab_back_counter;
}
