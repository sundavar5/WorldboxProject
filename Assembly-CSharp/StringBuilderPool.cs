using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x02000579 RID: 1401
public class StringBuilderPool : IDisposable, IEquatable<StringBuilderPool>, IEquatable<StringBuilder>
{
	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06002DED RID: 11757 RVA: 0x00165CF8 File Offset: 0x00163EF8
	private static StackPool<StringBuilder> pool
	{
		get
		{
			if (StringBuilderPool._pool == null)
			{
				StringBuilderPool._pool = new StackPool<StringBuilder>();
			}
			return StringBuilderPool._pool;
		}
	}

	// Token: 0x06002DEE RID: 11758 RVA: 0x00165D10 File Offset: 0x00163F10
	public StringBuilderPool()
	{
	}

	// Token: 0x06002DEF RID: 11759 RVA: 0x00165D28 File Offset: 0x00163F28
	public void Dispose()
	{
		if (!this._disposed)
		{
			this._string_builder.Clear();
			StringBuilderPool.pool.release(this._string_builder);
			this._disposed = true;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x00165D55 File Offset: 0x00163F55
	public StringBuilder string_builder
	{
		get
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("StringBuilderPool");
			}
			return this._string_builder;
		}
	}

	// Token: 0x06002DF1 RID: 11761 RVA: 0x00165D70 File Offset: 0x00163F70
	public StringBuilderPool ToTitleCase()
	{
		this._string_builder.ToTitleCase();
		return this;
	}

	// Token: 0x06002DF2 RID: 11762 RVA: 0x00165D7F File Offset: 0x00163F7F
	public StringBuilderPool ToUpper()
	{
		this._string_builder.ToUpper();
		return this;
	}

	// Token: 0x06002DF3 RID: 11763 RVA: 0x00165D8E File Offset: 0x00163F8E
	public StringBuilderPool ToUpperInvariant()
	{
		this._string_builder.ToUpperInvariant();
		return this;
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x00165D9D File Offset: 0x00163F9D
	public StringBuilderPool ToLower()
	{
		this._string_builder.ToLower();
		return this;
	}

	// Token: 0x06002DF5 RID: 11765 RVA: 0x00165DAC File Offset: 0x00163FAC
	public StringBuilderPool ToLowerInvariant()
	{
		this._string_builder.ToLowerInvariant();
		return this;
	}

	// Token: 0x06002DF6 RID: 11766 RVA: 0x00165DBB File Offset: 0x00163FBB
	public StringBuilderPool TrimEnd(params char[] trimChars)
	{
		this._string_builder.TrimEnd(trimChars);
		return this;
	}

	// Token: 0x06002DF7 RID: 11767 RVA: 0x00165DCB File Offset: 0x00163FCB
	public StringBuilderPool Cut(int startIndex, int end)
	{
		this._string_builder.CreateTrimmedString(startIndex, end);
		return this;
	}

	// Token: 0x06002DF8 RID: 11768 RVA: 0x00165DDC File Offset: 0x00163FDC
	public StringBuilderPool Remove(params char[] chars)
	{
		this._string_builder.Remove(chars);
		return this;
	}

	// Token: 0x06002DF9 RID: 11769 RVA: 0x00165DEC File Offset: 0x00163FEC
	public int IndexOf(char value)
	{
		return this._string_builder.IndexOf(value);
	}

	// Token: 0x06002DFA RID: 11770 RVA: 0x00165DFA File Offset: 0x00163FFA
	public int IndexOfAny(params char[] anyOf)
	{
		return this._string_builder.IndexOfAny(anyOf);
	}

	// Token: 0x06002DFB RID: 11771 RVA: 0x00165E08 File Offset: 0x00164008
	public int LastIndexOf(char value)
	{
		return this._string_builder.LastIndexOf(value);
	}

	// Token: 0x06002DFC RID: 11772 RVA: 0x00165E16 File Offset: 0x00164016
	public int LastIndexOfAny(char[] anyOf, int startIndex)
	{
		return this._string_builder.LastIndexOfAny(anyOf, startIndex);
	}

	// Token: 0x06002DFD RID: 11773 RVA: 0x00165E25 File Offset: 0x00164025
	public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
	{
		return this._string_builder.LastIndexOfAny(anyOf, startIndex, count);
	}

	// Token: 0x06002DFE RID: 11774 RVA: 0x00165E35 File Offset: 0x00164035
	public int LastIndexOfAny(params char[] anyOf)
	{
		return this._string_builder.LastIndexOfAny(anyOf);
	}

	// Token: 0x06002DFF RID: 11775 RVA: 0x00165E43 File Offset: 0x00164043
	public StringBuilderPool(int capacity)
	{
		this._string_builder = new StringBuilder(capacity);
	}

	// Token: 0x06002E00 RID: 11776 RVA: 0x00165E67 File Offset: 0x00164067
	public StringBuilderPool(string value)
	{
		this._string_builder = new StringBuilder(value);
	}

	// Token: 0x06002E01 RID: 11777 RVA: 0x00165E8B File Offset: 0x0016408B
	public StringBuilderPool(int capacity, int maxCapacity)
	{
		this._string_builder = new StringBuilder(capacity, maxCapacity);
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x00165EB0 File Offset: 0x001640B0
	public StringBuilderPool(string value, int capacity)
	{
		this._string_builder = new StringBuilder(value, capacity);
	}

	// Token: 0x06002E03 RID: 11779 RVA: 0x00165ED5 File Offset: 0x001640D5
	public StringBuilderPool(string value, int startIndex, int length, int capacity)
	{
		this._string_builder = new StringBuilder(value, startIndex, length, capacity);
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06002E04 RID: 11780 RVA: 0x00165EFD File Offset: 0x001640FD
	// (set) Token: 0x06002E05 RID: 11781 RVA: 0x00165F0A File Offset: 0x0016410A
	public int Capacity
	{
		get
		{
			return this._string_builder.Capacity;
		}
		set
		{
			this._string_builder.Capacity = value;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06002E06 RID: 11782 RVA: 0x00165F18 File Offset: 0x00164118
	public int MaxCapacity
	{
		get
		{
			return this._string_builder.MaxCapacity;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06002E07 RID: 11783 RVA: 0x00165F25 File Offset: 0x00164125
	// (set) Token: 0x06002E08 RID: 11784 RVA: 0x00165F32 File Offset: 0x00164132
	public int Length
	{
		get
		{
			return this._string_builder.Length;
		}
		set
		{
			this._string_builder.Length = value;
		}
	}

	// Token: 0x1700025C RID: 604
	public char this[int index]
	{
		get
		{
			return this._string_builder[index];
		}
		set
		{
			this._string_builder[index] = value;
		}
	}

	// Token: 0x06002E0B RID: 11787 RVA: 0x00165F5D File Offset: 0x0016415D
	public StringBuilderPool Append(bool value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E0C RID: 11788 RVA: 0x00165F6D File Offset: 0x0016416D
	public StringBuilderPool Append(byte value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E0D RID: 11789 RVA: 0x00165F7D File Offset: 0x0016417D
	public StringBuilderPool Append(char value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E0E RID: 11790 RVA: 0x00165F8D File Offset: 0x0016418D
	public StringBuilderPool Append(char[] value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E0F RID: 11791 RVA: 0x00165F9D File Offset: 0x0016419D
	public StringBuilderPool Append(char[] value, int startIndex, int charCount)
	{
		this._string_builder.Append(value, startIndex, charCount);
		return this;
	}

	// Token: 0x06002E10 RID: 11792 RVA: 0x00165FAF File Offset: 0x001641AF
	public StringBuilderPool Append(decimal value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E11 RID: 11793 RVA: 0x00165FBF File Offset: 0x001641BF
	public StringBuilderPool Append(double value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E12 RID: 11794 RVA: 0x00165FCF File Offset: 0x001641CF
	public StringBuilderPool Append(short value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E13 RID: 11795 RVA: 0x00165FDF File Offset: 0x001641DF
	public StringBuilderPool Append(int value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E14 RID: 11796 RVA: 0x00165FEF File Offset: 0x001641EF
	public StringBuilderPool Append(long value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E15 RID: 11797 RVA: 0x00165FFF File Offset: 0x001641FF
	public StringBuilderPool Append(object value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x0016600F File Offset: 0x0016420F
	public StringBuilderPool Append(ReadOnlyMemory<char> value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E17 RID: 11799 RVA: 0x00166024 File Offset: 0x00164224
	public StringBuilderPool Append(ReadOnlySpan<char> value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E18 RID: 11800 RVA: 0x00166034 File Offset: 0x00164234
	public StringBuilderPool Append(sbyte value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E19 RID: 11801 RVA: 0x00166044 File Offset: 0x00164244
	public StringBuilderPool Append(float value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E1A RID: 11802 RVA: 0x00166054 File Offset: 0x00164254
	public StringBuilderPool Append(string value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E1B RID: 11803 RVA: 0x00166064 File Offset: 0x00164264
	public StringBuilderPool Append(string value, int startIndex, int count)
	{
		this._string_builder.Append(value, startIndex, count);
		return this;
	}

	// Token: 0x06002E1C RID: 11804 RVA: 0x00166076 File Offset: 0x00164276
	public StringBuilderPool Append(StringBuilder value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E1D RID: 11805 RVA: 0x00166086 File Offset: 0x00164286
	public StringBuilderPool Append(StringBuilder value, int startIndex, int count)
	{
		this._string_builder.Append(value, startIndex, count);
		return this;
	}

	// Token: 0x06002E1E RID: 11806 RVA: 0x00166098 File Offset: 0x00164298
	public StringBuilderPool Append(StringBuilderPool value)
	{
		this._string_builder.Append(value.string_builder);
		return this;
	}

	// Token: 0x06002E1F RID: 11807 RVA: 0x001660AD File Offset: 0x001642AD
	public StringBuilderPool Append(StringBuilderPool value, int startIndex, int count)
	{
		this._string_builder.Append(value.string_builder, startIndex, count);
		return this;
	}

	// Token: 0x06002E20 RID: 11808 RVA: 0x001660C4 File Offset: 0x001642C4
	public StringBuilderPool Append(ushort value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x001660D4 File Offset: 0x001642D4
	public StringBuilderPool Append(uint value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x001660E4 File Offset: 0x001642E4
	public StringBuilderPool Append(ulong value)
	{
		this._string_builder.Append(value);
		return this;
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x001660F4 File Offset: 0x001642F4
	public StringBuilderPool AppendFormat(IFormatProvider provider, string format, params object[] args)
	{
		this._string_builder.AppendFormat(provider, format, args);
		return this;
	}

	// Token: 0x06002E24 RID: 11812 RVA: 0x00166106 File Offset: 0x00164306
	public StringBuilderPool AppendFormat(string format, object arg0)
	{
		this._string_builder.AppendFormat(format, arg0);
		return this;
	}

	// Token: 0x06002E25 RID: 11813 RVA: 0x00166117 File Offset: 0x00164317
	public StringBuilderPool AppendFormat(string format, object arg0, object arg1)
	{
		this._string_builder.AppendFormat(format, arg0, arg1);
		return this;
	}

	// Token: 0x06002E26 RID: 11814 RVA: 0x00166129 File Offset: 0x00164329
	public StringBuilderPool AppendFormat(string format, object arg0, object arg1, object arg2)
	{
		this._string_builder.AppendFormat(format, arg0, arg1, arg2);
		return this;
	}

	// Token: 0x06002E27 RID: 11815 RVA: 0x0016613D File Offset: 0x0016433D
	public StringBuilderPool AppendFormat(string format, params object[] args)
	{
		this._string_builder.AppendFormat(format, args);
		return this;
	}

	// Token: 0x06002E28 RID: 11816 RVA: 0x0016614E File Offset: 0x0016434E
	public StringBuilderPool AppendJoin(char separator, params object[] values)
	{
		this._string_builder.AppendJoin(separator, values);
		return this;
	}

	// Token: 0x06002E29 RID: 11817 RVA: 0x0016615F File Offset: 0x0016435F
	public StringBuilderPool AppendJoin(char separator, params string[] values)
	{
		this._string_builder.AppendJoin(separator, values);
		return this;
	}

	// Token: 0x06002E2A RID: 11818 RVA: 0x00166170 File Offset: 0x00164370
	public StringBuilderPool AppendJoin(string separator, params object[] values)
	{
		this._string_builder.AppendJoin(separator, values);
		return this;
	}

	// Token: 0x06002E2B RID: 11819 RVA: 0x00166181 File Offset: 0x00164381
	public StringBuilderPool AppendJoin(string separator, params string[] values)
	{
		this._string_builder.AppendJoin(separator, values);
		return this;
	}

	// Token: 0x06002E2C RID: 11820 RVA: 0x00166192 File Offset: 0x00164392
	public StringBuilderPool AppendJoin<T>(char separator, IEnumerable<T> values)
	{
		this._string_builder.AppendJoin<T>(separator, values);
		return this;
	}

	// Token: 0x06002E2D RID: 11821 RVA: 0x001661A3 File Offset: 0x001643A3
	public StringBuilderPool AppendJoin<T>(string separator, IEnumerable<T> values)
	{
		this._string_builder.AppendJoin<T>(separator, values);
		return this;
	}

	// Token: 0x06002E2E RID: 11822 RVA: 0x001661B4 File Offset: 0x001643B4
	public StringBuilderPool AppendLine()
	{
		this._string_builder.AppendLine();
		return this;
	}

	// Token: 0x06002E2F RID: 11823 RVA: 0x001661C3 File Offset: 0x001643C3
	public StringBuilderPool AppendLine(string value)
	{
		this._string_builder.AppendLine(value);
		return this;
	}

	// Token: 0x06002E30 RID: 11824 RVA: 0x001661D3 File Offset: 0x001643D3
	public StringBuilderPool Clear()
	{
		this._string_builder.Clear();
		return this;
	}

	// Token: 0x06002E31 RID: 11825 RVA: 0x001661E2 File Offset: 0x001643E2
	public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
	{
		this._string_builder.CopyTo(sourceIndex, destination, destinationIndex, count);
	}

	// Token: 0x06002E32 RID: 11826 RVA: 0x001661F4 File Offset: 0x001643F4
	public void CopyTo(int sourceIndex, Span<char> destination, int count)
	{
		this._string_builder.CopyTo(sourceIndex, destination, count);
	}

	// Token: 0x06002E33 RID: 11827 RVA: 0x00166204 File Offset: 0x00164404
	public int EnsureCapacity(int capacity)
	{
		return this._string_builder.EnsureCapacity(capacity);
	}

	// Token: 0x06002E34 RID: 11828 RVA: 0x00166212 File Offset: 0x00164412
	public bool Equals(ReadOnlySpan<char> span)
	{
		return this._string_builder.Equals(span);
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x00166220 File Offset: 0x00164420
	public bool Equals(StringBuilder sb)
	{
		return this._string_builder.Equals(sb);
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x0016622E File Offset: 0x0016442E
	public bool Equals(StringBuilderPool sb)
	{
		return this._string_builder.Equals(sb.string_builder);
	}

	// Token: 0x06002E37 RID: 11831 RVA: 0x00166241 File Offset: 0x00164441
	public StringBuilderPool Insert(int index, bool value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E38 RID: 11832 RVA: 0x00166252 File Offset: 0x00164452
	public StringBuilderPool Insert(int index, byte value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E39 RID: 11833 RVA: 0x00166263 File Offset: 0x00164463
	public StringBuilderPool Insert(int index, char value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E3A RID: 11834 RVA: 0x00166274 File Offset: 0x00164474
	public StringBuilderPool Insert(int index, char[] value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E3B RID: 11835 RVA: 0x00166285 File Offset: 0x00164485
	public StringBuilderPool Insert(int index, char[] value, int startIndex, int charCount)
	{
		this._string_builder.Insert(index, value, startIndex, charCount);
		return this;
	}

	// Token: 0x06002E3C RID: 11836 RVA: 0x00166299 File Offset: 0x00164499
	public StringBuilderPool Insert(int index, decimal value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E3D RID: 11837 RVA: 0x001662AA File Offset: 0x001644AA
	public StringBuilderPool Insert(int index, double value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E3E RID: 11838 RVA: 0x001662BB File Offset: 0x001644BB
	public StringBuilderPool Insert(int index, short value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E3F RID: 11839 RVA: 0x001662CC File Offset: 0x001644CC
	public StringBuilderPool Insert(int index, int value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E40 RID: 11840 RVA: 0x001662DD File Offset: 0x001644DD
	public StringBuilderPool Insert(int index, long value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E41 RID: 11841 RVA: 0x001662EE File Offset: 0x001644EE
	public StringBuilderPool Insert(int index, object value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E42 RID: 11842 RVA: 0x001662FF File Offset: 0x001644FF
	public StringBuilderPool Insert(int index, ReadOnlySpan<char> value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E43 RID: 11843 RVA: 0x00166310 File Offset: 0x00164510
	public StringBuilderPool Insert(int index, sbyte value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E44 RID: 11844 RVA: 0x00166321 File Offset: 0x00164521
	public StringBuilderPool Insert(int index, float value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E45 RID: 11845 RVA: 0x00166332 File Offset: 0x00164532
	public StringBuilderPool Insert(int index, string value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E46 RID: 11846 RVA: 0x00166343 File Offset: 0x00164543
	public StringBuilderPool Insert(int index, string value, int count)
	{
		this._string_builder.Insert(index, value, count);
		return this;
	}

	// Token: 0x06002E47 RID: 11847 RVA: 0x00166355 File Offset: 0x00164555
	public StringBuilderPool Insert(int index, ushort value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E48 RID: 11848 RVA: 0x00166366 File Offset: 0x00164566
	public StringBuilderPool Insert(int index, uint value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E49 RID: 11849 RVA: 0x00166377 File Offset: 0x00164577
	public StringBuilderPool Insert(int index, ulong value)
	{
		this._string_builder.Insert(index, value);
		return this;
	}

	// Token: 0x06002E4A RID: 11850 RVA: 0x00166388 File Offset: 0x00164588
	public StringBuilderPool Remove(int startIndex, int length)
	{
		this._string_builder.Remove(startIndex, length);
		return this;
	}

	// Token: 0x06002E4B RID: 11851 RVA: 0x00166399 File Offset: 0x00164599
	public StringBuilderPool Replace(char oldChar, char newChar)
	{
		this._string_builder.Replace(oldChar, newChar);
		return this;
	}

	// Token: 0x06002E4C RID: 11852 RVA: 0x001663AA File Offset: 0x001645AA
	public StringBuilderPool Replace(char oldChar, char newChar, int startIndex, int count)
	{
		this._string_builder.Replace(oldChar, newChar, startIndex, count);
		return this;
	}

	// Token: 0x06002E4D RID: 11853 RVA: 0x001663BE File Offset: 0x001645BE
	public StringBuilderPool Replace(string oldValue, string newValue)
	{
		this._string_builder.Replace(oldValue, newValue);
		return this;
	}

	// Token: 0x06002E4E RID: 11854 RVA: 0x001663CF File Offset: 0x001645CF
	public StringBuilderPool Replace(string oldValue, string newValue, int startIndex, int count)
	{
		this._string_builder.Replace(oldValue, newValue, startIndex, count);
		return this;
	}

	// Token: 0x06002E4F RID: 11855 RVA: 0x001663E3 File Offset: 0x001645E3
	public string ToString(int startIndex, int length)
	{
		return this._string_builder.ToString(startIndex, length);
	}

	// Token: 0x06002E50 RID: 11856 RVA: 0x001663F2 File Offset: 0x001645F2
	public override string ToString()
	{
		return this._string_builder.ToString();
	}

	// Token: 0x06002E51 RID: 11857 RVA: 0x00166400 File Offset: 0x00164600
	public Span<char> AsSpan()
	{
		Span<char> tSpan = new char[this._string_builder.Length];
		this._string_builder.CopyTo(0, tSpan, this._string_builder.Length);
		return tSpan;
	}

	// Token: 0x040022C4 RID: 8900
	[ThreadStatic]
	private static StackPool<StringBuilder> _pool;

	// Token: 0x040022C5 RID: 8901
	private StringBuilder _string_builder = StringBuilderPool.pool.get();

	// Token: 0x040022C6 RID: 8902
	private bool _disposed;
}
