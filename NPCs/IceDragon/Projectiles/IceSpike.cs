using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.IceDragon.Projectiles;

public class IceSpike : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 5;
		DisplayName.SetDefault("Icicle Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 14;
		Projectile.height = 14;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 550;
		Projectile.tileCollide = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 16)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 5)
			{
				Projectile.frame = 0;
			}
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}
}
