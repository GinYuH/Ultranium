using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class DemonMinion : ModProjectile
{
	public int shootTimer = 30;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Shade Demon");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 30;
		((ModProjectile)this).Projectile.height = 30;
		((ModProjectile)this).Projectile.netImportant = true;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.timeLeft = 18000;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).Projectile.type] = true;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.minionSlots = 1f;
		((ModProjectile)this).Projectile.aiStyle = 62;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		if (++((ModProjectile)this).Projectile.frameCounter >= 16)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 4)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		_ = ((ModProjectile)this).Projectile.type;
		((ModProjectile)this).Mod.Find<ModProjectile>("DemonMinion").Type;
		Player obj = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer ultraniumPlayer = (UltraniumPlayer)(object)obj.GetModPlayer(((ModProjectile)this).Mod, "UltraniumPlayer");
		obj.AddBuff(((ModProjectile)this).Mod.Find<ModBuff>("DemonBuff").Type, 3600, fromNetPvP: true);
		if (obj.dead)
		{
			ultraniumPlayer.DemonMinion = false;
		}
		if (ultraniumPlayer.DemonMinion)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
		shootTimer--;
		float num = 400f;
		((ModProjectile)this).Projectile.tileCollide = false;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!((Entity)nPC).active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(((ModProjectile)this).Projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(((ModProjectile)this).Projectile.position.X + (float)(((ModProjectile)this).Projectile.width / 2), ((ModProjectile)this).Projectile.position.Y + (float)(((ModProjectile)this).Projectile.height / 2));
			int num3 = ((ModProjectile)this).Mod.Find<ModProjectile>("DemonScythe").Type;
			float num4 = 24f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			int num6 = 50;
			if (shootTimer <= 0)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, num3, num6, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 30;
			}
		}
	}
}
