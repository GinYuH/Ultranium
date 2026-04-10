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
		((ModProjectile)this).Projectile.width = 4;
		((ModProjectile)this).Projectile.height = 4;
		((ModProjectile)this).Projectile.timeLeft = 400;
		((ModProjectile)this).Projectile.alpha = 255;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.ai[0] += 8f;
		_ = Main.player[Main.myPlayer];
		if (((ModProjectile)this).Projectile.ai[1] == 0f)
		{
			((ModProjectile)this).Projectile.ai[1] = 1f;
			if (!Filters.Scene["Shockwave"].IsActive())
			{
				Filters.Scene.Activate("Shockwave", ((ModProjectile)this).Projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed)
					.UseTargetPosition(((ModProjectile)this).Projectile.Center);
			}
		}
		else
		{
			((ModProjectile)this).Projectile.ai[1] += 1f;
			float num = ((ModProjectile)this).Projectile.ai[1] / 60f;
			float num2 = 200f;
			Filters.Scene["Shockwave"].GetShader().UseProgress(num).UseOpacity(num2 * (1f - num / 3f));
		}
	}

	public override void OnKill(int timeLeft)
	{
		Filters.Scene["Shockwave"].Deactivate();
	}
}
