using System;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal.Clones;

public class XenanisTentacleTip : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ethereal Tentacle");
	}

	public override void SetDefaults()
	{
		Projectile.width = 38;
		Projectile.height = 40;
		Projectile.tileCollide = false;
		Projectile.hostile = true;
		Projectile.alpha = 255;
		Projectile.penetrate = -1;
	}

	public override void AI()
	{
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		if (Projectile.localAI[0] != 0f)
		{
			Projectile.position -= Projectile.velocity * 1f;
		}
		Projectile.localAI[0] += 1f;
		if (Projectile.ai[0] == 0f)
		{
			Projectile.alpha -= (int)Projectile.localAI[1];
			if (Projectile.alpha <= 0)
			{
				Projectile.alpha = 0;
				Projectile.ai[0] = 1f;
				if (Projectile.ai[1] == 0f)
				{
					Projectile.ai[1] += 1f;
					Projectile.position += Projectile.velocity * 1f;
				}
			}
			return;
		}
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 40f)
		{
			Projectile.alpha += 15;
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
		}
	}
}
