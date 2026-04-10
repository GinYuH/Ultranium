using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class EtherealNote : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ethereal Note");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 18;
		((ModProjectile)this).projectile.height = 24;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.timeLeft = 300;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 8;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] >= 65f)
		{
			((ModProjectile)this).projectile.velocity *= 0.98f;
		}
	}

	public override void Kill(int timeLeft)
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
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center, vector, ((ModProjectile)this).mod.ProjectileType("EtherealTentacle"), ((ModProjectile)this).projectile.damage, 0f, Main.myPlayer, num2, num);
		}
	}
}
