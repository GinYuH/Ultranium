using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ocean;

public class BabySquid : ModProjectile
{
	public int shootTimer = 50;

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Baby Squid");
		Main.projFrames[Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.netImportant = true;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 18000;
		Projectile.penetrate = -1;
		Projectile.minion = true;
		Projectile.minionSlots = 1f;
		Projectile.aiStyle = 62;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 16)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
		_ = Projectile.type;
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer ultraniumPlayer = obj.GetModPlayer<UltraniumPlayer>();
        obj.AddBuff(Mod.Find<ModBuff>("BabySquidBuff").Type, 3600, quiet: false);
		if (obj.dead)
		{
			ultraniumPlayer.BabySquid = false;
		}
		if (ultraniumPlayer.BabySquid)
		{
			Projectile.timeLeft = 2;
		}
		shootTimer--;
		float num = 400f;
		Projectile.tileCollide = false;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!((Entity)nPC).active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(Projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2));
			int num3 = Mod.Find<ModProjectile>("BabySquidInk").Type;
			float num4 = 4f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			int damage = Projectile.damage;
			if (shootTimer <= 0)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(null, vector.X, vector.Y, vector2.X, vector2.Y, num3, damage, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 50;
			}
		}
	}
}
