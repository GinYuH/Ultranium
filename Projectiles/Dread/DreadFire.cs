using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread;

public class DreadFire : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Dread Flames");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 8;
		((ModProjectile)this).projectile.height = 8;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.timeLeft = 120;
		((ModProjectile)this).projectile.extraUpdates = 3;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.tileCollide = true;
	}

	public override void AI()
	{
		Lighting.AddLight(((ModProjectile)this).projectile.Center, (float)(255 - ((ModProjectile)this).projectile.alpha) * 0.15f / 255f, (float)(255 - ((ModProjectile)this).projectile.alpha) * 0.45f / 255f, (float)(255 - ((ModProjectile)this).projectile.alpha) * 0.05f / 255f);
		for (int i = 0; i < 5; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
