using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ocean.Projectiles;

public class InkCloud : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 3;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		//DisplayName.SetDefault("Ink Glob");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 18;
		Projectile.height = 26;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = 10;
		Projectile.timeLeft = 1200;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 11)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
			if (Projectile.frame >= 3)
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

	public override void AI()
	{
		Projectile.velocity *= 0.97f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(BuffID.Slow, 180, quiet: false);
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SpookyWood, 0f, -2f, 0, default(Color), 1.5f);
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
