using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006C2 RID: 1730
public class HeaderContainer : MonoBehaviour, ILayoutController
{
	// Token: 0x0600376E RID: 14190 RVA: 0x00190774 File Offset: 0x0018E974
	private void Awake()
	{
		if (this.content == null)
		{
			this.content = this.content_transform.GetComponent<VerticalLayoutGroup>();
		}
		this._vertical_layout_group = base.GetComponent<VerticalLayoutGroup>();
		this._default_padding = this._vertical_layout_group.padding;
		this._layout_element = base.GetComponent<LayoutElement>();
		this._default_top_padding = this.content.padding.top;
	}

	// Token: 0x0600376F RID: 14191 RVA: 0x001907E0 File Offset: 0x0018E9E0
	public void SetLayoutVertical()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		int tFinalHeight = this._default_top_padding;
		if (!this.hasAnyElementActive())
		{
			if (this._layout_element.preferredHeight != 0f)
			{
				this._vertical_layout_group.padding = new RectOffset(0, 0, 0, 0);
				this._layout_element.preferredHeight = 0f;
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.header_transform);
			}
		}
		else
		{
			if (this._layout_element.preferredHeight >= 0f)
			{
				this._vertical_layout_group.padding = this._default_padding;
				this._layout_element.preferredHeight = -1f;
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.header_transform);
			}
			tFinalHeight += (int)this.header_transform.rect.height;
		}
		if (this.content.padding.top != tFinalHeight)
		{
			this.content.padding.top = tFinalHeight;
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.content_transform);
			base.StartCoroutine(this.toggleRunes());
		}
	}

	// Token: 0x06003770 RID: 14192 RVA: 0x001908DB File Offset: 0x0018EADB
	public void SetLayoutHorizontal()
	{
	}

	// Token: 0x06003771 RID: 14193 RVA: 0x001908DD File Offset: 0x0018EADD
	private IEnumerator toggleRunes()
	{
		yield return null;
		bool tHasAnyElementActive = this.hasAnyElementActive();
		this.runes_container.gameObject.SetActive(tHasAnyElementActive);
		if (!tHasAnyElementActive)
		{
			yield break;
		}
		this.runes_container.localPosition = new Vector2(this.runes_container.localPosition.x, -this.header_transform.rect.height);
		yield break;
	}

	// Token: 0x06003772 RID: 14194 RVA: 0x001908EC File Offset: 0x0018EAEC
	private bool hasAnyElementActive()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).gameObject.activeInHierarchy)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04002914 RID: 10516
	public RectTransform header_transform;

	// Token: 0x04002915 RID: 10517
	public RectTransform content_transform;

	// Token: 0x04002916 RID: 10518
	public RectTransform runes_container;

	// Token: 0x04002917 RID: 10519
	public VerticalLayoutGroup content;

	// Token: 0x04002918 RID: 10520
	private VerticalLayoutGroup _vertical_layout_group;

	// Token: 0x04002919 RID: 10521
	private LayoutElement _layout_element;

	// Token: 0x0400291A RID: 10522
	private int _default_top_padding;

	// Token: 0x0400291B RID: 10523
	private RectOffset _default_padding;
}
