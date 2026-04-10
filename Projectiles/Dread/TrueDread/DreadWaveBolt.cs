using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadWaveBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Dread Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 8;
		((ModProjectile)this).projectile.height = 8;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.timeLeft = 600;
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
		for (int i = 0; i < 5; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		((ModProjectile)this).projectile.localAI[0] += 0.075f;
		if (((ModProjectile)this).projectile.localAI[0] > 8f)
		{
			((ModProjectile)this).projectile.localAI[0] = 8f;
		}
		float num2 = ((ModProjectile)this).projectile.localAI[0];
		float num3 = 16f;
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[1] == 0f)
		{
			if (((ModProjectile)this).projectile.ai[0] > num3 * 0.5f)
			{
				((ModProjectile)this).projectile.ai[0] = 0f;
				((ModProjectile)this).projectile.ai[1] = 1f;
			}
			else
			{
				Vector2 velocity = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num2));
				((ModProjectile)this).projectile.velocity = velocity;
			}
		}
		else
		{
			if (((ModProjectile)this).projectile.ai[0] <= num3)
			{
				Vector2 velocity2 = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num2));
				((ModProjectile)this).projectile.velocity = velocity2;
			}
			else
			{
				Vector2 velocity3 = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num2));
				((ModProjectile)this).projectile.velocity = velocity3;
			}
			if (((ModProjectile)this).projectile.ai[0] >= num3 * 2f)
			{
				((ModProjectile)this).projectile.ai[0] = 0f;
			}
		}
		float num4 = ((ModProjectile)this).projectile.Center.X;
		float num5 = ((ModProjectile)this).projectile.Center.Y;
		bool flag = false;
		for (int j = 0; j < 200; j++)
		{
			if (Main.npc[j].CanBeChasedBy(((ModProjectile)this).projectile) && Collision.CanHit(((ModProjectile)this).projectile.Center, 1, 1, Main.npc[j].Center, 1, 1))
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
			Vector2 vector = new Vector2(((ModProjectile)this).projectile.position.X + (float)((ModProjectile)this).projectile.width * 0.5f, ((ModProjectile)this).projectile.position.Y + (float)((ModProjectile)this).projectile.height * 0.5f);
			float num8 = num4 - vector.X;
			float num9 = num5 - vector.Y;
			float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
			num10 = 25f / num10;
			num8 *= num10;
			num9 *= num10;
			((ModProjectile)this).projectile.velocity.X = (((ModProjectile)this).projectile.velocity.X * 20f + num8) / 21f;
			((ModProjectile)this).projectile.velocity.Y = (((ModProjectile)this).projectile.velocity.Y * 20f + num9) / 21f;
		}
	}
}
