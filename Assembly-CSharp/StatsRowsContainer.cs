using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000761 RID: 1889
public class StatsRowsContainer : MonoBehaviour
{
	// Token: 0x06003BE4 RID: 15332 RVA: 0x001A21C7 File Offset: 0x001A03C7
	private void Awake()
	{
		this.init();
	}

	// Token: 0x06003BE5 RID: 15333 RVA: 0x001A21CF File Offset: 0x001A03CF
	private void OnEnable()
	{
		this.showStats();
		base.StartCoroutine(this.showRows());
	}

	// Token: 0x06003BE6 RID: 15334 RVA: 0x001A21E4 File Offset: 0x001A03E4
	protected virtual void init()
	{
		this.stats_window = base.GetComponentInParent<StatsWindow>();
		KeyValueField tStatsPrefab = Resources.Load<KeyValueField>("ui/KeyValueFieldStats");
		this._stats_pool = new ObjectPoolGenericMono<KeyValueField>(tStatsPrefab, base.transform);
	}

	// Token: 0x06003BE7 RID: 15335 RVA: 0x001A221A File Offset: 0x001A041A
	protected virtual void showStats()
	{
		this.stats_window.showStatsRows();
	}

	// Token: 0x06003BE8 RID: 15336 RVA: 0x001A2227 File Offset: 0x001A0427
	private IEnumerator showRows()
	{
		foreach (KeyValueField keyValueField in this.stats_rows)
		{
			keyValueField.gameObject.SetActive(true);
			yield return CoroutineHelper.wait_for_next_frame;
		}
		List<KeyValueField>.Enumerator enumerator = default(List<KeyValueField>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06003BE9 RID: 15337 RVA: 0x001A2236 File Offset: 0x001A0436
	private void OnDisable()
	{
		this._stats_pool.clear(true);
		this.stats_rows.Clear();
	}

	// Token: 0x06003BEA RID: 15338 RVA: 0x001A2250 File Offset: 0x001A0450
	internal KeyValueField getStatRow(string pKey)
	{
		KeyValueField tField = this._stats_pool.getNext();
		tField.gameObject.name = "[KV] " + pKey;
		tField.gameObject.SetActive(false);
		this.stats_rows.Add(tField);
		return tField;
	}

	// Token: 0x06003BEB RID: 15339 RVA: 0x001A2298 File Offset: 0x001A0498
	internal KeyValueField showStatRow(string pId, object pValue, string pColor, MetaType pMetaType = MetaType.None, long pMetaId = -1L, bool pColorText = false, string pIconPath = null, string pTooltipId = null, TooltipDataGetter pTooltipData = null, bool pLocalize = true)
	{
		KeyValueField tNewRow = this.getStatRow(pId);
		bool tShowIcon = !string.IsNullOrEmpty(pIconPath);
		if (tShowIcon)
		{
			Sprite tIcon = SpriteTextureLoader.getSprite("ui/Icons/" + pIconPath);
			tNewRow.icon.sprite = tIcon;
		}
		tNewRow.icon.gameObject.SetActive(tShowIcon);
		string tTextToShow = pLocalize ? LocalizedTextManager.getText(pId, null, false) : pId;
		if (string.IsNullOrEmpty(pId))
		{
			tNewRow.name_text.text = "";
		}
		else if (pColorText)
		{
			tNewRow.name_text.text = Toolbox.coloredString(tTextToShow, pColor);
		}
		else
		{
			tNewRow.name_text.text = tTextToShow;
		}
		if (!string.IsNullOrEmpty(pColor))
		{
			tNewRow.value.text = Toolbox.coloredString(pValue.ToString(), pColor);
		}
		else
		{
			tNewRow.value.text = pValue.ToString();
		}
		tNewRow.setMetaForTooltip(pMetaType, pMetaId, pTooltipId, pTooltipData);
		return tNewRow;
	}

	// Token: 0x04002BCD RID: 11213
	protected StatsWindow stats_window;

	// Token: 0x04002BCE RID: 11214
	private ObjectPoolGenericMono<KeyValueField> _stats_pool;

	// Token: 0x04002BCF RID: 11215
	protected List<KeyValueField> stats_rows = new List<KeyValueField>();
}
