using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DeathBoltSwirl : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Death Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 6;
		((ModProjectile)this).projectile.height = 6;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.timeLeft = 200;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 7;
	}

	public override void AI()
	{
		Vector2 spinningpoint = new Vector2(14f, 14f);
		Projectile projectile = Main.projectile[(int)((ModProjectile)this).projectile.ai[1]];
		((ModProjectile)this).projectile.ai[0] += 0.1f;
		((ModProjectile)this).projectile.position = projectile.position + spinningpoint.RotatedBy((double)((ModProjectile)this).projectile.ai[0] + (double)((ModProjectile)this).projectile.ai[1] * (Math.PI / 4.0));
		if (!((Entity)projectile).active)
		{
			((ModProjectile)this).projectile.Kill();
		}
		int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 89, ((ModProjectile)this).projectile.velocity.X * 1.2f, ((ModProjectile)this).projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 0.5f;
		Main.dust[num].noGravity = true;
	}
}
