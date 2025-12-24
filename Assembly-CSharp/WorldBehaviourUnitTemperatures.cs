using System;
using System.Collections.Generic;

// Token: 0x02000315 RID: 789
public class WorldBehaviourUnitTemperatures
{
	// Token: 0x06001D8A RID: 7562 RVA: 0x001078AC File Offset: 0x00105AAC
	public static void updateUnitTemperatures()
	{
		if (WorldBehaviourUnitTemperatures.temperatures.Count == 0)
		{
			return;
		}
		WorldBehaviourUnitTemperatures._temperature_updaters.Clear();
		bool tCleanFull = true;
		bool tCleanDead = false;
		foreach (Actor tActor2 in WorldBehaviourUnitTemperatures.temperatures.Keys)
		{
			if (tActor2.isRekt())
			{
				tCleanDead = true;
			}
			else
			{
				tCleanFull = false;
				int tCurrentTemperature = WorldBehaviourUnitTemperatures.temperatures[tActor2];
				int tNewTemp = tCurrentTemperature;
				if (tNewTemp > 0)
				{
					tNewTemp--;
					if (tNewTemp < 0)
					{
						tNewTemp = 0;
					}
				}
				else
				{
					tNewTemp++;
					if (tNewTemp > 0)
					{
						tNewTemp = 0;
					}
				}
				if (tNewTemp != tCurrentTemperature)
				{
					WorldBehaviourUnitTemperatures._temperature_updaters.Add(new TemperatureMod(tActor2, tNewTemp));
				}
			}
		}
		if (tCleanFull)
		{
			WorldBehaviourUnitTemperatures.temperatures.Clear();
		}
		else if (WorldBehaviourUnitTemperatures._temperature_updaters.Count > 0)
		{
			for (int i = 0; i < WorldBehaviourUnitTemperatures._temperature_updaters.Count; i++)
			{
				TemperatureMod tMod = WorldBehaviourUnitTemperatures._temperature_updaters[i];
				if (tMod.new_temperature == 0)
				{
					WorldBehaviourUnitTemperatures.temperatures.Remove(tMod.actor);
				}
				else
				{
					WorldBehaviourUnitTemperatures.temperatures[tMod.actor] = tMod.new_temperature;
				}
			}
		}
		if (tCleanDead)
		{
			WorldBehaviourUnitTemperatures.temperatures.RemoveByKey((Actor tActor) => tActor.isRekt());
		}
		WorldBehaviourUnitTemperatures._temperature_updaters.Clear();
	}

	// Token: 0x06001D8B RID: 7563 RVA: 0x00107A20 File Offset: 0x00105C20
	private static void changeUnitTemperature(Actor pActor, int pTemperatureChangeSpeed)
	{
		if (!WorldBehaviourUnitTemperatures.temperatures.ContainsKey(pActor))
		{
			WorldBehaviourUnitTemperatures.temperatures.Add(pActor, 0);
		}
		Dictionary<Actor, int> dictionary = WorldBehaviourUnitTemperatures.temperatures;
		dictionary[pActor] += pTemperatureChangeSpeed;
		float tCurrentTemperature = (float)WorldBehaviourUnitTemperatures.temperatures[pActor];
		if (pTemperatureChangeSpeed < 0)
		{
			pActor.finishStatusEffect("burning");
			if (tCurrentTemperature < -200f)
			{
				pActor.addStatusEffect("frozen", 0f, true);
				WorldBehaviourUnitTemperatures.temperatures[pActor] = 0;
				return;
			}
		}
		else
		{
			pActor.finishStatusEffect("frozen");
			if (tCurrentTemperature > 300f)
			{
				pActor.addStatusEffect("burning", 0f, true);
				WorldBehaviourUnitTemperatures.temperatures[pActor] = 0;
			}
		}
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x00107AD4 File Offset: 0x00105CD4
	public static void checkTile(WorldTile pTile, int pTemperatureChangeSpeed)
	{
		pTile.doUnits(delegate(Actor tActor)
		{
			WorldBehaviourUnitTemperatures.changeUnitTemperature(tActor, pTemperatureChangeSpeed);
		});
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x00107B00 File Offset: 0x00105D00
	public static void clear()
	{
		WorldBehaviourUnitTemperatures.temperatures.Clear();
		WorldBehaviourUnitTemperatures._temperature_updaters.Clear();
	}

	// Token: 0x06001D8E RID: 7566 RVA: 0x00107B16 File Offset: 0x00105D16
	public static void removeUnit(Actor pActor)
	{
		WorldBehaviourUnitTemperatures.temperatures.Remove(pActor);
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x00107B24 File Offset: 0x00105D24
	public static void debug(DebugTool pTool)
	{
		pTool.setText("units", WorldBehaviourUnitTemperatures.temperatures.Count, 0f, false, 0L, false, false, "");
		int tMax = 5;
		foreach (Actor tActor in WorldBehaviourUnitTemperatures.temperatures.Keys)
		{
			if (tActor.isAlive())
			{
				pTool.setText(": " + tActor.getName(), WorldBehaviourUnitTemperatures.temperatures[tActor].ToText(), 0f, false, 0L, false, false, "");
				tMax--;
				if (tMax == 0)
				{
					break;
				}
			}
		}
	}

	// Token: 0x0400161D RID: 5661
	public static Dictionary<Actor, int> temperatures = new Dictionary<Actor, int>();

	// Token: 0x0400161E RID: 5662
	private static List<TemperatureMod> _temperature_updaters = new List<TemperatureMod>();
}
