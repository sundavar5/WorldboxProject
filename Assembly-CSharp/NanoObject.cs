using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

// Token: 0x02000229 RID: 553
public class NanoObject : IComparable<NanoObject>, IEquatable<NanoObject>, IDisposable
{
	// Token: 0x17000123 RID: 291
	// (get) Token: 0x060014AE RID: 5294 RVA: 0x000DBF48 File Offset: 0x000DA148
	protected virtual MetaType meta_type
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x060014AF RID: 5295 RVA: 0x000DBF5A File Offset: 0x000DA15A
	// (set) Token: 0x060014B0 RID: 5296 RVA: 0x000DBF62 File Offset: 0x000DA162
	public double created_time_unscaled { get; set; }

	// Token: 0x060014B1 RID: 5297 RVA: 0x000DBF6B File Offset: 0x000DA16B
	public NanoObject()
	{
		this.setDefaultValues();
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000DBF79 File Offset: 0x000DA179
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void revive()
	{
		this.setDefaultValues();
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x000DBF81 File Offset: 0x000DA181
	protected virtual void setDefaultValues()
	{
		this.exists = true;
		this._alive = true;
		this.stats_dirty_version = 0;
		this.created_time_unscaled = 0.0;
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x000DBFA7 File Offset: 0x000DA1A7
	public virtual long getID()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x060014B5 RID: 5301 RVA: 0x000DBFB9 File Offset: 0x000DA1B9
	[JsonProperty(Order = -1)]
	public long id
	{
		get
		{
			return this.getID();
		}
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x000DBFC1 File Offset: 0x000DA1C1
	public virtual string getType()
	{
		return this.meta_type.AsString();
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x000DBFCE File Offset: 0x000DA1CE
	public virtual MetaType getMetaType()
	{
		return this.meta_type;
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x000DBFD6 File Offset: 0x000DA1D6
	public MetaTypeAsset getMetaTypeAsset()
	{
		return this.meta_type.getAsset();
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x000DBFE4 File Offset: 0x000DA1E4
	public virtual string getTypeID()
	{
		return this.getType() + "_" + this.getID().ToString();
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x060014BA RID: 5306 RVA: 0x000DC00F File Offset: 0x000DA20F
	// (set) Token: 0x060014BB RID: 5307 RVA: 0x000DC021 File Offset: 0x000DA221
	public virtual string name
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
		protected set
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000DC033 File Offset: 0x000DA233
	public void setName(string pName, bool pTrack = true)
	{
		if (pTrack)
		{
			this.trackName(false);
		}
		this.name = pName;
		if (pTrack)
		{
			this.trackName(true);
		}
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x000DC050 File Offset: 0x000DA250
	public virtual void trackName(bool pPostChange = false)
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000DC062 File Offset: 0x000DA262
	public virtual ColorAsset getColor()
	{
		return null;
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000DC065 File Offset: 0x000DA265
	public virtual double getFoundedTimestamp()
	{
		return 0.0;
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000DC070 File Offset: 0x000DA270
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isAlive()
	{
		return this._alive;
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x000DC078 File Offset: 0x000DA278
	public virtual bool hasDied()
	{
		throw new NotImplementedException(base.GetType().Name);
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000DC08A File Offset: 0x000DA28A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual void setAlive(bool pValue)
	{
		this._alive = pValue;
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000DC093 File Offset: 0x000DA293
	public int getStatsDirtyVersion()
	{
		return this.stats_dirty_version;
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x000DC09B File Offset: 0x000DA29B
	public void setHash(int pHash)
	{
		this._hashcode = pHash;
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x000DC0A4 File Offset: 0x000DA2A4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(NanoObject pObject)
	{
		return this._hashcode == pObject.GetHashCode();
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x000DC0B4 File Offset: 0x000DA2B4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int CompareTo(NanoObject pTarget)
	{
		return this.GetHashCode().CompareTo(pTarget.GetHashCode());
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x000DC0D5 File Offset: 0x000DA2D5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int GetHashCode()
	{
		return this._hashcode;
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000DC0DD File Offset: 0x000DA2DD
	public virtual void Dispose()
	{
	}

	// Token: 0x040011CD RID: 4557
	protected bool _alive;

	// Token: 0x040011CE RID: 4558
	public bool exists;

	// Token: 0x040011CF RID: 4559
	protected int _hashcode;

	// Token: 0x040011D0 RID: 4560
	protected int stats_dirty_version;
}
