using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class AquaBall : ModProjectile
{
	private int Bounces = 5;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Water Sphere");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 26;
		((ModProjectile)this).Projectile.height = 26;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 5;
		((ModProjectile)this).Projectile.timeLeft = 600;
		((ModProjectile)this).Projectile.light = 0f;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.spriteDirection = ((((ModProjectile)this).Projectile.velocity.X > 0f) ? 1 : (-1));
		if (((ModProjectile)this).Projectile.spriteDirection == 1)
		{
			((ModProjectile)this).Projectile.rotation += 0.7f;
		}
		if (((ModProjectile)this).Projectile.spriteDirection == -1)
		{
			((ModProjectile)this).Projectile.rotation += -0.7f;
		}
		((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y + 0.15f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Bounces--;
		if (Bounces <= 0)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		else
		{
			if (((ModProjectile)this).Projectile.velocity.X != oldVelocity.X)
			{
				((ModProjectile)this).Projectile.velocity.X = (0f - oldVelocity.X) * 0.8f;
			}
			if (((ModProjectile)this).Projectile.velocity.Y != oldVelocity.Y)
			{
				((ModProjectile)this).Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.8f;
			}
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
