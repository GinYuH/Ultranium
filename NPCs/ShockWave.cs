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
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.timeLeft = 400;
		Projectile.alpha = 255;
	}

	public override void AI()
	{
		Projectile.ai[0] += 8f;
		_ = Main.player[Main.myPlayer];
		if (Projectile.ai[1] == 0f)
		{
			Projectile.ai[1] = 1f;
			if (!Filters.Scene["Shockwave"].IsActive())
			{
				Filters.Scene.Activate("Shockwave", Projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed)
					.UseTargetPosition(Projectile.Center);
			}
		}
		else
		{
			Projectile.ai[1] += 1f;
			float num = Projectile.ai[1] / 60f;
			float num2 = 200f;
			Filters.Scene["Shockwave"].GetShader().UseProgress(num).UseOpacity(num2 * (1f - num / 3f));
		}
	}

	public override void OnKill(int timeLeft)
	{
		Filters.Scene["Shockwave"].Deactivate();
	}
}
