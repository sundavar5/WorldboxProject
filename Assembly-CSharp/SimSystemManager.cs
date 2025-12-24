using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x0200022B RID: 555
public abstract class SimSystemManager<TObject, TData> : SystemManager<TObject, TData>, IEnumerable<TObject>, IEnumerable where TObject : BaseSimObject, ILoadable<TData>, new() where TData : BaseObjectData, new()
{
	// Token: 0x060014CA RID: 5322 RVA: 0x000DC0EF File Offset: 0x000DA2EF
	public SimSystemManager()
	{
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x000DC10D File Offset: 0x000DA30D
	public void prepareArray()
	{
		this._container.prepareArray(this.Count);
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x000DC120 File Offset: 0x000DA320
	public override void loadFromSave(List<TData> pList)
	{
		base.loadFromSave(pList);
		this._container.checkAddRemove();
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x000DC134 File Offset: 0x000DA334
	protected override void addObject(TObject pObject)
	{
		base.addObject(pObject);
		this._container.Add(pObject);
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x000DC149 File Offset: 0x000DA349
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override void removeObject(TObject pObject)
	{
		base.removeObject(pObject);
		this._container.Remove(pObject);
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x000DC15E File Offset: 0x000DA35E
	public override void clear()
	{
		base.clear();
		this._container.Clear();
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000DC171 File Offset: 0x000DA371
	public void checkContainer()
	{
		this._container.checkAddRemove();
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x000DC17E File Offset: 0x000DA37E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IEnumerator<TObject> GetEnumerator()
	{
		return this._container.GetEnumerator();
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x000DC18B File Offset: 0x000DA38B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x000DC193 File Offset: 0x000DA393
	public string debugContainer()
	{
		return this._container.debug();
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x000DC1A0 File Offset: 0x000DA3A0
	public TObject GetRandom()
	{
		return this._container.GetRandom();
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000DC1AD File Offset: 0x000DA3AD
	public List<TObject> getSimpleList()
	{
		return this._container.getSimpleList();
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x000DC1BA File Offset: 0x000DA3BA
	public TObject[] getSimpleArray()
	{
		return this._container.getSimpleArray();
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x000DC1C7 File Offset: 0x000DA3C7
	internal virtual void scheduleDestroyOnPlay(TObject pObject)
	{
		this._to_destroy_objects.Add(pObject);
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x000DC1D8 File Offset: 0x000DA3D8
	internal void scheduleDestroyAllOnWorldClear()
	{
		foreach (TObject tObject in this)
		{
			this._to_destroy_objects.Add(tObject);
		}
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000DC228 File Offset: 0x000DA428
	internal bool checkObjectsToDestroy()
	{
		if (this._to_destroy_objects.Count <= 0)
		{
			return false;
		}
		foreach (TObject tObject in this._to_destroy_objects)
		{
			this.destroyObject(tObject);
		}
		this._to_destroy_objects.Clear();
		this.checkContainer();
		this.event_destroy = true;
		return true;
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x000DC2A4 File Offset: 0x000DA4A4
	protected virtual void destroyObject(TObject pObject)
	{
	}

	// Token: 0x040011D2 RID: 4562
	private readonly ObjectContainer<TObject> _container = new ObjectContainer<TObject>();

	// Token: 0x040011D3 RID: 4563
	private HashSet<TObject> _to_destroy_objects = new HashSet<TObject>();

	// Token: 0x040011D4 RID: 4564
	public bool event_destroy;

	// Token: 0x040011D5 RID: 4565
	public bool event_houses;
}
