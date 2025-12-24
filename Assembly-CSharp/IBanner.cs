using System;

// Token: 0x020000AB RID: 171
public interface IBanner : IBaseMono, IRefreshElement
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600058F RID: 1423
	MetaCustomizationAsset meta_asset { get; }

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000590 RID: 1424
	MetaTypeAsset meta_type_asset { get; }

	// Token: 0x06000591 RID: 1425
	NanoObject GetNanoObject();

	// Token: 0x06000592 RID: 1426
	void load(NanoObject pObject);

	// Token: 0x06000593 RID: 1427
	string getName();

	// Token: 0x06000594 RID: 1428
	void showTooltip();

	// Token: 0x06000595 RID: 1429 RVA: 0x000538F0 File Offset: 0x00051AF0
	void jump(float pSpeed = 0.1f, bool pSilent = false)
	{
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x000538F4 File Offset: 0x00051AF4
	void refresh()
	{
		NanoObject tNano = this.GetNanoObject();
		if (tNano == null)
		{
			return;
		}
		if (!tNano.isAlive())
		{
			return;
		}
		this.load(tNano);
	}
}
