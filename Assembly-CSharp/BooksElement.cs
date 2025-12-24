using System;
using System.Collections.Generic;

// Token: 0x02000646 RID: 1606
public class BooksElement : WindowMetaElementBase
{
	// Token: 0x0600344E RID: 13390 RVA: 0x00185793 File Offset: 0x00183993
	protected override void Awake()
	{
		this._books_window = base.GetComponentInParent<IBooksWindow>();
		base.Awake();
	}

	// Token: 0x0600344F RID: 13391 RVA: 0x001857A7 File Offset: 0x001839A7
	protected override void OnEnable()
	{
		this.books = this._books_window.getBooks();
		base.OnEnable();
	}

	// Token: 0x06003450 RID: 13392 RVA: 0x001857C0 File Offset: 0x001839C0
	protected override void OnDisable()
	{
		base.OnDisable();
		this.books = null;
	}

	// Token: 0x06003451 RID: 13393 RVA: 0x001857D0 File Offset: 0x001839D0
	public override bool checkRefreshWindow()
	{
		if (this.books != null)
		{
			foreach (long tBookID in this.books)
			{
				if (World.world.books.get(tBookID).isRekt())
				{
					return true;
				}
			}
		}
		return base.checkRefreshWindow();
	}

	// Token: 0x04002775 RID: 10101
	protected List<long> books;

	// Token: 0x04002776 RID: 10102
	private IBooksWindow _books_window;
}
