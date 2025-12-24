using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000604 RID: 1540
[RequireComponent(typeof(Button))]
public class ToggleObjects : MonoBehaviour
{
	// Token: 0x0600328F RID: 12943 RVA: 0x0017FBD3 File Offset: 0x0017DDD3
	private void Awake()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.toggle));
	}

	// Token: 0x06003290 RID: 12944 RVA: 0x0017FBF4 File Offset: 0x0017DDF4
	private void toggle()
	{
		if (this._elements == null)
		{
			return;
		}
		foreach (GameObject gameObject in this._elements)
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}

	// Token: 0x04002636 RID: 9782
	[SerializeField]
	private List<GameObject> _elements;
}
