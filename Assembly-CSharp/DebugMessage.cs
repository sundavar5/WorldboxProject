using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200052F RID: 1327
public class DebugMessage : MonoBehaviour
{
	// Token: 0x06002B64 RID: 11108 RVA: 0x001584AF File Offset: 0x001566AF
	private void Start()
	{
		DebugMessage.instance = this;
		this.list = new List<DebugMessageFly>();
	}

	// Token: 0x06002B65 RID: 11109 RVA: 0x001584C4 File Offset: 0x001566C4
	public void moveAll(DebugMessageFly pMessage)
	{
		this.messagesToMove.Clear();
		foreach (DebugMessageFly tMessage in this.list)
		{
			if (!(tMessage == pMessage) && Toolbox.Dist(0f, tMessage.transform.localPosition.y, 0f, pMessage.transform.localPosition.y) < 1f)
			{
				this.messagesToMove.Add(tMessage);
			}
		}
		foreach (DebugMessageFly debugMessageFly in this.messagesToMove)
		{
			debugMessageFly.moveUp();
		}
	}

	// Token: 0x06002B66 RID: 11110 RVA: 0x001585A8 File Offset: 0x001567A8
	public DebugMessageFly getOldMessage(Transform pTransform)
	{
		foreach (DebugMessageFly tMessage in this.list)
		{
			if (tMessage.originTransform == pTransform)
			{
				return tMessage;
			}
		}
		return null;
	}

	// Token: 0x06002B67 RID: 11111 RVA: 0x0015860C File Offset: 0x0015680C
	public static void log(Transform pTransofrm, string pMessage)
	{
		if (!Debug.isDebugBuild)
		{
			return;
		}
		if (!DebugMessage.log_enabled)
		{
			return;
		}
		DebugMessageFly tOldMsg = DebugMessage.instance.getOldMessage(pTransofrm);
		if (tOldMsg != null)
		{
			tOldMsg.addString(pMessage);
			return;
		}
		TextMesh component = Object.Instantiate<GameObject>(DebugMessage.instance.prefab).gameObject.GetComponent<TextMesh>();
		component.gameObject.GetComponent<MeshRenderer>().sortingOrder = 100;
		component.transform.parent = DebugMessage.instance.transform;
		DebugMessageFly tMsg = component.GetComponent<DebugMessageFly>();
		tMsg.originTransform = pTransofrm;
		tMsg.addString(pMessage);
		DebugMessage.instance.list.Add(tMsg);
	}

	// Token: 0x04002081 RID: 8321
	public GameObject prefab;

	// Token: 0x04002082 RID: 8322
	public static bool log_enabled;

	// Token: 0x04002083 RID: 8323
	public static DebugMessage instance;

	// Token: 0x04002084 RID: 8324
	public List<DebugMessageFly> list;

	// Token: 0x04002085 RID: 8325
	private List<DebugMessageFly> messagesToMove = new List<DebugMessageFly>();
}
