using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class DreadTooth : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Dread Tooth");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.scale = 1f;
		((ModProjectile)this).Projectile.width = 14;
		((ModProjectile)this).Projectile.height = 38;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.timeLeft = 18000;
		((ModProjectile)this).Projectile.tileCollide = true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = ((ModProjectile)this).Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).Projectile.rotation += 0f * (float)((ModProjectile)this).Projectile.direction;
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] >= 180f)
		{
			((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y + 0.1f;
			((ModProjectile)this).Projectile.velocity.X = ((ModProjectile)this).Projectile.velocity.X * 0.99f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
