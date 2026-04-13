using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Mushroom;

public class ShroomSpore : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shroom Spore");
		Main.projFrames[Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		Projectile.width = 12;
		Projectile.height = 16;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.penetrate = 3;
		Projectile.timeLeft = 600;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 16)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 3)
			{
				Projectile.frame = 0;
			}
		}
		Projectile.rotation = Projectile.velocity.X * 0.02f;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] < 5f)
		{
			Projectile.tileCollide = false;
		}
		if (Projectile.ai[0] >= 5f)
		{
			Projectile.tileCollide = true;
		}
		if (Projectile.ai[0] >= 60f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.05f;
		}
	}
}
