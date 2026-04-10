using System;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Clones;

public class XenanisTentacleTip : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ethereal Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 38;
		((ModProjectile)this).projectile.height = 40;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = -1;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).projectile.localAI[0] != 0f)
		{
			((ModProjectile)this).projectile.position -= ((ModProjectile)this).projectile.velocity * 1f;
		}
		((ModProjectile)this).projectile.localAI[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			((ModProjectile)this).projectile.alpha -= (int)((ModProjectile)this).projectile.localAI[1];
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
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] > 40f)
		{
			((ModProjectile)this).projectile.alpha += 15;
			if (((ModProjectile)this).projectile.alpha >= 255)
			{
				((ModProjectile)this).projectile.Kill();
			}
		}
	}
}
