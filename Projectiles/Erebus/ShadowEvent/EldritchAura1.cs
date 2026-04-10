using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class EldritchAura1 : ModProjectile
{
	public float spinAi;

	public bool reset = true;

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Eldritch Aura");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 4;
		((ModProjectile)this).projectile.height = 4;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.timeLeft = 4000;
		((ModProjectile)this).projectile.extraUpdates = 2;
	}

	public override void AI()
	{
		Vector2 spinningpoint = new Vector2(((ModProjectile)this).projectile.ai[0] * 8f, 0f);
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
			Main.dust[num].scale *= 0.2f;
			Main.dust[num].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 2;
	}
}
