using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class EtherealCoreBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 18;
		((ModProjectile)this).projectile.height = 18;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 2;
		((ModProjectile)this).projectile.timeLeft = 500;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 10;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 62, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 0.8f;
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 62);
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
		if (++((ModProjectile)this).projectile.frameCounter >= 4)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 4)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] > 7f)
		{
			((ModProjectile)this).projectile.ai[0] = 7f;
			int num = HomeOnTarget();
			if (num != -1)
			{
				NPC nPC = Main.npc[num];
				Vector2 value = ((ModProjectile)this).projectile.DirectionTo(nPC.Center) * 25f;
				((ModProjectile)this).projectile.velocity = Vector2.Lerp(((ModProjectile)this).projectile.velocity, value, 0.05f);
			}
		}
	}

	private int HomeOnTarget()
	{
		int num = -1;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (nPC.CanBeChasedBy(((ModProjectile)this).projectile))
			{
				_ = nPC.wet;
				float num2 = ((ModProjectile)this).projectile.Distance(nPC.Center);
				if (num2 <= 400f && (num == -1 || ((ModProjectile)this).projectile.Distance(Main.npc[num].Center) > num2))
				{
					num = i;
				}
			}
		}
		return num;
	}
}
