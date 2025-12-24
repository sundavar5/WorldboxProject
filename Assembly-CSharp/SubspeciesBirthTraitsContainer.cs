using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000764 RID: 1892
public class SubspeciesBirthTraitsContainer : MonoBehaviour, ITraitsContainer<ActorTrait, ActorTraitButton>
{
	// Token: 0x06003C19 RID: 15385 RVA: 0x001A2EB1 File Offset: 0x001A10B1
	public IReadOnlyCollection<ActorTraitButton> getTraitButtons()
	{
		return base.GetComponentsInChildren<ActorTraitButton>();
	}
}
