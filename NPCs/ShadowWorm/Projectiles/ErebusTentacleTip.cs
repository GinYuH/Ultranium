using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusTentacleTip : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Erebus Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 38;
		((ModProjectile)this).Projectile.height = 40;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.penetrate = -1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).Projectile.localAI[0] != 0f)
		{
			((ModProjectile)this).Projectile.position -= ((ModProjectile)this).Projectile.velocity * 1f;
		}
		((ModProjectile)this).Projectile.localAI[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			((ModProjectile)this).Projectile.alpha -= (int)((ModProjectile)this).Projectile.localAI[1];
			if (((ModProjectile)this).Projectile.alpha <= 0)
			{
				((ModProjectile)this).Projectile.alpha = 0;
				((ModProjectile)this).Projectile.ai[0] = 1f;
				if (((ModProjectile)this).Projectile.ai[1] == 0f)
				{
					((ModProjectile)this).Projectile.ai[1] += 1f;
					((ModProjectile)this).Projectile.position += ((ModProjectile)this).Projectile.velocity * 1f;
				}
			}
			return;
		}
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] > 40f)
		{
			((ModProjectile)this).Projectile.alpha += 15;
			if (((ModProjectile)this).Projectile.alpha >= 255)
			{
				((ModProjectile)this).Projectile.Kill();
			}
		}
	}
}
