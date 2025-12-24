using System;
using UnityEngine;

// Token: 0x02000759 RID: 1881
public class SelectedMetaWithUnit<TMeta, TMetaData> : SelectedMeta<TMeta, TMetaData>, ISelectedMetaWithUnit where TMeta : MetaObject<TMetaData>, IFavoriteable where TMetaData : MetaObjectData
{
	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06003B8C RID: 15244 RVA: 0x001A0F15 File Offset: 0x0019F115
	public SelectedMetaUnitElement unit_element
	{
		get
		{
			return this._unit_element;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06003B8D RID: 15245 RVA: 0x001A0F1D File Offset: 0x0019F11D
	public GameObject unit_element_separator
	{
		get
		{
			return this._unit_element_separator;
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06003B8E RID: 15246 RVA: 0x001A0F25 File Offset: 0x0019F125
	private ISelectedMetaWithUnit as_meta_with_unit
	{
		get
		{
			return this;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06003B8F RID: 15247 RVA: 0x001A0F28 File Offset: 0x0019F128
	// (set) Token: 0x06003B90 RID: 15248 RVA: 0x001A0F30 File Offset: 0x0019F130
	public int last_dirty_stats_unit { get; set; }

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x06003B91 RID: 15249 RVA: 0x001A0F39 File Offset: 0x0019F139
	// (set) Token: 0x06003B92 RID: 15250 RVA: 0x001A0F41 File Offset: 0x0019F141
	public Actor last_unit { get; set; }

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x06003B93 RID: 15251 RVA: 0x001A0F4A File Offset: 0x0019F14A
	public virtual string unit_title_locale_key { get; }

	// Token: 0x06003B94 RID: 15252 RVA: 0x001A0F52 File Offset: 0x0019F152
	public virtual bool hasUnit()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003B95 RID: 15253 RVA: 0x001A0F59 File Offset: 0x0019F159
	public virtual Actor getUnit()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003B96 RID: 15254 RVA: 0x001A0F60 File Offset: 0x0019F160
	protected override void updateElementsAlways(TMeta pNano)
	{
		base.updateElementsAlways(pNano);
		this.as_meta_with_unit.checkUnitElement();
		if (this.hasUnit())
		{
			this._unit_element.updateBarAndTask(this.getUnit());
		}
	}

	// Token: 0x06003B97 RID: 15255 RVA: 0x001A0F90 File Offset: 0x0019F190
	protected override void showStatsGeneral(TMeta pMeta)
	{
		base.showStatsGeneral(pMeta);
		if (this.hasUnit())
		{
			Actor tActor = this.getUnit();
			this._unit_element.showStats(tActor);
		}
	}

	// Token: 0x06003B98 RID: 15256 RVA: 0x001A0FBF File Offset: 0x0019F1BF
	public void avatarTouchScream()
	{
		this.as_meta_with_unit.avatarTouch();
	}

	// Token: 0x06003B99 RID: 15257 RVA: 0x001A0FCC File Offset: 0x0019F1CC
	protected override void clearLastObject()
	{
		base.clearLastObject();
		this.as_meta_with_unit.clearLastUnit();
	}

	// Token: 0x04002BAC RID: 11180
	[SerializeField]
	private SelectedMetaUnitElement _unit_element;

	// Token: 0x04002BAD RID: 11181
	[SerializeField]
	private GameObject _unit_element_separator;
}
