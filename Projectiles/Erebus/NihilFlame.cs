using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class NihilFlame : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).Projectile.type] = 6;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 15;
		((ModProjectile)this).Projectile.height = 15;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 3;
		((ModProjectile)this).Projectile.timeLeft = 260;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 6;
	}

	public override void AI()
	{
		if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			int num = 40;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).Projectile.width / 5f, ((ModProjectile)this).Projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).Projectile.Center;
				Vector2 vector2 = vector - ((ModProjectile)this).Projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, ((ModProjectile)this).Mod.Find<ModDust>("ShadowDustPurple").Type, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector2) * 3f;
				obj.fadeIn = 1.3f;
				((ModProjectile)this).Projectile.ai[0] = 1f;
			}
		}
		if (++((ModProjectile)this).Projectile.frameCounter >= 5)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 6)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] > 10f)
		{
			((ModProjectile)this).Projectile.ai[0] = 10f;
			int num2 = HomeOnTarget();
			if (num2 != -1)
			{
				NPC nPC = Main.npc[num2];
				Vector2 value = ((ModProjectile)this).Projectile.DirectionTo(nPC.Center) * 2f;
				((ModProjectile)this).Projectile.velocity = Vector2.Lerp(((ModProjectile)this).Projectile.velocity, value, 0.5f);
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
