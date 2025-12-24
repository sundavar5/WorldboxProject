using System;
using System.Collections.Generic;

// Token: 0x02000206 RID: 518
public static class ActorBagExtensions
{
	// Token: 0x0600123A RID: 4666 RVA: 0x000D45DB File Offset: 0x000D27DB
	public static ActorBag add(this ActorBag pBag, ResourceContainer pResourceContainer)
	{
		return pBag.add(pResourceContainer.id, pResourceContainer.amount);
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x000D45F0 File Offset: 0x000D27F0
	public static ActorBag add(this ActorBag pBag, string pID, int pAmount)
	{
		if (pBag == null)
		{
			pBag = new ActorBag();
		}
		if (pBag.dict == null)
		{
			pBag.dict = new Dictionary<string, ResourceContainer>();
		}
		ResourceContainer tContainer;
		if (pBag.dict.TryGetValue(pID, out tContainer))
		{
			tContainer.amount += pAmount;
		}
		else
		{
			tContainer = new ResourceContainer(pID, pAmount);
		}
		pBag.dict[pID] = tContainer;
		pBag.last_item_to_render = null;
		return pBag;
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x000D4658 File Offset: 0x000D2858
	public static ActorBag remove(this ActorBag pBag, string pID, int pAmount)
	{
		if (pBag.isEmpty())
		{
			return null;
		}
		ResourceContainer tContainer;
		if (pBag.dict.TryGetValue(pID, out tContainer))
		{
			tContainer.amount -= pAmount;
			if (tContainer.amount <= 0)
			{
				pBag.dict.Remove(pID);
			}
			else
			{
				pBag.dict[pID] = tContainer;
			}
			pBag.last_item_to_render = null;
		}
		return pBag;
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x000D46B8 File Offset: 0x000D28B8
	public static Dictionary<string, ResourceContainer> getResources(this ActorBag pBag)
	{
		return pBag.dict;
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x000D46C0 File Offset: 0x000D28C0
	public static bool hasResources(this ActorBag pBag)
	{
		if (pBag == null)
		{
			return false;
		}
		Dictionary<string, ResourceContainer> dict = pBag.dict;
		int? num = (dict != null) ? new int?(dict.Count) : null;
		int num2 = 0;
		return num.GetValueOrDefault() > num2 & num != null;
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000D4706 File Offset: 0x000D2906
	public static bool isEmpty(this ActorBag pBag)
	{
		return !pBag.hasResources();
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x000D4714 File Offset: 0x000D2914
	public static string getRandomResourceID(this ActorBag pBag)
	{
		if (pBag.isEmpty())
		{
			return string.Empty;
		}
		if (pBag.dict.Count == 0)
		{
			return string.Empty;
		}
		int tRandomIndex = Randy.randomInt(0, pBag.dict.Count);
		int tIndex = 0;
		foreach (string tKey in pBag.dict.Keys)
		{
			if (tIndex == tRandomIndex)
			{
				return tKey;
			}
			tIndex++;
		}
		return string.Empty;
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x000D47B0 File Offset: 0x000D29B0
	public static int getResource(this ActorBag pBag, string pID)
	{
		if (pBag.isEmpty())
		{
			return 0;
		}
		ResourceContainer tContainer;
		if (pBag.dict.TryGetValue(pID, out tContainer))
		{
			return tContainer.amount;
		}
		return 0;
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x000D47DF File Offset: 0x000D29DF
	public static void empty(this ActorBag pBag)
	{
		if (!pBag.isEmpty())
		{
			pBag.dict.Clear();
		}
		pBag = null;
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x000D47F8 File Offset: 0x000D29F8
	public static string getItemIDToRender(this ActorBag pBag)
	{
		if (pBag.hasResources())
		{
			if (string.IsNullOrEmpty(pBag.last_item_to_render))
			{
				using (Dictionary<string, ResourceContainer>.ValueCollection.Enumerator enumerator = pBag.getResources().Values.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						goto IL_65;
					}
					ResourceContainer tContainer = enumerator.Current;
					pBag.last_item_to_render = tContainer.id;
					return pBag.last_item_to_render;
				}
			}
			return pBag.last_item_to_render;
		}
		IL_65:
		return string.Empty;
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000D4884 File Offset: 0x000D2A84
	public static int countResources(this ActorBag pBag)
	{
		if (pBag.isEmpty())
		{
			return 0;
		}
		return pBag.dict.Count;
	}
}
