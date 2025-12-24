using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200064B RID: 1611
public class BrushSelectButton : MonoBehaviour
{
	// Token: 0x06003467 RID: 13415 RVA: 0x00185B4A File Offset: 0x00183D4A
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(delegate()
		{
			Config.current_brush = this._brush_asset.id;
			ScrollWindow.hideAllEvent(true);
		});
	}

	// Token: 0x06003468 RID: 13416 RVA: 0x00185B68 File Offset: 0x00183D68
	public void setup(BrushData pBrushData)
	{
		this._brush_asset = pBrushData;
		base.gameObject.name = this._brush_asset.id;
		this._brush_asset.setupImage(this.icon);
		base.GetComponent<TipButton>().textOnClick = this._brush_asset.getLocaleID();
	}

	// Token: 0x04002782 RID: 10114
	public Image icon;

	// Token: 0x04002783 RID: 10115
	private BrushData _brush_asset;
}
