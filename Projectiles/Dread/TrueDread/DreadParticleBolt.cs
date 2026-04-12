using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadParticleBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Dread Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.alpha = 255;
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 120;
		Projectile.extraUpdates = 3;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Ranged;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 6;
	}

	public override void AI()
	{
		int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 90, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 0.5f;
		Main.dust[num].noGravity = true;
	}
}
