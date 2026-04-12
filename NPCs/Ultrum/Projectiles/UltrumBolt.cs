using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ultrum.Projectiles;

public class UltrumBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Nature Bolt");
		Main.projFrames[Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1.2f;
		Projectile.width = 32;
		Projectile.height = 32;
		Projectile.aiStyle = -1;
		Projectile.hostile = true;
		Projectile.friendly = false;
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
		if (++Projectile.frameCounter >= 16)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 3)
			{
				Projectile.frame = 0;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
