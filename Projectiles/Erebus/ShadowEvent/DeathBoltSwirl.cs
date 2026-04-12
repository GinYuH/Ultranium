using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DeathBoltSwirl : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Death Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.width = 6;
		Projectile.height = 6;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.tileCollide = false;
		Projectile.alpha = 255;
		Projectile.penetrate = 3;
		Projectile.timeLeft = 200;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 7;
	}

	public override void AI()
	{
		Vector2 spinningpoint = new Vector2(14f, 14f);
		Projectile projectile = Main.projectile[(int)Projectile.ai[1]];
		Projectile.ai[0] += 0.1f;
		Projectile.position = projectile.position + spinningpoint.RotatedBy((double)Projectile.ai[0] + (double)Projectile.ai[1] * (Math.PI / 4.0));
		if (!((Entity)projectile).active)
		{
			Projectile.Kill();
		}
		int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 89, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 0.5f;
		Main.dust[num].noGravity = true;
	}
}
