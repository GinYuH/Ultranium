using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class DripProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
	}

	public override void SetDefaults()
	{
		Projectile.DamageType = DamageClass.Melee;
		Projectile.CloneDefaults(564);
		Projectile.damage = 24;
		Projectile.extraUpdates = 1;
		base.AIType = 564;
	}

	public override void PostAI()
	{
		Projectile.rotation -= 10f;
	}

	public override void AI()
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 140)
		{
			Projectile.frameCounter = 0;
			float num = (float)((double)Main.rand.Next(0, 361) * (Math.PI / 180.0));
			Vector2 vector = new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
			int num2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, 27, Projectile.damage, (float)Projectile.owner, 0, 0f, 0f);
			Main.projectile[num2].friendly = true;
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].velocity *= 7f;
		}
	}
}
