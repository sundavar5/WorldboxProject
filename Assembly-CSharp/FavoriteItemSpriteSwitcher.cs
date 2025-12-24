using System;
using System.Collections.Generic;

// Token: 0x0200084C RID: 2124
public class FavoriteItemSpriteSwitcher : SpriteSwitcher
{
	// Token: 0x0600427E RID: 17022 RVA: 0x001C2CD8 File Offset: 0x001C0ED8
	protected override bool hasAny()
	{
		using (IEnumerator<Item> enumerator = World.world.items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isFavorite())
				{
					return true;
				}
			}
		}
		return false;
	}
}
