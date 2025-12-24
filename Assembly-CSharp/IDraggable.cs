using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000568 RID: 1384
public interface IDraggable : IEndDragHandler, IEventSystemHandler
{
	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06002D23 RID: 11555
	Transform transform { get; }

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06002D24 RID: 11556
	bool spawn_particles_on_drag { get; }

	// Token: 0x06002D25 RID: 11557 RVA: 0x00160799 File Offset: 0x0015E999
	bool HasComponent<T>()
	{
		return this.transform.HasComponent<T>();
	}

	// Token: 0x06002D26 RID: 11558
	void KillDrag();
}
