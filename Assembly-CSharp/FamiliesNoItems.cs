using System;
using UnityEngine;

// Token: 0x0200068D RID: 1677
public class FamiliesNoItems : MonoBehaviour
{
	// Token: 0x060035BD RID: 13757 RVA: 0x0018989F File Offset: 0x00187A9F
	private void Awake()
	{
		this._inner = base.transform.GetChild(0).gameObject;
		this._families_window = base.GetComponentInParent<IMetaWithFamiliesWindow>();
	}

	// Token: 0x060035BE RID: 13758 RVA: 0x001898C4 File Offset: 0x00187AC4
	private void OnEnable()
	{
		this._inner.SetActive(!this._families_window.hasFamilies());
	}

	// Token: 0x040027F8 RID: 10232
	private GameObject _inner;

	// Token: 0x040027F9 RID: 10233
	private IMetaWithFamiliesWindow _families_window;
}
