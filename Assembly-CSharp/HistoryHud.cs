using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C3 RID: 1731
public class HistoryHud : MonoBehaviour
{
	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06003774 RID: 14196 RVA: 0x00190934 File Offset: 0x0018EB34
	// (set) Token: 0x06003775 RID: 14197 RVA: 0x00190981 File Offset: 0x0018EB81
	public bool raycastOn
	{
		get
		{
			return !MoveCamera.camera_drag_run && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2) && !MapBox.controlsLocked() && !MapBox.isControllingUnit() && !World.world.isAnyPowerSelected() && HistoryHud._raycast_on;
		}
		set
		{
			HistoryHud._raycast_on = value;
		}
	}

	// Token: 0x06003776 RID: 14198 RVA: 0x0019098C File Offset: 0x0018EB8C
	private void Awake()
	{
		HistoryHud.instance = this;
		this._content_group = base.transform.Find("Scroll View/Viewport/Content");
		this._parked_group = base.transform.Find("Scroll View/Viewport/Parked");
		this._parked_items = new ObjectPoolGenericMono<HistoryHudItem>(this._template_obj.GetComponent<HistoryHudItem>(), this._parked_group);
	}

	// Token: 0x06003777 RID: 14199 RVA: 0x001909E7 File Offset: 0x0018EBE7
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this._content_group.gameObject.GetComponent<RectTransform>().SetTop(0f);
		this._content_group.gameObject.GetComponent<RectTransform>().SetLeft(0f);
	}

	// Token: 0x06003778 RID: 14200 RVA: 0x00190A25 File Offset: 0x0018EC25
	private void OnDisable()
	{
		this.Clear();
	}

	// Token: 0x06003779 RID: 14201 RVA: 0x00190A2D File Offset: 0x0018EC2D
	private void Update()
	{
		this.checkEnabled();
		if (this._recalc)
		{
			this._recalc = false;
			this.recalcPositions();
		}
	}

	// Token: 0x0600377A RID: 14202 RVA: 0x00190A4C File Offset: 0x0018EC4C
	public static void disableRaycasts()
	{
		HistoryHud.instance.raycastOn = false;
	}

	// Token: 0x0600377B RID: 14203 RVA: 0x00190A59 File Offset: 0x0018EC59
	public static void enableRaycasts()
	{
		HistoryHud.instance.raycastOn = true;
	}

	// Token: 0x0600377C RID: 14204 RVA: 0x00190A68 File Offset: 0x0018EC68
	private float recalcPositions()
	{
		if (this._history_items.Count == 0)
		{
			return 0f;
		}
		float tNewBottom = 0f;
		float tMaxBottom = 0f;
		int tItemsToRemove = 0;
		if (this._history_items.Count > 10)
		{
			tItemsToRemove = this._history_items.Count - 10;
		}
		int i = 0;
		while (i < this._history_items.Count)
		{
			if (tItemsToRemove > 0)
			{
				if (this._history_items[i].target_bottom != (float)(tItemsToRemove * -15))
				{
					this._history_items[i].moveToAndDestroy((float)(tItemsToRemove * -15));
				}
				tItemsToRemove--;
				goto IL_BF;
			}
			if (!this._history_items[i].isRemoving())
			{
				if (this._history_items[i].target_bottom != tNewBottom)
				{
					this._history_items[i].moveTo(tNewBottom);
				}
				tNewBottom += 15f;
				goto IL_BF;
			}
			IL_DC:
			i++;
			continue;
			IL_BF:
			tMaxBottom = -this._history_items[i].GetComponent<RectTransform>().offsetMax.y;
			goto IL_DC;
		}
		if (tMaxBottom >= tNewBottom)
		{
			return tMaxBottom + 15f;
		}
		return tNewBottom;
	}

	// Token: 0x0600377D RID: 14205 RVA: 0x00190B74 File Offset: 0x0018ED74
	private bool checkEnabled()
	{
		if (!PlayerConfig.optionBoolEnabled("history_log"))
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			return false;
		}
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		return true;
	}

	// Token: 0x0600377E RID: 14206 RVA: 0x00190BC2 File Offset: 0x0018EDC2
	public void newHistory(WorldLogMessage pMessage)
	{
		if (!this.checkEnabled())
		{
			return;
		}
		this.newText(pMessage);
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600377F RID: 14207 RVA: 0x00190BE0 File Offset: 0x0018EDE0
	public void makeInactive(HistoryHudItem historyItem)
	{
		this._parked_items.resetParent(historyItem);
		this._parked_items.release(historyItem, true);
		this._history_items.Remove(historyItem);
		this._recalc = true;
	}

	// Token: 0x06003780 RID: 14208 RVA: 0x00190C10 File Offset: 0x0018EE10
	public void Clear()
	{
		for (int i = this._history_items.Count - 1; i >= 0; i--)
		{
			this.makeInactive(this._history_items[i]);
		}
	}

	// Token: 0x06003781 RID: 14209 RVA: 0x00190C48 File Offset: 0x0018EE48
	private void newText(WorldLogMessage pMessage)
	{
		HistoryHudItem tObj = this._parked_items.getNext();
		tObj.transform.SetParent(this._content_group);
		tObj.gameObject.name = "HistoryItem " + (this._history_items.Count + 1).ToString();
		tObj.gameObject.SetActive(true);
		RectTransform component = tObj.GetComponent<RectTransform>();
		component.localScale = Vector3.one;
		component.localPosition = Vector3.zero;
		component.SetLeft(0f);
		float newBottom = this.recalcPositions();
		component.SetTop(newBottom);
		component.sizeDelta = new Vector2(component.sizeDelta.x, 15f);
		tObj.target_bottom = newBottom;
		tObj.setMessage(pMessage);
		this._history_items.Add(tObj);
		this._recalc = true;
	}

	// Token: 0x0400291C RID: 10524
	public static HistoryHud instance;

	// Token: 0x0400291D RID: 10525
	[SerializeField]
	private GameObject _template_obj;

	// Token: 0x0400291E RID: 10526
	private List<HistoryHudItem> _history_items = new List<HistoryHudItem>(10);

	// Token: 0x0400291F RID: 10527
	private ObjectPoolGenericMono<HistoryHudItem> _parked_items;

	// Token: 0x04002920 RID: 10528
	private Transform _content_group;

	// Token: 0x04002921 RID: 10529
	private Transform _parked_group;

	// Token: 0x04002922 RID: 10530
	private const int HISTORY_ITEM_SIZE = 15;

	// Token: 0x04002923 RID: 10531
	private const int MAX_HISTORY_ITEMS = 10;

	// Token: 0x04002924 RID: 10532
	private const float START_POSITION = 0f;

	// Token: 0x04002925 RID: 10533
	private bool _recalc;

	// Token: 0x04002926 RID: 10534
	private static bool _raycast_on = true;
}
