using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ranged;

public class WaterJavelin : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ancient Javelin");
	}

	public override void SetDefaults()
	{
		Projectile.width = 68;
		Projectile.height = 68;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.8f;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] < 5f)
		{
			Projectile.tileCollide = false;
		}
		if (Projectile.ai[0] >= 5f)
		{
			Projectile.tileCollide = true;
		}
		if (Projectile.ai[0] >= 65f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Projectile.Kill();
		SoundEngine.PlaySound(SoundID.Item10, new Vector2(Projectile.position.X, Projectile.position.Y));
		return false;
	}
}
