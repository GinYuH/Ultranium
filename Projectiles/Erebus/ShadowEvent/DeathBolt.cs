using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DeathBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Death Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.width = 4;
		Projectile.height = 4;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.alpha = 60;
		Projectile.penetrate = 3;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 200;
		Projectile.DamageType = DamageClass.Magic;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 89, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override void AI()
	{
		if (Projectile.ai[0] == 0f)
		{
			int num = 2;
			_ = Projectile.whoAmI;
			for (int i = 0; i < num; i++)
			{
				int num2 = 8;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("DeathBoltSwirl").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, (float)(i * num2), (float)Projectile.whoAmI);
			}
			Projectile.ai[0] = 1f;
		}
		float num3 = Projectile.Center.X;
		float num4 = Projectile.Center.Y;
		float num5 = 400f;
		bool flag = false;
		for (int j = 0; j < 200; j++)
		{
			if (Main.npc[j].CanBeChasedBy(Projectile) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[j].Center, 1, 1))
			{
				float num6 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
				float num7 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
				float num8 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num6) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num7);
				if (num8 < num5)
				{
					num5 = num8;
					num3 = num6;
					num4 = num7;
					flag = true;
				}
			}
		}
		if (flag)
		{
			Vector2 vector = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num9 = num3 - vector.X;
			float num10 = num4 - vector.Y;
			float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
			num11 = 12f / num11;
			num9 *= num11;
			num10 *= num11;
			Projectile.velocity.X = (Projectile.velocity.X * 20f + num9) / 21f;
			Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num10) / 21f;
		}
	}
}
