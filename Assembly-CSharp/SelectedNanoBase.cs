using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200075B RID: 1883
public class SelectedNanoBase : MonoBehaviour
{
	// Token: 0x06003BAA RID: 15274 RVA: 0x001A1244 File Offset: 0x0019F444
	protected virtual void Awake()
	{
		this._powers_tab = base.GetComponent<PowersTab>();
		if (this.stats_icons == null || this.stats_icons.Length == 0)
		{
			Debug.LogError("SelectedNano: No StatsIconContainer found in children of " + base.gameObject.name);
		}
	}

	// Token: 0x06003BAB RID: 15275 RVA: 0x001A127D File Offset: 0x0019F47D
	public virtual void update()
	{
	}

	// Token: 0x06003BAC RID: 15276 RVA: 0x001A127F File Offset: 0x0019F47F
	protected PowerTabAsset getPowerTabAsset()
	{
		return AssetManager.power_tab_library.get(this.getPowerTabAssetID());
	}

	// Token: 0x06003BAD RID: 15277 RVA: 0x001A1291 File Offset: 0x0019F491
	protected virtual string getPowerTabAssetID()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003BAE RID: 15278 RVA: 0x001A1298 File Offset: 0x0019F498
	public void clickFavoriteMeta()
	{
		ICoreObject tCoreObject = this.getPowerTabAsset().meta_type.getAsset().get_selected() as ICoreObject;
		tCoreObject.switchFavorite();
		if (tCoreObject.isFavorite())
		{
			string tTitle = LocalizedTextManager.getText("favorited", null, false);
			WorldTip.instance.showToolbarText(tTitle);
		}
		this.updateFavoriteIcon(tCoreObject.isFavorite());
	}

	// Token: 0x06003BAF RID: 15279 RVA: 0x001A12F8 File Offset: 0x0019F4F8
	public void clickFavoriteUnit()
	{
		Actor tActor = (Actor)this.getPowerTabAsset().meta_type.getAsset().get_selected();
		tActor.switchFavorite();
		if (tActor.isFavorite())
		{
			string tTitle = LocalizedTextManager.getText("tip_favorite_icon", null, false);
			WorldTip.instance.showToolbarText(tTitle);
		}
		this.updateFavoriteIcon(tActor.isFavorite());
	}

	// Token: 0x06003BB0 RID: 15280 RVA: 0x001A1357 File Offset: 0x0019F557
	protected void updateFavoriteIcon(bool pStatus)
	{
		if (this._favorite_icon == null)
		{
			return;
		}
		if (pStatus)
		{
			this._favorite_icon.color = ColorStyleLibrary.m.favorite_selected;
			return;
		}
		this._favorite_icon.color = ColorStyleLibrary.m.favorite_not_selected;
	}

	// Token: 0x06003BB1 RID: 15281 RVA: 0x001A1398 File Offset: 0x0019F598
	protected void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		StatsIconContainer[] array = this.stats_icons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
		}
	}

	// Token: 0x04002BB6 RID: 11190
	public StatsIconContainer[] stats_icons;

	// Token: 0x04002BB7 RID: 11191
	protected PowersTab _powers_tab;

	// Token: 0x04002BB8 RID: 11192
	[SerializeField]
	private Image _favorite_icon;

	// Token: 0x04002BB9 RID: 11193
	[SerializeField]
	protected Image icon_left;

	// Token: 0x04002BBA RID: 11194
	[SerializeField]
	protected Image icon_right;

	// Token: 0x04002BBB RID: 11195
	[SerializeField]
	protected Text name_field;
}
