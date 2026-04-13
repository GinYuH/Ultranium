using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Magic;

public class ShadowRodBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadow Bolt");
		Main.projFrames[Projectile.type] = 5;
	}

	public override void SetDefaults()
	{
		Projectile.width = 22;
		Projectile.height = 22;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.tileCollide = true;
		Projectile.penetrate = 10;
		Projectile.timeLeft = 115;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = false;
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 5)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 5)
			{
				Projectile.frame = 0;
			}
		}
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.VilePowder);
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.VilePowder, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 10;
	}
}
