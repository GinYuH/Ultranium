using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadFlameBlast : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Dread Flame Bolt");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		Main.projFrames[Projectile.type] = 6;
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1.5f;
		Projectile.width = 24;
		Projectile.height = 26;
		Projectile.friendly = true;
		Projectile.tileCollide = true;
		Projectile.penetrate = 2;
		Projectile.timeLeft = 180;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Projectile.frameCounter++;
		if (Projectile.frameCounter >= 11)
		{
			Projectile.frame++;
			Projectile.frameCounter = 0;
			if (Projectile.frame >= 6)
			{
				Projectile.frame = 0;
			}
		}
		Texture2D texture = ModContent.Request<Texture2D>("Ultranium/Projectiles/Dread/TrueDread/DreadFlameBlastTrail").Value;
		Vector2 vector = new Vector2((float)texture.Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture.Height / Main.projFrames[Projectile.type] * Projectile.frame, texture.Width, texture.Height / Main.projFrames[Projectile.type]);
			Main.spriteBatch.Draw(texture, position, value, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 7;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
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
