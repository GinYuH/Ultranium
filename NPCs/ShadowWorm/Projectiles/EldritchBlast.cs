using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class EldritchBlast : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Eldritch Blast");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 30;
		Projectile.height = 74;
		Projectile.penetrate = 1;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.tileCollide = false;
		Projectile.ignoreWater = true;
		Projectile.alpha = 0;
		Projectile.timeLeft = 300;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowWorm/Projectiles/EldritchBlastTrail").Width() * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowWorm/Projectiles/EldritchBlastTrail").Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Projectile.localAI[0] += 1f;
		if (Projectile.localAI[0] == 12f)
		{
			Projectile.localAI[0] = 0f;
			for (int i = 0; i < 12; i++)
			{
				Vector2 spinningpoint = Vector2.UnitX * (0f - (float)Projectile.width) / 2f;
				spinningpoint += -Vector2.UnitY.RotatedBy((float)i * (float)Math.PI / 6f) * new Vector2(8f, 16f);
				spinningpoint = spinningpoint.RotatedBy(Projectile.rotation - (float)Math.PI / 2f);
				int num = Dust.NewDust(Projectile.Center, 0, 0, 89, 0f, 0f, 160);
				Main.dust[num].scale = 1.5f;
				Main.dust[num].noGravity = true;
				Main.dust[num].position = Projectile.Center + spinningpoint;
				Main.dust[num].velocity = Projectile.velocity * 0.1f;
				Main.dust[num].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
			}
		}
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 89, 0f, -2f, 0, default(Color), 1.5f);
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
