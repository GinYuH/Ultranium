using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadWaveBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.alpha = 255;
		Projectile.timeLeft = 600;
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
		for (int i = 0; i < 5; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		Projectile.localAI[0] += 0.075f;
		if (Projectile.localAI[0] > 8f)
		{
			Projectile.localAI[0] = 8f;
		}
		float num2 = Projectile.localAI[0];
		float num3 = 16f;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[1] == 0f)
		{
			if (Projectile.ai[0] > num3 * 0.5f)
			{
				Projectile.ai[0] = 0f;
				Projectile.ai[1] = 1f;
			}
			else
			{
				Vector2 velocity = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num2));
				Projectile.velocity = velocity;
			}
		}
		else
		{
			if (Projectile.ai[0] <= num3)
			{
				Vector2 velocity2 = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num2));
				Projectile.velocity = velocity2;
			}
			else
			{
				Vector2 velocity3 = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num2));
				Projectile.velocity = velocity3;
			}
			if (Projectile.ai[0] >= num3 * 2f)
			{
				Projectile.ai[0] = 0f;
			}
		}
		float num4 = Projectile.Center.X;
		float num5 = Projectile.Center.Y;
		bool flag = false;
		for (int j = 0; j < 200; j++)
		{
			if (Main.npc[j].CanBeChasedBy(Projectile) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[j].Center, 1, 1))
			{
				float num6 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
				float num7 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
				num4 = num6;
				num5 = num7;
				flag = true;
			}
		}
		if (flag)
		{
			Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num8 = num4 - vector.X;
			float num9 = num5 - vector.Y;
			float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
			num10 = 25f / num10;
			num8 *= num10;
			num9 *= num10;
			Projectile.velocity.X = (Projectile.velocity.X * 20f + num8) / 21f;
			Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num9) / 21f;
		}
	}
}
