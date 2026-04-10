using System;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DarkTentacleTip : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Dark Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 14;
		((ModProjectile)this).projectile.height = 14;
		((ModProjectile)this).projectile.aiStyle = 4;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.melee = true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			((ModProjectile)this).projectile.alpha -= 50;
			if (((ModProjectile)this).projectile.alpha <= 0)
			{
				((ModProjectile)this).projectile.alpha = 0;
				((ModProjectile)this).projectile.ai[0] = 1f;
				if (((ModProjectile)this).projectile.ai[1] == 0f)
				{
					((ModProjectile)this).projectile.ai[1] += 1f;
					((ModProjectile)this).projectile.position += ((ModProjectile)this).projectile.velocity * 1f;
				}
			}
			return;
		}
		if (((ModProjectile)this).projectile.alpha < 170 && ((ModProjectile)this).projectile.alpha + 5 >= 170)
		{
			for (int i = 0; i < 8; i++)
			{
				Dust obj = Main.dust[Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustBlack"), ((ModProjectile)this).projectile.velocity.X * 0.025f, ((ModProjectile)this).projectile.velocity.Y * 0.025f)];
				obj.noGravity = true;
				obj.velocity *= 0.5f;
			}
		}
		((ModProjectile)this).projectile.alpha += 5;
		if (((ModProjectile)this).projectile.alpha >= 255)
		{
			((ModProjectile)this).projectile.Kill();
		}
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(((ModProjectile)this).projectile.position + ((ModProjectile)this).projectile.velocity, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustBlack"), ((ModProjectile)this).projectile.oldVelocity.X * 0.025f, ((ModProjectile)this).projectile.oldVelocity.Y * 0.025f);
		}
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 6;
	}
}
