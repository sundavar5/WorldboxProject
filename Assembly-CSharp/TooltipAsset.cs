using System;

// Token: 0x0200077C RID: 1916
[Serializable]
public class TooltipAsset : Asset
{
	// Token: 0x04002C26 RID: 11302
	public string prefab_id = "tooltips/tooltip_normal";

	// Token: 0x04002C27 RID: 11303
	public string sound;

	// Token: 0x04002C28 RID: 11304
	public string color;

	// Token: 0x04002C29 RID: 11305
	public TooltipShowAction callback;

	// Token: 0x04002C2A RID: 11306
	public TooltipShowAction callback_text_animated;
}
