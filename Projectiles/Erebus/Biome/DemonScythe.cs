using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class DemonScythe : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shade Sickle");
	}

	public override void SetDefaults()
	{
		Projectile.width = 48;
		Projectile.height = 48;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 1;
		Projectile.tileCollide = true;
		Projectile.timeLeft = 120;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 10;
	}

	public override void AI()
	{
		Projectile.rotation += 0.4f;
	}

	public override void OnKill(int timeLeft)
	{
		int num = 25;
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = (Vector2.One * new Vector2((float)Projectile.width / 5f, Projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + Projectile.Center;
			Vector2 vector2 = vector - Projectile.Center;
			Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, DustID.GemEmerald, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
			obj.noGravity = true;
			obj.noLight = false;
			obj.velocity = Vector2.Normalize(vector2) * 3f;
			obj.fadeIn = 1.3f;
			Projectile.ai[0] = 1f;
		}
	}
}
