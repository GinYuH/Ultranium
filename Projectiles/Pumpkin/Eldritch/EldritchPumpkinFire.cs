using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin.Eldritch;

public class EldritchPumpkinFire : ModProjectile
{
	private int Bounces = 3;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Pumpkin Fire");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 16;
		((ModProjectile)this).Projectile.height = 16;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 1;
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
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] > 60f)
		{
			((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y + 0.15f;
		}
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 6);
			dust.noGravity = true;
			dust.scale = 1f;
		}
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
