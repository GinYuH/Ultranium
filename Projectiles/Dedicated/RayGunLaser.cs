using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dedicated;

public class RayGunLaser : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ray Gun Laser");
	}

	public override void SetDefaults()
	{
		Projectile.width = 2;
		Projectile.height = 42;
		Projectile.alpha = 0;
		Projectile.timeLeft = 240;
		Projectile.friendly = true;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = false;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = 4;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 7;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
		for (int i = 0; i < 8; i++)
		{
			Vector2 spinningpoint = Vector2.UnitX * (0f - (float)Projectile.width) / 2f;
			spinningpoint += -Vector2.UnitY.RotatedBy((float)i * (float)Math.PI / 6f) * new Vector2(8f, 16f);
			spinningpoint = spinningpoint.RotatedBy(Projectile.rotation - (float)Math.PI / 2f);
			int num = Dust.NewDust(Projectile.Center, 0, 0, 89, 0f, 0f, 160);
			Main.dust[num].scale = 1.5f;
			Main.dust[num].noGravity = true;
			Main.dust[num].position = Projectile.Center + spinningpoint;
			Main.dust[num].velocity = Projectile.velocity * 0.1f;
			Main.dust[num].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
		}
		Projectile.localAI[0] += 0.075f;
		if (Projectile.localAI[0] > 8f)
		{
			Projectile.localAI[0] = 8f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 89, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
