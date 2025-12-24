using System;
using System.Collections;

// Token: 0x020001F9 RID: 505
public class ActorVisibleDataArray
{
	// Token: 0x060011BF RID: 4543 RVA: 0x000D193B File Offset: 0x000CFB3B
	public void prepare(int pTargetSize)
	{
		this.array = Toolbox.checkArraySize<Actor>(this.array, pTargetSize);
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x000D194F File Offset: 0x000CFB4F
	public void addFromCollection(ICollection pList)
	{
		if (pList.Count == 0)
		{
			return;
		}
		pList.CopyTo(this.array, this.count);
		this.count += pList.Count;
	}

	// Token: 0x04000FC8 RID: 4040
	public Actor[] array = new Actor[0];

	// Token: 0x04000FC9 RID: 4041
	public int count;
}
