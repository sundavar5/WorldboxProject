using System;
using UnityEngine;

// Token: 0x02000459 RID: 1113
public static class GameObjectExtensions
{
	// Token: 0x06002634 RID: 9780 RVA: 0x001388C0 File Offset: 0x00136AC0
	public static T AddOrGetComponent<T>(this GameObject pGameObject) where T : Component
	{
		T tOutComponent;
		if (!pGameObject.TryGetComponent<T>(out tOutComponent))
		{
			return pGameObject.AddComponent<T>();
		}
		return tOutComponent;
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x001388E0 File Offset: 0x00136AE0
	public static bool HasComponent<T>(this GameObject pGameObject)
	{
		T t;
		return pGameObject.TryGetComponent<T>(out t);
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x001388F5 File Offset: 0x00136AF5
	public static bool HasComponent<T>(this Component pComponent)
	{
		return pComponent.gameObject.HasComponent<T>();
	}

	// Token: 0x06002637 RID: 9783 RVA: 0x00138902 File Offset: 0x00136B02
	public static T AddComponent<T>(this Component pComponent) where T : Component
	{
		return pComponent.gameObject.AddComponent<T>();
	}
}
