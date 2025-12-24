using System;
using System.Collections;
using System.Collections.Generic;
using C5;

namespace EpPathFinding.cs
{
	// Token: 0x02000869 RID: 2153
	public class IntervalHeap<T> : CollectionValueBase<T>, IPriorityQueue<T>, IExtensible<T>, ICollectionValue<T>, IEnumerable<!0>, IEnumerable, IShowable, IFormattable where T : class
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x001C9D61 File Offset: 0x001C7F61
		public override EventTypeEnum ListenableEvents
		{
			get
			{
				return EventTypeEnum.Basic;
			}
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x001C9D68 File Offset: 0x001C7F68
		private void SwapFirstWithLast(int cell1, int cell2)
		{
			T first = this.heap[cell1].first;
			this.UpdateFirst(cell1, this.heap[cell2].last);
			this.UpdateLast(cell2, first);
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x001C9DA8 File Offset: 0x001C7FA8
		private void SwapLastWithLast(int cell1, int cell2)
		{
			T last = this.heap[cell2].last;
			this.UpdateLast(cell2, this.heap[cell1].last);
			this.UpdateLast(cell1, last);
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x001C9DE8 File Offset: 0x001C7FE8
		private void SwapFirstWithFirst(int cell1, int cell2)
		{
			T first = this.heap[cell2].first;
			this.UpdateFirst(cell2, this.heap[cell1].first);
			this.UpdateFirst(cell1, first);
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x001C9E28 File Offset: 0x001C8028
		private bool HeapifyMin(int cell)
		{
			bool swappedroot = false;
			if (2 * cell + 1 < this.size && this.comparer.Compare(this.heap[cell].first, this.heap[cell].last) > 0)
			{
				swappedroot = true;
				this.SwapFirstWithLast(cell, cell);
			}
			int currentmin = cell;
			int i = 2 * cell + 1;
			int r = i + 1;
			if (2 * i < this.size && this.comparer.Compare(this.heap[i].first, this.heap[currentmin].first) < 0)
			{
				currentmin = i;
			}
			if (2 * r < this.size && this.comparer.Compare(this.heap[r].first, this.heap[currentmin].first) < 0)
			{
				currentmin = r;
			}
			if (currentmin != cell)
			{
				this.SwapFirstWithFirst(currentmin, cell);
				this.HeapifyMin(currentmin);
			}
			return swappedroot;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x001C9F1C File Offset: 0x001C811C
		private bool HeapifyMax(int cell)
		{
			bool swappedroot = false;
			if (2 * cell + 1 < this.size && this.comparer.Compare(this.heap[cell].last, this.heap[cell].first) < 0)
			{
				swappedroot = true;
				this.SwapFirstWithLast(cell, cell);
			}
			int currentmax = cell;
			int i = 2 * cell + 1;
			int r = i + 1;
			bool firstmax = false;
			if (2 * i + 1 < this.size)
			{
				if (this.comparer.Compare(this.heap[i].last, this.heap[currentmax].last) > 0)
				{
					currentmax = i;
				}
			}
			else if (2 * i + 1 == this.size && this.comparer.Compare(this.heap[i].first, this.heap[currentmax].last) > 0)
			{
				currentmax = i;
				firstmax = true;
			}
			if (2 * r + 1 < this.size)
			{
				if (this.comparer.Compare(this.heap[r].last, this.heap[currentmax].last) > 0)
				{
					currentmax = r;
				}
			}
			else if (2 * r + 1 == this.size && this.comparer.Compare(this.heap[r].first, this.heap[currentmax].last) > 0)
			{
				currentmax = r;
				firstmax = true;
			}
			if (currentmax != cell)
			{
				if (firstmax)
				{
					this.SwapFirstWithLast(currentmax, cell);
				}
				else
				{
					this.SwapLastWithLast(currentmax, cell);
				}
				this.HeapifyMax(currentmax);
			}
			return swappedroot;
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x001CA0AC File Offset: 0x001C82AC
		private void BubbleUpMin(int i)
		{
			if (i > 0)
			{
				T min = this.heap[i].first;
				T iv = min;
				int num = (i + 1) / 2;
				int p;
				while (i > 0 && this.comparer.Compare(iv, min = this.heap[p = (i + 1) / 2 - 1].first) < 0)
				{
					this.UpdateFirst(i, min);
					i = p;
				}
				this.UpdateFirst(i, iv);
			}
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x001CA11C File Offset: 0x001C831C
		private void BubbleUpMax(int i)
		{
			if (i > 0)
			{
				T max = this.heap[i].last;
				T iv = max;
				int num = (i + 1) / 2;
				int p;
				while (i > 0 && this.comparer.Compare(iv, max = this.heap[p = (i + 1) / 2 - 1].last) > 0)
				{
					this.UpdateLast(i, max);
					i = p;
				}
				this.UpdateLast(i, iv);
			}
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x001CA18A File Offset: 0x001C838A
		public IntervalHeap() : this(16)
		{
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x001CA194 File Offset: 0x001C8394
		public IntervalHeap(int capacity) : this(capacity, Comparer<T>.Default, EqualityComparer<T>.Default)
		{
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x001CA1A8 File Offset: 0x001C83A8
		private IntervalHeap(int capacity, IComparer<T> comparer, IEqualityComparer<T> itemequalityComparer)
		{
			if (comparer == null)
			{
				throw new NullReferenceException("Item comparer cannot be null");
			}
			this.comparer = comparer;
			if (itemequalityComparer == null)
			{
				throw new NullReferenceException("Item equality comparer cannot be null");
			}
			this.itemequalityComparer = itemequalityComparer;
			int length;
			for (length = 1; length < capacity; length <<= 1)
			{
			}
			this.heap = new IntervalHeap<T>.Interval[length];
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x001CA1FF File Offset: 0x001C83FF
		public T FindMin()
		{
			if (this.size == 0)
			{
				throw new NoSuchItemException();
			}
			return this.heap[0].first;
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x001CA220 File Offset: 0x001C8420
		public T DeleteMin()
		{
			IPriorityQueueHandle<T> priorityQueueHandle;
			return this.DeleteMin(out priorityQueueHandle);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x001CA238 File Offset: 0x001C8438
		public T FindMax()
		{
			if (this.size == 0)
			{
				throw new NoSuchItemException("Heap is empty");
			}
			if (this.size == 1)
			{
				return this.heap[0].first;
			}
			return this.heap[0].last;
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x001CA284 File Offset: 0x001C8484
		public T DeleteMax()
		{
			IPriorityQueueHandle<T> priorityQueueHandle;
			return this.DeleteMax(out priorityQueueHandle);
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x001CA299 File Offset: 0x001C8499
		public IComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x001CA2A4 File Offset: 0x001C84A4
		public void Clear()
		{
			this.stamp++;
			if (this.size == 0)
			{
				return;
			}
			int len = (this.size % 2 == 0) ? (this.size / 2) : (this.size / 2 + 1);
			IntervalHeap<T>.Interval[] _heap = this.heap;
			for (int i = 0; i < len; i++)
			{
				_heap[i].first = default(T);
				_heap[i].last = default(T);
			}
			this.size = 0;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600438A RID: 17290 RVA: 0x001CA325 File Offset: 0x001C8525
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x001CA328 File Offset: 0x001C8528
		public bool AllowsDuplicates
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600438C RID: 17292 RVA: 0x001CA32B File Offset: 0x001C852B
		public virtual IEqualityComparer<T> EqualityComparer
		{
			get
			{
				return this.itemequalityComparer;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x001CA333 File Offset: 0x001C8533
		public virtual bool DuplicatesByCounting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x001CA336 File Offset: 0x001C8536
		public bool Add(T item)
		{
			this.stamp++;
			if (this.add(item))
			{
				this.raiseItemsAdded(item, 1);
				this.raiseCollectionChanged();
				return true;
			}
			return false;
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x001CA360 File Offset: 0x001C8560
		private bool add(T item)
		{
			if (this.size == 0)
			{
				this.size = 1;
				this.UpdateFirst(0, item);
				return true;
			}
			if (this.size == 2 * this.heap.Length)
			{
				IntervalHeap<T>.Interval[] newheap = new IntervalHeap<T>.Interval[2 * this.heap.Length];
				Array.Copy(this.heap, newheap, this.heap.Length);
				this.heap = newheap;
			}
			if (this.size % 2 == 0)
			{
				int i = this.size / 2;
				int p = (i + 1) / 2 - 1;
				T tmp = this.heap[p].last;
				if (this.comparer.Compare(item, tmp) > 0)
				{
					this.UpdateFirst(i, tmp);
					this.UpdateLast(p, item);
					this.BubbleUpMax(p);
				}
				else
				{
					this.UpdateFirst(i, item);
					if (this.comparer.Compare(item, this.heap[p].first) < 0)
					{
						this.BubbleUpMin(i);
					}
				}
			}
			else
			{
				int j = this.size / 2;
				T other = this.heap[j].first;
				if (this.comparer.Compare(item, other) < 0)
				{
					this.UpdateLast(j, other);
					this.UpdateFirst(j, item);
					this.BubbleUpMin(j);
				}
				else
				{
					this.UpdateLast(j, item);
					this.BubbleUpMax(j);
				}
			}
			this.size++;
			return true;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x001CA4BC File Offset: 0x001C86BC
		private void UpdateLast(int cell, T item)
		{
			this.heap[cell].last = item;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x001CA4D0 File Offset: 0x001C86D0
		private void UpdateFirst(int cell, T item)
		{
			this.heap[cell].first = item;
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x001CA4E4 File Offset: 0x001C86E4
		public void AddAll(IEnumerable<T> items)
		{
			this.stamp++;
			int oldsize = this.size;
			foreach (T item in items)
			{
				this.add(item);
			}
			if (this.size != oldsize)
			{
				if ((this.ActiveEvents & EventTypeEnum.Added) != EventTypeEnum.None)
				{
					foreach (T item2 in items)
					{
						this.raiseItemsAdded(item2, 1);
					}
				}
				this.raiseCollectionChanged();
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06004393 RID: 17299 RVA: 0x001CA594 File Offset: 0x001C8794
		public override bool IsEmpty
		{
			get
			{
				return this.size == 0;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x001CA59F File Offset: 0x001C879F
		public override int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06004395 RID: 17301 RVA: 0x001CA5A7 File Offset: 0x001C87A7
		public override Speed CountSpeed
		{
			get
			{
				return Speed.Constant;
			}
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x001CA5AA File Offset: 0x001C87AA
		public override T Choose()
		{
			if (this.size == 0)
			{
				throw new NoSuchItemException("Collection is empty");
			}
			return this.heap[0].first;
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x001CA5D0 File Offset: 0x001C87D0
		public override IEnumerator<T> GetEnumerator()
		{
			int mystamp = this.stamp;
			int num;
			for (int i = 0; i < this.size; i = num + 1)
			{
				if (mystamp != this.stamp)
				{
					throw new CollectionModifiedException();
				}
				yield return (i % 2 == 0) ? this.heap[i >> 1].first : this.heap[i >> 1].last;
				num = i;
			}
			yield break;
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x001CA5E0 File Offset: 0x001C87E0
		private bool Check(int i, T min, T max)
		{
			bool retval = true;
			IntervalHeap<T>.Interval interval = this.heap[i];
			T first = interval.first;
			T last = interval.last;
			if (2 * i + 1 == this.size)
			{
				if (this.comparer.Compare(min, first) > 0)
				{
					Logger.Log(string.Format("Cell {0}: parent.first({1}) > first({2})  [size={3}]", new object[]
					{
						i,
						min,
						first,
						this.size
					}));
					retval = false;
				}
				if (this.comparer.Compare(first, max) > 0)
				{
					Logger.Log(string.Format("Cell {0}: first({1}) > parent.last({2})  [size={3}]", new object[]
					{
						i,
						first,
						max,
						this.size
					}));
					retval = false;
				}
				return retval;
			}
			if (this.comparer.Compare(min, first) > 0)
			{
				Logger.Log(string.Format("Cell {0}: parent.first({1}) > first({2})  [size={3}]", new object[]
				{
					i,
					min,
					first,
					this.size
				}));
				retval = false;
			}
			if (this.comparer.Compare(first, last) > 0)
			{
				Logger.Log(string.Format("Cell {0}: first({1}) > last({2})  [size={3}]", new object[]
				{
					i,
					first,
					last,
					this.size
				}));
				retval = false;
			}
			if (this.comparer.Compare(last, max) > 0)
			{
				Logger.Log(string.Format("Cell {0}: last({1}) > parent.last({2})  [size={3}]", new object[]
				{
					i,
					last,
					max,
					this.size
				}));
				retval = false;
			}
			int j = 2 * i + 1;
			int r = j + 1;
			if (2 * j < this.size)
			{
				retval = (retval && this.Check(j, first, last));
			}
			if (2 * r < this.size)
			{
				retval = (retval && this.Check(r, first, last));
			}
			return retval;
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x001CA808 File Offset: 0x001C8A08
		public bool Check()
		{
			if (this.size == 0)
			{
				return true;
			}
			if (this.size == 1)
			{
				return this.heap[0].first != null;
			}
			return this.Check(0, this.heap[0].first, this.heap[0].last);
		}

		// Token: 0x170003CE RID: 974
		public T this[IPriorityQueueHandle<T> handle]
		{
			get
			{
				int cell;
				bool isfirst;
				this.CheckHandle(handle, out cell, out isfirst);
				if (!isfirst)
				{
					return this.heap[cell].last;
				}
				return this.heap[cell].first;
			}
			set
			{
				this.Replace(handle, value);
			}
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x001CA8B8 File Offset: 0x001C8AB8
		public bool Find(IPriorityQueueHandle<T> handle, out T item)
		{
			IntervalHeap<T>.Handle myhandle = handle as IntervalHeap<T>.Handle;
			if (myhandle == null)
			{
				item = default(T);
				return false;
			}
			int toremove = myhandle.index;
			int cell = toremove / 2;
			bool isfirst = toremove % 2 == 0;
			if (toremove == -1 || toremove >= this.size)
			{
				item = default(T);
				return false;
			}
			if (null != myhandle)
			{
				item = default(T);
				return false;
			}
			item = (isfirst ? this.heap[cell].first : this.heap[cell].last);
			return true;
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x001CA93C File Offset: 0x001C8B3C
		public bool Add(ref IPriorityQueueHandle<T> handle, T item)
		{
			this.stamp++;
			IntervalHeap<T>.Handle myhandle = (IntervalHeap<T>.Handle)handle;
			if (myhandle == null)
			{
				handle = new IntervalHeap<T>.Handle();
			}
			else if (myhandle.index != -1)
			{
				throw new InvalidPriorityQueueHandleException("Handle not valid for reuse");
			}
			if (this.add(item))
			{
				this.raiseItemsAdded(item, 1);
				this.raiseCollectionChanged();
				return true;
			}
			return false;
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x001CA99C File Offset: 0x001C8B9C
		public T Delete(IPriorityQueueHandle<T> handle)
		{
			this.stamp++;
			int cell;
			bool isfirst;
			this.CheckHandle(handle, out cell, out isfirst).index = -1;
			int lastcell = (this.size - 1) / 2;
			T retval;
			if (cell == lastcell)
			{
				if (isfirst)
				{
					retval = this.heap[cell].first;
					if (this.size % 2 == 0)
					{
						this.UpdateFirst(cell, this.heap[cell].last);
						this.heap[cell].last = default(T);
					}
					else
					{
						this.heap[cell].first = default(T);
					}
				}
				else
				{
					retval = this.heap[cell].last;
					this.heap[cell].last = default(T);
				}
				this.size--;
			}
			else if (isfirst)
			{
				retval = this.heap[cell].first;
				if (this.size % 2 == 0)
				{
					this.UpdateFirst(cell, this.heap[lastcell].last);
					this.heap[lastcell].last = default(T);
				}
				else
				{
					this.UpdateFirst(cell, this.heap[lastcell].first);
					this.heap[lastcell].first = default(T);
				}
				this.size--;
				if (this.HeapifyMin(cell))
				{
					this.BubbleUpMax(cell);
				}
				else
				{
					this.BubbleUpMin(cell);
				}
			}
			else
			{
				retval = this.heap[cell].last;
				if (this.size % 2 == 0)
				{
					this.UpdateLast(cell, this.heap[lastcell].last);
					this.heap[lastcell].last = default(T);
				}
				else
				{
					this.UpdateLast(cell, this.heap[lastcell].first);
					this.heap[lastcell].first = default(T);
				}
				this.size--;
				if (this.HeapifyMax(cell))
				{
					this.BubbleUpMin(cell);
				}
				else
				{
					this.BubbleUpMax(cell);
				}
			}
			this.raiseItemsRemoved(retval, 1);
			this.raiseCollectionChanged();
			return retval;
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x001CABE4 File Offset: 0x001C8DE4
		private IntervalHeap<T>.Handle CheckHandle(IPriorityQueueHandle<T> handle, out int cell, out bool isfirst)
		{
			IntervalHeap<T>.Handle myhandle = (IntervalHeap<T>.Handle)handle;
			int toremove = myhandle.index;
			cell = toremove / 2;
			isfirst = (toremove % 2 == 0);
			if (toremove == -1 || toremove >= this.size)
			{
				throw new InvalidPriorityQueueHandleException("Invalid handle, index out of range");
			}
			if (null != myhandle)
			{
				throw new InvalidPriorityQueueHandleException("Invalid handle, doesn't match queue");
			}
			return myhandle;
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x001CAC34 File Offset: 0x001C8E34
		public T Replace(IPriorityQueueHandle<T> handle, T item)
		{
			this.stamp++;
			int cell;
			bool isfirst;
			this.CheckHandle(handle, out cell, out isfirst);
			if (this.size == 0)
			{
				throw new NoSuchItemException();
			}
			T retval;
			if (isfirst)
			{
				retval = this.heap[cell].first;
				this.heap[cell].first = item;
				if (this.size != 1)
				{
					if (this.size == 2 * cell + 1)
					{
						int p = (cell + 1) / 2 - 1;
						if (this.comparer.Compare(item, this.heap[p].last) > 0)
						{
							this.UpdateFirst(cell, this.heap[p].last);
							this.UpdateLast(p, item);
							this.BubbleUpMax(p);
						}
						else
						{
							this.BubbleUpMin(cell);
						}
					}
					else if (this.HeapifyMin(cell))
					{
						this.BubbleUpMax(cell);
					}
					else
					{
						this.BubbleUpMin(cell);
					}
				}
			}
			else
			{
				retval = this.heap[cell].last;
				this.heap[cell].last = item;
				if (this.HeapifyMax(cell))
				{
					this.BubbleUpMin(cell);
				}
				else
				{
					this.BubbleUpMax(cell);
				}
			}
			this.raiseItemsRemoved(retval, 1);
			this.raiseItemsAdded(item, 1);
			this.raiseCollectionChanged();
			return retval;
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x001CAD75 File Offset: 0x001C8F75
		public T FindMin(out IPriorityQueueHandle<T> handle)
		{
			if (this.size == 0)
			{
				throw new NoSuchItemException();
			}
			handle = null;
			return this.heap[0].first;
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x001CAD9C File Offset: 0x001C8F9C
		public T FindMax(out IPriorityQueueHandle<T> handle)
		{
			if (this.size == 0)
			{
				throw new NoSuchItemException();
			}
			if (this.size == 1)
			{
				handle = null;
				return this.heap[0].first;
			}
			handle = null;
			return this.heap[0].last;
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x001CADEC File Offset: 0x001C8FEC
		public T DeleteMin(out IPriorityQueueHandle<T> handle)
		{
			this.stamp++;
			if (this.size == 0)
			{
				throw new NoSuchItemException();
			}
			T retval = this.heap[0].first;
			handle = null;
			if (this.size == 1)
			{
				this.size = 0;
				this.heap[0].first = default(T);
			}
			else
			{
				int lastcell = (this.size - 1) / 2;
				if (this.size % 2 == 0)
				{
					this.UpdateFirst(0, this.heap[lastcell].last);
					this.heap[lastcell].last = default(T);
				}
				else
				{
					this.UpdateFirst(0, this.heap[lastcell].first);
					this.heap[lastcell].first = default(T);
				}
				this.size--;
				this.HeapifyMin(0);
			}
			this.raiseItemsRemoved(retval, 1);
			this.raiseCollectionChanged();
			return retval;
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x001CAEF0 File Offset: 0x001C90F0
		public T DeleteMax(out IPriorityQueueHandle<T> handle)
		{
			this.stamp++;
			if (this.size == 0)
			{
				throw new NoSuchItemException();
			}
			handle = null;
			T retval;
			if (this.size == 1)
			{
				this.size = 0;
				retval = this.heap[0].first;
				this.heap[0].first = default(T);
			}
			else
			{
				retval = this.heap[0].last;
				int lastcell = (this.size - 1) / 2;
				if (this.size % 2 == 0)
				{
					this.UpdateLast(0, this.heap[lastcell].last);
					this.heap[lastcell].last = default(T);
				}
				else
				{
					this.UpdateLast(0, this.heap[lastcell].first);
					this.heap[lastcell].first = default(T);
				}
				this.size--;
				this.HeapifyMax(0);
			}
			this.raiseItemsRemoved(retval, 1);
			this.raiseCollectionChanged();
			return retval;
		}

		// Token: 0x0400311A RID: 12570
		private int stamp;

		// Token: 0x0400311B RID: 12571
		private readonly IComparer<T> comparer;

		// Token: 0x0400311C RID: 12572
		private readonly IEqualityComparer<T> itemequalityComparer;

		// Token: 0x0400311D RID: 12573
		private IntervalHeap<T>.Interval[] heap;

		// Token: 0x0400311E RID: 12574
		private int size;

		// Token: 0x02000B39 RID: 2873
		private struct Interval
		{
			// Token: 0x06005413 RID: 21523 RVA: 0x0020EAB8 File Offset: 0x0020CCB8
			public override string ToString()
			{
				return string.Format("[{0}; {1}]", this.first, this.last);
			}

			// Token: 0x04003D08 RID: 15624
			internal T first;

			// Token: 0x04003D09 RID: 15625
			internal T last;
		}

		// Token: 0x02000B3A RID: 2874
		private class Handle : IPriorityQueueHandle<T>
		{
			// Token: 0x06005414 RID: 21524 RVA: 0x0020EADA File Offset: 0x0020CCDA
			public override string ToString()
			{
				return string.Format("[{0}]", this.index);
			}

			// Token: 0x04003D0A RID: 15626
			internal int index = -1;
		}
	}
}
