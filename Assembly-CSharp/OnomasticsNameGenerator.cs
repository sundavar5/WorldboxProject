using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200071D RID: 1821
public class OnomasticsNameGenerator : MonoBehaviour
{
	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06003A0F RID: 14863 RVA: 0x0019BEC6 File Offset: 0x0019A0C6
	private string _male_color
	{
		get
		{
			return ColorStyleLibrary.m.color_text_pumpkin;
		}
	}

	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06003A10 RID: 14864 RVA: 0x0019BED2 File Offset: 0x0019A0D2
	private string _female_color
	{
		get
		{
			return ColorStyleLibrary.m.color_text_pumpkin_light;
		}
	}

	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06003A11 RID: 14865 RVA: 0x0019BEDE File Offset: 0x0019A0DE
	private string _separator_color
	{
		get
		{
			return ColorStyleLibrary.m.color_text_grey_dark;
		}
	}

	// Token: 0x06003A12 RID: 14866 RVA: 0x0019BEEC File Offset: 0x0019A0EC
	protected void updateNameGeneration(OnomasticsData pData)
	{
		this._timer_name += Time.deltaTime;
		if (this._timer_name <= 0.2f)
		{
			return;
		}
		this._timer_name = 0f;
		if (this._index_name >= 20 || Toolbox.removeRichTextTags(this.text_name_examples.text).Length >= 250)
		{
			return;
		}
		this._index_name++;
		ActorSex tSex = Randy.randomBool() ? ActorSex.Female : ActorSex.Male;
		string tNewName = pData.generateName(tSex, 0, null);
		if (tNewName == "Rebr")
		{
			if (this.text_name_examples.text != string.Empty)
			{
				return;
			}
			tSex = ((tSex == ActorSex.Female) ? ActorSex.Male : ActorSex.Female);
			tNewName = pData.generateName(tSex, 0, null);
		}
		if (string.IsNullOrEmpty(tNewName))
		{
			return;
		}
		string tString = this.text_name_examples.text;
		if (tString.Length > 0)
		{
			tString += ", ";
		}
		tString += Toolbox.coloredText(tNewName, (tSex == ActorSex.Male) ? this._male_color : this._female_color, false);
		this.text_name_examples.text = tString;
		this.textExamplesEffect(this.text_name_examples.gameObject.transform, 1f, 0.03f, 0.3f);
	}

	// Token: 0x06003A13 RID: 14867 RVA: 0x0019C033 File Offset: 0x0019A233
	private void textExamplesEffect(Transform pTransformTarget, float pDefaultScale = 1f, float pPower = 0.1f, float pDuration = 0.3f)
	{
		pTransformTarget.DOKill(true);
		pTransformTarget.localScale = new Vector3(pDefaultScale, pDefaultScale, pDefaultScale);
		pTransformTarget.DOPunchScale(new Vector3(pPower, pPower, pPower), pDuration, 1, 1f);
	}

	// Token: 0x06003A14 RID: 14868 RVA: 0x0019C062 File Offset: 0x0019A262
	protected void clickRegenerate()
	{
		this._index_name = 0;
		this.text_name_examples.text = string.Empty;
		this.text_name_examples.color = Toolbox.makeColor(this._separator_color);
	}

	// Token: 0x04002AF3 RID: 10995
	private const int MAX_STRING_LENGTH = 250;

	// Token: 0x04002AF4 RID: 10996
	private const int AMOUNT_NAME_EXAMPLES = 20;

	// Token: 0x04002AF5 RID: 10997
	private const float TIMER_NAME_INTERVAL = 0.2f;

	// Token: 0x04002AF6 RID: 10998
	private float _timer_name;

	// Token: 0x04002AF7 RID: 10999
	private int _index_name;

	// Token: 0x04002AF8 RID: 11000
	public Text text_name_examples;
}
