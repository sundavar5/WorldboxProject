using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000622 RID: 1570
public class CorruptedTreesManager : MonoBehaviour
{
	// Token: 0x06003364 RID: 13156 RVA: 0x00182FDC File Offset: 0x001811DC
	public void Start()
	{
		this._objects = new List<CorruptedTreeObject>();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			CorruptedTreeObject tObj = base.transform.GetChild(i).GetComponent<CorruptedTreeObject>();
			this._objects.Add(tObj);
			tObj.transform.GetChild(0).gameObject.SetActive(false);
			tObj.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.click(tObj);
			});
		}
		this.win_icon.gameObject.SetActive(false);
	}

	// Token: 0x06003365 RID: 13157 RVA: 0x00183090 File Offset: 0x00181290
	public void click(CorruptedTreeObject pObject)
	{
		if (pObject.used)
		{
			return;
		}
		pObject.used = true;
		pObject.transform.GetChild(0).gameObject.SetActive(true);
		pObject.GetComponent<Image>().enabled = false;
		string text = "162534";
		string tWinCheck = text ?? "";
		this.currentString += pObject.transform.name;
		tWinCheck = tWinCheck.Substring(0, this.currentString.Length);
		if (text.CompareTo(tWinCheck) == 0)
		{
			this.win();
			return;
		}
		if (!tWinCheck.Contains(this.currentString))
		{
			this.lost();
		}
	}

	// Token: 0x06003366 RID: 13158 RVA: 0x00183132 File Offset: 0x00181332
	private void win()
	{
		this.win_icon.gameObject.SetActive(true);
		AchievementLibrary.the_corrupted_trees.check(null);
	}

	// Token: 0x06003367 RID: 13159 RVA: 0x00183154 File Offset: 0x00181354
	private void lost()
	{
		foreach (CorruptedTreeObject corruptedTreeObject in this._objects)
		{
			corruptedTreeObject.GetComponent<UiCreature>().click();
		}
	}

	// Token: 0x040026FE RID: 9982
	private string currentString = "";

	// Token: 0x040026FF RID: 9983
	private List<CorruptedTreeObject> _objects;

	// Token: 0x04002700 RID: 9984
	public GameObject win_icon;
}
