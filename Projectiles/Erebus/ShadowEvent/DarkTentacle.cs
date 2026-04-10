using System;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DarkTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Dark Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 22;
		((ModProjectile)this).Projectile.height = 22;
		((ModProjectile)this).Projectile.aiStyle = 4;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 1.57f;
		if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			((ModProjectile)this).Projectile.alpha -= 50;
			if (((ModProjectile)this).Projectile.alpha > 0)
			{
				return;
			}
			((ModProjectile)this).Projectile.alpha = 0;
			((ModProjectile)this).Projectile.ai[0] = 1f;
			if (((ModProjectile)this).Projectile.ai[1] == 0f)
			{
				((ModProjectile)this).Projectile.ai[1] += 1f;
				((ModProjectile)this).Projectile.position += ((ModProjectile)this).Projectile.velocity * 1f;
			}
			if (Main.myPlayer == ((ModProjectile)this).Projectile.owner)
			{
				int num = ((ModProjectile)this).Projectile.type;
				if (((ModProjectile)this).Projectile.ai[1] >= 20f + (float)Main.rand.Next(0, 6))
				{
					num = ((ModProjectile)this).Mod.Find<ModProjectile>("DarkTentacleTip").Type;
				}
				int number = Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.position.X + ((ModProjectile)this).Projectile.velocity.X + (float)(((ModProjectile)this).Projectile.width / 2), ((ModProjectile)this).Projectile.position.Y + ((ModProjectile)this).Projectile.velocity.Y + (float)(((ModProjectile)this).Projectile.height / 2), ((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y, num, ((ModProjectile)this).Projectile.damage, ((ModProjectile)this).Projectile.knockBack, ((ModProjectile)this).Projectile.owner, 0f, ((ModProjectile)this).Projectile.ai[1] + 1f);
				NetMessage.SendData(27, -1, -1, null, number);
			}
		}
		else
		{
			((ModProjectile)this).Projectile.alpha += 2;
			if (((ModProjectile)this).Projectile.alpha >= 255)
			{
				((ModProjectile)this).Projectile.Kill();
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(((ModProjectile)this).Projectile.position + ((ModProjectile)this).Projectile.velocity, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, ((ModProjectile)this).Mod.Find<ModDust>("ShadowDustBlack").Type, ((ModProjectile)this).Projectile.oldVelocity.X * 0.025f, ((ModProjectile)this).Projectile.oldVelocity.Y * 0.025f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 6;
	}
}
