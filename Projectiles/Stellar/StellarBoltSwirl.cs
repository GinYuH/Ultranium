using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Stellar;

public class StellarBoltSwirl : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Stellar Bolt");
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
		((ModProjectile)this).projectile.penetrate = 1;
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
		int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("StellarDust"), ((ModProjectile)this).projectile.velocity.X * 1.2f, ((ModProjectile)this).projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 0.5f;
		Main.dust[num].noGravity = true;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("StellarDust"), 0f, -2f, 0, default(Color), 2f);
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
