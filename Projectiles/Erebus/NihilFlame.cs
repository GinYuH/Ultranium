using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class NihilFlame : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).projectile.type] = 6;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 15;
		((ModProjectile)this).projectile.height = 15;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.timeLeft = 260;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 6;
	}

	public override void AI()
	{
		if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			int num = 40;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).projectile.width / 5f, ((ModProjectile)this).projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).projectile.Center;
				Vector2 vector2 = vector - ((ModProjectile)this).projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, ((ModProjectile)this).mod.DustType("ShadowDustPurple"), vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector2) * 3f;
				obj.fadeIn = 1.3f;
				((ModProjectile)this).projectile.ai[0] = 1f;
			}
		}
		if (++((ModProjectile)this).projectile.frameCounter >= 5)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 6)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] > 10f)
		{
			((ModProjectile)this).projectile.ai[0] = 10f;
			int num2 = HomeOnTarget();
			if (num2 != -1)
			{
				NPC nPC = Main.npc[num2];
				Vector2 value = ((ModProjectile)this).projectile.DirectionTo(nPC.Center) * 2f;
				((ModProjectile)this).projectile.velocity = Vector2.Lerp(((ModProjectile)this).projectile.velocity, value, 0.5f);
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
