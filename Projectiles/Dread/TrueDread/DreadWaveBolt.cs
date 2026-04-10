using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadWaveBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Dread Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 8;
		((ModProjectile)this).Projectile.height = 8;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.timeLeft = 600;
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
		for (int i = 0; i < 5; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		((ModProjectile)this).Projectile.localAI[0] += 0.075f;
		if (((ModProjectile)this).Projectile.localAI[0] > 8f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 8f;
		}
		float num2 = ((ModProjectile)this).Projectile.localAI[0];
		float num3 = 16f;
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[1] == 0f)
		{
			if (((ModProjectile)this).Projectile.ai[0] > num3 * 0.5f)
			{
				((ModProjectile)this).Projectile.ai[0] = 0f;
				((ModProjectile)this).Projectile.ai[1] = 1f;
			}
			else
			{
				Vector2 velocity = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num2));
				((ModProjectile)this).Projectile.velocity = velocity;
			}
		}
		else
		{
			if (((ModProjectile)this).Projectile.ai[0] <= num3)
			{
				Vector2 velocity2 = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(num2));
				((ModProjectile)this).Projectile.velocity = velocity2;
			}
			else
			{
				Vector2 velocity3 = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(0f - num2));
				((ModProjectile)this).Projectile.velocity = velocity3;
			}
			if (((ModProjectile)this).Projectile.ai[0] >= num3 * 2f)
			{
				((ModProjectile)this).Projectile.ai[0] = 0f;
			}
		}
		float num4 = ((ModProjectile)this).Projectile.Center.X;
		float num5 = ((ModProjectile)this).Projectile.Center.Y;
		bool flag = false;
		for (int j = 0; j < 200; j++)
		{
			if (Main.npc[j].CanBeChasedBy(((ModProjectile)this).Projectile) && Collision.CanHit(((ModProjectile)this).Projectile.Center, 1, 1, Main.npc[j].Center, 1, 1))
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
			Vector2 vector = new Vector2(((ModProjectile)this).Projectile.position.X + (float)((ModProjectile)this).Projectile.width * 0.5f, ((ModProjectile)this).Projectile.position.Y + (float)((ModProjectile)this).Projectile.height * 0.5f);
			float num8 = num4 - vector.X;
			float num9 = num5 - vector.Y;
			float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
			num10 = 25f / num10;
			num8 *= num10;
			num9 *= num10;
			((ModProjectile)this).Projectile.velocity.X = (((ModProjectile)this).Projectile.velocity.X * 20f + num8) / 21f;
			((ModProjectile)this).Projectile.velocity.Y = (((ModProjectile)this).Projectile.velocity.Y * 20f + num9) / 21f;
		}
	}
}
