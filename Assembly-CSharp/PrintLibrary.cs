using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class PrintLibrary : MonoBehaviour
{
	// Token: 0x06001AC4 RID: 6852 RVA: 0x000F9FC4 File Offset: 0x000F81C4
	private void Awake()
	{
		PrintLibrary._instance = this;
		for (int i = 0; i < this.list.Count; i++)
		{
			PrintTemplate tTemp = this.list[i];
			this.calcSteps(tTemp);
			this._dict.Add(tTemp.name, tTemp);
			if (tTemp.name.Contains("quake"))
			{
				this._list_quakes.Add(tTemp);
				this.addRotatedQuake(tTemp, 90);
				this.addRotatedQuake(tTemp, 180);
				this.addRotatedQuake(tTemp, 360);
				this.addRotatedQuake(tTemp, -360);
				this.addRotatedQuake(tTemp, -90);
				this.addRotatedQuake(tTemp, -180);
				this.addRotatedQuake(tTemp, -270);
			}
		}
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x000FA088 File Offset: 0x000F8288
	private void addRotatedQuake(PrintTemplate pOrigin, int pRotation)
	{
		PrintTemplate tTemplate = new PrintTemplate();
		tTemplate.name = pOrigin.name + "_" + pRotation.ToString();
		Texture2D tTexture = Object.Instantiate<Texture2D>(pOrigin.graphics);
		tTemplate.graphics = TextureRotator.Rotate(tTexture, pRotation, new Color32(0, 0, 0, 0));
		this.calcSteps(tTemplate);
		this._list_quakes.Add(tTemplate);
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x000FA0F0 File Offset: 0x000F82F0
	private void calcSteps(PrintTemplate pPrint)
	{
		List<PrintStep> tSteps = new List<PrintStep>();
		int width = pPrint.graphics.width;
		int height = pPrint.graphics.height;
		for (int xx = 1; xx < width - 1; xx++)
		{
			for (int yy = 1; yy < height - 1; yy++)
			{
				Color tColor = pPrint.graphics.GetPixel(xx, yy);
				if (!(tColor == this._color_0))
				{
					PrintStep tStep = new PrintStep
					{
						x = xx - 1 - width / 2,
						y = yy - 1 - height / 2,
						action = 1
					};
					tSteps.Add(tStep);
					if (tColor == this._color_2)
					{
						tSteps.Add(tStep);
					}
					else if (tColor == this._color_3)
					{
						tSteps.Add(tStep);
						tSteps.Add(tStep);
					}
				}
			}
		}
		pPrint.steps = tSteps.ToArray();
		pPrint.steps_per_tick = (int)((float)pPrint.steps.Length * 0.005f + 1f);
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x000FA1FF File Offset: 0x000F83FF
	public static PrintTemplate getTemplate(string pTemplateID)
	{
		return PrintLibrary._instance._dict[pTemplateID];
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x000FA211 File Offset: 0x000F8411
	public static List<PrintTemplate> getQuakes()
	{
		return PrintLibrary._instance._list_quakes;
	}

	// Token: 0x040014C2 RID: 5314
	private readonly Color _color_0 = Toolbox.makeColor("#FFFFFF");

	// Token: 0x040014C3 RID: 5315
	private readonly Color _color_1 = Toolbox.makeColor("#CCCCCC");

	// Token: 0x040014C4 RID: 5316
	private readonly Color _color_2 = Toolbox.makeColor("#7F7F7F");

	// Token: 0x040014C5 RID: 5317
	private readonly Color _color_3 = Toolbox.makeColor("#000000");

	// Token: 0x040014C6 RID: 5318
	public List<PrintTemplate> list;

	// Token: 0x040014C7 RID: 5319
	private readonly Dictionary<string, PrintTemplate> _dict = new Dictionary<string, PrintTemplate>();

	// Token: 0x040014C8 RID: 5320
	private readonly List<PrintTemplate> _list_quakes = new List<PrintTemplate>();

	// Token: 0x040014C9 RID: 5321
	private static PrintLibrary _instance;
}
