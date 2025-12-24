using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000530 RID: 1328
public class DebugMessageFly : MonoBehaviour
{
	// Token: 0x06002B69 RID: 11113 RVA: 0x001586BD File Offset: 0x001568BD
	private void Awake()
	{
		this.textMesh = base.GetComponent<TextMesh>();
	}

	// Token: 0x06002B6A RID: 11114 RVA: 0x001586CC File Offset: 0x001568CC
	public void addString(string pText)
	{
		if (this.textMesh.color.a < 0.3f)
		{
			this.listString.Clear();
		}
		else if (this.listString.Count > 20)
		{
			this.listString.RemoveAt(0);
		}
		this.listString.Add(pText);
		Vector3 pos = new Vector3(this.originTransform.localPosition.x, this.originTransform.localPosition.y);
		base.transform.localPosition = pos;
		string tNewString = "";
		foreach (string tString in this.listString)
		{
			tNewString = tNewString + tString + "\n";
		}
		this.textMesh.text = tNewString;
		Color tC = this.textMesh.color;
		tC.a = 1f;
		this.textMesh.color = tC;
	}

	// Token: 0x06002B6B RID: 11115 RVA: 0x001587DC File Offset: 0x001569DC
	public void moveUp()
	{
		Vector3 tV = base.transform.localPosition;
		tV.y += 3f;
		base.transform.localPosition = tV;
	}

	// Token: 0x06002B6C RID: 11116 RVA: 0x00158814 File Offset: 0x00156A14
	private void Update()
	{
		Vector3 tS = base.transform.localScale;
		tS.x += 2f * Time.deltaTime;
		if (tS.x > 1f)
		{
			tS.x = 1f;
		}
		base.transform.localScale = tS;
		Vector3 tV = base.transform.localPosition;
		tV.y += 0.5f * Time.deltaTime;
		base.transform.localPosition = tV;
		Color tC = this.textMesh.color;
		tC.a -= 0.3f * Time.deltaTime;
		this.textMesh.color = tC;
		if (tC.a <= 0f)
		{
			Object.Destroy(base.gameObject);
			DebugMessage.instance.list.Remove(this);
		}
	}

	// Token: 0x04002086 RID: 8326
	private List<string> listString = new List<string>();

	// Token: 0x04002087 RID: 8327
	public Transform originTransform;

	// Token: 0x04002088 RID: 8328
	private TextMesh textMesh;
}
