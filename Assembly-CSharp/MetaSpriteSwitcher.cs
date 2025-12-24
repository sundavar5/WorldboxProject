using System;

// Token: 0x0200084E RID: 2126
public class MetaSpriteSwitcher : SpriteSwitcher
{
	// Token: 0x06004282 RID: 17026 RVA: 0x001C2D98 File Offset: 0x001C0F98
	protected override bool hasAny()
	{
		return AssetManager.meta_type_library.getAsset(this.meta_type).has_any();
	}

	// Token: 0x040030B8 RID: 12472
	public MetaType meta_type;
}
