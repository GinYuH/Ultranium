using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dedicated;

public class RayGunLaser : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Ray Gun Laser");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 2;
		((ModProjectile)this).Projectile.height = 42;
		((ModProjectile)this).Projectile.alpha = 0;
		((ModProjectile)this).Projectile.timeLeft = 240;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.ignoreWater = false;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.penetrate = 4;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 7;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
		for (int i = 0; i < 8; i++)
		{
			Vector2 spinningpoint = Vector2.UnitX * (0f - (float)((ModProjectile)this).Projectile.width) / 2f;
			spinningpoint += -Vector2.UnitY.RotatedBy((float)i * (float)Math.PI / 6f) * new Vector2(8f, 16f);
			spinningpoint = spinningpoint.RotatedBy(((ModProjectile)this).Projectile.rotation - (float)Math.PI / 2f);
			int num = Dust.NewDust(((ModProjectile)this).Projectile.Center, 0, 0, 89, 0f, 0f, 160);
			Main.dust[num].scale = 1.5f;
			Main.dust[num].noGravity = true;
			Main.dust[num].position = ((ModProjectile)this).Projectile.Center + spinningpoint;
			Main.dust[num].velocity = ((ModProjectile)this).Projectile.velocity * 0.1f;
			Main.dust[num].velocity = Vector2.Normalize(((ModProjectile)this).Projectile.Center - ((ModProjectile)this).Projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
		}
		((ModProjectile)this).Projectile.localAI[0] += 0.075f;
		if (((ModProjectile)this).Projectile.localAI[0] > 8f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 8f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 89, 0f, -2f, 0, default(Color), 1.5f);
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
