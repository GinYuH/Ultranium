using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin.Eldritch;

public class EldritchPumpkibomb : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Pumpkibomb");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 50;
		((ModProjectile)this).Projectile.height = 56;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 85;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.aiStyle = 0;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation += 0.1f * (float)((ModProjectile)this).Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		((ModProjectile)this).Projectile.Kill();
		Vector2 spinningpoint = new Vector2(3f, 0f).RotatedByRandom(Math.PI * 2.0);
		for (int i = 0; i < 5; i++)
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
			Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center, vector, ((ModProjectile)this).Mod.Find<ModProjectile>("EldritchPumpkinTentacle").Type, ((ModProjectile)this).Projectile.damage, 0f, Main.myPlayer, num2, num);
		}
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
		int num = 2;
		int num2 = Main.rand.Next(0, 180);
		for (int i = 0; i < num; i++)
		{
			float num3 = MathHelper.ToRadians(270 / num * i + num2);
			Vector2 vector = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(num3);
			vector.Normalize();
			vector.X *= 3f;
			vector.Y *= 3f;
			Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, 402, ((ModProjectile)this).Projectile.damage, 2f, ((ModProjectile)this).Projectile.owner, 0f, 0f);
		}
		for (int j = 0; j < 20; j++)
		{
			int num4 = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num4].velocity *= 1.4f;
		}
		for (int k = 0; k < 10; k++)
		{
			int num5 = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[num5].noGravity = true;
			Main.dust[num5].velocity *= 5f;
			num5 = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num5].velocity *= 3f;
		}
		int num6 = Gore.NewGore(null, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num6].velocity *= 0.4f;
		Gore gore = Main.gore[num6];
		gore.velocity.X = gore.velocity.X + 1f;
		Gore gore2 = Main.gore[num6];
		gore2.velocity.Y = gore2.velocity.Y + 1f;
		num6 = Gore.NewGore(null, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num6].velocity *= 0.4f;
		Gore gore3 = Main.gore[num6];
		gore3.velocity.X = gore3.velocity.X - 1f;
		Gore gore4 = Main.gore[num6];
		gore4.velocity.Y = gore4.velocity.Y + 1f;
		num6 = Gore.NewGore(null, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num6].velocity *= 0.4f;
		Gore gore5 = Main.gore[num6];
		gore5.velocity.X = gore5.velocity.X + 1f;
		Gore gore6 = Main.gore[num6];
		gore6.velocity.Y = gore6.velocity.Y - 1f;
		num6 = Gore.NewGore(null, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64));
		Main.gore[num6].velocity *= 0.4f;
		Gore gore7 = Main.gore[num6];
		gore7.velocity.X = gore7.velocity.X - 1f;
		Gore gore8 = Main.gore[num6];
		gore8.velocity.Y = gore8.velocity.Y - 1f;
	}
}
