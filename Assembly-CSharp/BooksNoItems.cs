using System;
using UnityEngine;

// Token: 0x02000648 RID: 1608
public class BooksNoItems : MonoBehaviour
{
	// Token: 0x0600345B RID: 13403 RVA: 0x001859B7 File Offset: 0x00183BB7
	private void Awake()
	{
		this._inner = base.transform.GetChild(0).gameObject;
		this._books_window = base.GetComponentInParent<IBooksWindow>();
	}

	// Token: 0x0600345C RID: 13404 RVA: 0x001859DC File Offset: 0x00183BDC
	private void OnEnable()
	{
		this._inner.SetActive(!this._books_window.hasBooks());
	}

	// Token: 0x0400277C RID: 10108
	private GameObject _inner;

	// Token: 0x0400277D RID: 10109
	private IBooksWindow _books_window;
}
