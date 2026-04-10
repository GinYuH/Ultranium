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
		((ModProjectile)this).DisplayName.SetDefault("Eldritch Sound Pulse");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 16;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.timeLeft = 240;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.ignoreWater = false;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.penetrate = 4;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 7;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
		for (int i = 0; i < 12; i++)
		{
			Vector2 spinningpoint = Vector2.UnitX * (0f - (float)((ModProjectile)this).projectile.width) / 2f;
			spinningpoint += -Vector2.UnitY.RotatedBy((float)i * (float)Math.PI / 6f) * new Vector2(8f, 16f);
			spinningpoint = spinningpoint.RotatedBy(((ModProjectile)this).projectile.rotation - (float)Math.PI / 2f);
			int num = Dust.NewDust(((ModProjectile)this).projectile.Center, 0, 0, 89, 0f, 0f, 160);
			Main.dust[num].scale = 1.5f;
			Main.dust[num].noGravity = true;
			Main.dust[num].position = ((ModProjectile)this).projectile.Center + spinningpoint;
			Main.dust[num].velocity = ((ModProjectile)this).projectile.velocity * 0.1f;
			Main.dust[num].velocity = Vector2.Normalize(((ModProjectile)this).projectile.Center - ((ModProjectile)this).projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
		}
		((ModProjectile)this).projectile.localAI[0] += 0.075f;
		if (((ModProjectile)this).projectile.localAI[0] > 8f)
		{
			((ModProjectile)this).projectile.localAI[0] = 8f;
		}
		float num2 = ((ModProjectile)this).projectile.Center.X;
		float num3 = ((ModProjectile)this).projectile.Center.Y;
		float num4 = 400f;
		bool flag = false;
		for (int j = 0; j < 200; j++)
		{
			if (Main.npc[j].CanBeChasedBy(((ModProjectile)this).projectile) && Collision.CanHit(((ModProjectile)this).projectile.Center, 1, 1, Main.npc[j].Center, 1, 1))
			{
				float num5 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
				float num6 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
				float num7 = Math.Abs(((ModProjectile)this).projectile.position.X + (float)(((ModProjectile)this).projectile.width / 2) - num5) + Math.Abs(((ModProjectile)this).projectile.position.Y + (float)(((ModProjectile)this).projectile.height / 2) - num6);
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
			Vector2 vector = new Vector2(((ModProjectile)this).projectile.position.X + (float)((ModProjectile)this).projectile.width * 0.5f, ((ModProjectile)this).projectile.position.Y + (float)((ModProjectile)this).projectile.height * 0.5f);
			float num8 = num2 - vector.X;
			float num9 = num3 - vector.Y;
			float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
			num10 = 25f / num10;
			num8 *= num10;
			num9 *= num10;
			((ModProjectile)this).projectile.velocity.X = (((ModProjectile)this).projectile.velocity.X * 20f + num8) / 21f;
			((ModProjectile)this).projectile.velocity.Y = (((ModProjectile)this).projectile.velocity.Y * 20f + num9) / 21f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		NumBounces++;
		if (NumBounces > 3)
		{
			((ModProjectile)this).projectile.Kill();
		}
		else
		{
			((ModProjectile)this).projectile.ai[0] += 0.1f;
			if (((ModProjectile)this).projectile.velocity.X != oldVelocity.X)
			{
				((ModProjectile)this).projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (((ModProjectile)this).projectile.velocity.Y != oldVelocity.Y)
			{
				((ModProjectile)this).projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			((ModProjectile)this).projectile.velocity *= 0.75f;
		}
		return false;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 89, 0f, -2f, 0, default(Color), 1.5f);
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
