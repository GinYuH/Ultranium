using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class EtherealTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Ethereal Note Tentacle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 40;
		((ModProjectile)this).Projectile.height = 40;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.MaxUpdates = 3;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.alpha = 255;
	}

	public override void AI()
	{
		Vector2 center = ((ModProjectile)this).Projectile.Center;
		((ModProjectile)this).Projectile.scale = 1f - ((ModProjectile)this).Projectile.localAI[0];
		((ModProjectile)this).Projectile.width = (int)(20f * ((ModProjectile)this).Projectile.scale);
		((ModProjectile)this).Projectile.height = ((ModProjectile)this).Projectile.width;
		((ModProjectile)this).Projectile.position.X = center.X - (float)(((ModProjectile)this).Projectile.width / 2);
		((ModProjectile)this).Projectile.position.Y = center.Y - (float)(((ModProjectile)this).Projectile.height / 2);
		if ((double)((ModProjectile)this).Projectile.localAI[0] < 0.1)
		{
			((ModProjectile)this).Projectile.localAI[0] += 0.01f;
		}
		else
		{
			((ModProjectile)this).Projectile.localAI[0] += 0.025f;
		}
		if (((ModProjectile)this).Projectile.localAI[0] >= 0.95f)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		((ModProjectile)this).Projectile.velocity.X = ((ModProjectile)this).Projectile.velocity.X + ((ModProjectile)this).Projectile.ai[0] * 1.5f;
		((ModProjectile)this).Projectile.velocity.Y = ((ModProjectile)this).Projectile.velocity.Y + ((ModProjectile)this).Projectile.ai[1] * 1.5f;
		if (((ModProjectile)this).Projectile.velocity.Length() > 16f)
		{
			((ModProjectile)this).Projectile.velocity.Normalize();
			((ModProjectile)this).Projectile.velocity *= 16f;
		}
		((ModProjectile)this).Projectile.ai[0] *= 1.05f;
		((ModProjectile)this).Projectile.ai[1] *= 1.05f;
		if (((ModProjectile)this).Projectile.scale < 1f)
		{
			for (int i = 0; (float)i < ((ModProjectile)this).Projectile.scale * 10f; i++)
			{
				int num = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 62, ((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y, 100, default(Color), 1.1f);
				Main.dust[num].position = (Main.dust[num].position + ((ModProjectile)this).Projectile.Center) / 2f;
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 0.1f;
				Main.dust[num].velocity -= ((ModProjectile)this).Projectile.velocity * (1.3f - ((ModProjectile)this).Projectile.scale);
				Main.dust[num].fadeIn = 100 + ((ModProjectile)this).Projectile.owner;
				_ = Main.dust[num];
			}
		}
	}
}
