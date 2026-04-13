using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Nature;

public class NatureBlastBase : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ultranium Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = 10;
		Projectile.timeLeft = 1;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = true;
		Projectile.alpha = 255;
	}

	public override void AI()
	{
		Projectile.velocity *= 0f;
	}

	public override void OnKill(int timeLeft)
	{
		int num = 6;
		int num2 = 650;
		for (float num3 = 0f; num3 < (float)num; num3 += 1f)
		{
			Vector2 vector = Projectile.Center + new Vector2(0f, num2).RotatedBy((double)num3 * (Math.PI * 2.0 / (double)num));
			Vector2 vector2 = Projectile.Center - vector;
			vector2.Normalize();
			vector2 *= 2f;
			_ = Main.projectile[Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector.X, vector.Y, vector2.X, vector2.Y, Mod.Find<ModProjectile>("NatureBlast").Type, Projectile.damage, 6f, 0, 0f, 0f)];
		}
	}
}
