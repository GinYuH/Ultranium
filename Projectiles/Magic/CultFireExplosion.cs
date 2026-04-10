using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Magic;

public class CultFireExplosion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Flame Explosion");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 5;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 50;
		((ModProjectile)this).Projectile.height = 50;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreAI()
	{
		((ModProjectile)this).Projectile.frameCounter++;
		if (((ModProjectile)this).Projectile.frameCounter > 4)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			((ModProjectile)this).Projectile.frame++;
			if (((ModProjectile)this).Projectile.frame >= Main.projFrames[((ModProjectile)this).Projectile.type])
			{
				((ModProjectile)this).Projectile.Kill();
			}
		}
		return false;
	}
}
