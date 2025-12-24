using System;

// Token: 0x0200055A RID: 1370
public class AvatarCombineHandItem
{
	// Token: 0x06002C9A RID: 11418 RVA: 0x0015DFD2 File Offset: 0x0015C1D2
	public AvatarCombineHandItem(IHandRenderer pHandRenderer)
	{
		this.hand_renderer = pHandRenderer;
	}

	// Token: 0x0400222C RID: 8748
	public readonly IHandRenderer hand_renderer;
}
