using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000541 RID: 1345
public class UiDebugButton : MonoBehaviour
{
	// Token: 0x06002BF1 RID: 11249 RVA: 0x0015B060 File Offset: 0x00159260
	public void Awake()
	{
		string tStr = base.gameObject.transform.name;
		try
		{
			this._debug_option = (DebugOption)Enum.Parse(typeof(DebugOption), tStr);
		}
		catch (Exception)
		{
			Debug.LogError("THERE'S NO DEBUG OPTION CALLED " + tStr);
			throw;
		}
		this.button.onClick.AddListener(new UnityAction(this.click));
	}

	// Token: 0x06002BF2 RID: 11250 RVA: 0x0015B0DC File Offset: 0x001592DC
	public void Start()
	{
		this.text.text = base.transform.gameObject.name;
		this.checkButtonGraphics();
	}

	// Token: 0x06002BF3 RID: 11251 RVA: 0x0015B0FF File Offset: 0x001592FF
	private void OnEnable()
	{
		this.checkButtonGraphics();
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x0015B108 File Offset: 0x00159308
	private void OnValidate()
	{
		string name = base.gameObject.transform.name;
		string tSplitText = "";
		int tCharI = 0;
		foreach (char tChar in name)
		{
			if (tCharI == 0)
			{
				tSplitText += tChar.ToString();
			}
			else
			{
				if (char.IsUpper(tChar))
				{
					tSplitText += " ";
				}
				tSplitText += tChar.ToString();
			}
			tCharI++;
		}
		this.text.text = tSplitText;
	}

	// Token: 0x06002BF5 RID: 11253 RVA: 0x0015B18E File Offset: 0x0015938E
	public void click()
	{
		DebugConfig.switchOption(this._debug_option);
		this.checkButtonGraphics();
	}

	// Token: 0x06002BF6 RID: 11254 RVA: 0x0015B1A1 File Offset: 0x001593A1
	private void checkButtonGraphics()
	{
		if (DebugConfig.isOn(this._debug_option))
		{
			this.button.GetComponent<Image>().sprite = this.button_on;
			return;
		}
		this.button.GetComponent<Image>().sprite = this.button_off;
	}

	// Token: 0x040021C5 RID: 8645
	public Sprite button_on;

	// Token: 0x040021C6 RID: 8646
	public Sprite button_off;

	// Token: 0x040021C7 RID: 8647
	public Text text;

	// Token: 0x040021C8 RID: 8648
	public Image iconOn;

	// Token: 0x040021C9 RID: 8649
	public Button button;

	// Token: 0x040021CA RID: 8650
	private DebugOption _debug_option;
}
