using System;

// Token: 0x0200073C RID: 1852
public class ReligionElement : WindowMetaElement<Religion, ReligionData>
{
	// Token: 0x17000352 RID: 850
	// (get) Token: 0x06003ADD RID: 15069 RVA: 0x0019F72D File Offset: 0x0019D92D
	protected Religion religion
	{
		get
		{
			return this.meta_object;
		}
	}
}
