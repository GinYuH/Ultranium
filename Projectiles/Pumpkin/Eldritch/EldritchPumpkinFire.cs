using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin.Eldritch;

public class EldritchPumpkinFire : ModProjectile
{
	private int Bounces = 3;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Pumpkin Fire");
	}

	public override void SetDefaults()
	{
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 600;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		Projectile.spriteDirection = ((Projectile.velocity.X > 0f) ? 1 : (-1));
		if (Projectile.spriteDirection == 1)
		{
			Projectile.rotation += 0.7f;
		}
		if (Projectile.spriteDirection == -1)
		{
			Projectile.rotation += -0.7f;
		}
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 60f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
		}
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6);
			dust.noGravity = true;
			dust.scale = 1f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Bounces--;
		if (Bounces <= 0)
		{
			Projectile.Kill();
		}
		else
		{
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = (0f - oldVelocity.X) * 0.8f;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.8f;
			}
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
