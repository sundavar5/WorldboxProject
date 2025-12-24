using System;
using System.Collections.Generic;
using LayoutGroupExt;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000850 RID: 2128
public class WorldLawCategory : MonoBehaviour
{
	// Token: 0x0600428E RID: 17038 RVA: 0x001C2F5E File Offset: 0x001C115E
	public void init(WorldLawGroupAsset pGroupAsset)
	{
		this._asset = pGroupAsset;
		this._title.color = this._asset.getColor();
		this._title.GetComponent<LocalizedText>().setKeyAndUpdate(this._asset.getLocaleID());
	}

	// Token: 0x0600428F RID: 17039 RVA: 0x001C2F98 File Offset: 0x001C1198
	public void addElement(WorldLawElement pElement)
	{
		this._laws_list.Add(pElement);
		pElement.setSelectionColor(ColorStyleLibrary.m.getSelectorColor());
	}

	// Token: 0x06004290 RID: 17040 RVA: 0x001C2FB8 File Offset: 0x001C11B8
	public void updateCounter()
	{
		int tSelected = 0;
		using (HashSet<WorldLawElement>.Enumerator enumerator = this._laws_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isLawEnabled())
				{
					tSelected++;
				}
			}
		}
		this._selected_counter.text = string.Format("{0} / {1}", tSelected, this._laws_list.Count);
	}

	// Token: 0x06004291 RID: 17041 RVA: 0x001C303C File Offset: 0x001C123C
	public void updateButtons()
	{
		foreach (WorldLawElement worldLawElement in this._laws_list)
		{
			worldLawElement.updateStatus();
		}
	}

	// Token: 0x040030BF RID: 12479
	[SerializeField]
	private Text _title;

	// Token: 0x040030C0 RID: 12480
	[SerializeField]
	private Text _selected_counter;

	// Token: 0x040030C1 RID: 12481
	public GridLayoutGroupExtended grid;

	// Token: 0x040030C2 RID: 12482
	private WorldLawGroupAsset _asset;

	// Token: 0x040030C3 RID: 12483
	private HashSet<WorldLawElement> _laws_list = new HashSet<WorldLawElement>();
}
