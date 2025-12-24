using System;
using System.Collections.Generic;

// Token: 0x02000240 RID: 576
[Serializable]
public class StorageBooks : IDisposable
{
	// Token: 0x060015E6 RID: 5606 RVA: 0x000E0EBA File Offset: 0x000DF0BA
	public void Dispose()
	{
		this.list_books.Clear();
		this.list_books = null;
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x000E0ECE File Offset: 0x000DF0CE
	public void addBook(Book pBook)
	{
		this.list_books.Add(pBook.id);
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x000E0EE1 File Offset: 0x000DF0E1
	public bool hasAny()
	{
		return this.list_books.Count > 0;
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x000E0EF1 File Offset: 0x000DF0F1
	public int totalBooks()
	{
		return this.list_books.Count;
	}

	// Token: 0x04001260 RID: 4704
	public List<long> list_books = new List<long>();
}
