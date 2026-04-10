using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class EtherealTentacle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ethereal Note Tentacle");
	}

	public override void SetDefaults()
	{
		Projectile.width = 40;
		Projectile.height = 40;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.MaxUpdates = 3;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.tileCollide = false;
		Projectile.alpha = 255;
	}

	public override void AI()
	{
		Vector2 center = Projectile.Center;
		Projectile.scale = 1f - Projectile.localAI[0];
		Projectile.width = (int)(20f * Projectile.scale);
		Projectile.height = Projectile.width;
		Projectile.position.X = center.X - (float)(Projectile.width / 2);
		Projectile.position.Y = center.Y - (float)(Projectile.height / 2);
		if ((double)Projectile.localAI[0] < 0.1)
		{
			Projectile.localAI[0] += 0.01f;
		}
		else
		{
			Projectile.localAI[0] += 0.025f;
		}
		if (Projectile.localAI[0] >= 0.95f)
		{
			Projectile.Kill();
		}
		Projectile.velocity.X = Projectile.velocity.X + Projectile.ai[0] * 1.5f;
		Projectile.velocity.Y = Projectile.velocity.Y + Projectile.ai[1] * 1.5f;
		if (Projectile.velocity.Length() > 16f)
		{
			Projectile.velocity.Normalize();
			Projectile.velocity *= 16f;
		}
		Projectile.ai[0] *= 1.05f;
		Projectile.ai[1] *= 1.05f;
		if (Projectile.scale < 1f)
		{
			for (int i = 0; (float)i < Projectile.scale * 10f; i++)
			{
				int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 62, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.1f);
				Main.dust[num].position = (Main.dust[num].position + Projectile.Center) / 2f;
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 0.1f;
				Main.dust[num].velocity -= Projectile.velocity * (1.3f - Projectile.scale);
				Main.dust[num].fadeIn = 100 + Projectile.owner;
				_ = Main.dust[num];
			}
		}
	}
}
