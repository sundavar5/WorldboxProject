using System;
using System.Collections.Generic;

// Token: 0x0200064A RID: 1610
public interface IBooksWindow
{
	// Token: 0x06003465 RID: 13413
	List<long> getBooks();

	// Token: 0x06003466 RID: 13414 RVA: 0x00185B3A File Offset: 0x00183D3A
	bool hasBooks()
	{
		return this.getBooks().Count > 0;
	}
}
