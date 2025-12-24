using System;
using ai.behaviours;

// Token: 0x020003A6 RID: 934
public class BehTryToRead : BehCityActor
{
	// Token: 0x060021ED RID: 8685 RVA: 0x0011E2D0 File Offset: 0x0011C4D0
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.city.hasBooksToRead(pActor))
		{
			return BehResult.Stop;
		}
		Book tBook;
		if (pActor.hasTag("can_read_any_book"))
		{
			tBook = pActor.city.getRandomBook();
		}
		else
		{
			if (!pActor.hasLanguage())
			{
				return BehResult.Stop;
			}
			if (!pActor.city.hasBooksOfLanguage(pActor.language))
			{
				return BehResult.Stop;
			}
			tBook = pActor.city.getRandomBookOfLanguage(pActor.language);
		}
		if (tBook == null)
		{
			return BehResult.Stop;
		}
		tBook.readIt();
		pActor.beh_book_target = tBook;
		return BehResult.Continue;
	}
}
