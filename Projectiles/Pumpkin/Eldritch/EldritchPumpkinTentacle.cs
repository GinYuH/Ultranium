using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin.Eldritch;

public class EldritchPumpkinTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Shade Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 40;
		((ModProjectile)this).projectile.height = 40;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = 5;
		((ModProjectile)this).projectile.MaxUpdates = 3;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.alpha = 255;
	}

	public override void AI()
	{
		Vector2 center = ((ModProjectile)this).projectile.Center;
		((ModProjectile)this).projectile.scale = 1f - ((ModProjectile)this).projectile.localAI[0];
		((ModProjectile)this).projectile.width = (int)(20f * ((ModProjectile)this).projectile.scale);
		((ModProjectile)this).projectile.height = ((ModProjectile)this).projectile.width;
		((ModProjectile)this).projectile.position.X = center.X - (float)(((ModProjectile)this).projectile.width / 2);
		((ModProjectile)this).projectile.position.Y = center.Y - (float)(((ModProjectile)this).projectile.height / 2);
		if ((double)((ModProjectile)this).projectile.localAI[0] < 0.1)
		{
			((ModProjectile)this).projectile.localAI[0] += 0.01f;
		}
		else
		{
			((ModProjectile)this).projectile.localAI[0] += 0.025f;
		}
		if (((ModProjectile)this).projectile.localAI[0] >= 0.95f)
		{
			((ModProjectile)this).projectile.Kill();
		}
		((ModProjectile)this).projectile.velocity.X = ((ModProjectile)this).projectile.velocity.X + ((ModProjectile)this).projectile.ai[0] * 1.5f;
		((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + ((ModProjectile)this).projectile.ai[1] * 1.5f;
		if (((ModProjectile)this).projectile.velocity.Length() > 16f)
		{
			((ModProjectile)this).projectile.velocity.Normalize();
			((ModProjectile)this).projectile.velocity *= 16f;
		}
		((ModProjectile)this).projectile.ai[0] *= 1.05f;
		((ModProjectile)this).projectile.ai[1] *= 1.05f;
		if (((ModProjectile)this).projectile.scale < 1f)
		{
			for (int i = 0; (float)i < ((ModProjectile)this).projectile.scale * 10f; i++)
			{
				int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustPurple"), ((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y, 100, default(Color), 1.1f);
				Main.dust[num].position = (Main.dust[num].position + ((ModProjectile)this).projectile.Center) / 2f;
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 0.1f;
				Main.dust[num].velocity -= ((ModProjectile)this).projectile.velocity * (1.3f - ((ModProjectile)this).projectile.scale);
				Main.dust[num].fadeIn = 100 + ((ModProjectile)this).projectile.owner;
				_ = Main.dust[num];
			}
		}
	}
}
