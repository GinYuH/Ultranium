using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class NihilFlame : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 6;
	}

	public override void SetDefaults()
	{
		Projectile.width = 15;
		Projectile.height = 15;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
		Projectile.penetrate = 3;
		Projectile.timeLeft = 260;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 6;
	}

	public override void AI()
	{
		if (Projectile.ai[0] == 0f)
		{
			int num = 40;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)Projectile.width / 5f, Projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + Projectile.Center;
				Vector2 vector2 = vector - Projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, Mod.Find<ModDust>("ShadowDustPurple").Type, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector2) * 3f;
				obj.fadeIn = 1.3f;
				Projectile.ai[0] = 1f;
			}
		}
		if (++Projectile.frameCounter >= 5)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
		}
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 10f)
		{
			Projectile.ai[0] = 10f;
			int num2 = HomeOnTarget();
			if (num2 != -1)
			{
				NPC nPC = Main.npc[num2];
				Vector2 value = Projectile.DirectionTo(nPC.Center) * 2f;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 0.5f);
			}
		}
	}

	private int HomeOnTarget()
	{
		int num = -1;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (nPC.CanBeChasedBy(Projectile))
			{
				_ = nPC.wet;
				float num2 = Projectile.Distance(nPC.Center);
				if (num2 <= 400f && (num == -1 || Projectile.Distance(Main.npc[num].Center) > num2))
				{
					num = i;
				}
			}
		}
		return num;
	}
}
