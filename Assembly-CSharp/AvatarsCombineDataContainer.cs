using System;
using System.Collections.Generic;

// Token: 0x0200055B RID: 1371
public class AvatarsCombineDataContainer
{
	// Token: 0x06002C9B RID: 11419 RVA: 0x0015DFE4 File Offset: 0x0015C1E4
	public void add(string pId, int pAmount)
	{
		AvatarsCombineDataElement tElement = new AvatarsCombineDataElement(this._dict.Count + 1, pAmount);
		this._dict.Add(pId, tElement);
		this._list.Add(tElement);
	}

	// Token: 0x06002C9C RID: 11420 RVA: 0x0015E020 File Offset: 0x0015C220
	public int getListIndex(int pIndex, string pId)
	{
		AvatarsCombineDataElement tElement = this._dict[pId];
		int num = tElement.order_index - 1;
		int divisor = 1;
		for (int i = num + 1; i < this._list.Count; i++)
		{
			divisor *= this._list[i].total_amount;
		}
		return pIndex / divisor % tElement.total_amount;
	}

	// Token: 0x06002C9D RID: 11421 RVA: 0x0015E079 File Offset: 0x0015C279
	public void clear()
	{
		this._dict.Clear();
		this._list.Clear();
	}

	// Token: 0x06002C9E RID: 11422 RVA: 0x0015E094 File Offset: 0x0015C294
	public int totalCombinations()
	{
		int tResult = 1;
		for (int i = 0; i < this._list.Count; i++)
		{
			tResult *= this._list[i].total_amount;
		}
		return tResult;
	}

	// Token: 0x0400222D RID: 8749
	private Dictionary<string, AvatarsCombineDataElement> _dict = new Dictionary<string, AvatarsCombineDataElement>();

	// Token: 0x0400222E RID: 8750
	private List<AvatarsCombineDataElement> _list = new List<AvatarsCombineDataElement>();
}
