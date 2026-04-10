using System;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DarkTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Dark Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 22;
		((ModProjectile)this).projectile.height = 22;
		((ModProjectile)this).projectile.aiStyle = 4;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.melee = true;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			((ModProjectile)this).projectile.alpha -= 50;
			if (((ModProjectile)this).projectile.alpha > 0)
			{
				return;
			}
			((ModProjectile)this).projectile.alpha = 0;
			((ModProjectile)this).projectile.ai[0] = 1f;
			if (((ModProjectile)this).projectile.ai[1] == 0f)
			{
				((ModProjectile)this).projectile.ai[1] += 1f;
				((ModProjectile)this).projectile.position += ((ModProjectile)this).projectile.velocity * 1f;
			}
			if (Main.myPlayer == ((ModProjectile)this).projectile.owner)
			{
				int num = ((ModProjectile)this).projectile.type;
				if (((ModProjectile)this).projectile.ai[1] >= 20f + (float)Main.rand.Next(0, 6))
				{
					num = ((ModProjectile)this).mod.ProjectileType("DarkTentacleTip");
				}
				int number = Projectile.NewProjectile(((ModProjectile)this).projectile.position.X + ((ModProjectile)this).projectile.velocity.X + (float)(((ModProjectile)this).projectile.width / 2), ((ModProjectile)this).projectile.position.Y + ((ModProjectile)this).projectile.velocity.Y + (float)(((ModProjectile)this).projectile.height / 2), ((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y, num, ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, 0f, ((ModProjectile)this).projectile.ai[1] + 1f);
				NetMessage.SendData(27, -1, -1, null, number);
			}
		}
		else
		{
			((ModProjectile)this).projectile.alpha += 2;
			if (((ModProjectile)this).projectile.alpha >= 255)
			{
				((ModProjectile)this).projectile.Kill();
			}
		}
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(((ModProjectile)this).projectile.position + ((ModProjectile)this).projectile.velocity, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustBlack"), ((ModProjectile)this).projectile.oldVelocity.X * 0.025f, ((ModProjectile)this).projectile.oldVelocity.Y * 0.025f);
		}
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 6;
	}
}
