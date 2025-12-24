using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005CF RID: 1487
public class ButtonsViewer : MonoBehaviour
{
	// Token: 0x060030E8 RID: 12520 RVA: 0x00177DF8 File Offset: 0x00175FF8
	private void Start()
	{
		this.content = base.transform.parent;
		this.canvas = CanvasMain.instance.canvas_ui;
		this.buttons = new List<PowerButton>();
		int childCount = base.transform.childCount;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject tObject = base.transform.GetChild(i).gameObject;
			if (tObject.HasComponent<PowerButton>() && tObject.activeSelf)
			{
				this.buttons.Add(tObject.GetComponent<PowerButton>());
			}
			else if (!tObject.HasComponent<Image>() || !tObject.activeSelf)
			{
				Object.Destroy(tObject);
			}
		}
	}

	// Token: 0x060030E9 RID: 12521 RVA: 0x00177EA0 File Offset: 0x001760A0
	private void Update()
	{
		if (this.lastX == this.content.position.x && this.lastY == this.content.position.y)
		{
			return;
		}
		this.lastX = this.content.position.x;
		this.lastY = this.content.position.y;
		int yo = 0;
		int yo2 = 0;
		bool foundHidden = false;
		for (int i = 0; i < this.buttons.Count; i++)
		{
			PowerButton tButton = this.buttons[i];
			if (foundHidden)
			{
				yo2++;
				tButton.gameObject.SetActive(false);
			}
			else
			{
				yo++;
				Vector3[] v = new Vector3[4];
				tButton.rect_transform.GetWorldCorners(v);
				float maxX = Mathf.Max(new float[]
				{
					v[0].x,
					v[1].x,
					v[2].x,
					v[3].x
				});
				float minX = Mathf.Min(new float[]
				{
					v[0].x,
					v[1].x,
					v[2].x,
					v[3].x
				});
				if (maxX < 0f || minX > (float)Screen.width)
				{
					tButton.gameObject.SetActive(false);
					if (minX > (float)Screen.width)
					{
						foundHidden = true;
					}
				}
				else
				{
					tButton.gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x040024EA RID: 9450
	private List<PowerButton> buttons;

	// Token: 0x040024EB RID: 9451
	private Transform content;

	// Token: 0x040024EC RID: 9452
	private float lastX;

	// Token: 0x040024ED RID: 9453
	private float lastY;

	// Token: 0x040024EE RID: 9454
	private Canvas canvas;
}
