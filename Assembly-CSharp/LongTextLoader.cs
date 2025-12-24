using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000773 RID: 1907
public class LongTextLoader : MonoBehaviour
{
	// Token: 0x06003C67 RID: 15463 RVA: 0x001A39A2 File Offset: 0x001A1BA2
	private void Start()
	{
		this.m_text = base.GetComponent<Text>();
		this.create();
		this.finish();
	}

	// Token: 0x06003C68 RID: 15464 RVA: 0x001A39BC File Offset: 0x001A1BBC
	private void finish()
	{
		RectTransform tRect = this.m_text.GetComponent<RectTransform>();
		tRect.sizeDelta = new Vector2(tRect.sizeDelta.x, this.m_text.preferredHeight + 10f);
		RectTransform component = base.transform.parent.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.x, tRect.sizeDelta.y);
		float tLocPos = -component.transform.localPosition.y;
		component.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, tRect.sizeDelta.y + 20f + tLocPos);
	}

	// Token: 0x06003C69 RID: 15465 RVA: 0x001A3A6C File Offset: 0x001A1C6C
	public virtual void create()
	{
		try
		{
			this.m_text.text = this.textAsset.text;
		}
		catch (Exception)
		{
			Debug.LogError("LongTextLoader: Text File is too long");
		}
	}

	// Token: 0x04002BE1 RID: 11233
	public TextAsset textAsset;

	// Token: 0x04002BE2 RID: 11234
	protected Text m_text;
}
