using System;
using System.Collections.Generic;

namespace life.taxi
{
	// Token: 0x02000878 RID: 2168
	public class TaxiRequest
	{
		// Token: 0x0600440F RID: 17423 RVA: 0x001CC24E File Offset: 0x001CA44E
		public TaxiRequest(Actor pActor, Kingdom pKingdom, WorldTile pStartTile, WorldTile pTargetTile)
		{
			this._kingdom = pKingdom;
			this.addActor(pActor);
			this._tile_start = pStartTile;
			this._tile_target = pTargetTile;
			this.setState(TaxiRequestState.Pending);
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x001CC286 File Offset: 0x001CA486
		public WorldTile getTileTarget()
		{
			return this._tile_target;
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x001CC28E File Offset: 0x001CA48E
		public WorldTile getTileStart()
		{
			return this._tile_start;
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x001CC296 File Offset: 0x001CA496
		public bool isSameKingdom(Kingdom pKingdom)
		{
			return this._kingdom == pKingdom;
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x001CC2A1 File Offset: 0x001CA4A1
		public HashSet<Actor> getActors()
		{
			return this._actors;
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x001CC2A9 File Offset: 0x001CA4A9
		public int countActors()
		{
			return this._actors.Count;
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x001CC2B6 File Offset: 0x001CA4B6
		public bool hasActor(Actor pActor)
		{
			return this._actors.Contains(pActor);
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x001CC2C9 File Offset: 0x001CA4C9
		public bool addActor(Actor pActor)
		{
			return this._actors.Add(pActor);
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x001CC2D7 File Offset: 0x001CA4D7
		public void cancel()
		{
			this.setState(TaxiRequestState.Finished);
			this.clear();
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x001CC2E6 File Offset: 0x001CA4E6
		public void finish()
		{
			this.setState(TaxiRequestState.Finished);
			this.clear();
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x001CC2F8 File Offset: 0x001CA4F8
		private void checkList()
		{
			using (ListPool<Actor> tToRemove = new ListPool<Actor>(this._actors.Count))
			{
				foreach (Actor tActor in this._actors)
				{
					bool tRemove = false;
					if (tActor.isRekt())
					{
						tRemove = true;
					}
					else if (!tActor.current_tile.isSameIsland(this._tile_start))
					{
						tRemove = true;
					}
					else if (tActor.current_tile.isSameIsland(this._tile_target))
					{
						tRemove = true;
					}
					if (tRemove)
					{
						tToRemove.Add(tActor);
					}
				}
				this._actors.ExceptWith(tToRemove);
			}
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x001CC3C0 File Offset: 0x001CA5C0
		public void removeDeadUnits()
		{
			using (ListPool<Actor> tToRemove = new ListPool<Actor>(this._actors.Count))
			{
				foreach (Actor tActor in this._actors)
				{
					if (tActor.isRekt())
					{
						tToRemove.Add(tActor);
					}
				}
				if (tToRemove.Count != 0)
				{
					this._actors.ExceptWith(tToRemove);
				}
			}
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x001CC45C File Offset: 0x001CA65C
		public bool isAlreadyUsedByBoat(Actor pBoatActor)
		{
			return this.isState(TaxiRequestState.Assigned) && this.isStillLegit() && this.isAssignedToBoat(pBoatActor);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x001CC47B File Offset: 0x001CA67B
		public bool isAssignedToBoat(Actor pBoatActor)
		{
			return this._boat.actor == pBoatActor;
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x001CC48C File Offset: 0x001CA68C
		public bool isStillLegit()
		{
			if (!this.isState(TaxiRequestState.Pending) && !this.hasAssignedBoat())
			{
				return false;
			}
			if (this.isState(TaxiRequestState.Finished))
			{
				return false;
			}
			if ((this.isState(TaxiRequestState.Assigned) || this.isState(TaxiRequestState.Loading) || this.isState(TaxiRequestState.Transporting)) && this.hasAssignedBoat() && this._boat.hasPassengers())
			{
				return true;
			}
			this.checkList();
			return this._actors.Count != 0;
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x001CC4FF File Offset: 0x001CA6FF
		public bool hasAssignedBoat()
		{
			return this._boat != null && !this._boat.actor.isRekt();
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x001CC51E File Offset: 0x001CA71E
		public void assign(Boat pTaxi)
		{
			this._boat = pTaxi;
			this.setState(TaxiRequestState.Assigned);
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x001CC52E File Offset: 0x001CA72E
		public Boat getBoat()
		{
			return this._boat;
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x001CC536 File Offset: 0x001CA736
		public bool isBoatNearDock()
		{
			return this._boat.isNearDock();
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x001CC543 File Offset: 0x001CA743
		public void setState(TaxiRequestState pState)
		{
			this.state = pState;
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x001CC54C File Offset: 0x001CA74C
		public void cancelForLatePassengers()
		{
			using (ListPool<Actor> tToRemove = new ListPool<Actor>(this._actors.Count))
			{
				foreach (Actor tActor in this._actors)
				{
					if (!tActor.is_inside_boat || tActor.inside_boat != this._boat)
					{
						tToRemove.Add(tActor);
					}
				}
				this._actors.ExceptWith(tToRemove);
			}
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x001CC5EC File Offset: 0x001CA7EC
		public bool embarkToBoat(Actor pActor)
		{
			return this._actors.Remove(pActor);
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x001CC5FC File Offset: 0x001CA7FC
		public bool everyoneEmbarked()
		{
			this.checkList();
			foreach (Actor tActor in this._actors)
			{
				if (!this._boat.hasPassenger(tActor))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x001CC664 File Offset: 0x001CA864
		public bool isState(TaxiRequestState pState)
		{
			return this.state == pState;
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x001CC66F File Offset: 0x001CA86F
		public void clear()
		{
			this._actors.Clear();
			this._boat = null;
			this._tile_target = null;
			this._tile_start = null;
			this._kingdom = null;
		}

		// Token: 0x04003147 RID: 12615
		private WorldTile _tile_target;

		// Token: 0x04003148 RID: 12616
		private WorldTile _tile_start;

		// Token: 0x04003149 RID: 12617
		private readonly HashSet<Actor> _actors = new HashSet<Actor>();

		// Token: 0x0400314A RID: 12618
		private Boat _boat;

		// Token: 0x0400314B RID: 12619
		public TaxiRequestState state;

		// Token: 0x0400314C RID: 12620
		private Kingdom _kingdom;
	}
}
