using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class AquaBall : ModProjectile
{
	private int Bounces = 5;

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Water Sphere");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 26;
		((ModProjectile)this).projectile.height = 26;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 5;
		((ModProjectile)this).projectile.timeLeft = 600;
		((ModProjectile)this).projectile.light = 0f;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.spriteDirection = ((((ModProjectile)this).projectile.velocity.X > 0f) ? 1 : (-1));
		if (((ModProjectile)this).projectile.spriteDirection == 1)
		{
			((ModProjectile)this).projectile.rotation += 0.7f;
		}
		if (((ModProjectile)this).projectile.spriteDirection == -1)
		{
			((ModProjectile)this).projectile.rotation += -0.7f;
		}
		((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + 0.15f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Bounces--;
		if (Bounces <= 0)
		{
			((ModProjectile)this).projectile.Kill();
		}
		else
		{
			if (((ModProjectile)this).projectile.velocity.X != oldVelocity.X)
			{
				((ModProjectile)this).projectile.velocity.X = (0f - oldVelocity.X) * 0.8f;
			}
			if (((ModProjectile)this).projectile.velocity.Y != oldVelocity.Y)
			{
				((ModProjectile)this).projectile.velocity.Y = (0f - oldVelocity.Y) * 0.8f;
			}
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
