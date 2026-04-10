using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class MidnightPro : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).Projectile.type] = 3;
		// ((ModProjectile)this).DisplayName.SetDefault("Shadow Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 24;
		((ModProjectile)this).Projectile.height = 26;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.timeLeft = 120;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = false;
	}

	public override void AI()
	{
		int num = Dust.NewDust(((ModProjectile)this).Projectile.position + ((ModProjectile)this).Projectile.velocity, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 21);
		Main.dust[num].noGravity = true;
		Main.dust[num].velocity = Vector2.Zero;
		Main.dust[num].velocity = Vector2.Zero;
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
		if (++((ModProjectile)this).Projectile.frameCounter >= 16)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 3)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 10;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 21, 0f, -2f, 0, default(Color), 1.5f);
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
