using System;

// Token: 0x02000224 RID: 548
public class MetaObjectCounter<TObject, TData> where TObject : MetaObject<TData> where TData : MetaObjectData
{
	// Token: 0x0600143D RID: 5181 RVA: 0x000DAE64 File Offset: 0x000D9064
	public MetaObjectCounter(TObject pMetaObject)
	{
		this.meta_object = pMetaObject;
	}

	// Token: 0x0400119E RID: 4510
	public TObject meta_object;

	// Token: 0x0400119F RID: 4511
	public int amount;
}
