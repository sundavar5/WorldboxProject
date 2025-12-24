using System;
using System.Collections.Generic;

// Token: 0x02000334 RID: 820
public class CapturingZonesCalculator
{
	// Token: 0x06001FDA RID: 8154 RVA: 0x001125B8 File Offset: 0x001107B8
	public static void getListToDraw(City pCity, int pTicks, ListPool<TileZone> pResults)
	{
		pResults.Clear();
		WorldTile tile = pCity.getTile(false);
		TileZone tCityZone = (tile != null) ? tile.zone : null;
		if (tCityZone == null)
		{
			tCityZone = pCity.zones[0];
		}
		Queue<TileZone> tCurrentWave = CapturingZonesCalculator._currentWave;
		tCurrentWave.Enqueue(tCityZone);
		CapturingZonesCalculator._zoneTicks = pTicks;
		while (tCurrentWave.Count > 0 && CapturingZonesCalculator._zoneTicks != 0)
		{
			TileZone tZone = tCurrentWave.Dequeue();
			CapturingZonesCalculator.check(tZone, pCity);
			pResults.Add(tZone);
			if (tCurrentWave.Count == 0)
			{
				Queue<TileZone> nextWave = tCurrentWave;
				tCurrentWave = CapturingZonesCalculator._nextWave;
				CapturingZonesCalculator._nextWave = nextWave;
			}
		}
		CapturingZonesCalculator._nextWave.Clear();
		CapturingZonesCalculator._waveChecked.Clear();
		tCurrentWave.Clear();
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x00112658 File Offset: 0x00110858
	private static void check(TileZone pTargetZone, City pCity)
	{
		CapturingZonesCalculator._zoneTicks--;
		CapturingZonesCalculator._waveChecked.Add(pTargetZone);
		foreach (TileZone tZone in pTargetZone.neighbours)
		{
			if (tZone.city == pCity && !CapturingZonesCalculator._waveChecked.Contains(tZone))
			{
				CapturingZonesCalculator._waveChecked.Add(tZone);
				CapturingZonesCalculator._nextWave.Enqueue(tZone);
			}
		}
	}

	// Token: 0x04001730 RID: 5936
	private static int _zoneTicks = 0;

	// Token: 0x04001731 RID: 5937
	private static Queue<TileZone> _currentWave = new Queue<TileZone>();

	// Token: 0x04001732 RID: 5938
	private static Queue<TileZone> _nextWave = new Queue<TileZone>();

	// Token: 0x04001733 RID: 5939
	private static HashSet<TileZone> _waveChecked = new HashSet<TileZone>();
}
