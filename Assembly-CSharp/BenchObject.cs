using System;

// Token: 0x020004FF RID: 1279
public class BenchObject
{
	// Token: 0x06002A71 RID: 10865 RVA: 0x0014EA13 File Offset: 0x0014CC13
	public void update(float pElapsed)
	{
		this.updateMove(pElapsed);
		this.updateMove(pElapsed);
		this.updateMove(pElapsed);
		this.updateMove(pElapsed);
		this.updateMove(pElapsed);
	}

	// Token: 0x06002A72 RID: 10866 RVA: 0x0014EA38 File Offset: 0x0014CC38
	public void updateMove(float pElapsed)
	{
		this.derp += 22;
		if (this.derp == 1000)
		{
			this.derp += 10;
			if (this.derp < 10)
			{
				this.derp += 5;
				return;
			}
			this.derp -= 5;
		}
	}

	// Token: 0x04001F88 RID: 8072
	public int derp;
}
