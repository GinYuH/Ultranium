using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class ErebusGuitarPulsePurple : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Sound Pulse");
	}

	public override void SetDefaults()
	{
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.alpha = 255;
		Projectile.timeLeft = 240;
		Projectile.friendly = true;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = false;
		Projectile.DamageType = DamageClass.Magic;
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
		Projectile.velocity *= 1.03f;
		for (int i = 0; i < 12; i++)
		{
			Vector2 spinningpoint = Vector2.UnitX * (0f - (float)Projectile.width) / 2f;
			spinningpoint += -Vector2.UnitY.RotatedBy((float)i * (float)Math.PI / 6f) * new Vector2(8f, 16f);
			spinningpoint = spinningpoint.RotatedBy(Projectile.rotation - (float)Math.PI / 2f);
			int num = Dust.NewDust(Projectile.Center, 0, 0, Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, 0f, 160);
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
		float num2 = Projectile.Center.X;
		float num3 = Projectile.Center.Y;
		float num4 = 400f;
		bool flag = false;
		for (int j = 0; j < 200; j++)
		{
			if (Main.npc[j].CanBeChasedBy(Projectile) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[j].Center, 1, 1))
			{
				float num5 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
				float num6 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
				float num7 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num5) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num6);
				if (num7 < num4)
				{
					num4 = num7;
					num2 = num5;
					num3 = num6;
					flag = true;
				}
			}
		}
		if (flag)
		{
			Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num8 = num2 - vector.X;
			float num9 = num3 - vector.Y;
			float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
			num10 = 25f / num10;
			num8 *= num10;
			num9 *= num10;
			Projectile.velocity.X = (Projectile.velocity.X * 20f + num8) / 21f;
			Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num9) / 21f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.Kill();
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustPurple").Type, 0f, -2f, 0, default(Color), 1.5f);
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
