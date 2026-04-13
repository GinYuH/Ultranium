using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class InkGlob : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 6;
		//DisplayName.SetDefault("Ink Glob");
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
		Projectile.timeLeft = 300;
		Projectile.tileCollide = false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Slow, 60, quiet: false);
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 16)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}
}
