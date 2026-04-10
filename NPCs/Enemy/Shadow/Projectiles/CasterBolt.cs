using System;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Shadow.Projectiles;

public class CasterBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).Projectile.type] = 6;
		// ((ModProjectile)this).DisplayName.SetDefault("Eldritch Glob");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 1f;
		((ModProjectile)this).Projectile.width = 14;
		((ModProjectile)this).Projectile.height = 38;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.timeLeft = 300;
		((ModProjectile)this).Projectile.tileCollide = true;
	}

	public override void AI()
	{
		if (++((ModProjectile)this).Projectile.frameCounter >= 16)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 6)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
	}
}
