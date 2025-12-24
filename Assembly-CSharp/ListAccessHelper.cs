using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

// Token: 0x0200046E RID: 1134
internal static class ListAccessHelper
{
	// Token: 0x04001D1B RID: 7451
	public static readonly int ItemsOffset = 0;

	// Token: 0x04001D1C RID: 7452
	public static readonly int SizeOffset = 8;

	// Token: 0x04001D1D RID: 7453
	public static readonly int VersionOffset = 12;

	// Token: 0x02000A39 RID: 2617
	internal class ListDataHelper<T> : IEquatable<ListAccessHelper.ListDataHelper<T>>
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x001FCBC0 File Offset: 0x001FADC0
		[Nullable(1)]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ListAccessHelper.ListDataHelper<T>);
			}
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x001FCBCC File Offset: 0x001FADCC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ListDataHelper");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x001FCC18 File Offset: 0x001FAE18
		[NullableContext(1)]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("_items = ");
			builder.Append(this._items);
			builder.Append(", _size = ");
			builder.Append(this._size.ToString());
			builder.Append(", _version = ");
			builder.Append(this._version.ToString());
			return true;
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x001FCC8C File Offset: 0x001FAE8C
		[CompilerGenerated]
		public static bool operator !=([Nullable(new byte[]
		{
			2,
			0
		})] ListAccessHelper.ListDataHelper<T> left, [Nullable(new byte[]
		{
			2,
			0
		})] ListAccessHelper.ListDataHelper<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x001FCC98 File Offset: 0x001FAE98
		[CompilerGenerated]
		public static bool operator ==([Nullable(new byte[]
		{
			2,
			0
		})] ListAccessHelper.ListDataHelper<T> left, [Nullable(new byte[]
		{
			2,
			0
		})] ListAccessHelper.ListDataHelper<T> right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x001FCCAC File Offset: 0x001FAEAC
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<T[]>.Default.GetHashCode(this._items)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this._size)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this._version);
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x001FCD0E File Offset: 0x001FAF0E
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ListAccessHelper.ListDataHelper<T>);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x001FCD1C File Offset: 0x001FAF1C
		[CompilerGenerated]
		public virtual bool Equals([Nullable(new byte[]
		{
			2,
			0
		})] ListAccessHelper.ListDataHelper<T> other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<T[]>.Default.Equals(this._items, other._items) && EqualityComparer<int>.Default.Equals(this._size, other._size) && EqualityComparer<int>.Default.Equals(this._version, other._version));
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x001FCD95 File Offset: 0x001FAF95
		[CompilerGenerated]
		protected ListDataHelper([Nullable(new byte[]
		{
			1,
			0
		})] ListAccessHelper.ListDataHelper<T> original)
		{
			this._items = original._items;
			this._size = original._size;
			this._version = original._version;
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x001FCDC1 File Offset: 0x001FAFC1
		public ListDataHelper()
		{
		}

		// Token: 0x04003894 RID: 14484
		public T[] _items;

		// Token: 0x04003895 RID: 14485
		public int _size;

		// Token: 0x04003896 RID: 14486
		public int _version;
	}
}
