using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin.Eldritch;

public class EldritchPumpkinSentry : ModProjectile
{
	public int shootTimer = 20;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Eldritch Pumpkin");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 74;
		((ModProjectile)this).Projectile.height = 62;
		((ModProjectile)this).Projectile.timeLeft = 7200;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.minionSlots = 1f;
		((ModProjectile)this).Projectile.sentry = true;
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
		((ModProjectile)this).Projectile.velocity.Y += 2f;
		shootTimer++;
		float num = 450f;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!((Entity)nPC).active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(((ModProjectile)this).Projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(((ModProjectile)this).Projectile.position.X + (float)(((ModProjectile)this).Projectile.width / 2), ((ModProjectile)this).Projectile.position.Y + (float)(((ModProjectile)this).Projectile.height / 2));
			int num3 = ((ModProjectile)this).Mod.Find<ModProjectile>("EldritchSeed").Type;
			float num4 = 9f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			int damage = ((ModProjectile)this).Projectile.damage;
			if (shootTimer >= 25)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, num3, damage, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 0;
			}
		}
	}
}
