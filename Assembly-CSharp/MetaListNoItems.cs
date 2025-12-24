using System;
using UnityEngine;

// Token: 0x020007DC RID: 2012
public class MetaListNoItems : MonoBehaviour
{
	// Token: 0x170003AD RID: 941
	// (get) Token: 0x06003F63 RID: 16227 RVA: 0x001B540E File Offset: 0x001B360E
	protected IMetaObject meta_object
	{
		get
		{
			return this._window.getCoreObject() as IMetaObject;
		}
	}

	// Token: 0x06003F64 RID: 16228 RVA: 0x001B5420 File Offset: 0x001B3620
	private void Awake()
	{
		this._inner = base.transform.GetChild(0).gameObject;
		this._window = base.GetComponentInParent<IMetaWindow>();
	}

	// Token: 0x06003F65 RID: 16229 RVA: 0x001B5445 File Offset: 0x001B3645
	private void OnEnable()
	{
		this._inner.SetActive(!this.hasMetas());
	}

	// Token: 0x06003F66 RID: 16230 RVA: 0x001B545B File Offset: 0x001B365B
	protected virtual bool hasMetas()
	{
		throw new NotImplementedException();
	}

	// Token: 0x04002E0B RID: 11787
	private GameObject _inner;

	// Token: 0x04002E0C RID: 11788
	private IMetaWindow _window;
}
