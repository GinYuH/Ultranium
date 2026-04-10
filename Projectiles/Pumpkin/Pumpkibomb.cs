using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin;

public class Pumpkibomb : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Pumpkibomb");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 36;
		((ModProjectile)this).projectile.aiStyle = 18;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.timeLeft = 2000;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.aiStyle = 0;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 10;
	}

	public override void Kill(int timeLeft)
	{
		Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 14, 1f, 0f);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num].velocity *= 1.4f;
		}
		for (int j = 0; j < 10; j++)
		{
			int num2 = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[num2].noGravity = true;
			Main.dust[num2].velocity *= 5f;
			num2 = Dust.NewDust(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num2].velocity *= 3f;
		}
		int num3 = Gore.NewGore(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num3].velocity *= 0.4f;
		Gore gore = Main.gore[num3];
		gore.velocity.X = gore.velocity.X + 1f;
		Gore gore2 = Main.gore[num3];
		gore2.velocity.Y = gore2.velocity.Y + 1f;
		num3 = Gore.NewGore(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num3].velocity *= 0.4f;
		Gore gore3 = Main.gore[num3];
		gore3.velocity.X = gore3.velocity.X - 1f;
		Gore gore4 = Main.gore[num3];
		gore4.velocity.Y = gore4.velocity.Y + 1f;
		num3 = Gore.NewGore(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num3].velocity *= 0.4f;
		Gore gore5 = Main.gore[num3];
		gore5.velocity.X = gore5.velocity.X + 1f;
		Gore gore6 = Main.gore[num3];
		gore6.velocity.Y = gore6.velocity.Y - 1f;
		num3 = Gore.NewGore(new Vector2(((ModProjectile)this).projectile.position.X, ((ModProjectile)this).projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num3].velocity *= 0.4f;
		Gore gore7 = Main.gore[num3];
		gore7.velocity.X = gore7.velocity.X - 1f;
		Gore gore8 = Main.gore[num3];
		gore8.velocity.Y = gore8.velocity.Y - 1f;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation += 0.1f * (float)((ModProjectile)this).projectile.direction;
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] >= 50f)
		{
			((ModProjectile)this).projectile.velocity.Y = ((ModProjectile)this).projectile.velocity.Y + 0.13f;
			((ModProjectile)this).projectile.velocity.X = ((ModProjectile)this).projectile.velocity.X * 0.99f;
		}
	}
}
