using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin;

public class Pumpkibomb : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Pumpkibomb");
	}

	public override void SetDefaults()
	{
		Projectile.width = 30;
		Projectile.height = 36;
		Projectile.aiStyle = ProjAIStyleID.Sickle;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = 1;
		Projectile.tileCollide = true;
		Projectile.timeLeft = 2000;
		Projectile.extraUpdates = 1;
		Projectile.aiStyle = 0;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 10;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num].velocity *= 1.4f;
		}
		for (int j = 0; j < 10; j++)
		{
			int num2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[num2].noGravity = true;
			Main.dust[num2].velocity *= 5f;
			num2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num2].velocity *= 3f;
		}
		int num3 = Gore.NewGore(null, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num3].velocity *= 0.4f;
		Gore gore = Main.gore[num3];
		gore.velocity.X = gore.velocity.X + 1f;
		Gore gore2 = Main.gore[num3];
		gore2.velocity.Y = gore2.velocity.Y + 1f;
		num3 = Gore.NewGore(null, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num3].velocity *= 0.4f;
		Gore gore3 = Main.gore[num3];
		gore3.velocity.X = gore3.velocity.X - 1f;
		Gore gore4 = Main.gore[num3];
		gore4.velocity.Y = gore4.velocity.Y + 1f;
		num3 = Gore.NewGore(null, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num3].velocity *= 0.4f;
		Gore gore5 = Main.gore[num3];
		gore5.velocity.X = gore5.velocity.X + 1f;
		Gore gore6 = Main.gore[num3];
		gore6.velocity.Y = gore6.velocity.Y - 1f;
		num3 = Gore.NewGore(null, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num3].velocity *= 0.4f;
		Gore gore7 = Main.gore[num3];
		gore7.velocity.X = gore7.velocity.X - 1f;
		Gore gore8 = Main.gore[num3];
		gore8.velocity.Y = gore8.velocity.Y - 1f;
	}

	public override void AI()
	{
		Projectile.rotation += 0.1f * (float)Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 50f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.13f;
			Projectile.velocity.X = Projectile.velocity.X * 0.99f;
		}
	}
}
