using System;
using UnityEngine;

// Token: 0x020003E6 RID: 998
public static class TownPlans
{
	// Token: 0x060022C1 RID: 8897 RVA: 0x00122EB8 File Offset: 0x001210B8
	public static bool isInPassableRingMap(TileZone pZone, TileZone pCenterZone = null)
	{
		TileZone tCenterZone = World.world.zone_calculator.getMapCenterZone();
		return TownPlans.isInPassableRing(pZone, tCenterZone);
	}

	// Token: 0x060022C2 RID: 8898 RVA: 0x00122EDC File Offset: 0x001210DC
	public static bool isInPassableRing(TileZone pZone, TileZone pCityCenterZone)
	{
		float num = Toolbox.Dist(pZone.x, pZone.y, pCityCenterZone.x, pCityCenterZone.y);
		float tRingWidth = 1f;
		float tSkipSize = 1f;
		float tCycleSize = tRingWidth + tSkipSize;
		return num % tCycleSize >= tRingWidth;
	}

	// Token: 0x060022C3 RID: 8899 RVA: 0x00122F20 File Offset: 0x00121120
	public static bool isPassableCross(TileZone pZone, TileZone pCityZone)
	{
		int num = Mathf.Abs(pCityZone.x - pZone.x);
		int tDeltaY = Mathf.Abs(pCityZone.y - pZone.y);
		return num <= 1 || tDeltaY <= 1;
	}

	// Token: 0x060022C4 RID: 8900 RVA: 0x00122F5C File Offset: 0x0012115C
	public static bool isPassableLineHorizontal(TileZone pZone, TileZone _ = null)
	{
		return pZone.y % 2 == 0;
	}

	// Token: 0x060022C5 RID: 8901 RVA: 0x00122F69 File Offset: 0x00121169
	public static bool isPassableLineVertical(TileZone pZone, TileZone _ = null)
	{
		return pZone.x % 2 == 0;
	}

	// Token: 0x060022C6 RID: 8902 RVA: 0x00122F76 File Offset: 0x00121176
	public static bool isPassableDiagonal(TileZone pZone, TileZone _ = null)
	{
		return (pZone.x + pZone.y) % 3 != 0;
	}

	// Token: 0x060022C7 RID: 8903 RVA: 0x00122F8C File Offset: 0x0012118C
	public static bool isPassableDiamond(TileZone pZone, TileZone _ = null)
	{
		return (pZone.x + pZone.y) % 2 != 0;
	}

	// Token: 0x060022C8 RID: 8904 RVA: 0x00122FA2 File Offset: 0x001211A2
	public static bool isPassableDiamondCluster(TileZone pZone, TileZone _ = null)
	{
		return (pZone.x / 2 + pZone.y / 2) % 2 == 0;
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x00122FBC File Offset: 0x001211BC
	public static bool isPassableHoneycomb(TileZone pZone, TileZone _ = null)
	{
		int tRowOffset = (pZone.y % 2 == 0) ? 2 : 0;
		return (pZone.x + tRowOffset) % 4 == 0;
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x00122FE5 File Offset: 0x001211E5
	public static bool isPassableBrickHorizontal(TileZone pZone, TileZone _ = null)
	{
		return TownPlans.isPassableLineVertical(pZone, null) && TownPlans.isPassableDiagonal(pZone, null);
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x00122FFE File Offset: 0x001211FE
	public static bool isPassableBrickVertical(TileZone pZone, TileZone _ = null)
	{
		return TownPlans.isPassableLineHorizontal(pZone, null) && TownPlans.isPassableDiagonal(pZone, null);
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x00123017 File Offset: 0x00121217
	public static bool isPassableLatticeSmall(TileZone pZone, TileZone _ = null)
	{
		return TownPlans.isPassableLattice(pZone, 2, 1);
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x00123021 File Offset: 0x00121221
	public static bool isPassableLatticeMedium(TileZone pZone, TileZone _ = null)
	{
		return TownPlans.isPassableLattice(pZone, 3, 1);
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x0012302B File Offset: 0x0012122B
	public static bool isPassableLatticeBig(TileZone pZone, TileZone _ = null)
	{
		return TownPlans.isPassableLattice(pZone, 4, 2);
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x00123038 File Offset: 0x00121238
	public static bool isPassableMadmanLabyrinth(TileZone pZone, TileZone _ = null)
	{
		float tNoiseScale = 0.7f;
		float tPathThreshold = 0.4f;
		return Mathf.PerlinNoise((float)pZone.x * tNoiseScale, (float)pZone.y * tNoiseScale) > tPathThreshold;
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x0012306C File Offset: 0x0012126C
	private static bool isPassableLattice(TileZone pZone, int pSpacing, int pWidth)
	{
		bool flag = pZone.x % pSpacing < pWidth;
		bool tIsPassableRow = pZone.y % pSpacing < pWidth;
		return flag || tIsPassableRow;
	}

	// Token: 0x060022D1 RID: 8913 RVA: 0x00123098 File Offset: 0x00121298
	private static bool isPassableClusters(TileZone pZone, int pSpacing, int pWidth)
	{
		bool flag = pZone.x % pSpacing < pWidth;
		bool tIsPassableRow = pZone.y % pSpacing < pWidth;
		return !flag && !tIsPassableRow;
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x001230C5 File Offset: 0x001212C5
	public static bool isPassableClustersSmall(TileZone pZone, TileZone _ = null)
	{
		return TownPlans.isPassableClusters(pZone, 3, 1);
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x001230CF File Offset: 0x001212CF
	public static bool isPassableClustersMedium(TileZone pZone, TileZone _ = null)
	{
		return TownPlans.isPassableClusters(pZone, 4, 1);
	}

	// Token: 0x060022D4 RID: 8916 RVA: 0x001230D9 File Offset: 0x001212D9
	public static bool isPassableClustersBig(TileZone pZone, TileZone _ = null)
	{
		return TownPlans.isPassableClusters(pZone, 5, 1);
	}

	// Token: 0x060022D5 RID: 8917 RVA: 0x001230E4 File Offset: 0x001212E4
	public static bool debugVisualizeZone(TileZone pZone, TileZone pCursorZone = null)
	{
		DebugVariables tTestVars = DebugVariables.instance;
		if (tTestVars == null)
		{
			return false;
		}
		bool tAllow = true;
		if (tTestVars.layout_cross && !TownPlans.isPassableCross(pZone, pCursorZone))
		{
			tAllow = false;
		}
		if (tTestVars.layout_ring && !TownPlans.isInPassableRing(pZone, pCursorZone))
		{
			tAllow = false;
		}
		if (tTestVars.layout_lines_horizontal && !TownPlans.isPassableLineHorizontal(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_lines_vertical && !TownPlans.isPassableLineVertical(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_diagonal && !TownPlans.isPassableDiagonal(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_diamond && !TownPlans.isPassableDiamond(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_diamond_cluster && !TownPlans.isPassableDiamondCluster(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_lattice_small && !TownPlans.isPassableLatticeSmall(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_lattice_medium && !TownPlans.isPassableLatticeMedium(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_lattice_big && !TownPlans.isPassableLatticeBig(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_clusters_small && !TownPlans.isPassableClustersSmall(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_clusters_medium && !TownPlans.isPassableClustersMedium(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_clusters_big && !TownPlans.isPassableClustersBig(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_map_ring && !TownPlans.isInPassableRingMap(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_honeycomb && !TownPlans.isPassableHoneycomb(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_brick_horizontal && !TownPlans.isPassableBrickHorizontal(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_brick_vertical && !TownPlans.isPassableBrickVertical(pZone, null))
		{
			tAllow = false;
		}
		if (tTestVars.layout_madman_labyrinth && !TownPlans.isPassableMadmanLabyrinth(pZone, null))
		{
			tAllow = false;
		}
		return tAllow;
	}
}
