using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200072D RID: 1837
public class PopulationPyramidController : MonoBehaviour
{
	// Token: 0x06003A85 RID: 14981 RVA: 0x0019E00C File Offset: 0x0019C20C
	private void Awake()
	{
		if (this._pool_rows == null)
		{
			for (int i = 0; i < this._population_pyramid_container.childCount; i++)
			{
				Object.Destroy(this._population_pyramid_container.GetChild(i).gameObject);
			}
			this._pool_rows = new ObjectPoolGenericMono<PopulationPyramidRow>(this._population_pyramid_row, this._population_pyramid_container);
		}
	}

	// Token: 0x06003A86 RID: 14982 RVA: 0x0019E064 File Offset: 0x0019C264
	private void OnEnable()
	{
		this.load();
	}

	// Token: 0x06003A87 RID: 14983 RVA: 0x0019E06C File Offset: 0x0019C26C
	private void OnDisable()
	{
		this.clearBars();
	}

	// Token: 0x06003A88 RID: 14984 RVA: 0x0019E074 File Offset: 0x0019C274
	internal void load()
	{
		this.calculateLifespan();
		this.calculateAgeData();
		this.calculateAgeGroups();
		base.StartCoroutine(this.showBars());
	}

	// Token: 0x06003A89 RID: 14985 RVA: 0x0019E095 File Offset: 0x0019C295
	private void clearBars()
	{
		this._units = null;
		this._pool_rows.clear(true);
	}

	// Token: 0x06003A8A RID: 14986 RVA: 0x0019E0AC File Offset: 0x0019C2AC
	private void calculateLifespan()
	{
		IMetaObject tSelectedObject = (IMetaObject)AssetManager.meta_type_library.getAsset(this._meta_type).get_selected();
		this._units = tSelectedObject.getUnits();
		this._max_life_span = tSelectedObject.getMaxPossibleLifespan();
		foreach (Actor tActor in this._units)
		{
			if (tActor.isAlive())
			{
				int tAge = tActor.getAge();
				if (tAge > this._max_life_span)
				{
					this._max_life_span = tAge;
				}
			}
		}
		this._max_life_span = Mathf.CeilToInt((float)this._max_life_span / 20f) * 20;
		this._max_life_span = Mathf.Clamp(this._max_life_span, 40, 100);
		this._age_steps = this._max_life_span / 10;
	}

	// Token: 0x06003A8B RID: 14987 RVA: 0x0019E188 File Offset: 0x0019C388
	private void calculateAgeData()
	{
		this._max_amount = 0;
		this._age_data.Clear();
		foreach (Actor tActor in this._units)
		{
			if (tActor.isAlive())
			{
				int tAgeGroup = tActor.getAge() / this._age_steps;
				if (!this._age_data.ContainsKey(tAgeGroup))
				{
					this._age_data[tAgeGroup] = new GenderCount();
				}
				int tAmount = 0;
				if (tActor.isSexMale())
				{
					GenderCount genderCount = this._age_data[tAgeGroup];
					int num = genderCount.males + 1;
					genderCount.males = num;
					tAmount = num;
				}
				if (tActor.isSexFemale())
				{
					GenderCount genderCount2 = this._age_data[tAgeGroup];
					int num = genderCount2.females + 1;
					genderCount2.females = num;
					tAmount = num;
				}
				if (tAmount > this._max_amount)
				{
					this._max_amount = tAmount;
				}
			}
		}
	}

	// Token: 0x06003A8C RID: 14988 RVA: 0x0019E280 File Offset: 0x0019C480
	private void calculateAgeGroups()
	{
		this._age_groups.Clear();
		int tMax = 10;
		using (Dictionary<int, GenderCount>.KeyCollection.Enumerator enumerator = this._age_data.Keys.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int tAgeGroup = enumerator.Current;
				if (tAgeGroup > tMax)
				{
					tMax = tAgeGroup;
				}
			}
			goto IL_58;
		}
		IL_48:
		this._age_groups.Add(tMax--);
		IL_58:
		if (tMax < 0)
		{
			return;
		}
		goto IL_48;
	}

	// Token: 0x06003A8D RID: 14989 RVA: 0x0019E2FC File Offset: 0x0019C4FC
	private IEnumerator showBars()
	{
		foreach (int tAgeGroup in this._age_groups)
		{
			if (this._age_data.ContainsKey(tAgeGroup) || tAgeGroup <= 10)
			{
				PopulationPyramidRow tRow = this._pool_rows.getNext();
				tRow.setAgeGroup(tAgeGroup * this._age_steps, (tAgeGroup + 1) * this._age_steps - 1);
				if (this._age_data.ContainsKey(tAgeGroup))
				{
					int tAmountTotal = this._age_data[tAgeGroup].males + this._age_data[tAgeGroup].females;
					tRow.setMaleCount(this._age_data[tAgeGroup].males, this._max_amount);
					tRow.setFemaleCount(this._age_data[tAgeGroup].females, this._max_amount);
					tRow.setColorTextBasedOnAmount(tAmountTotal);
				}
				else
				{
					tRow.setColorTextBasedOnAmount(0);
					tRow.setMaleCount(0, this._max_amount);
					tRow.setFemaleCount(0, this._max_amount);
				}
				yield return CoroutineHelper.wait_for_next_frame;
			}
		}
		List<int>.Enumerator enumerator = default(List<int>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x04002B2E RID: 11054
	[SerializeField]
	private MetaType _meta_type = MetaType.Special;

	// Token: 0x04002B2F RID: 11055
	[SerializeField]
	private Transform _population_pyramid_container;

	// Token: 0x04002B30 RID: 11056
	[SerializeField]
	private PopulationPyramidRow _population_pyramid_row;

	// Token: 0x04002B31 RID: 11057
	private Dictionary<int, GenderCount> _age_data = new Dictionary<int, GenderCount>();

	// Token: 0x04002B32 RID: 11058
	private List<int> _age_groups = new List<int>(20);

	// Token: 0x04002B33 RID: 11059
	private int _max_amount;

	// Token: 0x04002B34 RID: 11060
	private ObjectPoolGenericMono<PopulationPyramidRow> _pool_rows;

	// Token: 0x04002B35 RID: 11061
	private int _max_life_span;

	// Token: 0x04002B36 RID: 11062
	private int _age_steps;

	// Token: 0x04002B37 RID: 11063
	private IEnumerable<Actor> _units;
}
