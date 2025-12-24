using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

// Token: 0x020001DE RID: 478
public class ObjectContainer<T> : IEnumerable<T>, IEnumerable
{
	// Token: 0x06000DDC RID: 3548 RVA: 0x000BE909 File Offset: 0x000BCB09
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IEnumerator<T> GetEnumerator()
	{
		return this._hashSet.GetEnumerator();
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x000BE91B File Offset: 0x000BCB1B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x000BE923 File Offset: 0x000BCB23
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Contains(T pObject, bool pCheckToAddRemove = true)
	{
		if (pCheckToAddRemove)
		{
			if (this._to_add.Contains(pObject))
			{
				return true;
			}
			if (this._to_remove.Contains(pObject))
			{
				return false;
			}
		}
		return this._hashSet.Contains(pObject);
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x000BE954 File Offset: 0x000BCB54
	public void Clear()
	{
		this._hashSet.Clear();
		this._to_add.Clear();
		this._to_remove.Clear();
		this._simple_list.Clear();
		this._dirty_list = false;
		this._dirty_array = false;
		this._dirty_container = false;
		if (this._array_count > 0)
		{
			Array.Clear(this._array, 0, this._array.Length);
			this._array_count = 0;
		}
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x000BE9C6 File Offset: 0x000BCBC6
	public void doChecks()
	{
		this.checkAddRemove();
		this.checkSimpleListDirty();
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x000BE9D4 File Offset: 0x000BCBD4
	public bool isDirtyContainer()
	{
		return this._dirty_container;
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x000BE9DC File Offset: 0x000BCBDC
	public void checkAddRemove()
	{
		if (this._to_add.Count > 0)
		{
			if (this._hashSet.Count == 0)
			{
				HashSet<T> tTemp = this._hashSet;
				this._hashSet = this._to_add;
				this._to_add = tTemp;
			}
			else
			{
				this._hashSet.UnionWith(this._to_add);
				this._to_add.Clear();
			}
			this._dirty_list = true;
			this._dirty_array = true;
		}
		if (this._to_remove.Count > 0)
		{
			this._hashSet.ExceptWith(this._to_remove);
			this._to_remove.Clear();
			this._dirty_list = true;
			this._dirty_array = true;
		}
		this._dirty_container = false;
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x000BEA89 File Offset: 0x000BCC89
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public List<T> getSimpleList()
	{
		this.checkSimpleListDirty();
		return this._simple_list;
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x000BEA97 File Offset: 0x000BCC97
	public T[] getFastSimpleArray()
	{
		if (this._dirty_array)
		{
			this._dirty_array = false;
			this._hashSet.CopyTo(this._array, 0);
			this._array_count = this._hashSet.Count;
		}
		return this._array;
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x000BEAD4 File Offset: 0x000BCCD4
	public T[] getSimpleArray()
	{
		if (this._dirty_array)
		{
			this._dirty_array = false;
			this.prepareArray(this._hashSet.Count);
			this._hashSet.CopyTo(this._array, 0);
			this._array_count = this._hashSet.Count;
		}
		return this._array;
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x000BEB2A File Offset: 0x000BCD2A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void checkSimpleListDirty()
	{
		if (this._dirty_list)
		{
			List<T> simple_list = this._simple_list;
			simple_list.Clear();
			simple_list.AddRange(this._hashSet);
			this._dirty_list = false;
		}
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x000BEB52 File Offset: 0x000BCD52
	public void prepareArray(int pSize)
	{
		this._array = Toolbox.checkArraySize<T>(this._array, pSize);
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x000BEB68 File Offset: 0x000BCD68
	[CanBeNull]
	public T GetRandom()
	{
		this.checkAddRemove();
		List<T> tList = this.getSimpleList();
		if (tList.Count == 0)
		{
			return default(T);
		}
		return tList.GetRandom<T>();
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x000BEB9A File Offset: 0x000BCD9A
	public int Count
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return this._hashSet.Count;
		}
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x000BEBA8 File Offset: 0x000BCDA8
	public string debug()
	{
		return string.Concat(new string[]
		{
			this._hashSet.Count.ToString(),
			"/",
			this._to_add.Count.ToString(),
			"/",
			this._to_remove.Count.ToString()
		});
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x000BEC12 File Offset: 0x000BCE12
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(T pObject)
	{
		this._to_add.Add(pObject);
		this._to_remove.Remove(pObject);
		this._dirty_container = true;
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x000BEC35 File Offset: 0x000BCE35
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Remove(T pObject)
	{
		this._to_remove.Add(pObject);
		this._to_add.Remove(pObject);
		this._dirty_container = true;
	}

	// Token: 0x04000E39 RID: 3641
	private HashSet<T> _to_remove = new HashSet<T>();

	// Token: 0x04000E3A RID: 3642
	private HashSet<T> _to_add = new HashSet<T>();

	// Token: 0x04000E3B RID: 3643
	private HashSet<T> _hashSet = new HashSet<T>();

	// Token: 0x04000E3C RID: 3644
	private T[] _array;

	// Token: 0x04000E3D RID: 3645
	private int _array_count;

	// Token: 0x04000E3E RID: 3646
	private bool _dirty_list;

	// Token: 0x04000E3F RID: 3647
	private bool _dirty_array;

	// Token: 0x04000E40 RID: 3648
	private bool _dirty_container;

	// Token: 0x04000E41 RID: 3649
	private readonly List<T> _simple_list = new List<T>();
}
