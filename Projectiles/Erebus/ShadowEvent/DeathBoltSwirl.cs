using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DeathBoltSwirl : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Death Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 6;
		((ModProjectile)this).Projectile.height = 6;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.penetrate = 3;
		((ModProjectile)this).Projectile.timeLeft = 200;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 7;
	}

	public override void AI()
	{
		Vector2 spinningpoint = new Vector2(14f, 14f);
		Projectile projectile = Main.projectile[(int)((ModProjectile)this).Projectile.ai[1]];
		((ModProjectile)this).Projectile.ai[0] += 0.1f;
		((ModProjectile)this).Projectile.position = projectile.position + spinningpoint.RotatedBy((double)((ModProjectile)this).Projectile.ai[0] + (double)((ModProjectile)this).Projectile.ai[1] * (Math.PI / 4.0));
		if (!((Entity)projectile).active)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		int num = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 89, ((ModProjectile)this).Projectile.velocity.X * 1.2f, ((ModProjectile)this).Projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 0.5f;
		Main.dust[num].noGravity = true;
	}
}
