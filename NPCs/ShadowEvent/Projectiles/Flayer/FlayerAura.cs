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
		((ModProjectile)this).DisplayName.SetDefault("Flayer Aura");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 4;
		((ModProjectile)this).projectile.height = 4;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.timeLeft = 4000;
		((ModProjectile)this).projectile.extraUpdates = 2;
	}

	public override void AI()
	{
		Vector2 spinningpoint = new Vector2(((ModProjectile)this).projectile.ai[0] * 10f, 0f);
		Projectile projectile = Main.projectile[(int)((ModProjectile)this).projectile.ai[1]];
		if (reset)
		{
			if (((ModProjectile)this).projectile.ai[0] == 1f)
			{
				spinAi -= 1f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 2f)
			{
				spinAi -= 2f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 3f)
			{
				spinAi -= 3f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 4f)
			{
				spinAi -= 4f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 5f)
			{
				spinAi -= 5f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 6f)
			{
				spinAi -= 6f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 7f)
			{
				spinAi -= 7f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 8f)
			{
				spinAi -= 8f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 9f)
			{
				spinAi -= 9f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 10f)
			{
				spinAi -= 10f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 11f)
			{
				spinAi -= 11f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 12f)
			{
				spinAi -= 12f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 13f)
			{
				spinAi -= 13f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 14f)
			{
				spinAi -= 14f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 15f)
			{
				spinAi -= 15f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 16f)
			{
				spinAi -= 16f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 17f)
			{
				spinAi -= 17f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 18f)
			{
				spinAi -= 18f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 19f)
			{
				spinAi -= 19f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 20f)
			{
				spinAi -= 20f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 21f)
			{
				spinAi -= 21f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 22f)
			{
				spinAi -= 22f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 23f)
			{
				spinAi -= 23f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 24f)
			{
				spinAi -= 24f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 25f)
			{
				spinAi -= 25f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 26f)
			{
				spinAi -= 26f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 27f)
			{
				spinAi -= 27f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 28f)
			{
				spinAi -= 28f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 29f)
			{
				spinAi -= 29f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 30f)
			{
				spinAi -= 30f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 31f)
			{
				spinAi -= 31f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 32f)
			{
				spinAi -= 32f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 33f)
			{
				spinAi -= 33f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 34f)
			{
				spinAi -= 34f;
			}
			if (((ModProjectile)this).projectile.ai[0] == 35f)
			{
				spinAi -= 35f;
			}
			reset = false;
		}
		spinAi += 0.03f;
		((ModProjectile)this).projectile.Center = projectile.Center + spinningpoint.RotatedBy((double)spinAi + (double)((ModProjectile)this).projectile.ai[1] * (Math.PI / 4.0));
		if (!((Entity)projectile).active)
		{
			((ModProjectile)this).projectile.Kill();
		}
		if (Main.rand.Next(2) == 0)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustBlack"), ((ModProjectile)this).projectile.velocity.X * 1.2f, ((ModProjectile)this).projectile.velocity.Y * 1.2f, 130, default(Color), 4.75f);
			Main.dust[num].velocity *= 0.1f;
			Main.dust[num].scale *= 0.5f;
			Main.dust[num].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 2;
	}
}
