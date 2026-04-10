using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread;

public class DreadFire : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Dread Flames");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 8;
		((ModProjectile)this).Projectile.height = 8;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.timeLeft = 120;
		((ModProjectile)this).Projectile.extraUpdates = 3;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.tileCollide = true;
	}

	public override void AI()
	{
		Lighting.AddLight(((ModProjectile)this).Projectile.Center, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.15f / 255f, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.45f / 255f, (float)(255 - ((ModProjectile)this).Projectile.alpha) * 0.05f / 255f);
		for (int i = 0; i < 5; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
