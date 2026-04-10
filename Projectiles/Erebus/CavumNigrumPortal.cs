using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class CavumNigrumPortal : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Black Hole");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 48;
		((ModProjectile)this).projectile.height = 38;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.timeLeft = 240;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation += 0.15f * (float)((ModProjectile)this).projectile.direction;
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] > 20f)
		{
			((ModProjectile)this).projectile.ai[0] = 20f;
			int num = HomeOnTarget();
			if (num != -1)
			{
				NPC nPC = Main.npc[num];
				Vector2 value = ((ModProjectile)this).projectile.DirectionTo(nPC.Center) * 12f;
				((ModProjectile)this).projectile.velocity = Vector2.Lerp(((ModProjectile)this).projectile.velocity, value, 1f / 12f);
			}
		}
	}

	private int HomeOnTarget()
	{
		int num = -1;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (nPC.CanBeChasedBy(((ModProjectile)this).projectile))
			{
				_ = nPC.wet;
				float num2 = ((ModProjectile)this).projectile.Distance(nPC.Center);
				if (num2 <= 400f && (num == -1 || ((ModProjectile)this).projectile.Distance(Main.npc[num].Center) > num2))
				{
					num = i;
				}
			}
		}
		return num;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 89, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
