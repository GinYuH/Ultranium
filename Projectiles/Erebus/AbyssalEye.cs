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
		//DisplayName.SetDefault("Eldritch Monolith");
		Main.projPet[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.width = 62;
		Projectile.height = 76;
		Projectile.minion = true;
		Projectile.penetrate = -1;
		Projectile.hostile = false;
		Projectile.friendly = false;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.alpha = 0;
		Projectile.timeLeft = 10000;
	}

	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		Projectile.position.X = player.Center.X - 31f;
		Projectile.position.Y = player.Center.Y - 100f;
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (!((Entity)player).active || !modPlayer.EldritchSummonSet)
		{
			((Entity)Projectile).active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.EldritchSummonSet = false;
		}
		if (modPlayer.EldritchSummonSet)
		{
			Projectile.timeLeft = 2;
		}
		shootTimer++;
		float num = 700f;
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
			int num3 = Mod.Find<ModProjectile>("NoctisBlast").Type;
			float num4 = 10f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			int num6 = 320;
			if (shootTimer >= 25)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector.X, vector.Y, vector2.X, vector2.Y, num3, num6, 0f, Main.myPlayer, 0f, 0f);
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
