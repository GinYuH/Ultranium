using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.IceDragon.Projectiles;

public class IceSpike : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).Projectile.type] = 5;
		// ((ModProjectile)this).DisplayName.SetDefault("Icicle Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 1f;
		((ModProjectile)this).Projectile.width = 14;
		((ModProjectile)this).Projectile.height = 14;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.timeLeft = 550;
		((ModProjectile)this).Projectile.tileCollide = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		if (++((ModProjectile)this).Projectile.frameCounter >= 16)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 5)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
	}
}
