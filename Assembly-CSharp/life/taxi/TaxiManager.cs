using System;
using System.Collections.Generic;

namespace life.taxi
{
	// Token: 0x02000875 RID: 2165
	public class TaxiManager
	{
		// Token: 0x06004404 RID: 17412 RVA: 0x001CBF1C File Offset: 0x001CA11C
		public static void newRequest(Actor pActor, WorldTile pTileTarget)
		{
			if (pActor.is_inside_boat)
			{
				return;
			}
			TaxiRequest tRequest = null;
			foreach (TaxiRequest iRequest in TaxiManager.list)
			{
				if (iRequest.isSameKingdom(pActor.kingdom) && !iRequest.isState(TaxiRequestState.Transporting) && !iRequest.isState(TaxiRequestState.Finished) && iRequest.getTileTarget().isSameIsland(pTileTarget) && iRequest.getTileStart().isSameIsland(pActor.current_tile))
				{
					tRequest = iRequest;
					break;
				}
			}
			if (tRequest != null)
			{
				tRequest.addActor(pActor);
				return;
			}
			tRequest = new TaxiRequest(pActor, pActor.kingdom, pActor.current_tile, pTileTarget);
			TaxiManager.list.Add(tRequest);
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x001CBFE4 File Offset: 0x001CA1E4
		public static void cancelRequest(TaxiRequest pRequest)
		{
			pRequest.cancel();
			TaxiManager.list.Remove(pRequest);
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x001CBFF8 File Offset: 0x001CA1F8
		public static TaxiRequest getRequestForActor(Actor pActor)
		{
			foreach (TaxiRequest tRequest in TaxiManager.list)
			{
				if (tRequest.hasActor(pActor))
				{
					return tRequest;
				}
			}
			return null;
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x001CC054 File Offset: 0x001CA254
		public static TaxiRequest getNewRequestForBoat(Actor pBoatActor)
		{
			TaxiRequest tBest = null;
			foreach (TaxiRequest tRequest in TaxiManager.list)
			{
				if (tRequest.isAlreadyUsedByBoat(pBoatActor))
				{
					return tRequest;
				}
				if (tRequest.isState(TaxiRequestState.Pending) && tRequest.isStillLegit() && tRequest.isSameKingdom(pBoatActor.kingdom) && (tBest == null || tBest.countActors() < tRequest.countActors()))
				{
					tBest = tRequest;
				}
			}
			return tBest;
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x001CC0E4 File Offset: 0x001CA2E4
		public static void clear()
		{
			foreach (TaxiRequest taxiRequest in TaxiManager.list)
			{
				taxiRequest.clear();
			}
			TaxiManager.list.Clear();
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x001CC140 File Offset: 0x001CA340
		public static void finish(TaxiRequest pRequest)
		{
			pRequest.finish();
			TaxiManager.list.Remove(pRequest);
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x001CC154 File Offset: 0x001CA354
		public static void cancelTaxiRequestForActor(Actor pActor)
		{
			TaxiRequest tRequest = TaxiManager.getRequestForActor(pActor);
			if (tRequest == null)
			{
				return;
			}
			TaxiManager.cancelRequest(tRequest);
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x001CC174 File Offset: 0x001CA374
		public static void update(float pElapsed)
		{
			if (TaxiManager.timer_check > 0f)
			{
				TaxiManager.timer_check -= pElapsed;
				return;
			}
			TaxiManager.timer_check = 5f;
			int i = 0;
			while (TaxiManager.list.Count > i)
			{
				TaxiRequest tRequest = TaxiManager.list[i];
				if (tRequest.isStillLegit())
				{
					i++;
				}
				else
				{
					tRequest.finish();
					TaxiManager.list.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x001CC1E0 File Offset: 0x001CA3E0
		public static void removeDeadUnits()
		{
			foreach (TaxiRequest taxiRequest in TaxiManager.list)
			{
				taxiRequest.removeDeadUnits();
			}
		}

		// Token: 0x0400313C RID: 12604
		public static List<TaxiRequest> list = new List<TaxiRequest>();

		// Token: 0x0400313D RID: 12605
		private static float timer_check = 0f;
	}
}
