using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class WaterJavelin : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ancient Javelin");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 68;
		((ModProjectile)this).projectile.height = 68;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.tileCollide = true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 0.8f;
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] < 5f)
		{
			((ModProjectile)this).projectile.tileCollide = false;
		}
		if (((ModProjectile)this).projectile.ai[0] >= 5f)
		{
			((ModProjectile)this).projectile.tileCollide = true;
		}
		if (((ModProjectile)this).projectile.ai[0] >= 65f)
		{
			((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + 0.15f;
			((ModProjectile)this).projectile.velocity.X = ((ModProjectile)this).projectile.velocity.X * 0.99f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		((ModProjectile)this).projectile.Kill();
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 10, 1f, 0f);
		return false;
	}
}
