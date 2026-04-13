using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles.Flayer;

public class FlayerAura : ModProjectile
{
	public float spinAi;

	public bool reset = true;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Flayer Aura");
	}

	public override void SetDefaults()
	{
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.alpha = 255;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 4000;
		Projectile.extraUpdates = 2;
	}

	public override void AI()
	{
		Vector2 spinningpoint = new Vector2(Projectile.ai[0] * 10f, 0f);
		Projectile projectile = Main.projectile[(int)Projectile.ai[1]];
		if (reset)
		{
			if (Projectile.ai[0] == 1f)
			{
				spinAi -= 1f;
			}
			if (Projectile.ai[0] == 2f)
			{
				spinAi -= 2f;
			}
			if (Projectile.ai[0] == 3f)
			{
				spinAi -= 3f;
			}
			if (Projectile.ai[0] == 4f)
			{
				spinAi -= 4f;
			}
			if (Projectile.ai[0] == 5f)
			{
				spinAi -= 5f;
			}
			if (Projectile.ai[0] == 6f)
			{
				spinAi -= 6f;
			}
			if (Projectile.ai[0] == 7f)
			{
				spinAi -= 7f;
			}
			if (Projectile.ai[0] == 8f)
			{
				spinAi -= 8f;
			}
			if (Projectile.ai[0] == 9f)
			{
				spinAi -= 9f;
			}
			if (Projectile.ai[0] == 10f)
			{
				spinAi -= 10f;
			}
			if (Projectile.ai[0] == 11f)
			{
				spinAi -= 11f;
			}
			if (Projectile.ai[0] == 12f)
			{
				spinAi -= 12f;
			}
			if (Projectile.ai[0] == 13f)
			{
				spinAi -= 13f;
			}
			if (Projectile.ai[0] == 14f)
			{
				spinAi -= 14f;
			}
			if (Projectile.ai[0] == 15f)
			{
				spinAi -= 15f;
			}
			if (Projectile.ai[0] == 16f)
			{
				spinAi -= 16f;
			}
			if (Projectile.ai[0] == 17f)
			{
				spinAi -= 17f;
			}
			if (Projectile.ai[0] == 18f)
			{
				spinAi -= 18f;
			}
			if (Projectile.ai[0] == 19f)
			{
				spinAi -= 19f;
			}
			if (Projectile.ai[0] == 20f)
			{
				spinAi -= 20f;
			}
			if (Projectile.ai[0] == 21f)
			{
				spinAi -= 21f;
			}
			if (Projectile.ai[0] == 22f)
			{
				spinAi -= 22f;
			}
			if (Projectile.ai[0] == 23f)
			{
				spinAi -= 23f;
			}
			if (Projectile.ai[0] == 24f)
			{
				spinAi -= 24f;
			}
			if (Projectile.ai[0] == 25f)
			{
				spinAi -= 25f;
			}
			if (Projectile.ai[0] == 26f)
			{
				spinAi -= 26f;
			}
			if (Projectile.ai[0] == 27f)
			{
				spinAi -= 27f;
			}
			if (Projectile.ai[0] == 28f)
			{
				spinAi -= 28f;
			}
			if (Projectile.ai[0] == 29f)
			{
				spinAi -= 29f;
			}
			if (Projectile.ai[0] == 30f)
			{
				spinAi -= 30f;
			}
			if (Projectile.ai[0] == 31f)
			{
				spinAi -= 31f;
			}
			if (Projectile.ai[0] == 32f)
			{
				spinAi -= 32f;
			}
			if (Projectile.ai[0] == 33f)
			{
				spinAi -= 33f;
			}
			if (Projectile.ai[0] == 34f)
			{
				spinAi -= 34f;
			}
			if (Projectile.ai[0] == 35f)
			{
				spinAi -= 35f;
			}
			reset = false;
		}
		spinAi += 0.03f;
		Projectile.Center = projectile.Center + spinningpoint.RotatedBy((double)spinAi + (double)Projectile.ai[1] * (Math.PI / 4.0));
		if (!((Entity)projectile).active)
		{
			Projectile.Kill();
		}
		if (Main.rand.Next(2) == 0)
		{
			int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustBlack").Type, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 4.75f);
			Main.dust[num].velocity *= 0.1f;
			Main.dust[num].scale *= 0.5f;
			Main.dust[num].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 2;
	}
}
