using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Blood;

public class TheGout : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 1;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.width = 16;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.CloneDefaults(564);
		((ModProjectile)this).projectile.damage = 24;
		((ModProjectile)this).projectile.extraUpdates = 1;
		base.aiType = 564;
	}

	public override void PostAI()
	{
		((ModProjectile)this).projectile.rotation -= 10f;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter >= 150)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
			float num = (float)((double)Main.rand.Next(0, 361) * (Math.PI / 180.0));
			Vector2 vector = new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
			int num2 = Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).mod.ProjectileType("BloodSwirl"), ((ModProjectile)this).projectile.damage, (float)((ModProjectile)this).projectile.owner, 0, 0f, 0f);
			Main.projectile[num2].friendly = true;
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].velocity *= 7f;
		}
	}
}
