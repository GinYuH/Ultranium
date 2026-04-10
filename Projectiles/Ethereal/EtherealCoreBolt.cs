using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class EtherealCoreBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 18;
		((ModProjectile)this).Projectile.height = 18;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 2;
		((ModProjectile)this).Projectile.timeLeft = 500;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 10;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 62, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).Projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 0.8f;
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 62);
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
		if (++((ModProjectile)this).Projectile.frameCounter >= 4)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 4)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] > 7f)
		{
			((ModProjectile)this).Projectile.ai[0] = 7f;
			int num = HomeOnTarget();
			if (num != -1)
			{
				NPC nPC = Main.npc[num];
				Vector2 value = ((ModProjectile)this).Projectile.DirectionTo(nPC.Center) * 25f;
				((ModProjectile)this).Projectile.velocity = Vector2.Lerp(((ModProjectile)this).Projectile.velocity, value, 0.05f);
			}
		}
	}

	private int HomeOnTarget()
	{
		int num = -1;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (nPC.CanBeChasedBy(((ModProjectile)this).Projectile))
			{
				_ = nPC.wet;
				float num2 = ((ModProjectile)this).Projectile.Distance(nPC.Center);
				if (num2 <= 400f && (num == -1 || ((ModProjectile)this).Projectile.Distance(Main.npc[num].Center) > num2))
				{
					num = i;
				}
			}
		}
		return num;
	}
}
