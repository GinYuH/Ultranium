using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class C4Boom : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("C4 Explosion");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 70;
		((ModProjectile)this).Projectile.height = 70;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.timeLeft = 600;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.usesLocalNPCImmunity = true;
		((ModProjectile)this).Projectile.localNPCHitCooldown = 60;
		((ModProjectile)this).Projectile.alpha = 100;
	}

	public override void AI()
	{
		Dust dust = Dust.NewDustDirect(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 178);
		dust.noGravity = true;
		dust.scale = 1.6f;
		if (++((ModProjectile)this).Projectile.frameCounter >= 4)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 4)
			{
				((ModProjectile)this).Projectile.Kill();
			}
		}
		((ModProjectile)this).Projectile.velocity.X *= 0f;
		((ModProjectile)this).Projectile.velocity.Y *= 0f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnKill(int timeLeft)
	{
		((ModProjectile)this).Projectile.timeLeft = 0;
	}
}
