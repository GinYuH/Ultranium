using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Mushroom;

public class ShroomSpore : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Shroom Spore");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 12;
		((ModProjectile)this).Projectile.height = 16;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 3;
		((ModProjectile)this).Projectile.timeLeft = 600;
		((ModProjectile)this).Projectile.light = 0f;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		if (++((ModProjectile)this).Projectile.frameCounter >= 16)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 3)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.X * 0.02f;
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] < 5f)
		{
			((ModProjectile)this).Projectile.tileCollide = false;
		}
		if (((ModProjectile)this).Projectile.ai[0] >= 5f)
		{
			((ModProjectile)this).Projectile.tileCollide = true;
		}
		if (((ModProjectile)this).Projectile.ai[0] >= 60f)
		{
			((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y + 0.05f;
		}
	}
}
