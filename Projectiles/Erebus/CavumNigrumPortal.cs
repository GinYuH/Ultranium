using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class CavumNigrumPortal : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Black Hole");
	}

	public override void SetDefaults()
	{
		Projectile.width = 48;
		Projectile.height = 38;
		Projectile.penetrate = -1;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.timeLeft = 240;
	}

	public override void AI()
	{
		Projectile.rotation += 0.15f * (float)Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] > 20f)
		{
			Projectile.ai[0] = 20f;
			int num = HomeOnTarget();
			if (num != -1)
			{
				NPC nPC = Main.npc[num];
				Vector2 value = Projectile.DirectionTo(nPC.Center) * 12f;
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, value, 1f / 12f);
			}
		}
	}

	private int HomeOnTarget()
	{
		int num = -1;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (nPC.CanBeChasedBy(Projectile))
			{
				_ = nPC.wet;
				float num2 = Projectile.Distance(nPC.Center);
				if (num2 <= 400f && (num == -1 || Projectile.Distance(Main.npc[num].Center) > num2))
				{
					num = i;
				}
			}
		}
		return num;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 89, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
