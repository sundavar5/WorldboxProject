using System;

// Token: 0x02000643 RID: 1603
public interface IAugmentationsWindow<TEditor> where TEditor : IAugmentationsEditor
{
	// Token: 0x06003441 RID: 13377
	void updateStats();

	// Token: 0x06003442 RID: 13378
	T GetComponentInChildren<T>(bool includeInactive = false);

	// Token: 0x06003443 RID: 13379
	void reloadBanner();

	// Token: 0x06003444 RID: 13380 RVA: 0x00185630 File Offset: 0x00183830
	TEditor getEditor()
	{
		return this.GetComponentInChildren<TEditor>(true);
	}
}
