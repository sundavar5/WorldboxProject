using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200069F RID: 1695
public class EasterEggBanner : MonoBehaviour
{
	// Token: 0x06003651 RID: 13905 RVA: 0x0018B424 File Offset: 0x00189624
	private void OnEnable()
	{
		this.nextChance();
	}

	// Token: 0x06003652 RID: 13906 RVA: 0x0018B42C File Offset: 0x0018962C
	private void nextChance()
	{
		bool tShow = Randy.randomChance(0.1f + this._cur_random_accumulation);
		if (!tShow)
		{
			this._cur_random_accumulation += 0.01f;
		}
		else
		{
			this._cur_random_accumulation = 0f;
		}
		this._container_with_elements.SetActive(tShow);
	}

	// Token: 0x06003653 RID: 13907 RVA: 0x0018B479 File Offset: 0x00189679
	private void clearChance()
	{
		this._cur_random_accumulation = 0f;
		this._container_with_elements.SetActive(false);
	}

	// Token: 0x06003654 RID: 13908 RVA: 0x0018B494 File Offset: 0x00189694
	private void Update()
	{
		if (!this._container_with_elements.activeSelf)
		{
			return;
		}
		bool tIsDraggingItem = Config.isDraggingItem();
		bool flag = tIsDraggingItem;
		bool? dragging_item = this._dragging_item;
		if (flag == dragging_item.GetValueOrDefault() & dragging_item != null)
		{
			return;
		}
		this._dragging_item = new bool?(tIsDraggingItem);
		if (!tIsDraggingItem)
		{
			this.clearChance();
		}
	}

	// Token: 0x0400283C RID: 10300
	[SerializeField]
	private GameObject _container_with_elements;

	// Token: 0x0400283D RID: 10301
	private float _cur_random_accumulation;

	// Token: 0x0400283E RID: 10302
	private const float BASE_CHANCE = 0.1f;

	// Token: 0x0400283F RID: 10303
	private const float ACCUMULATION_STEP = 0.01f;

	// Token: 0x04002840 RID: 10304
	private bool? _dragging_item;

	// Token: 0x04002841 RID: 10305
	public Image main_image;
}
