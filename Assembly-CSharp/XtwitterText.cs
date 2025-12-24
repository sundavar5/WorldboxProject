using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200061D RID: 1565
public class XtwitterText : MonoBehaviour
{
	// Token: 0x06003354 RID: 13140 RVA: 0x00182AF1 File Offset: 0x00180CF1
	private void Awake()
	{
		this._text = base.GetComponent<Text>();
	}

	// Token: 0x06003355 RID: 13141 RVA: 0x00182B00 File Offset: 0x00180D00
	private void Update()
	{
		this._timer -= Time.deltaTime;
		if (this._timer <= 0f)
		{
			this._timer = 2f;
			this._text.text = this._strings[this._index];
			this._index = (this._index + 1) % this._strings.Length;
			base.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f, 10, 1f);
		}
	}

	// Token: 0x040026E8 RID: 9960
	private Text _text;

	// Token: 0x040026E9 RID: 9961
	private string[] _strings = new string[]
	{
		"Twitter",
		"Xwitter",
		"??",
		"X?",
		"X??",
		"X???"
	};

	// Token: 0x040026EA RID: 9962
	private int _index;

	// Token: 0x040026EB RID: 9963
	private float _timer = 2f;

	// Token: 0x040026EC RID: 9964
	private const int INTERVAL = 2;

	// Token: 0x040026ED RID: 9965
	private Tweener _current_tween;
}
