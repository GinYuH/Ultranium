using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class AbyssalEye : ModProjectile
{
	public int shootTimer = 20;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Eldritch Monolith");
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 62;
		((ModProjectile)this).Projectile.height = 76;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.alpha = 0;
		((ModProjectile)this).Projectile.timeLeft = 10000;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		((ModProjectile)this).Projectile.position.X = player.Center.X - 31f;
		((ModProjectile)this).Projectile.position.Y = player.Center.Y - 100f;
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (!((Entity)player).active || !modPlayer.EldritchSummonSet)
		{
			((Entity)((ModProjectile)this).Projectile).active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.EldritchSummonSet = false;
		}
		if (modPlayer.EldritchSummonSet)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
		shootTimer++;
		float num = 700f;
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
			int num3 = ((ModProjectile)this).Mod.Find<ModProjectile>("NoctisBlast").Type;
			float num4 = 10f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			int num6 = 320;
			if (shootTimer >= 25)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(null, vector.X, vector.Y, vector2.X, vector2.Y, num3, num6, 0f, Main.myPlayer, 0f, 0f);
				}
				if (modPlayer.EldritchSummonBuff)
				{
					shootTimer = 20;
				}
				else
				{
					shootTimer = 0;
				}
			}
		}
	}
}
