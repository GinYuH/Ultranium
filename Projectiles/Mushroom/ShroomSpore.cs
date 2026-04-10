using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Mushroom;

public class ShroomSpore : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Shroom Spore");
		Main.projFrames[((ModProjectile)this).projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 12;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.timeLeft = 600;
		((ModProjectile)this).projectile.light = 0f;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = true;
	}

	public override void AI()
	{
		if (++((ModProjectile)this).projectile.frameCounter >= 16)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			if (++((ModProjectile)this).projectile.frame >= 3)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.X * 0.02f;
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] < 5f)
		{
			((ModProjectile)this).projectile.tileCollide = false;
		}
		if (((ModProjectile)this).projectile.ai[0] >= 5f)
		{
			((ModProjectile)this).projectile.tileCollide = true;
		}
		if (((ModProjectile)this).projectile.ai[0] >= 60f)
		{
			((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + 0.05f;
		}
	}
}
