using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
[Serializable]
public class BaseCategoryAsset : Asset, ILocalizedAsset
{
	// Token: 0x06000568 RID: 1384 RVA: 0x00052FBF File Offset: 0x000511BF
	public virtual string getLocaleID()
	{
		return this.name;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00052FC7 File Offset: 0x000511C7
	public Color getColor()
	{
		if (this._color == null)
		{
			this._color = new Color?(Toolbox.makeColor(this.color));
		}
		return this._color.Value;
	}

	// Token: 0x040005BF RID: 1471
	public string name;

	// Token: 0x040005C0 RID: 1472
	public string color;

	// Token: 0x040005C1 RID: 1473
	public bool show_counter = true;

	// Token: 0x040005C2 RID: 1474
	[NonSerialized]
	public Color? _color;
}
