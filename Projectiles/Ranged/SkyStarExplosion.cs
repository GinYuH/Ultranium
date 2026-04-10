using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class SkyStarExplosion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Holy Explosion");
		Main.projFrames[((ModProjectile)this).projectile.type] = 6;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 70;
		((ModProjectile)this).projectile.height = 70;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.timeLeft = 600;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.usesLocalNPCImmunity = true;
		((ModProjectile)this).projectile.localNPCHitCooldown = 60;
		((ModProjectile)this).projectile.alpha = 100;
	}

	public override void AI()
	{
		Dust dust = Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 228);
		dust.noGravity = true;
		dust.scale = 1.6f;
		if (++((ModProjectile)this).projectile.frameCounter >= 4)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 6)
			{
				((ModProjectile)this).projectile.Kill();
			}
		}
		((ModProjectile)this).projectile.velocity.X *= 0f;
		((ModProjectile)this).projectile.velocity.Y *= 0f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void Kill(int timeLeft)
	{
		((ModProjectile)this).projectile.timeLeft = 0;
	}
}
