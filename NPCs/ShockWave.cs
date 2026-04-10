using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Ultranium.NPCs;

public class ShockWave : ModProjectile
{
	private int rippleCount = 4;

	private int rippleSize = 5;

	private int rippleSpeed = 25;

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 4;
		((ModProjectile)this).projectile.height = 4;
		((ModProjectile)this).projectile.timeLeft = 400;
		((ModProjectile)this).projectile.alpha = 255;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.ai[0] += 8f;
		_ = Main.player[Main.myPlayer];
		if (((ModProjectile)this).projectile.ai[1] == 0f)
		{
			((ModProjectile)this).projectile.ai[1] = 1f;
			if (!Filters.Scene["Shockwave"].IsActive())
			{
				Filters.Scene.Activate("Shockwave", ((ModProjectile)this).projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed)
					.UseTargetPosition(((ModProjectile)this).projectile.Center);
			}
		}
		else
		{
			((ModProjectile)this).projectile.ai[1] += 1f;
			float num = ((ModProjectile)this).projectile.ai[1] / 60f;
			float num2 = 200f;
			Filters.Scene["Shockwave"].GetShader().UseProgress(num).UseOpacity(num2 * (1f - num / 3f));
		}
	}

	public override void Kill(int timeLeft)
	{
		Filters.Scene["Shockwave"].Deactivate();
	}
}
