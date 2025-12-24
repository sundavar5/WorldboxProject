using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200057B RID: 1403
public struct RandomListEnumerator<T> : IEnumerator<!0>, IEnumerator, IDisposable, IEnumerable<!0>, IEnumerable
{
	// Token: 0x06002E5F RID: 11871 RVA: 0x00166567 File Offset: 0x00164767
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RandomListEnumerator(List<T> source)
	{
		this = new RandomListEnumerator<T>(source, source.Count);
	}

	// Token: 0x06002E60 RID: 11872 RVA: 0x00166578 File Offset: 0x00164778
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RandomListEnumerator(List<T> source, int itemsCount)
	{
		this._source = source;
		this._maxItems = itemsCount;
		this._itemsCount = itemsCount;
		this._index = -1;
		this._offset = Randy.randomInt(0, this._itemsCount);
	}

	// Token: 0x06002E61 RID: 11873 RVA: 0x001665B5 File Offset: 0x001647B5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RandomListEnumerator(List<T> source, int itemsCount, int maxItems)
	{
		this._source = source;
		this._itemsCount = itemsCount;
		this._maxItems = Mathf.Min(maxItems, this._itemsCount);
		this._index = -1;
		this._offset = Randy.randomInt(0, this._itemsCount);
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06002E62 RID: 11874 RVA: 0x001665F0 File Offset: 0x001647F0
	private readonly int Index
	{
		get
		{
			return (this._index + this._offset) % this._itemsCount;
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06002E63 RID: 11875 RVA: 0x00166606 File Offset: 0x00164806
	public readonly T Current
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: MaybeNull]
		get
		{
			return this._source[this.Index];
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06002E64 RID: 11876 RVA: 0x00166619 File Offset: 0x00164819
	T IEnumerator<!0>.Current
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: MaybeNull]
		get
		{
			return this._source[this.Index];
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06002E65 RID: 11877 RVA: 0x0016662C File Offset: 0x0016482C
	[Nullable(2)]
	object IEnumerator.Current
	{
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: MaybeNull]
		get
		{
			return this._source[this.Index];
		}
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x00166644 File Offset: 0x00164844
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool MoveNext()
	{
		int num = this._index + 1;
		this._index = num;
		return num < this._maxItems;
	}

	// Token: 0x06002E67 RID: 11879 RVA: 0x0016666A File Offset: 0x0016486A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Reset()
	{
		this._index = -1;
	}

	// Token: 0x06002E68 RID: 11880 RVA: 0x00166673 File Offset: 0x00164873
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RandomListEnumerator<T> GetEnumerator()
	{
		return this;
	}

	// Token: 0x06002E69 RID: 11881 RVA: 0x0016667B File Offset: 0x0016487B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator<T> IEnumerable<!0>.GetEnumerator()
	{
		return this;
	}

	// Token: 0x06002E6A RID: 11882 RVA: 0x00166688 File Offset: 0x00164888
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this;
	}

	// Token: 0x06002E6B RID: 11883 RVA: 0x00166695 File Offset: 0x00164895
	public readonly void Dispose()
	{
	}

	// Token: 0x040022CC RID: 8908
	private readonly List<T> _source;

	// Token: 0x040022CD RID: 8909
	private readonly int _itemsCount;

	// Token: 0x040022CE RID: 8910
	private readonly int _maxItems;

	// Token: 0x040022CF RID: 8911
	private int _index;

	// Token: 0x040022D0 RID: 8912
	private readonly int _offset;
}
