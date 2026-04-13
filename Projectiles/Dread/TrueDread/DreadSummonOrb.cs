using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadSummonOrb : ModProjectile
{
	private int Timer;

	public int shootTimer = 20;

	private Color[] ColorCycle = new Color[2]
	{
		new Color(200, 0, 0),
		new Color(124, 7, 31)
	};

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread Energy Orb");
		Main.projFrames[Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		Projectile.width = 50;
		Projectile.height = 50;
		Projectile.timeLeft = 2700;
		Projectile.friendly = false;
		Projectile.hostile = false;
		Projectile.penetrate = -1;
		Projectile.ignoreWater = true;
		Projectile.minion = true;
		Projectile.minionSlots = 1f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
	}

	public override void AI()
	{
		if (++Projectile.frameCounter >= 7)
		{
			Projectile.frameCounter = 0;
			if (++Projectile.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
		Projectile.velocity *= 0f;
		shootTimer++;
		float num = 700f;
		Projectile.tileCollide = false;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (!nPC.active || nPC.friendly || nPC.damage <= 0 || nPC.dontTakeDamage || !(Vector2.Distance(Projectile.Center, nPC.Center) <= num))
			{
				continue;
			}
			int num2 = 1;
			Vector2 vector = new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2));
			int num3 = Mod.Find<ModProjectile>("DreadFlameBlast").Type;
			float num4 = 8f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			if (shootTimer >= 25)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector.X, vector.Y, vector2.X, vector2.Y, num3, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 0;
			}
		}
		Timer++;
		if (Timer >= 1680)
		{
			Projectile.scale += 0.01f;
		}
		if (Timer >= 1740)
		{
			int num6 = 25;
			for (int k = 0; k < num6; k++)
			{
				Vector2 vector3 = (Vector2.One * new Vector2((float)Projectile.width / 7f, (float)Projectile.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num6 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num6) + Projectile.Center;
				Vector2 vector4 = vector3 - Projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector3 + vector4, 0, 0, DustID.GemRuby, vector4.X * 2f, vector4.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector4) * 3f;
				obj.fadeIn = 1.3f;
			}
		}
		if (Timer == 1800)
		{
			SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
			for (int l = 0; l < 100; l++)
			{
				int num7 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 1.5f);
				Main.dust[num7].noGravity = false;
				Main.dust[num7].scale = 3.5f;
				Main.dust[num7].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				Main.dust[num7].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				if (Main.dust[num7].position != Projectile.Center)
				{
					Main.dust[num7].velocity = Projectile.DirectionTo(Main.dust[num7].position) * 10f;
				}
			}
			for (int m = 0; m < 20; m++)
			{
				Vector2 vector5 = ((float)Math.PI / 10f * (float)m).ToRotationVector2();
				vector5.Normalize();
				vector5 *= 6f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector5.X, vector5.Y, Mod.Find<ModProjectile>("DreadFlameBlast").Type, Projectile.damage * 2, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (Timer > 1800)
		{
			Projectile.Kill();
		}
	}
}
