using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread;

public class DreadFire : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Dread Flames");
	}

	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 120;
		Projectile.extraUpdates = 3;
		Projectile.alpha = 255;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		Lighting.AddLight(Projectile.Center, (float)(255 - Projectile.alpha) * 0.15f / 255f, (float)(255 - Projectile.alpha) * 0.45f / 255f, (float)(255 - Projectile.alpha) * 0.05f / 255f);
		for (int i = 0; i < 5; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
