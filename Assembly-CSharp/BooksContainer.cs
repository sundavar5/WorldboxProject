using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000645 RID: 1605
public class BooksContainer : BooksElement
{
	// Token: 0x06003448 RID: 13384 RVA: 0x001856A9 File Offset: 0x001838A9
	protected override void Awake()
	{
		this._prefab_book = Resources.Load<CultureBookButton>("ui/PrefabBook");
		this._pool_books = new ObjectPoolGenericMono<CultureBookButton>(this._prefab_book, this._books_grid);
		base.Awake();
	}

	// Token: 0x06003449 RID: 13385 RVA: 0x001856D8 File Offset: 0x001838D8
	protected override IEnumerator showContent()
	{
		BooksContainer.<showContent>d__5 <showContent>d__ = new BooksContainer.<showContent>d__5(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x0600344A RID: 13386 RVA: 0x001856E7 File Offset: 0x001838E7
	public void loadBookButton(long pBookID)
	{
		this._pool_books.getNext().load(pBookID);
	}

	// Token: 0x0600344B RID: 13387 RVA: 0x001856FC File Offset: 0x001838FC
	protected override void clear()
	{
		this._pool_books.clear(true);
		if (this._title != null)
		{
			this._title.gameObject.SetActive(false);
		}
		this._books_grid.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x0600344C RID: 13388 RVA: 0x0018574C File Offset: 0x0018394C
	protected override void clearInitial()
	{
		for (int i = 0; i < this._books_grid.childCount; i++)
		{
			Object.Destroy(this._books_grid.GetChild(i).gameObject);
		}
		base.clearInitial();
	}

	// Token: 0x04002771 RID: 10097
	private ObjectPoolGenericMono<CultureBookButton> _pool_books;

	// Token: 0x04002772 RID: 10098
	private CultureBookButton _prefab_book;

	// Token: 0x04002773 RID: 10099
	[SerializeField]
	private Transform _title;

	// Token: 0x04002774 RID: 10100
	[SerializeField]
	private Transform _books_grid;
}
