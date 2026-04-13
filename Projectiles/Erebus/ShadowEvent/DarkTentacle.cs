using System;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DarkTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Dark Tentacle");
	}

	public override void SetDefaults()
	{
		Projectile.width = 22;
		Projectile.height = 22;
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
			if (Projectile.alpha > 0)
			{
				return;
			}
			Projectile.alpha = 0;
			Projectile.ai[0] = 1f;
			if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] += 1f;
				Projectile.position += Projectile.velocity * 1f;
			}
			if (Main.myPlayer == Projectile.owner)
			{
				int num = Projectile.type;
				if (Projectile.ai[1] >= 20f + (float)Main.rand.Next(0, 6))
				{
					num = Mod.Find<ModProjectile>("DarkTentacleTip").Type;
				}
				int number = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + Projectile.velocity.X + (float)(Projectile.width / 2), Projectile.position.Y + Projectile.velocity.Y + (float)(Projectile.height / 2), Projectile.velocity.X, Projectile.velocity.Y, num, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, Projectile.ai[1] + 1f);
				NetMessage.SendData(27, -1, -1, null, number);
			}
		}
		else
		{
			Projectile.alpha += 2;
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
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
