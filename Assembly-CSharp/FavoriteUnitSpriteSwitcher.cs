using System;
using System.Collections.Generic;

// Token: 0x0200084D RID: 2125
public class FavoriteUnitSpriteSwitcher : SpriteSwitcher
{
	// Token: 0x06004280 RID: 17024 RVA: 0x001C2D38 File Offset: 0x001C0F38
	protected override bool hasAny()
	{
		using (IEnumerator<Actor> enumerator = World.world.units.GetEnumerator())
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
