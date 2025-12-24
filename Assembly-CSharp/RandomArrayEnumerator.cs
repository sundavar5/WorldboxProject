using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200057A RID: 1402
public struct RandomArrayEnumerator<T> : IEnumerator<!0>, IEnumerator, IDisposable, IEnumerable<!0>, IEnumerable
{
	// Token: 0x06002E52 RID: 11858 RVA: 0x0016643C File Offset: 0x0016463C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RandomArrayEnumerator(T[] source)
	{
		this = new RandomArrayEnumerator<T>(source, source.Length);
	}

	// Token: 0x06002E53 RID: 11859 RVA: 0x00166448 File Offset: 0x00164648
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RandomArrayEnumerator(T[] source, int itemsCount)
	{
		this._source = source;
		this._maxItems = itemsCount;
		this._itemsCount = itemsCount;
		this._index = -1;
		this._offset = Randy.randomInt(0, this._itemsCount);
	}

	// Token: 0x06002E54 RID: 11860 RVA: 0x00166485 File Offset: 0x00164685
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RandomArrayEnumerator(T[] source, int itemsCount, int maxItems)
	{
		this._source = source;
		this._itemsCount = itemsCount;
		this._maxItems = Mathf.Min(maxItems, this._itemsCount);
		this._index = -1;
		this._offset = Randy.randomInt(0, this._itemsCount);
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06002E55 RID: 11861 RVA: 0x001664C0 File Offset: 0x001646C0
	private readonly int Index
	{
		get
		{
			return (this._index + this._offset) % this._itemsCount;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06002E56 RID: 11862 RVA: 0x001664D6 File Offset: 0x001646D6
	public readonly ref T Current
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: MaybeNull]
		get
		{
			return ref this._source[this.Index];
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06002E57 RID: 11863 RVA: 0x001664E9 File Offset: 0x001646E9
	T IEnumerator<!0>.Current
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: MaybeNull]
		get
		{
			return this._source[this.Index];
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06002E58 RID: 11864 RVA: 0x001664FC File Offset: 0x001646FC
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

	// Token: 0x06002E59 RID: 11865 RVA: 0x00166514 File Offset: 0x00164714
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool MoveNext()
	{
		int num = this._index + 1;
		this._index = num;
		return num < this._maxItems;
	}

	// Token: 0x06002E5A RID: 11866 RVA: 0x0016653A File Offset: 0x0016473A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Reset()
	{
		this._index = -1;
	}

	// Token: 0x06002E5B RID: 11867 RVA: 0x00166543 File Offset: 0x00164743
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public RandomArrayEnumerator<T> GetEnumerator()
	{
		return this;
	}

	// Token: 0x06002E5C RID: 11868 RVA: 0x0016654B File Offset: 0x0016474B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator<T> IEnumerable<!0>.GetEnumerator()
	{
		return this;
	}

	// Token: 0x06002E5D RID: 11869 RVA: 0x00166558 File Offset: 0x00164758
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this;
	}

	// Token: 0x06002E5E RID: 11870 RVA: 0x00166565 File Offset: 0x00164765
	public readonly void Dispose()
	{
	}

	// Token: 0x040022C7 RID: 8903
	private readonly T[] _source;

	// Token: 0x040022C8 RID: 8904
	private readonly int _itemsCount;

	// Token: 0x040022C9 RID: 8905
	private readonly int _maxItems;

	// Token: 0x040022CA RID: 8906
	private int _index;

	// Token: 0x040022CB RID: 8907
	private readonly int _offset;
}
