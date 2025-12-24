using System;

// Token: 0x02000845 RID: 2117
public struct WindowStats
{
	// Token: 0x06004251 RID: 16977 RVA: 0x001C0FCB File Offset: 0x001BF1CB
	public void setCurrent(string pCurrent)
	{
		if (this.current == pCurrent)
		{
			return;
		}
		if (this.current != null && this.previous != this.current)
		{
			this.previous = this.current;
		}
		this.current = pCurrent;
	}

	// Token: 0x0400305F RID: 12383
	public int opens;

	// Token: 0x04003060 RID: 12384
	public int closes;

	// Token: 0x04003061 RID: 12385
	public int shows;

	// Token: 0x04003062 RID: 12386
	public int hides;

	// Token: 0x04003063 RID: 12387
	public string previous;

	// Token: 0x04003064 RID: 12388
	public string current;
}
