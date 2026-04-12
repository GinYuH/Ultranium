using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread.Projectiles;

public class DreadScythe : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		DisplayName.SetDefault("Dread Scythe");
	}

	public override void SetDefaults()
	{
		Projectile.tileCollide = false;
		Projectile.width = 48;
		Projectile.height = 48;
		Projectile.aiStyle = 0;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.penetrate = 2;
		Projectile.timeLeft = 180;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
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

	public override void AI()
	{
		Projectile.rotation += 0.2f;
		if (Projectile.localAI[1] < 60f)
		{
			Projectile.velocity *= 0.95f;
		}
		Projectile.localAI[1] += 1f;
		if (Projectile.localAI[1] == 60f)
		{
			double num = Math.Atan2(Main.player[Main.myPlayer].position.Y - Projectile.position.Y, Main.player[Main.myPlayer].position.X - Projectile.position.X);
			Projectile.velocity = new Vector2((float)Math.Cos(num), (float)Math.Sin(num)) * 16f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		((Entity)Projectile).active = false;
		Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
		Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 90, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 2f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
