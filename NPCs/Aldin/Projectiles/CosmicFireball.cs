using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Aldin.Projectiles;

public class CosmicFireball : ModProjectile
{
	private int Timer;

	private Color[] ColorCycle = new Color[2]
	{
		new Color(117, 235, 215),
		new Color(62, 30, 152)
	};

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cosmic Fireball");
		Main.projFrames[Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 36;
		Projectile.height = 36;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.aiStyle = 0;
		Projectile.penetrate = 1;
		Projectile.extraUpdates = 1;
		Projectile.timeLeft = 300;
		Projectile.tileCollide = false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 11)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
			if (Projectile.frame >= 4)
			{
				Projectile.frame = 0;
			}
		}
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[Projectile.type] * Projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[Projectile.type]);
			Main.spriteBatch.Draw(texture2D, position, value, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float amount = (float)(Main.GameUpdateCount % 60) / 60f;
		int num = (int)(Main.GameUpdateCount / 60 % 2);
		return Color.Lerp(ColorCycle[num], ColorCycle[(num + 1) % 2], amount);
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
		Projectile.localAI[1] += 1f;
		if (Projectile.localAI[1] == 1f)
		{
			for (int i = 0; i < 40; i++)
			{
				int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, -2f, 0, default(Color), 1.5f);
				Main.dust[num].noGravity = true;
				Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
				if (Main.dust[num].position != Projectile.Center)
				{
					Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
				}
			}
		}
		if (Projectile.localAI[1] < 75f)
		{
			Projectile.velocity *= 0f;
		}
		if (Projectile.localAI[1] == 75f)
		{
			double num2 = Math.Atan2(Main.player[Main.myPlayer].position.Y - Projectile.position.Y, Main.player[Main.myPlayer].position.X - Projectile.position.X);
			Projectile.velocity = new Vector2((float)Math.Cos(num2), (float)Math.Sin(num2)) * 10f;
		}
		if (Projectile.localAI[1] > 135f && Projectile.localAI[1] < 150f)
		{
			Projectile.velocity *= 0f;
		}
		if (Projectile.localAI[1] == 150f)
		{
			double num3 = Math.Atan2(Main.player[Main.myPlayer].position.Y - Projectile.position.Y, Main.player[Main.myPlayer].position.X - Projectile.position.X);
			Projectile.velocity = new Vector2((float)Math.Cos(num3), (float)Math.Sin(num3)) * 10f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != Projectile.Center)
			{
				Main.dust[num].velocity = Projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
