using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Magic;

public class CultistFire : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Fireblast");
		Main.projFrames[Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 32;
		Projectile.height = 32;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.extraUpdates = 1;
		Projectile.tileCollide = true;
		Projectile.timeLeft = 200;
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

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 180);
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
				int num = Dust.NewDust(Projectile.Center, 0, 0, 6, 0f, 0f, 160);
				Main.dust[num].scale = 1.1f;
				Main.dust[num].noGravity = true;
				Main.dust[num].position = Projectile.Center + spinningpoint;
				Main.dust[num].velocity = Projectile.velocity * 0.1f;
				Main.dust[num].velocity = Vector2.Normalize(Projectile.Center - Projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
			}
		}
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
	}

	public override void OnKill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			SoundEngine.PlaySound(SoundID.Item14, new Vector2(Projectile.position.X, Projectile.position.Y));
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("CultFireExplosion").Type, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
			int num = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, -2f, 0, default(Color), 1.5f);
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
