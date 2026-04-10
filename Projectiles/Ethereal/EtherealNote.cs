using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class EtherealNote : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ethereal Note");
	}

	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 24;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 300;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 8;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 65f)
		{
			Projectile.velocity *= 0.98f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
		for (int i = 0; i < 6; i++)
		{
			Vector2 vector = spinningpoint.RotatedBy(Math.PI / 3.0 * ((double)i + Main.rand.NextDouble() - 0.5));
			float num = (float)Main.rand.Next(10, 80) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num *= -1f;
			}
			float num2 = (float)Main.rand.Next(10, 80) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num2 *= -1f;
			}
			Projectile.NewProjectile(null, Projectile.Center, vector, Mod.Find<ModProjectile>("EtherealTentacle").Type, Projectile.damage, 0f, Main.myPlayer, num2, num);
		}
	}
}
