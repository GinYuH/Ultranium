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
		((ModProjectile)this).DisplayName.SetDefault("Cosmic Telegraph");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 48;
		((ModProjectile)this).projectile.height = 144;
		((ModProjectile)this).projectile.aiStyle = 0;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.timeLeft = 120;
		((ModProjectile)this).projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.velocity *= 0f;
	}
}
