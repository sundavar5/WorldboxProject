using System;
using System.Collections.Generic;
using db;
using UnityEngine;

// Token: 0x020006C0 RID: 1728
public class GraphTimeScaleContainer : MonoBehaviour
{
	// Token: 0x06003763 RID: 14179 RVA: 0x001903D8 File Offset: 0x0018E5D8
	public void calcBounds()
	{
		if (this._controller == null)
		{
			this._controller = base.GetComponentInParent<GraphController>();
		}
		this._available_time_scales.Clear();
		this._available_time_scales.Add(GraphTimeScale.year_10);
		foreach (NanoObject pObject in this._controller.getObjects())
		{
			using (ListPool<GraphTimeScale> tAvailableTimeScales = DBGetter.getTimeScales(pObject))
			{
				foreach (GraphTimeScale ptr in tAvailableTimeScales)
				{
					GraphTimeScale tScale = ptr;
					if (!this._available_time_scales.Contains(tScale))
					{
						this._available_time_scales.Add(tScale);
					}
				}
			}
		}
		bool tAnyTimeScalesAvailable = this._available_time_scales.Count > 1;
		ButtonGraphScalePlusMinus[] componentsInChildren = base.GetComponentsInChildren<ButtonGraphScalePlusMinus>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.SetActive(tAnyTimeScalesAvailable);
		}
	}

	// Token: 0x06003764 RID: 14180 RVA: 0x00190504 File Offset: 0x0018E704
	public bool resetTimeScale()
	{
		this.calcBounds();
		if (!this._available_time_scales.Contains(this.current_scale))
		{
			this.current_scale = this._available_time_scales.Last<GraphTimeScale>();
			return true;
		}
		return false;
	}

	// Token: 0x06003765 RID: 14181 RVA: 0x00190533 File Offset: 0x0018E733
	public void setTimeScale(GraphTimeScale pScale)
	{
		this.current_scale = pScale;
	}

	// Token: 0x06003766 RID: 14182 RVA: 0x0019053C File Offset: 0x0018E73C
	public ListPool<GraphTimeScale> sharedTimeScales()
	{
		ListPool<GraphTimeScale> tScales = new ListPool<GraphTimeScale>((GraphTimeScale[])Enum.GetValues(typeof(GraphTimeScale)));
		foreach (NanoObject tCurrentObject in this._controller.getObjects())
		{
			using (ListPool<GraphTimeScale> tAvailableTimeScales = DBGetter.getTimeScales(tCurrentObject))
			{
				tScales.RemoveAll((GraphTimeScale tScale) => !tAvailableTimeScales.Contains(tScale));
			}
		}
		return tScales;
	}

	// Token: 0x06003767 RID: 14183 RVA: 0x001905F0 File Offset: 0x0018E7F0
	public bool randomizeTimeScale()
	{
		if (this._available_time_scales.Count < 2)
		{
			return false;
		}
		bool result;
		using (ListPool<GraphTimeScale> tScales = this.sharedTimeScales())
		{
			if (tScales.Count == 0)
			{
				result = false;
			}
			else
			{
				if (tScales.Count > 2)
				{
					tScales.Shift<GraphTimeScale>();
				}
				GraphTimeScale tScale = tScales.GetRandom<GraphTimeScale>();
				if (tScale != this.current_scale)
				{
					this.current_scale = tScale;
					result = true;
				}
				else
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x06003768 RID: 14184 RVA: 0x0019066C File Offset: 0x0018E86C
	public void timeScaleMinus()
	{
		int currentScaleIndex = (int)this.current_scale;
		if (currentScaleIndex > 0)
		{
			this.current_scale = (GraphTimeScale)(currentScaleIndex - 1);
			return;
		}
		this.current_scale = (GraphTimeScale)(this._available_time_scales.Count - 1);
	}

	// Token: 0x06003769 RID: 14185 RVA: 0x001906A4 File Offset: 0x0018E8A4
	public void timeScalePlus()
	{
		int currentScaleIndex = (int)this.current_scale;
		if (currentScaleIndex < this._available_time_scales.Count - 1)
		{
			this.current_scale = currentScaleIndex + GraphTimeScale.year_25;
			return;
		}
		this.current_scale = GraphTimeScale.year_10;
	}

	// Token: 0x0600376A RID: 14186 RVA: 0x001906DC File Offset: 0x0018E8DC
	public string getIndexString()
	{
		if (this._available_time_scales.Count == 0)
		{
			return "";
		}
		return string.Concat(new string[]
		{
			" (",
			((int)(this.current_scale + 1)).ToString(),
			"/",
			this._available_time_scales.Count.ToString(),
			")"
		});
	}

	// Token: 0x0600376B RID: 14187 RVA: 0x00190748 File Offset: 0x0018E948
	public GraphTimeScale getCurrentScale()
	{
		return this.current_scale;
	}

	// Token: 0x0400290F RID: 10511
	public GraphTimeScale current_scale;

	// Token: 0x04002910 RID: 10512
	private List<GraphTimeScale> _available_time_scales = new List<GraphTimeScale>();

	// Token: 0x04002911 RID: 10513
	private GraphController _controller;
}
