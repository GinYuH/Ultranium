using System;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DarkTentacleTip : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Dark Tentacle");
	}

	public override void SetDefaults()
	{
		Projectile.width = 14;
		Projectile.height = 14;
		Projectile.aiStyle = 4;
		Projectile.friendly = true;
		Projectile.alpha = 255;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
		if (Projectile.ai[0] == 0f)
		{
			Projectile.alpha -= 50;
			if (Projectile.alpha <= 0)
			{
				Projectile.alpha = 0;
				Projectile.ai[0] = 1f;
				if (Projectile.ai[1] == 0f)
				{
					Projectile.ai[1] += 1f;
					Projectile.position += Projectile.velocity * 1f;
				}
			}
			return;
		}
		if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
		{
			for (int i = 0; i < 8; i++)
			{
				Dust obj = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustBlack").Type, Projectile.velocity.X * 0.025f, Projectile.velocity.Y * 0.025f)];
				obj.noGravity = true;
				obj.velocity *= 0.5f;
			}
		}
		Projectile.alpha += 5;
		if (Projectile.alpha >= 255)
		{
			Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, Mod.Find<ModDust>("ShadowDustBlack").Type, Projectile.oldVelocity.X * 0.025f, Projectile.oldVelocity.Y * 0.025f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 6;
	}
}
