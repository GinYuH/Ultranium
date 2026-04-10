using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Magic;

public class CultFireExplosion : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Flame Explosion");
		Main.projFrames[((ModProjectile)this).projectile.type] = 5;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 50;
		((ModProjectile)this).projectile.height = 50;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.magic = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreAI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter > 4)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			((ModProjectile)this).projectile.frame++;
			if (((ModProjectile)this).projectile.frame >= Main.projFrames[((ModProjectile)this).projectile.type])
			{
				((ModProjectile)this).projectile.Kill();
			}
		}
		return false;
	}
}
