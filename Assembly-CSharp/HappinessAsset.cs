using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x02000108 RID: 264
[Serializable]
public class HappinessAsset : Asset, ILocalizedAsset, IMultiLocalesAsset
{
	// Token: 0x0600080F RID: 2063 RVA: 0x00070C5C File Offset: 0x0006EE5C
	public virtual Sprite getSprite()
	{
		if (this._cached_sprite == null)
		{
			this._cached_sprite = SpriteTextureLoader.getSprite(this.path_icon);
		}
		return this._cached_sprite;
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00070C7D File Offset: 0x0006EE7D
	public string getLocaleID()
	{
		return "happiness_" + this.id;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00070C8F File Offset: 0x0006EE8F
	public IEnumerable<string> getLocaleIDs()
	{
		int num;
		for (int i = 0; i < this.dialogs_amount; i = num + 1)
		{
			yield return this.getHappinnessDialogID() + i.ToString();
			num = i;
		}
		yield break;
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00070C9F File Offset: 0x0006EE9F
	public string getHappinnessDialogID()
	{
		return "happiness_dialog_" + this.id + "_";
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00070CB8 File Offset: 0x0006EEB8
	public string getTextSingleReport()
	{
		int tIndex = Random.Range(0, this.dialogs_amount);
		return this.getHappinnessDialogID() + tIndex.ToString();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00070CE4 File Offset: 0x0006EEE4
	public string getRandomTextSingleReportLocalized()
	{
		return LocalizedTextManager.getText(this.getTextSingleReport(), null, false);
	}

	// Token: 0x04000861 RID: 2145
	public HappinessDelegateCalc calc;

	// Token: 0x04000862 RID: 2146
	public int value;

	// Token: 0x04000863 RID: 2147
	public string pot_task_id;

	// Token: 0x04000864 RID: 2148
	public int pot_amount;

	// Token: 0x04000865 RID: 2149
	public int index;

	// Token: 0x04000866 RID: 2150
	public string path_icon;

	// Token: 0x04000867 RID: 2151
	public bool ignored_by_psychopaths;

	// Token: 0x04000868 RID: 2152
	[DefaultValue(true)]
	public bool show_change_happiness_effect = true;

	// Token: 0x04000869 RID: 2153
	public int dialogs_amount = 4;

	// Token: 0x0400086A RID: 2154
	private Sprite _cached_sprite;
}
