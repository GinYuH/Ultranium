using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class ErebusGuitarPulse : ModProjectile
{
	private int NumBounces;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Eldritch Sound Pulse");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 16;
		((ModProjectile)this).Projectile.height = 16;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.timeLeft = 240;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.ignoreWater = false;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
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
		for (int i = 0; i < 12; i++)
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
		float num2 = ((ModProjectile)this).Projectile.Center.X;
		float num3 = ((ModProjectile)this).Projectile.Center.Y;
		float num4 = 400f;
		bool flag = false;
		for (int j = 0; j < 200; j++)
		{
			if (Main.npc[j].CanBeChasedBy(((ModProjectile)this).Projectile) && Collision.CanHit(((ModProjectile)this).Projectile.Center, 1, 1, Main.npc[j].Center, 1, 1))
			{
				float num5 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
				float num6 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
				float num7 = Math.Abs(((ModProjectile)this).Projectile.position.X + (float)(((ModProjectile)this).Projectile.width / 2) - num5) + Math.Abs(((ModProjectile)this).Projectile.position.Y + (float)(((ModProjectile)this).Projectile.height / 2) - num6);
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
			Vector2 vector = new Vector2(((ModProjectile)this).Projectile.position.X + (float)((ModProjectile)this).Projectile.width * 0.5f, ((ModProjectile)this).Projectile.position.Y + (float)((ModProjectile)this).Projectile.height * 0.5f);
			float num8 = num2 - vector.X;
			float num9 = num3 - vector.Y;
			float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
			num10 = 25f / num10;
			num8 *= num10;
			num9 *= num10;
			((ModProjectile)this).Projectile.velocity.X = (((ModProjectile)this).Projectile.velocity.X * 20f + num8) / 21f;
			((ModProjectile)this).Projectile.velocity.Y = (((ModProjectile)this).Projectile.velocity.Y * 20f + num9) / 21f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		NumBounces++;
		if (NumBounces > 3)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		else
		{
			((ModProjectile)this).Projectile.ai[0] += 0.1f;
			if (((ModProjectile)this).Projectile.velocity.X != oldVelocity.X)
			{
				((ModProjectile)this).Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (((ModProjectile)this).Projectile.velocity.Y != oldVelocity.Y)
			{
				((ModProjectile)this).Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			((ModProjectile)this).Projectile.velocity *= 0.75f;
		}
		return false;
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
