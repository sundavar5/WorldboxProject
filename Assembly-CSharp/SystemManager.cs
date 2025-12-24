using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

// Token: 0x0200022C RID: 556
public abstract class SystemManager<TObject, TData> : BaseSystemManager where TObject : NanoObject, ILoadable<TData>, new() where TData : BaseSystemData, new()
{
	// Token: 0x17000127 RID: 295
	// (get) Token: 0x060014DB RID: 5339 RVA: 0x000DC2A6 File Offset: 0x000DA4A6
	public int version
	{
		get
		{
			return this._version;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x060014DC RID: 5340 RVA: 0x000DC2AE File Offset: 0x000DA4AE
	public override int Count
	{
		get
		{
			return this.dict.Count;
		}
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x000DC2BB File Offset: 0x000DA4BB
	public override void clear()
	{
		this._version++;
		this.dict.Clear();
		base.clear();
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x000DC2DC File Offset: 0x000DA4DC
	public virtual void update(float pElapsed)
	{
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000DC2E0 File Offset: 0x000DA4E0
	[CanBeNull]
	[Pure]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TObject get(long pID)
	{
		if (!pID.hasValue())
		{
			return default(TObject);
		}
		TObject tObject;
		this.dict.TryGetValue(pID, out tObject);
		return tObject;
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x000DC310 File Offset: 0x000DA510
	public sealed override void ClearAllDisposed()
	{
		foreach (TObject tObject in this._to_dispose)
		{
			tObject.Dispose();
			this.putForRecycle(tObject);
		}
		this._to_dispose.Clear();
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x000DC37C File Offset: 0x000DA57C
	public void scheduleToDisposeObject(TObject pObject)
	{
		this._to_dispose.Add(pObject);
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x000DC38B File Offset: 0x000DA58B
	public void scheduleToDisposeHashSet(HashSet<TObject> pHashSet)
	{
		this._to_dispose.UnionWith(pHashSet);
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x000DC399 File Offset: 0x000DA599
	public void scheduleToDisposeList(List<TObject> pList)
	{
		this._to_dispose.UnionWith(pList);
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000DC3A7 File Offset: 0x000DA5A7
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void putForRecycle(TObject pObject)
	{
		pObject.exists = false;
		this._dead_objects.Push(pObject);
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x000DC3C1 File Offset: 0x000DA5C1
	[MustUseReturnValue]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TObject newObject(long pSpecialID)
	{
		return this.newObjectFromID(pSpecialID);
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000DC3CC File Offset: 0x000DA5CC
	[MustUseReturnValue]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TObject newObject()
	{
		long tId = World.world.map_stats.getNextId(this.type_id);
		return this.newObjectFromID(tId);
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000DC3F8 File Offset: 0x000DA5F8
	[MustUseReturnValue]
	private TObject newObjectFromID(long pID)
	{
		TData tdata = Activator.CreateInstance<TData>();
		tdata.id = pID;
		tdata.created_time = World.world.getCurWorldTime();
		TData tData = tdata;
		TObject tNewObject = this.getNextObject();
		tNewObject.setData(tData);
		tNewObject.created_time_unscaled = (double)Time.time;
		this.addObject(tNewObject);
		return tNewObject;
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x000DC458 File Offset: 0x000DA658
	[MustUseReturnValue]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TObject getNextObject()
	{
		TObject tObject;
		if (this._dead_objects.Count > 0)
		{
			tObject = this._dead_objects.Pop();
			this.revive(tObject);
		}
		else
		{
			tObject = Activator.CreateInstance<TObject>();
			tObject.setHash(BaseSystemManager._latest_hash++);
		}
		return tObject;
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000DC4A8 File Offset: 0x000DA6A8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual void loadFromSave(List<TData> pList)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			TData tData = pList[i];
			if (tData.id == -1L)
			{
				tData.id = World.world.map_stats.getNextId(this.type_id);
			}
			this.loadObject(tData);
		}
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x000DC508 File Offset: 0x000DA708
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual TObject loadObject(TData pData)
	{
		TObject tObject = this.getNextObject();
		tObject.loadData(pData);
		this.addObject(tObject);
		return tObject;
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x000DC530 File Offset: 0x000DA730
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected virtual void addObject(TObject pObject)
	{
		this.dict.Add(pObject.getID(), pObject);
		this.somethingChanged();
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x000DC54F File Offset: 0x000DA74F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public virtual void removeObject(TObject pObject)
	{
		pObject.setAlive(false);
		this.dict.Remove(pObject.getID());
		this.scheduleToDisposeObject(pObject);
		this.somethingChanged();
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x000DC581 File Offset: 0x000DA781
	public void somethingChanged()
	{
		BaseSystemManager.anything_changed = true;
		this._version++;
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x000DC597 File Offset: 0x000DA797
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected void revive(TObject pObject)
	{
		this._counter_recycled += 1L;
		pObject.revive();
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000DC5B4 File Offset: 0x000DA7B4
	public override void showDebugTool(DebugTool pTool)
	{
		string tType = base.GetType().ToString();
		tType = tType.Substring(tType.LastIndexOf('.') + 1);
		int tAliveCheck = 0;
		using (Stack<TObject>.Enumerator enumerator = this._dead_objects.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isAlive())
				{
					tAliveCheck++;
				}
			}
		}
		pTool.setText(tType, string.Concat(new string[]
		{
			"a: ",
			this.Count.ToString(),
			" | d: ",
			this._dead_objects.Count.ToString(),
			" | r : ",
			this._counter_recycled.ToString(),
			" | ad: ",
			tAliveCheck.ToString()
		}), 0f, false, 0L, false, false, "");
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000DC6B0 File Offset: 0x000DA8B0
	public override string debugShort()
	{
		if (this.Count == 0 && this._dead_objects.Count == 0)
		{
			return "";
		}
		if (this._dead_objects.Count > 0)
		{
			return string.Format("{0} [a:{1}|d:{2}]", this.type_id, this.Count, this._dead_objects.Count);
		}
		return string.Format("{0} [{1}]", this.type_id, this.Count);
	}

	// Token: 0x040011D6 RID: 4566
	protected string type_id;

	// Token: 0x040011D7 RID: 4567
	private int _version;

	// Token: 0x040011D8 RID: 4568
	protected readonly Dictionary<long, TObject> dict = new Dictionary<long, TObject>();

	// Token: 0x040011D9 RID: 4569
	private HashSet<TObject> _to_dispose = new HashSet<TObject>();

	// Token: 0x040011DA RID: 4570
	protected readonly Stack<TObject> _dead_objects = new Stack<TObject>();

	// Token: 0x040011DB RID: 4571
	protected long _counter_recycled;
}
