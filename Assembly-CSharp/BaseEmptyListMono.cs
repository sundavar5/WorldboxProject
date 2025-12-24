using System;
using UnityEngine;

// Token: 0x020007CE RID: 1998
public class BaseEmptyListMono : MonoBehaviour
{
	// Token: 0x06003F02 RID: 16130 RVA: 0x001B4682 File Offset: 0x001B2882
	public void Awake()
	{
		this.rect_transform = base.GetComponent<RectTransform>();
	}

	// Token: 0x06003F03 RID: 16131 RVA: 0x001B4690 File Offset: 0x001B2890
	public void assignObject(NanoObject pObject)
	{
		this.meta_object = pObject;
		this.has_object = true;
	}

	// Token: 0x06003F04 RID: 16132 RVA: 0x001B46A0 File Offset: 0x001B28A0
	public void assignElement(MonoBehaviour pElement)
	{
		this.element = pElement;
		this.has_element = true;
	}

	// Token: 0x06003F05 RID: 16133 RVA: 0x001B46B0 File Offset: 0x001B28B0
	public bool hasElement()
	{
		return this.has_element;
	}

	// Token: 0x06003F06 RID: 16134 RVA: 0x001B46B8 File Offset: 0x001B28B8
	public void clearElement()
	{
		this.element = null;
		this.has_element = false;
	}

	// Token: 0x06003F07 RID: 16135 RVA: 0x001B46C8 File Offset: 0x001B28C8
	public void clearObject()
	{
		this.meta_object = null;
		this.has_object = false;
	}

	// Token: 0x06003F08 RID: 16136 RVA: 0x001B46D8 File Offset: 0x001B28D8
	public bool hasObject()
	{
		return this.has_object;
	}

	// Token: 0x06003F09 RID: 16137 RVA: 0x001B46E0 File Offset: 0x001B28E0
	public void debugUpdateName(bool tVisible)
	{
		if (string.IsNullOrEmpty(this.debug_original_name))
		{
			this.debug_original_name = base.gameObject.name;
		}
		if (tVisible)
		{
			base.gameObject.name = "(v) (" + base.gameObject.transform.childCount.ToString() + ") " + this.debug_original_name;
			return;
		}
		base.gameObject.name = "(i) (" + base.gameObject.transform.childCount.ToString() + ") " + this.debug_original_name;
	}

	// Token: 0x04002DDD RID: 11741
	internal NanoObject meta_object;

	// Token: 0x04002DDE RID: 11742
	internal MonoBehaviour element;

	// Token: 0x04002DDF RID: 11743
	private bool has_element;

	// Token: 0x04002DE0 RID: 11744
	private bool has_object;

	// Token: 0x04002DE1 RID: 11745
	public RectTransform rect_transform;

	// Token: 0x04002DE2 RID: 11746
	internal string debug_original_name;
}
