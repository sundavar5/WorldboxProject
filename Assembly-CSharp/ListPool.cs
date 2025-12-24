using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

// Token: 0x02000573 RID: 1395
[Serializable]
public sealed class ListPool<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IDisposable
{
	// Token: 0x06002D7E RID: 11646 RVA: 0x001646E3 File Offset: 0x001628E3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ListPool()
	{
		this._items = ListPool<T>._arrayPool.Rent(32);
	}

	// Token: 0x06002D7F RID: 11647 RVA: 0x001646FD File Offset: 0x001628FD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ListPool(int capacity)
	{
		this._items = ListPool<T>._arrayPool.Rent((capacity < 32) ? 32 : capacity);
	}

	// Token: 0x06002D80 RID: 11648 RVA: 0x00164720 File Offset: 0x00162920
	public ListPool(ICollection<T> collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException("collection");
		}
		T[] buffer = ListPool<T>._arrayPool.Rent((collection.Count > 32) ? collection.Count : 32);
		collection.CopyTo(buffer, 0);
		this._items = buffer;
		this.Count = collection.Count;
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x0016477C File Offset: 0x0016297C
	public ListPool(IEnumerable<T> source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		int _count = source.Count<T>();
		this._items = ListPool<T>._arrayPool.Rent((_count > 32) ? _count : 32);
		T[] buffer = this._items;
		this.Count = 0;
		int count = 0;
		using (IEnumerator<T> enumerator = source.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (count < buffer.Length)
				{
					buffer[count] = enumerator.Current;
					count++;
				}
				else
				{
					this.Count = count;
					this.AddWithResize(enumerator.Current);
					count++;
					buffer = this._items;
				}
			}
			this.Count = count;
		}
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x0016483C File Offset: 0x00162A3C
	public ListPool(T[] source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		int capacity = (source.Length > 32) ? source.Length : 32;
		T[] buffer = ListPool<T>._arrayPool.Rent(capacity);
		source.CopyTo(buffer, 0);
		this._items = buffer;
		this.Count = source.Length;
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x00164890 File Offset: 0x00162A90
	public ListPool(ReadOnlySpan<T> source)
	{
		int capacity = (source.Length > 32) ? source.Length : 32;
		T[] buffer = ListPool<T>._arrayPool.Rent(capacity);
		source.CopyTo(buffer);
		this._items = buffer;
		this.Count = source.Length;
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06002D84 RID: 11652 RVA: 0x001648E7 File Offset: 0x00162AE7
	public int Capacity
	{
		get
		{
			return this._items.Length;
		}
	}

	// Token: 0x06002D85 RID: 11653 RVA: 0x001648F1 File Offset: 0x00162AF1
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Dispose()
	{
		if (ListPool<T>._should_clean)
		{
			this.Clear();
		}
		this.Count = 0;
		ListPool<T>._arrayPool.Return(this._items, false);
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06002D86 RID: 11654 RVA: 0x00164918 File Offset: 0x00162B18
	int ICollection.Count
	{
		get
		{
			return this.Count;
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06002D87 RID: 11655 RVA: 0x00164920 File Offset: 0x00162B20
	bool IList.IsFixedSize
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06002D88 RID: 11656 RVA: 0x00164923 File Offset: 0x00162B23
	bool ICollection.IsSynchronized
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06002D89 RID: 11657 RVA: 0x00164926 File Offset: 0x00162B26
	bool IList.IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06002D8A RID: 11658 RVA: 0x00164929 File Offset: 0x00162B29
	object ICollection.SyncRoot
	{
		get
		{
			if (this._syncRoot == null)
			{
				Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
			}
			return this._syncRoot;
		}
	}

	// Token: 0x06002D8B RID: 11659 RVA: 0x0016494C File Offset: 0x00162B4C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	int IList.Add(object item)
	{
		if (item is T)
		{
			T itemAsTSource = (T)((object)item);
			this.Add(itemAsTSource);
			return this.Count - 1;
		}
		throw new ArgumentException(string.Format("Wrong value type. Expected {0}, got: '{1}'.", typeof(T), item), "item");
	}

	// Token: 0x06002D8C RID: 11660 RVA: 0x0016499C File Offset: 0x00162B9C
	bool IList.Contains(object item)
	{
		if (item is T)
		{
			T itemAsTSource = (T)((object)item);
			return this.Contains(itemAsTSource);
		}
		throw new ArgumentException(string.Format("Wrong value type. Expected {0}, got: '{1}'.", typeof(T), item), "item");
	}

	// Token: 0x06002D8D RID: 11661 RVA: 0x001649E0 File Offset: 0x00162BE0
	int IList.IndexOf(object item)
	{
		if (item is T)
		{
			T itemAsTSource = (T)((object)item);
			return this.IndexOf(itemAsTSource);
		}
		throw new ArgumentException(string.Format("Wrong value type. Expected {0}, got: '{1}'.", typeof(T), item), "item");
	}

	// Token: 0x06002D8E RID: 11662 RVA: 0x00164A24 File Offset: 0x00162C24
	void IList.Remove(object item)
	{
		if (item is T)
		{
			T itemAsTSource = (T)((object)item);
			this.Remove(itemAsTSource);
			return;
		}
		if (item != null)
		{
			throw new ArgumentException(string.Format("Wrong value type. Expected {0}, got: '{1}'.", typeof(T), item), "item");
		}
	}

	// Token: 0x06002D8F RID: 11663 RVA: 0x00164A6C File Offset: 0x00162C6C
	void IList.Insert(int index, object item)
	{
		if (item is T)
		{
			T itemAsTSource = (T)((object)item);
			this.Insert(index, itemAsTSource);
			return;
		}
		throw new ArgumentException(string.Format("Wrong value type. Expected {0}, got: '{1}'.", typeof(T), item), "item");
	}

	// Token: 0x06002D90 RID: 11664 RVA: 0x00164AB0 File Offset: 0x00162CB0
	void ICollection.CopyTo(Array array, int arrayIndex)
	{
		Array.Copy(this._items, 0, array, arrayIndex, this.Count);
	}

	// Token: 0x17000252 RID: 594
	object IList.this[int index]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: MaybeNull]
		get
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException("index");
			}
			return this._items[index];
		}
		set
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException("index");
			}
			if (value is T)
			{
				T valueAsTSource = (T)((object)value);
				this._items[index] = valueAsTSource;
				return;
			}
			throw new ArgumentException(string.Format("Wrong value type. Expected {0}, got: '{1}'.", typeof(T), value), "value");
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06002D93 RID: 11667 RVA: 0x00164B4D File Offset: 0x00162D4D
	// (set) Token: 0x06002D94 RID: 11668 RVA: 0x00164B55 File Offset: 0x00162D55
	public int Count { get; private set; }

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06002D95 RID: 11669 RVA: 0x00164B5E File Offset: 0x00162D5E
	public bool IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06002D96 RID: 11670 RVA: 0x00164B64 File Offset: 0x00162D64
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(T item)
	{
		T[] buffer = this._items;
		int count = this.Count;
		if (count < buffer.Length)
		{
			buffer[count] = item;
			this.Count = count + 1;
			return;
		}
		this.AddWithResize(item);
	}

	// Token: 0x06002D97 RID: 11671 RVA: 0x00164B9E File Offset: 0x00162D9E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Clear()
	{
		if (this.Count > 0)
		{
			Array.Clear(this._items, 0, this.Count);
			this.Count = 0;
		}
	}

	// Token: 0x06002D98 RID: 11672 RVA: 0x00164BC2 File Offset: 0x00162DC2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Clear(int pAfterIndex)
	{
		if (this.Count > 0 && pAfterIndex < this.Count)
		{
			Array.Clear(this._items, pAfterIndex, this.Count - pAfterIndex);
			this.Count = pAfterIndex;
		}
	}

	// Token: 0x06002D99 RID: 11673 RVA: 0x00164BF1 File Offset: 0x00162DF1
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Contains(T item)
	{
		return this.IndexOf(item) > -1;
	}

	// Token: 0x06002D9A RID: 11674 RVA: 0x00164BFD File Offset: 0x00162DFD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int IndexOf(T item)
	{
		return Array.IndexOf<T>(this._items, item, 0, this.Count);
	}

	// Token: 0x06002D9B RID: 11675 RVA: 0x00164C12 File Offset: 0x00162E12
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void CopyTo(T[] array, int arrayIndex)
	{
		Array.Copy(this._items, 0, array, arrayIndex, this.Count);
	}

	// Token: 0x06002D9C RID: 11676 RVA: 0x00164C28 File Offset: 0x00162E28
	public bool Remove(T item)
	{
		if (item == null)
		{
			return false;
		}
		int index = this.IndexOf(item);
		if (index == -1)
		{
			return false;
		}
		this.RemoveAt(index);
		return true;
	}

	// Token: 0x06002D9D RID: 11677 RVA: 0x00164C58 File Offset: 0x00162E58
	public void Insert(int index, T item)
	{
		int count = this.Count;
		T[] buffer = this._items;
		if (buffer.Length == count)
		{
			int newCapacity = count * 2;
			this.EnsureCapacity(newCapacity);
			buffer = this._items;
		}
		if (index < count)
		{
			Array.Copy(buffer, index, buffer, index + 1, count - index);
			buffer[index] = item;
			int count2 = this.Count;
			this.Count = count2 + 1;
			return;
		}
		if (index == count)
		{
			buffer[index] = item;
			int count2 = this.Count;
			this.Count = count2 + 1;
			return;
		}
		throw new IndexOutOfRangeException("index");
	}

	// Token: 0x06002D9E RID: 11678 RVA: 0x00164CE0 File Offset: 0x00162EE0
	public void RemoveAt(int index)
	{
		int count = this.Count;
		T[] buffer = this._items;
		if (index >= count)
		{
			throw new IndexOutOfRangeException("index");
		}
		count--;
		Array.Copy(buffer, index + 1, buffer, index, count - index);
		if (ListPool<T>._should_clean)
		{
			buffer[count] = default(T);
		}
		this.Count = count;
	}

	// Token: 0x06002D9F RID: 11679 RVA: 0x00164D3C File Offset: 0x00162F3C
	public int RemoveAll(Predicate<T> match)
	{
		int count = this.Count;
		T[] buffer = this._items;
		int freeIndex = 0;
		while (freeIndex < count && !match(buffer[freeIndex]))
		{
			freeIndex++;
		}
		if (freeIndex >= count)
		{
			return 0;
		}
		int current = freeIndex + 1;
		while (current < count)
		{
			while (current < count && match(buffer[current]))
			{
				current++;
			}
			if (current < count)
			{
				buffer[freeIndex++] = buffer[current++];
			}
		}
		if (ListPool<T>._should_clean)
		{
			Array.Clear(buffer, freeIndex, count - freeIndex);
		}
		int result = count - freeIndex;
		this.Count = freeIndex;
		return result;
	}

	// Token: 0x17000255 RID: 597
	public T this[int index]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: MaybeNull]
		get
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException("index");
			}
			return this._items[index];
		}
		set
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException("index");
			}
			this._items[index] = value;
		}
	}

	// Token: 0x06002DA2 RID: 11682 RVA: 0x00164E13 File Offset: 0x00163013
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator<T> IEnumerable<!0>.GetEnumerator()
	{
		return new ListPool<T>.Enumerator(this._items, this.Count);
	}

	// Token: 0x06002DA3 RID: 11683 RVA: 0x00164E2B File Offset: 0x0016302B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return new ListPool<T>.Enumerator(this._items, this.Count);
	}

	// Token: 0x06002DA4 RID: 11684 RVA: 0x00164E44 File Offset: 0x00163044
	public void AddRange(Span<T> items)
	{
		int count = this.Count;
		T[] buffer = this._items;
		if (buffer.Length - items.Length - count < 0)
		{
			this.EnsureCapacity(buffer.Length + items.Length);
			buffer = this._items;
		}
		items.CopyTo(buffer.AsSpan<T>().Slice(count));
		this.Count += items.Length;
	}

	// Token: 0x06002DA5 RID: 11685 RVA: 0x00164EB8 File Offset: 0x001630B8
	public void AddRange(ReadOnlySpan<T> items)
	{
		int count = this.Count;
		T[] buffer = this._items;
		if (buffer.Length - items.Length - count < 0)
		{
			this.EnsureCapacity(buffer.Length + items.Length);
			buffer = this._items;
		}
		items.CopyTo(buffer.AsSpan<T>().Slice(count));
		this.Count += items.Length;
	}

	// Token: 0x06002DA6 RID: 11686 RVA: 0x00164F2C File Offset: 0x0016312C
	public void AddRange(T[] items)
	{
		int count = this.Count;
		T[] buffer = this._items;
		if (buffer.Length - items.Length - count < 0)
		{
			this.EnsureCapacity(buffer.Length + items.Length);
			buffer = this._items;
		}
		Array.Copy(items, 0, buffer, count, items.Length);
		this.Count += items.Length;
	}

	// Token: 0x06002DA7 RID: 11687 RVA: 0x00164F88 File Offset: 0x00163188
	public void AddRange(IEnumerable<T> items)
	{
		int count = this.Count;
		T[] buffer = this._items;
		ICollection<T> collection = items as ICollection<T>;
		if (collection != null)
		{
			if (buffer.Length - collection.Count - count < 0)
			{
				this.EnsureCapacity(buffer.Length + collection.Count);
				buffer = this._items;
			}
			collection.CopyTo(buffer, count);
			this.Count += collection.Count;
			return;
		}
		foreach (T item in items)
		{
			if (count < buffer.Length)
			{
				buffer[count] = item;
				count++;
			}
			else
			{
				this.Count = count;
				this.AddWithResize(item);
				count++;
				buffer = this._items;
			}
		}
		this.Count = count;
	}

	// Token: 0x06002DA8 RID: 11688 RVA: 0x00165060 File Offset: 0x00163260
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Span<T> AsSpan()
	{
		return this._items.AsSpan(0, this.Count);
	}

	// Token: 0x06002DA9 RID: 11689 RVA: 0x00165074 File Offset: 0x00163274
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Memory<T> AsMemory()
	{
		return this._items.AsMemory(0, this.Count);
	}

	// Token: 0x06002DAA RID: 11690 RVA: 0x00165088 File Offset: 0x00163288
	[MethodImpl(MethodImplOptions.NoInlining)]
	private void AddWithResize(T item)
	{
		ArrayPool<T> arrayPool = ListPool<T>._arrayPool;
		T[] oldBuffer = this._items;
		T[] newBuffer = arrayPool.Rent(oldBuffer.Length * 2);
		int count = oldBuffer.Length;
		Array.Copy(oldBuffer, 0, newBuffer, 0, count);
		newBuffer[count] = item;
		this._items = newBuffer;
		this.Count = count + 1;
		arrayPool.Return(oldBuffer, ListPool<T>._should_clean);
	}

	// Token: 0x06002DAB RID: 11691 RVA: 0x001650E0 File Offset: 0x001632E0
	public void EnsureCapacity(int capacity)
	{
		if (capacity <= this.Capacity)
		{
			return;
		}
		ArrayPool<T> arrayPool = ListPool<T>._arrayPool;
		T[] newBuffer = arrayPool.Rent(capacity);
		T[] oldBuffer = this._items;
		Array.Copy(oldBuffer, 0, newBuffer, 0, oldBuffer.Length);
		this._items = newBuffer;
		arrayPool.Return(oldBuffer, ListPool<T>._should_clean);
	}

	// Token: 0x06002DAC RID: 11692 RVA: 0x00165129 File Offset: 0x00163329
	public T[] GetRawBuffer()
	{
		return this._items;
	}

	// Token: 0x06002DAD RID: 11693 RVA: 0x00165131 File Offset: 0x00163331
	public void SetOffsetManually(int offset)
	{
		this.Count = offset;
	}

	// Token: 0x06002DAE RID: 11694 RVA: 0x0016513A File Offset: 0x0016333A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ListPool<T>.Enumerator GetEnumerator()
	{
		return new ListPool<T>.Enumerator(this._items, this.Count);
	}

	// Token: 0x06002DAF RID: 11695 RVA: 0x0016514D File Offset: 0x0016334D
	public void Sort()
	{
		this.Sort(0, this.Count, null);
	}

	// Token: 0x06002DB0 RID: 11696 RVA: 0x0016515D File Offset: 0x0016335D
	public void Sort(IComparer<T> comparer)
	{
		this.Sort(0, this.Count, comparer);
	}

	// Token: 0x06002DB1 RID: 11697 RVA: 0x0016516D File Offset: 0x0016336D
	public void Sort(int index, int count, IComparer<T> comparer)
	{
		Array.Sort<T>(this._items, index, count, comparer);
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x0016517D File Offset: 0x0016337D
	public void Sort(Comparison<T> comparison)
	{
		Array.Sort<T>(this._items, 0, this.Count, Comparer<T>.Create(comparison));
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x00165197 File Offset: 0x00163397
	public void Reverse()
	{
		Array.Reverse<T>(this._items, 0, this.Count);
	}

	// Token: 0x06002DB4 RID: 11700 RVA: 0x001651AB File Offset: 0x001633AB
	public void Reverse(int index, int count)
	{
		Array.Reverse<T>(this._items, index, count);
	}

	// Token: 0x040022B5 RID: 8885
	private const int MinimumCapacity = 32;

	// Token: 0x040022B6 RID: 8886
	private T[] _items;

	// Token: 0x040022B7 RID: 8887
	[Nullable(2)]
	[NonSerialized]
	private object _syncRoot;

	// Token: 0x040022B8 RID: 8888
	private static readonly ArrayPool<T> _arrayPool = ArrayPool<T>.Shared;

	// Token: 0x040022B9 RID: 8889
	private static readonly bool _should_clean = !typeof(T).IsValueType && typeof(string) != typeof(T);

	// Token: 0x02000A79 RID: 2681
	public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
	{
		// Token: 0x06004F7E RID: 20350 RVA: 0x001FFB57 File Offset: 0x001FDD57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Enumerator(T[] source, int itemsCount)
		{
			this._source = source;
			this._itemsCount = itemsCount;
			this._index = -1;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x001FFB6E File Offset: 0x001FDD6E
		public readonly ref T Current
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			[return: MaybeNull]
			get
			{
				return ref this._source[this._index];
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06004F80 RID: 20352 RVA: 0x001FFB81 File Offset: 0x001FDD81
		T IEnumerator<!0>.Current
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			[return: MaybeNull]
			get
			{
				return this._source[this._index];
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x001FFB94 File Offset: 0x001FDD94
		[Nullable(2)]
		object IEnumerator.Current
		{
			[NullableContext(2)]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			[return: MaybeNull]
			get
			{
				return this._source[this._index];
			}
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x001FFBAC File Offset: 0x001FDDAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool MoveNext()
		{
			int num = this._index + 1;
			this._index = num;
			return num < this._itemsCount;
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x001FFBD2 File Offset: 0x001FDDD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Reset()
		{
			this._index = -1;
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x001FFBDB File Offset: 0x001FDDDB
		public readonly void Dispose()
		{
		}

		// Token: 0x04003962 RID: 14690
		private readonly T[] _source;

		// Token: 0x04003963 RID: 14691
		private readonly int _itemsCount;

		// Token: 0x04003964 RID: 14692
		private int _index;
	}
}
