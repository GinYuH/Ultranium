using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class SolSparks : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Sol Sparks");
	}

	public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 6;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.timeLeft = 20;
		Projectile.alpha = 255;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		int num = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 228);
		int num2 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 228);
		Main.dust[num].noGravity = true;
		Main.dust[num2].noGravity = true;
		Main.dust[num2].velocity = Vector2.Zero;
		Main.dust[num2].velocity = Vector2.Zero;
		Main.dust[num2].scale = 1f;
		Main.dust[num].scale = 1f;
	}
}
