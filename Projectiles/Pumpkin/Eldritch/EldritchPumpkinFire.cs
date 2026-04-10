using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin.Eldritch;

public class EldritchPumpkinFire : ModProjectile
{
	private int Bounces = 3;

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Pumpkin Fire");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 16;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 1;
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
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] > 60f)
		{
			((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + 0.15f;
		}
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 6);
			dust.noGravity = true;
			dust.scale = 1f;
		}
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
