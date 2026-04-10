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
		((ModProjectile)this).DisplayName.SetDefault("Eldritch Monolith");
		Main.projPet[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 62;
		((ModProjectile)this).projectile.height = 76;
		((ModProjectile)this).projectile.minion = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 0;
		((ModProjectile)this).projectile.timeLeft = 10000;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		((ModProjectile)this).projectile.position.X = player.Center.X - 31f;
		((ModProjectile)this).projectile.position.Y = player.Center.Y - 100f;
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (!((Entity)player).active || !modPlayer.EldritchSummonSet)
		{
			((Entity)((ModProjectile)this).projectile).active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.EldritchSummonSet = false;
		}
		if (modPlayer.EldritchSummonSet)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
		shootTimer++;
		float num = 700f;
		((ModProjectile)this).projectile.tileCollide = false;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!((Entity)nPC).active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(((ModProjectile)this).projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(((ModProjectile)this).projectile.position.X + (float)(((ModProjectile)this).projectile.width / 2), ((ModProjectile)this).projectile.position.Y + (float)(((ModProjectile)this).projectile.height / 2));
			int num3 = ((ModProjectile)this).mod.ProjectileType("NoctisBlast");
			float num4 = 10f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			int num6 = 320;
			if (shootTimer >= 25)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, num3, num6, 0f, Main.myPlayer, 0f, 0f);
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
