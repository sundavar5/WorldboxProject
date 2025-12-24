using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200054E RID: 1358
public class BaseDebugAssetElement<TAsset> : MonoBehaviour where TAsset : Asset
{
	// Token: 0x06002C45 RID: 11333 RVA: 0x0015C524 File Offset: 0x0015A724
	private void Awake()
	{
		this.rect_transform = base.GetComponent<RectTransform>();
		this.asset_button.onClick.AddListener(new UnityAction(this.showAssetWindow));
		this.asset_button.OnHover(delegate()
		{
			BaseDebugAssetElement<TAsset>.selected_asset = this.asset;
		});
	}

	// Token: 0x06002C46 RID: 11334 RVA: 0x0015C571 File Offset: 0x0015A771
	public virtual void setData(TAsset pAsset)
	{
		this.asset = pAsset;
		this.title.text = this.asset.id;
		this.initAnimations();
		this.initStats();
	}

	// Token: 0x06002C47 RID: 11335 RVA: 0x0015C5A1 File Offset: 0x0015A7A1
	protected virtual void initAnimations()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002C48 RID: 11336 RVA: 0x0015C5A8 File Offset: 0x0015A7A8
	public virtual void update()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002C49 RID: 11337 RVA: 0x0015C5AF File Offset: 0x0015A7AF
	public virtual void stopAnimations()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002C4A RID: 11338 RVA: 0x0015C5B6 File Offset: 0x0015A7B6
	public virtual void startAnimations()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002C4B RID: 11339 RVA: 0x0015C5BD File Offset: 0x0015A7BD
	protected virtual void initStats()
	{
		this.stats_description.text = "";
		this.stats_values.text = "";
	}

	// Token: 0x06002C4C RID: 11340 RVA: 0x0015C5E0 File Offset: 0x0015A7E0
	protected void showStat(string pID, object pValue)
	{
		Text text = this.stats_description;
		text.text = text.text + LocalizedTextManager.getText(pID, null, false) + "\n";
		Text text2 = this.stats_values;
		text2.text = text2.text + ((pValue != null) ? pValue.ToString() : null) + "\n";
	}

	// Token: 0x06002C4D RID: 11341 RVA: 0x0015C637 File Offset: 0x0015A837
	protected virtual void showAssetWindow()
	{
		BaseDebugAssetElement<TAsset>.selected_asset = this.asset;
	}

	// Token: 0x040021F4 RID: 8692
	public static TAsset selected_asset;

	// Token: 0x040021F5 RID: 8693
	internal TAsset asset;

	// Token: 0x040021F6 RID: 8694
	public Sprite no_animation;

	// Token: 0x040021F7 RID: 8695
	public Button asset_button;

	// Token: 0x040021F8 RID: 8696
	public Text title;

	// Token: 0x040021F9 RID: 8697
	public Text stats_description;

	// Token: 0x040021FA RID: 8698
	public Text stats_values;

	// Token: 0x040021FB RID: 8699
	internal RectTransform rect_transform;
}
