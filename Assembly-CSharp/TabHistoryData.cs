using System;

// Token: 0x0200075E RID: 1886
public struct TabHistoryData
{
	// Token: 0x06003BBE RID: 15294 RVA: 0x001A15F4 File Offset: 0x0019F7F4
	public TabHistoryData(NanoObject pObject)
	{
		this.meta_type = pObject.getMetaType();
		this.id = pObject.id;
	}

	// Token: 0x06003BBF RID: 15295 RVA: 0x001A160E File Offset: 0x0019F80E
	public NanoObject getNanoObject()
	{
		return AssetManager.meta_type_library.getAsset(this.meta_type).get(this.id);
	}

	// Token: 0x04002BBF RID: 11199
	public readonly MetaType meta_type;

	// Token: 0x04002BC0 RID: 11200
	public readonly long id;
}
