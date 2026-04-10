using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Nature;

public class NatureBlastBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ultranium Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 4;
		((ModProjectile)this).projectile.height = 4;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = 10;
		((ModProjectile)this).projectile.timeLeft = 1;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 255;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.velocity *= 0f;
	}

	public override void Kill(int timeLeft)
	{
		int num = 6;
		int num2 = 650;
		for (float num3 = 0f; num3 < (float)num; num3 += 1f)
		{
			Vector2 vector = ((ModProjectile)this).projectile.Center + new Vector2(0f, num2).RotatedBy((double)num3 * (Math.PI * 2.0 / (double)num));
			Vector2 vector2 = ((ModProjectile)this).projectile.Center - vector;
			vector2.Normalize();
			vector2 *= 2f;
			_ = Main.projectile[Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, ((ModProjectile)this).mod.ProjectileType("NatureBlast"), ((ModProjectile)this).projectile.damage, 6f, 0, 0f, 0f)];
		}
	}
}
