using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000006 RID: 6
public class ChangeTextInHindi : MonoBehaviour
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
	private void Start()
	{
		string text = base.gameObject.GetComponent<Text>().text;
		base.gameObject.GetComponent<Text>().SetHindiText(text);
	}
}
