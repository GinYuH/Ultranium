using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class MidnightPro : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).projectile.type] = 3;
		((ModProjectile)this).DisplayName.SetDefault("Shadow Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 24;
		((ModProjectile)this).projectile.height = 26;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.timeLeft = 120;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = false;
	}

	public override void AI()
	{
		int num = Dust.NewDust(((ModProjectile)this).projectile.position + ((ModProjectile)this).projectile.velocity, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 21);
		Main.dust[num].noGravity = true;
		Main.dust[num].velocity = Vector2.Zero;
		Main.dust[num].velocity = Vector2.Zero;
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
		if (++((ModProjectile)this).projectile.frameCounter >= 16)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 3)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 10;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 21, 0f, -2f, 0, default(Color), 1.5f);
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
