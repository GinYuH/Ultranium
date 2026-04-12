using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusDustBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eldritch Bolt");
	}

	public override void SetDefaults()
	{
		Projectile.alpha = 255;
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.tileCollide = true;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 600;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = false;
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
		int num = Dust.NewDust(Projectile.Center, 0, 0, 89, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 0, default(Color), 2f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 1f;
		Main.dust[num].noGravity = true;
		Projectile.localAI[1] += 1f;
		if (Projectile.localAI[1] == 1f)
		{
			double num2 = Math.Atan2(Main.player[Main.myPlayer].position.Y - Projectile.position.Y, Main.player[Main.myPlayer].position.X - Projectile.position.X);
			Projectile.velocity = new Vector2((float)Math.Cos(num2), (float)Math.Sin(num2)) * 16f;
		}
	}
}
