using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005F7 RID: 1527
public class SaveSlotManager : MonoBehaviour
{
	// Token: 0x060031DF RID: 12767 RVA: 0x0017C628 File Offset: 0x0017A828
	private void Init()
	{
		SaveManager.clearCurrentSelectedWorld();
		int tOffsetX = 65;
		int tOffsetY = 65;
		int yy = 0;
		int tID = 1;
		int tRows = 10;
		for (int i = 0; i < tRows; i++)
		{
			GameObject tButton = Object.Instantiate<GameObject>(this.slotButtonPrefab, this.buttonsContainer.transform);
			tButton.transform.localPosition = new Vector3((float)(-(float)tOffsetX), (float)(-(float)yy * tOffsetY));
			this.setID(tButton, tID++);
			tButton = Object.Instantiate<GameObject>(this.slotButtonPrefab, this.buttonsContainer.transform);
			tButton.transform.localPosition = new Vector3(0f, (float)(-(float)yy * tOffsetY));
			this.setID(tButton, tID++);
			tButton = Object.Instantiate<GameObject>(this.slotButtonPrefab, this.buttonsContainer.transform);
			tButton.transform.localPosition = new Vector3((float)tOffsetX, (float)(-(float)yy * tOffsetY));
			this.setID(tButton, tID++);
			yy++;
		}
		this.content.sizeDelta = new Vector2(0f, (float)(tRows * tOffsetY));
	}

	// Token: 0x060031E0 RID: 12768 RVA: 0x0017C736 File Offset: 0x0017A936
	private void OnEnable()
	{
		this.loaded = false;
		this.Init();
	}

	// Token: 0x060031E1 RID: 12769 RVA: 0x0017C748 File Offset: 0x0017A948
	private void Update()
	{
		foreach (LevelPreviewButton tSlot in this.previews)
		{
			if (!tSlot.loaded && !tSlot.loading)
			{
				tSlot.reloadImage();
				break;
			}
		}
	}

	// Token: 0x060031E2 RID: 12770 RVA: 0x0017C7AC File Offset: 0x0017A9AC
	private void OnDisable()
	{
		for (int i = 0; i < this.containers.Count; i++)
		{
			this.previews[i].checkTextureDestroy();
			Object.Destroy(this.containers[i]);
			this.containers[i] = null;
		}
		this.previews.Clear();
		this.containers.Clear();
	}

	// Token: 0x060031E3 RID: 12771 RVA: 0x0017C814 File Offset: 0x0017AA14
	private void setID(GameObject pContainer, int pID)
	{
		Transform tButton = pContainer.transform.Find("AnimationContainer/Mask/SizeContainer/Button");
		tButton.GetComponent<SlotButtonCallback>().slotID = pID;
		tButton.GetComponent<LevelPreviewButton>().loaded = false;
		tButton.GetComponent<LevelPreviewButton>().worldNetUpload = this.worldNetUpload;
		if (pID > 1)
		{
			tButton.GetComponent<LevelPreviewButton>().premiumOnly = true;
		}
		this.previews.Add(tButton.GetComponent<LevelPreviewButton>());
		this.containers.Add(pContainer);
	}

	// Token: 0x040025CE RID: 9678
	public GameObject buttonsContainer;

	// Token: 0x040025CF RID: 9679
	private List<LevelPreviewButton> previews = new List<LevelPreviewButton>();

	// Token: 0x040025D0 RID: 9680
	private List<GameObject> containers = new List<GameObject>();

	// Token: 0x040025D1 RID: 9681
	public GameObject slotButtonPrefab;

	// Token: 0x040025D2 RID: 9682
	public RectTransform content;

	// Token: 0x040025D3 RID: 9683
	private Vector3 originalPos;

	// Token: 0x040025D4 RID: 9684
	public bool loaded;

	// Token: 0x040025D5 RID: 9685
	public bool worldNetUpload;
}
