using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000600 RID: 1536
public class StatisticsRows : MonoBehaviour
{
	// Token: 0x0600326C RID: 12908 RVA: 0x0017ED15 File Offset: 0x0017CF15
	private void Awake()
	{
		this.init();
	}

	// Token: 0x0600326D RID: 12909 RVA: 0x0017ED1D File Offset: 0x0017CF1D
	private void OnEnable()
	{
		this.refreshStats();
	}

	// Token: 0x0600326E RID: 12910 RVA: 0x0017ED28 File Offset: 0x0017CF28
	private void OnDisable()
	{
		foreach (ValueTuple<KeyValueField, MetaType, string> valueTuple in this._all_stats)
		{
			valueTuple.Item1.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600326F RID: 12911 RVA: 0x0017ED84 File Offset: 0x0017CF84
	protected virtual void init()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003270 RID: 12912 RVA: 0x0017ED8C File Offset: 0x0017CF8C
	protected void addStatRow(StatisticsAsset pAsset)
	{
		string tID = pAsset.id;
		MetaType tMetaType = pAsset.world_stats_meta_type;
		KeyValueField tField = Object.Instantiate<KeyValueField>(this._stat_prefab, base.transform);
		UiGameStat component = tField.GetComponent<UiGameStat>();
		component.setAsset(pAsset);
		component.updateText();
		bool tHasIcon = !string.IsNullOrEmpty(pAsset.path_icon);
		if (tHasIcon)
		{
			Sprite tIcon = pAsset.getIcon();
			tField.icon.sprite = tIcon;
		}
		tField.icon.gameObject.SetActive(tHasIcon);
		this.setupField(tField, tMetaType, tID);
		tField.gameObject.SetActive(false);
		this._all_stats.Add(new ValueTuple<KeyValueField, MetaType, string>(tField, tMetaType, tID));
	}

	// Token: 0x06003271 RID: 12913 RVA: 0x0017EE2C File Offset: 0x0017D02C
	private void refreshStats()
	{
		foreach (ValueTuple<KeyValueField, MetaType, string> valueTuple in this._all_stats)
		{
			KeyValueField tField = valueTuple.Item1;
			MetaType tMetaType = valueTuple.Item2;
			string tID = valueTuple.Item3;
			this.setupField(tField, tMetaType, tID);
		}
		base.StartCoroutine(this.refreshRoutine());
	}

	// Token: 0x06003272 RID: 12914 RVA: 0x0017EEA4 File Offset: 0x0017D0A4
	private void setupField(KeyValueField pField, MetaType pMetaType, string pID)
	{
		StatisticsAsset tAsset = AssetManager.statistics_library.get(pID);
		if (pMetaType.isNone())
		{
			TooltipDataGetter tGetter = () => new TooltipData
			{
				tip_name = tAsset.getLocaleID(),
				tip_description = tAsset.getDescriptionID()
			};
			pField.setMetaForTooltip(pMetaType, -1L, "normal", tGetter);
			return;
		}
		MetaIdGetter get_meta_id = tAsset.get_meta_id;
		pField.setMetaForTooltip(pMetaType, (get_meta_id != null) ? get_meta_id(tAsset) : -1L, null, null);
	}

	// Token: 0x06003273 RID: 12915 RVA: 0x0017EF14 File Offset: 0x0017D114
	private IEnumerator refreshRoutine()
	{
		foreach (ValueTuple<KeyValueField, MetaType, string> valueTuple in this._all_stats)
		{
			valueTuple.Item1.gameObject.SetActive(true);
			yield return new WaitForSecondsRealtime(0.005f);
		}
		List<ValueTuple<KeyValueField, MetaType, string>>.Enumerator enumerator = default(List<ValueTuple<KeyValueField, MetaType, string>>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x04002620 RID: 9760
	[SerializeField]
	private KeyValueField _stat_prefab;

	// Token: 0x04002621 RID: 9761
	[TupleElementNames(new string[]
	{
		"field",
		"type",
		"id"
	})]
	private List<ValueTuple<KeyValueField, MetaType, string>> _all_stats = new List<ValueTuple<KeyValueField, MetaType, string>>();
}
