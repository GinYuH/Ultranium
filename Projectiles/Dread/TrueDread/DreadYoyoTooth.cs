using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadYoyoTooth : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Tooth");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 14;
		Projectile.height = 26;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 3;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 120;
		Projectile.tileCollide = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 5;
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

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
