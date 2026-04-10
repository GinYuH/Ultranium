using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class C4Boom : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("C4 Explosion");
		Main.projFrames[Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		Projectile.width = 70;
		Projectile.height = 70;
		Projectile.penetrate = -1;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.timeLeft = 600;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 60;
		Projectile.alpha = 100;
	}

	public override void AI()
	{
		Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 178);
		dust.noGravity = true;
		dust.scale = 1.6f;
		if (++Projectile.frameCounter >= 4)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 4)
			{
				Projectile.Kill();
			}
		}
		Projectile.velocity.X *= 0f;
		Projectile.velocity.Y *= 0f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnKill(int timeLeft)
	{
		Projectile.timeLeft = 0;
	}
}
