using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class SolSparks : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Sol Sparks");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 6;
		((ModProjectile)this).projectile.height = 6;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.timeLeft = 20;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.light = 0f;
		((ModProjectile)this).projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		int num = Dust.NewDust(((ModProjectile)this).projectile.position + ((ModProjectile)this).projectile.velocity, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 228);
		int num2 = Dust.NewDust(((ModProjectile)this).projectile.position + ((ModProjectile)this).projectile.velocity, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 228);
		Main.dust[num].noGravity = true;
		Main.dust[num2].noGravity = true;
		Main.dust[num2].velocity = Vector2.Zero;
		Main.dust[num2].velocity = Vector2.Zero;
		Main.dust[num2].scale = 1f;
		Main.dust[num].scale = 1f;
	}
}
