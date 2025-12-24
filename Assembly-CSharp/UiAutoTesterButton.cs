using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000540 RID: 1344
public class UiAutoTesterButton : MonoBehaviour
{
	// Token: 0x06002BEA RID: 11242 RVA: 0x0015AE8A File Offset: 0x0015908A
	public void Awake()
	{
		this.button.onClick.AddListener(new UnityAction(this.click));
		this._tester_name = base.gameObject.transform.name;
	}

	// Token: 0x06002BEB RID: 11243 RVA: 0x0015AEBE File Offset: 0x001590BE
	public void Start()
	{
		this._tester_name = base.gameObject.transform.name;
		this.checkButtonGraphics();
	}

	// Token: 0x06002BEC RID: 11244 RVA: 0x0015AEDC File Offset: 0x001590DC
	private void OnEnable()
	{
		this.checkButtonGraphics();
	}

	// Token: 0x06002BED RID: 11245 RVA: 0x0015AEE4 File Offset: 0x001590E4
	private void OnValidate()
	{
		string name = base.gameObject.transform.name;
		string tSplitText = "";
		int tCharI = 0;
		bool tNextUpper = true;
		foreach (char tChar in name)
		{
			if (tNextUpper)
			{
				tChar = char.ToUpper(tChar);
				tNextUpper = false;
			}
			if (tCharI == 0)
			{
				tSplitText += tChar.ToString();
			}
			else
			{
				if (tChar == '_')
				{
					tChar = ' ';
					tNextUpper = true;
				}
				tSplitText += tChar.ToString();
			}
			tCharI++;
		}
		this.text.text = tSplitText;
	}

	// Token: 0x06002BEE RID: 11246 RVA: 0x0015AF78 File Offset: 0x00159178
	public void click()
	{
		AssetManager.loadAutoTester();
		if (World.world.auto_tester.active_tester == this._tester_name)
		{
			World.world.auto_tester.toggleAutoTester();
		}
		else
		{
			World.world.auto_tester.create(this._tester_name);
			World.world.auto_tester.gameObject.SetActive(true);
		}
		this.checkButtonGraphics();
		ScrollWindow.hideAllEvent(true);
	}

	// Token: 0x06002BEF RID: 11247 RVA: 0x0015AFF0 File Offset: 0x001591F0
	private void checkButtonGraphics()
	{
		if (World.world.auto_tester.active && World.world.auto_tester.active_tester == this._tester_name)
		{
			this.button.GetComponent<Image>().sprite = this.button_on;
			return;
		}
		this.button.GetComponent<Image>().sprite = this.button_off;
	}

	// Token: 0x040021C0 RID: 8640
	public Sprite button_on;

	// Token: 0x040021C1 RID: 8641
	public Sprite button_off;

	// Token: 0x040021C2 RID: 8642
	public Text text;

	// Token: 0x040021C3 RID: 8643
	public Button button;

	// Token: 0x040021C4 RID: 8644
	private string _tester_name;
}
