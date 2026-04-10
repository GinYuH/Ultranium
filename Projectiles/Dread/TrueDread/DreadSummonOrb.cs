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
		// ((ModProjectile)this).DisplayName.SetDefault("Dread Energy Orb");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 50;
		((ModProjectile)this).Projectile.height = 50;
		((ModProjectile)this).Projectile.timeLeft = 2700;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.minionSlots = 1f;
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
		if (++((ModProjectile)this).Projectile.frameCounter >= 7)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 4)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
		((ModProjectile)this).Projectile.velocity *= 0f;
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
			int num3 = ((ModProjectile)this).Mod.Find<ModProjectile>("DreadFlameBlast").Type;
			float num4 = 8f;
			float num5 = (float)Math.Atan2(vector.Y - (nPC.position.Y + (float)nPC.height * 0.5f), vector.X - (nPC.position.X + (float)nPC.width * 0.5f));
			if (shootTimer >= 25)
			{
				for (int j = 0; j < num2; j++)
				{
					Vector2 vector2 = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0)).RotatedByRandom(MathHelper.ToRadians(20f));
					Projectile.NewProjectile(null, vector.X, vector.Y, vector2.X, vector2.Y, num3, ((ModProjectile)this).Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				}
				shootTimer = 0;
			}
		}
		Timer++;
		if (Timer >= 1680)
		{
			((ModProjectile)this).Projectile.scale += 0.01f;
		}
		if (Timer >= 1740)
		{
			int num6 = 25;
			for (int k = 0; k < num6; k++)
			{
				Vector2 vector3 = (Vector2.One * new Vector2((float)((ModProjectile)this).Projectile.width / 7f, (float)((ModProjectile)this).Projectile.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num6 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num6) + ((ModProjectile)this).Projectile.Center;
				Vector2 vector4 = vector3 - ((ModProjectile)this).Projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector3 + vector4, 0, 0, 90, vector4.X * 2f, vector4.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector4) * 3f;
				obj.fadeIn = 1.3f;
			}
		}
		if (Timer == 1800)
		{
			SoundEngine.PlaySound(SoundID.Item14, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
			for (int l = 0; l < 100; l++)
			{
				int num7 = Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
				Main.dust[num7].noGravity = false;
				Main.dust[num7].scale = 3.5f;
				Main.dust[num7].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				Main.dust[num7].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				if (Main.dust[num7].position != ((ModProjectile)this).Projectile.Center)
				{
					Main.dust[num7].velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.dust[num7].position) * 10f;
				}
			}
			for (int m = 0; m < 20; m++)
			{
				Vector2 vector5 = ((float)Math.PI / 10f * (float)m).ToRotationVector2();
				vector5.Normalize();
				vector5 *= 6f;
				Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector5.X, vector5.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("DreadFlameBlast").Type, ((ModProjectile)this).Projectile.damage * 2, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (Timer > 1800)
		{
			((ModProjectile)this).Projectile.Kill();
		}
	}
}
