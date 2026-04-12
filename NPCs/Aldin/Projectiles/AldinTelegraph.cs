using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class AldinTelegraph : ModProjectile
{
	private int Timer;

	private Color[] ColorCycle = new Color[2]
	{
		new Color(153, 255, 178),
		new Color(83, 168, 222)
	};

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Cosmic Telegraph");
	}

	public override void SetDefaults()
	{
		Projectile.width = 48;
		Projectile.height = 144;
		Projectile.aiStyle = 0;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 120;
		Projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
	}

	public override void AI()
	{
		Projectile.velocity *= 0f;
	}
}
