using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Mushroom;

public class ShroomBulb : ModProjectile
{
	public int shootTimer = 20;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Fungus Bolt");
		Main.projFrames[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 38;
		Projectile.height = 32;
		Projectile.friendly = false;
		Projectile.hostile = false;
		Projectile.penetrate = -1;
		Projectile.ignoreWater = true;
        Projectile.timeLeft = Projectile.SentryLifeTime;
        Projectile.sentry = true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 7)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 2)
			{
				Projectile.frame = 0;
			}
		}
		Projectile.velocity.Y += 2f;
		shootTimer++;
		float num = 200f;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!nPC.active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(Projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2));
			int num3 = Mod.Find<ModProjectile>("ShroomSpore").Type;
			float num4 = 6f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			int num6 = 12;
			if (shootTimer >= 45)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector.X, vector.Y, vector2.X, vector2.Y, num3, num6, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 0;
			}
		}
	}
}
