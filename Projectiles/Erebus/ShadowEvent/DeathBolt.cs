using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DeathBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Death Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 4;
		((ModProjectile)this).projectile.height = 4;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 60;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.timeLeft = 200;
		((ModProjectile)this).projectile.magic = true;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 89, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}

	public override void AI()
	{
		if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			int num = 2;
			_ = ((ModProjectile)this).projectile.whoAmI;
			for (int i = 0; i < num; i++)
			{
				int num2 = 8;
				Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, 0f, 0f, ((ModProjectile)this).mod.ProjectileType("DeathBoltSwirl"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, (float)(i * num2), (float)((ModProjectile)this).projectile.whoAmI);
			}
			((ModProjectile)this).projectile.ai[0] = 1f;
		}
		float num3 = ((ModProjectile)this).projectile.Center.X;
		float num4 = ((ModProjectile)this).projectile.Center.Y;
		float num5 = 400f;
		bool flag = false;
		for (int j = 0; j < 200; j++)
		{
			if (Main.npc[j].CanBeChasedBy(((ModProjectile)this).projectile) && Collision.CanHit(((ModProjectile)this).projectile.Center, 1, 1, Main.npc[j].Center, 1, 1))
			{
				float num6 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
				float num7 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
				float num8 = Math.Abs(((ModProjectile)this).projectile.position.X + (float)(((ModProjectile)this).projectile.width / 2) - num6) + Math.Abs(((ModProjectile)this).projectile.position.Y + (float)(((ModProjectile)this).projectile.height / 2) - num7);
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
			Vector2 vector = new Vector2(((ModProjectile)this).projectile.position.X + (float)((ModProjectile)this).projectile.width * 0.5f, ((ModProjectile)this).projectile.position.Y + (float)((ModProjectile)this).projectile.height * 0.5f);
			float num9 = num3 - vector.X;
			float num10 = num4 - vector.Y;
			float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
			num11 = 12f / num11;
			num9 *= num11;
			num10 *= num11;
			((ModProjectile)this).projectile.velocity.X = (((ModProjectile)this).projectile.velocity.X * 20f + num9) / 21f;
			((ModProjectile)this).projectile.velocity.Y = (((ModProjectile)this).projectile.velocity.Y * 20f + num10) / 21f;
		}
	}
}
