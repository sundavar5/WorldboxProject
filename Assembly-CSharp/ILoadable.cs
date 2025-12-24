using System;

// Token: 0x020001D3 RID: 467
public interface ILoadable<TData>
{
	// Token: 0x06000DBB RID: 3515
	void setData(TData pData);

	// Token: 0x06000DBC RID: 3516
	void loadData(TData pData);
}
