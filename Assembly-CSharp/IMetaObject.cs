using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200021F RID: 543
public interface IMetaObject : ICoreObject
{
	// Token: 0x060013B6 RID: 5046 RVA: 0x000D93D4 File Offset: 0x000D75D4
	int getMaxPossibleLifespan()
	{
		int tCount = 0;
		int tMax = 0;
		foreach (Actor actor in this.getUnits())
		{
			int tLifespan = (int)actor.stats["lifespan"];
			if (tLifespan > tMax)
			{
				tMax = tLifespan;
			}
			tCount++;
		}
		if (tCount == 0)
		{
			return 100;
		}
		return tMax;
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x000D9440 File Offset: 0x000D7640
	float getRatioAdults()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isAdult())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x000D94B0 File Offset: 0x000D76B0
	float getRatioMales()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isSexMale())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x000D9520 File Offset: 0x000D7720
	float getRatioFemales()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isSexFemale())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x000D9590 File Offset: 0x000D7790
	float getRatioChildren()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.isAdult())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x000D9600 File Offset: 0x000D7800
	float getRatioHoused()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.hasHouse())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x000D9670 File Offset: 0x000D7870
	float getRatioHomeless()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.hasHouse())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x000D96E0 File Offset: 0x000D78E0
	float getRatioHungry()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isHungry())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x000D9750 File Offset: 0x000D7950
	float getRatioStarving()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isStarving())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x000D97C0 File Offset: 0x000D79C0
	float getRatioSick()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isSick())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x000D9830 File Offset: 0x000D7A30
	float getRatioHappy()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isHappy())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x000D98A0 File Offset: 0x000D7AA0
	float getRatioUnhappy()
	{
		int tCount = 0;
		float tTotal = 0f;
		using (IEnumerator<Actor> enumerator = this.getUnits().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isUnhappy())
				{
					tTotal += 1f;
				}
				tCount++;
			}
		}
		if (tCount <= 0)
		{
			return 0f;
		}
		return tTotal / (float)tCount;
	}

	// Token: 0x060013C2 RID: 5058
	MetaTypeAsset getMetaTypeAsset();

	// Token: 0x060013C3 RID: 5059
	bool hasUnits();

	// Token: 0x060013C4 RID: 5060
	int countUnits();

	// Token: 0x060013C5 RID: 5061
	IEnumerable<Actor> getUnits();

	// Token: 0x060013C6 RID: 5062
	Actor getRandomUnit();

	// Token: 0x060013C7 RID: 5063
	Actor getRandomActorForReaper();

	// Token: 0x060013C8 RID: 5064
	int countFamilies();

	// Token: 0x060013C9 RID: 5065
	IEnumerable<Family> getFamilies();

	// Token: 0x060013CA RID: 5066
	bool hasFamilies();

	// Token: 0x060013CB RID: 5067
	ActorAsset getActorAsset();

	// Token: 0x060013CC RID: 5068
	Sprite getSpriteIcon();

	// Token: 0x060013CD RID: 5069
	bool isCursorOver();

	// Token: 0x060013CE RID: 5070
	void setCursorOver();

	// Token: 0x060013CF RID: 5071
	ColorAsset getColor();

	// Token: 0x060013D0 RID: 5072
	MetaObjectData getMetaData();

	// Token: 0x060013D1 RID: 5073
	int getRenown();

	// Token: 0x060013D2 RID: 5074
	int getPopulationPeople();

	// Token: 0x060013D3 RID: 5075
	long getTotalKills();

	// Token: 0x060013D4 RID: 5076
	long getTotalDeaths();

	// Token: 0x060013D5 RID: 5077
	bool isSelected();

	// Token: 0x060013D6 RID: 5078
	Actor getOldestVisibleUnit();

	// Token: 0x060013D7 RID: 5079
	Actor getOldestVisibleUnitForNameplatesCached();

	// Token: 0x060013D8 RID: 5080
	bool hasCities();

	// Token: 0x060013D9 RID: 5081
	IEnumerable<City> getCities();

	// Token: 0x060013DA RID: 5082
	bool hasKingdoms();

	// Token: 0x060013DB RID: 5083
	IEnumerable<Kingdom> getKingdoms();

	// Token: 0x060013DC RID: 5084
	bool hasDied();
}
