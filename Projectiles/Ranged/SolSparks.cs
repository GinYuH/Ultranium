using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class SolSparks : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Sol Sparks");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 6;
		((ModProjectile)this).Projectile.height = 6;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.timeLeft = 20;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.light = 0f;
		((ModProjectile)this).Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		int num = Dust.NewDust(((ModProjectile)this).Projectile.position + ((ModProjectile)this).Projectile.velocity, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 228);
		int num2 = Dust.NewDust(((ModProjectile)this).Projectile.position + ((ModProjectile)this).Projectile.velocity, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 228);
		Main.dust[num].noGravity = true;
		Main.dust[num2].noGravity = true;
		Main.dust[num2].velocity = Vector2.Zero;
		Main.dust[num2].velocity = Vector2.Zero;
		Main.dust[num2].scale = 1f;
		Main.dust[num].scale = 1f;
	}
}
