using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Projectiles;

public class CosmicBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Cosmic Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 32;
		Projectile.height = 32;
		Projectile.aiStyle = -1;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 360;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
		Projectile.alpha = 0;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}
}
