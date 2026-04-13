using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class DreadTooth : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Tooth");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 14;
		Projectile.height = 38;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 18000;
		Projectile.tileCollide = true;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 180f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
