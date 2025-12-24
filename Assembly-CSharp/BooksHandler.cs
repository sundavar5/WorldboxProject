using System;
using System.Collections.Generic;

// Token: 0x02000647 RID: 1607
public class BooksHandler
{
	// Token: 0x06003453 RID: 13395 RVA: 0x00185850 File Offset: 0x00183A50
	public void setMeta(Culture pCulture = null, Language pLanguage = null, Religion pReligion = null)
	{
		this._culture = pCulture;
		this._language = pLanguage;
		this._religion = pReligion;
		this.setDirty();
	}

	// Token: 0x06003454 RID: 13396 RVA: 0x0018586D File Offset: 0x00183A6D
	public List<long> getList()
	{
		this.checkBooks();
		return this._books;
	}

	// Token: 0x06003455 RID: 13397 RVA: 0x0018587B File Offset: 0x00183A7B
	public int count()
	{
		return this.getList().Count;
	}

	// Token: 0x06003456 RID: 13398 RVA: 0x00185888 File Offset: 0x00183A88
	public bool hasBooks()
	{
		return this.count() > 0;
	}

	// Token: 0x06003457 RID: 13399 RVA: 0x00185893 File Offset: 0x00183A93
	public void setDirty()
	{
		this._books_dirty = true;
	}

	// Token: 0x06003458 RID: 13400 RVA: 0x0018589C File Offset: 0x00183A9C
	private void checkBooks()
	{
		if (!this._books_dirty)
		{
			return;
		}
		this._books_dirty = false;
		this._books.Clear();
		foreach (Book tBook in World.world.books)
		{
			if (this._culture != null && tBook.getCulture() == this._culture)
			{
				this._books.Add(tBook.id);
			}
			else if (this._language != null && tBook.getLanguage() == this._language)
			{
				this._books.Add(tBook.id);
			}
			else if (this._religion != null && tBook.getReligion() == this._religion)
			{
				this._books.Add(tBook.id);
			}
		}
	}

	// Token: 0x06003459 RID: 13401 RVA: 0x00185980 File Offset: 0x00183B80
	public void clear()
	{
		this.setDirty();
		this._culture = null;
		this._language = null;
		this._religion = null;
	}

	// Token: 0x04002777 RID: 10103
	private readonly List<long> _books = new List<long>();

	// Token: 0x04002778 RID: 10104
	private bool _books_dirty = true;

	// Token: 0x04002779 RID: 10105
	private Culture _culture;

	// Token: 0x0400277A RID: 10106
	private Language _language;

	// Token: 0x0400277B RID: 10107
	private Religion _religion;
}
