using System;
using System.Collections.Generic;

// Token: 0x02000420 RID: 1056
public static class RegionLinkHashes
{
	// Token: 0x06002490 RID: 9360 RVA: 0x0013049C File Offset: 0x0012E69C
	public static void addHash(int pHash, MapRegion pRegion)
	{
		RegionLink tLink;
		if (!RegionLinkHashes._dict.TryGetValue(pHash, out tLink))
		{
			tLink = RegionLinkHashes._pool.get();
			tLink.reset();
			tLink.id = pHash;
			RegionLinkHashes._dict[tLink.id] = tLink;
		}
		if (tLink.regions.Add(pRegion))
		{
			pRegion.addLink(tLink);
		}
	}

	// Token: 0x06002491 RID: 9361 RVA: 0x001304F6 File Offset: 0x0012E6F6
	public static int getCount()
	{
		return RegionLinkHashes._dict.Count;
	}

	// Token: 0x06002492 RID: 9362 RVA: 0x00130504 File Offset: 0x0012E704
	public static void clear()
	{
		foreach (RegionLink tLink in RegionLinkHashes._dict.Values)
		{
			tLink.reset();
			RegionLinkHashes._pool.release(tLink);
		}
		RegionLinkHashes._dict.Clear();
	}

	// Token: 0x06002493 RID: 9363 RVA: 0x00130570 File Offset: 0x0012E770
	public static RegionLink getHash(int pHash)
	{
		RegionLink tLink;
		RegionLinkHashes._dict.TryGetValue(pHash, out tLink);
		return tLink;
	}

	// Token: 0x06002494 RID: 9364 RVA: 0x0013058C File Offset: 0x0012E78C
	public static void remove(RegionLink pLink, MapRegion pRegion)
	{
		pLink.regions.Remove(pRegion);
		if (pLink.regions.Count == 0 && RegionLinkHashes._dict.Remove(pLink.id))
		{
			pLink.reset();
			RegionLinkHashes._pool.release(pLink);
		}
	}

	// Token: 0x06002495 RID: 9365 RVA: 0x001305CC File Offset: 0x0012E7CC
	public static void debug(DebugTool pTool)
	{
		pTool.setText("hashes", RegionLinkHashes._dict.Count, 0f, false, 0L, false, false, "");
	}

	// Token: 0x04001A6E RID: 6766
	private static readonly Dictionary<int, RegionLink> _dict = new Dictionary<int, RegionLink>();

	// Token: 0x04001A6F RID: 6767
	private static readonly StackPool<RegionLink> _pool = new StackPool<RegionLink>();
}
