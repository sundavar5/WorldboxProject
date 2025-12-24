using System;

// Token: 0x020000BF RID: 191
[Serializable]
public class BuildingFundament
{
	// Token: 0x06000601 RID: 1537 RVA: 0x00057310 File Offset: 0x00055510
	public BuildingFundament(int pLeft, int pRight, int pTop, int pBottom)
	{
		this.left = pLeft;
		this.right = pRight;
		this.top = pTop;
		this.bottom = pBottom;
		this.width = this.right + this.left + 1;
		this.height = this.top + this.bottom + 1;
	}

	// Token: 0x040006AB RID: 1707
	public readonly int left;

	// Token: 0x040006AC RID: 1708
	public readonly int right;

	// Token: 0x040006AD RID: 1709
	public readonly int top;

	// Token: 0x040006AE RID: 1710
	public readonly int bottom;

	// Token: 0x040006AF RID: 1711
	public readonly int width;

	// Token: 0x040006B0 RID: 1712
	public readonly int height;
}
