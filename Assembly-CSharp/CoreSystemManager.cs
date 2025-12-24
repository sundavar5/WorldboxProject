using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x0200021D RID: 541
public abstract class CoreSystemManager<TObject, TData> : SystemManager<TObject, TData>, IEnumerable<TObject>, IEnumerable where TObject : CoreSystemObject<TData>, new() where TData : BaseSystemData, new()
{
	// Token: 0x06001390 RID: 5008 RVA: 0x000D8E0E File Offset: 0x000D700E
	private void setListDirty()
	{
		this._dirty_list = true;
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x000D8E17 File Offset: 0x000D7017
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IEnumerator<TObject> GetEnumerator()
	{
		return this._hashset.GetEnumerator();
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x000D8E29 File Offset: 0x000D7029
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06001393 RID: 5011 RVA: 0x000D8E31 File Offset: 0x000D7031
	public override int Count
	{
		get
		{
			return this._hashset.Count;
		}
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x000D8E3E File Offset: 0x000D703E
	public override void checkLists()
	{
		if (this._dirty_list)
		{
			this._dirty_list = false;
			this.list.Clear();
			this.list.AddRange(this._hashset);
		}
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x000D8E6B File Offset: 0x000D706B
	public override void loadFromSave(List<TData> pList)
	{
		base.loadFromSave(pList);
		this.setListDirty();
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000D8E7A File Offset: 0x000D707A
	protected override void addObject(TObject pObject)
	{
		base.addObject(pObject);
		this.setListDirty();
		this._hashset.Add(pObject);
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000D8E98 File Offset: 0x000D7098
	public virtual List<TData> save(List<TObject> pList = null)
	{
		if (pList == null)
		{
			pList = this.list;
		}
		List<TData> tList = new List<TData>();
		foreach (TObject tObject in pList)
		{
			if (tObject.isAlive())
			{
				tObject.save();
				tList.Add(tObject.data);
			}
		}
		return tList;
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x000D8F1C File Offset: 0x000D711C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override void removeObject(TObject pObject)
	{
		base.removeObject(pObject);
		this._hashset.Remove(pObject);
		this.setListDirty();
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x000D8F38 File Offset: 0x000D7138
	public TObject getRandom()
	{
		foreach (TObject tObject in this.list.LoopRandom<TObject>())
		{
			if (this._hashset.Contains(tObject))
			{
				return tObject;
			}
		}
		return default(TObject);
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x000D8FA0 File Offset: 0x000D71A0
	public override void clear()
	{
		foreach (TObject tobject in this._hashset)
		{
			tobject.setAlive(false);
		}
		base.scheduleToDisposeHashSet(this._hashset);
		this._hashset.Clear();
		this.list.Clear();
		this.setListDirty();
		base.clear();
	}

	// Token: 0x0400118C RID: 4492
	public readonly List<TObject> list = new List<TObject>(512);

	// Token: 0x0400118D RID: 4493
	private readonly HashSet<TObject> _hashset = new HashSet<TObject>(512);

	// Token: 0x0400118E RID: 4494
	private bool _dirty_list;
}
