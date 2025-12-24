using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x0200021E RID: 542
public abstract class CoreSystemObject<TData> : NanoObject, ICoreObject, ILoadable<TData>, IFavoriteable where TData : BaseSystemData
{
	// Token: 0x17000101 RID: 257
	// (get) Token: 0x0600139C RID: 5020 RVA: 0x000D904C File Offset: 0x000D724C
	public virtual BaseSystemManager manager
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x000D9053 File Offset: 0x000D7253
	public bool isFavorite()
	{
		return this.data.favorite;
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x000D9065 File Offset: 0x000D7265
	public void switchFavorite()
	{
		this.data.favorite = !this.data.favorite;
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x000D908A File Offset: 0x000D728A
	public virtual void setFavorite(bool pState)
	{
		this.data.favorite = pState;
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x000D909D File Offset: 0x000D729D
	public virtual bool updateColor(ColorAsset pColor)
	{
		return false;
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x000D90A0 File Offset: 0x000D72A0
	public virtual void save()
	{
		this.data.save();
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x000D90B2 File Offset: 0x000D72B2
	public virtual void loadData(TData pData)
	{
		this.setData(pData);
		this.data.load();
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x000D90CB File Offset: 0x000D72CB
	public virtual void setData(TData pData)
	{
		this.data = pData;
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x000D90D4 File Offset: 0x000D72D4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override void setAlive(bool pValue)
	{
		this._alive = pValue;
		if (!pValue && this.data.died_time == 0.0)
		{
			this.data.died_time = World.world.getCurWorldTime();
		}
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x000D9120 File Offset: 0x000D7320
	public override bool hasDied()
	{
		return this.data.died_time > 0.0;
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x000D913D File Offset: 0x000D733D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual int getAge()
	{
		return Date.getYearsSince(this.data.created_time);
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x000D9154 File Offset: 0x000D7354
	public override double getFoundedTimestamp()
	{
		return this.data.created_time;
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000D9166 File Offset: 0x000D7366
	public bool isJustCreated()
	{
		return Math.Abs(this.data.created_time - World.world.getCurWorldTime()) <= 0.05000000074505806;
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x000D9196 File Offset: 0x000D7396
	public string getFoundedDate()
	{
		return Date.getDate(this.data.created_time);
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x000D91AD File Offset: 0x000D73AD
	public string getDiedDate()
	{
		return Date.getDate(this.data.died_time);
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x000D91C4 File Offset: 0x000D73C4
	public string getFoundedYear()
	{
		return Date.getYearDate(this.data.created_time);
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x000D91DB File Offset: 0x000D73DB
	public string getDiedYear()
	{
		return Date.getYearDate(this.data.died_time);
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x000D91F2 File Offset: 0x000D73F2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int getAgeMonths()
	{
		return Date.getMonthsSince(this.data.created_time);
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x060013AE RID: 5038 RVA: 0x000D9209 File Offset: 0x000D7409
	// (set) Token: 0x060013AF RID: 5039 RVA: 0x000D921B File Offset: 0x000D741B
	public override string name
	{
		get
		{
			return this.data.name;
		}
		protected set
		{
			this.data.name = value;
		}
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x000D9230 File Offset: 0x000D7430
	public override void trackName(bool pPostChange = false)
	{
		if (string.IsNullOrEmpty(this.data.name))
		{
			return;
		}
		if (pPostChange && (this.data.past_names == null || this.data.past_names.Count == 0))
		{
			return;
		}
		BaseSystemData baseSystemData = this.data;
		if (baseSystemData.past_names == null)
		{
			baseSystemData.past_names = new List<NameEntry>();
		}
		if (this.data.past_names.Count == 0)
		{
			NameEntry tNewEntry = new NameEntry(this.data.name, false, -1, this.data.created_time);
			this.data.past_names.Add(tNewEntry);
			return;
		}
		if (this.data.past_names.Last<NameEntry>().name == this.data.name)
		{
			return;
		}
		NameEntry tNewEntry2 = new NameEntry(this.data.name, this.data.custom_name);
		this.data.past_names.Add(tNewEntry2);
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x000D9367 File Offset: 0x000D7567
	public override void Dispose()
	{
		TData tdata = this.data;
		if (tdata != null)
		{
			tdata.Dispose();
		}
		this.data = default(TData);
		base.Dispose();
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000D9391 File Offset: 0x000D7591
	public string obsidian_name_id
	{
		get
		{
			return this.data.obsidian_name_id;
		}
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x000D93A3 File Offset: 0x000D75A3
	public sealed override long getID()
	{
		return this.data.id;
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x000D93B5 File Offset: 0x000D75B5
	public string getBirthday()
	{
		return Date.getDate(this.data.created_time);
	}

	// Token: 0x0400118F RID: 4495
	public TData data;
}
