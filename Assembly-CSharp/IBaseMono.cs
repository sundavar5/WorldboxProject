using System;
using UnityEngine;

// Token: 0x02000463 RID: 1123
public interface IBaseMono
{
	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06002655 RID: 9813
	Transform transform { get; }

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06002656 RID: 9814
	GameObject gameObject { get; }

	// Token: 0x06002657 RID: 9815
	T GetComponent<T>();

	// Token: 0x06002658 RID: 9816 RVA: 0x00139328 File Offset: 0x00137528
	T AddComponent<T>() where T : Component
	{
		return this.gameObject.AddComponent<T>();
	}

	// Token: 0x06002659 RID: 9817 RVA: 0x00139335 File Offset: 0x00137535
	bool HasComponent<T>() where T : Component
	{
		return this.gameObject.HasComponent<T>();
	}
}
