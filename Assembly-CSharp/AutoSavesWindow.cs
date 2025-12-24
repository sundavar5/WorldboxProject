using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000739 RID: 1849
public class AutoSavesWindow : MonoBehaviour
{
	// Token: 0x06003ACD RID: 15053 RVA: 0x0019F4D2 File Offset: 0x0019D6D2
	private void OnEnable()
	{
		this.prepareList();
		this.prepareSaves();
	}

	// Token: 0x06003ACE RID: 15054 RVA: 0x0019F4E0 File Offset: 0x0019D6E0
	private void prepareSaves()
	{
		this._showQueue.Clear();
		using (ListPool<AutoSaveData> tDatas = AutoSaveManager.getAutoSaves())
		{
			for (int i = 0; i < tDatas.Count; i++)
			{
				AutoSaveData tData = tDatas[i];
				this._showQueue.Enqueue(tData);
			}
		}
	}

	// Token: 0x06003ACF RID: 15055 RVA: 0x0019F540 File Offset: 0x0019D740
	private void Update()
	{
		if (this._timer > 0f)
		{
			this._timer -= Time.deltaTime;
			return;
		}
		this._timer = 0.02f;
		this.showNextItemFromQueue();
	}

	// Token: 0x06003AD0 RID: 15056 RVA: 0x0019F574 File Offset: 0x0019D774
	private void showNextItemFromQueue()
	{
		if (this._showQueue.Count == 0)
		{
			return;
		}
		AutoSaveData tData = this._showQueue.Dequeue();
		this.renderMapElement(tData);
	}

	// Token: 0x06003AD1 RID: 15057 RVA: 0x0019F5A4 File Offset: 0x0019D7A4
	private void prepareList()
	{
		foreach (AutoSaveElement autoSaveElement in this.elements)
		{
			Object.Destroy(autoSaveElement.gameObject);
		}
		this.elements.Clear();
	}

	// Token: 0x06003AD2 RID: 15058 RVA: 0x0019F604 File Offset: 0x0019D804
	private void renderMapElement(AutoSaveData pData)
	{
		AutoSaveElement tElement = Object.Instantiate<AutoSaveElement>(this._element_prefab, this._elements_parent.transform);
		this.elements.Add(tElement);
		tElement.load(pData);
	}

	// Token: 0x04002B7E RID: 11134
	[SerializeField]
	private AutoSaveElement _element_prefab;

	// Token: 0x04002B7F RID: 11135
	private List<AutoSaveElement> elements = new List<AutoSaveElement>();

	// Token: 0x04002B80 RID: 11136
	private Queue<AutoSaveData> _showQueue = new Queue<AutoSaveData>();

	// Token: 0x04002B81 RID: 11137
	[SerializeField]
	private VerticalLayoutGroup _elements_parent;

	// Token: 0x04002B82 RID: 11138
	private float _timer;
}
