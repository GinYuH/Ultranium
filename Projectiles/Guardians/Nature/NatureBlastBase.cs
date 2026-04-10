using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Nature;

public class NatureBlastBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Ultranium Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 1f;
		((ModProjectile)this).Projectile.width = 4;
		((ModProjectile)this).Projectile.height = 4;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.penetrate = 10;
		((ModProjectile)this).Projectile.timeLeft = 1;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.alpha = 255;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.velocity *= 0f;
	}

	public override void OnKill(int timeLeft)
	{
		int num = 6;
		int num2 = 650;
		for (float num3 = 0f; num3 < (float)num; num3 += 1f)
		{
			Vector2 vector = ((ModProjectile)this).Projectile.Center + new Vector2(0f, num2).RotatedBy((double)num3 * (Math.PI * 2.0 / (double)num));
			Vector2 vector2 = ((ModProjectile)this).Projectile.Center - vector;
			vector2.Normalize();
			vector2 *= 2f;
			_ = Main.projectile[Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("NatureBlast").Type, ((ModProjectile)this).Projectile.damage, 6f, 0, 0f, 0f)];
		}
	}
}
