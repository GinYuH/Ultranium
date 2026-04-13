using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusToothBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Erebus Toothball");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 42;
		Projectile.height = 44;
		Projectile.hostile = true;
		Projectile.tileCollide = false;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 420;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowWorm/Projectiles/ErebusToothBallTrail").Width() * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowWorm/Projectiles/ErebusToothBallTrail").Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
		}
		if ((Projectile.ai[0] -= 1f) > 0f)
		{
			float num = Projectile.velocity.Length();
			num += Projectile.ai[1];
			Projectile.velocity = Vector2.Normalize(Projectile.velocity) * num;
		}
		else if (Projectile.ai[0] == 0f)
		{
			Projectile.ai[1] = (int)Player.FindClosest(Projectile.Center, 0, 0);
			if (Projectile.ai[1] != -1f && ((Entity)Main.player[(int)Projectile.ai[1]]).active && !Main.player[(int)Projectile.ai[1]].dead)
			{
				Projectile.velocity = Projectile.DirectionTo(Main.player[(int)Projectile.ai[1]].Center);
				Projectile.netUpdate = true;
			}
			else
			{
				Projectile.Kill();
			}
		}
		else
		{
			Projectile.tileCollide = true;
			if ((Projectile.localAI[1] += 1f) < 90f)
			{
				Projectile.velocity *= 1.04f;
			}
			if (Projectile.localAI[1] < 120f)
			{
				float curAngle = Projectile.velocity.ToRotation();
				float targetAngle = (Main.player[(int)Projectile.ai[1]].Center - Projectile.Center).ToRotation();
				Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
			}
		}
		Projectile.rotation += 0.2f;
	}

	public override void OnKill(int timeLeft)
	{
		int num = 15;
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = (Vector2.One * new Vector2((float)Projectile.width / 5f, Projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + Projectile.Center;
			Vector2 vector2 = vector - Projectile.Center;
			Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, DustID.GemEmerald, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
			obj.noGravity = true;
			obj.noLight = false;
			obj.velocity = Vector2.Normalize(vector2) * 3f;
			obj.fadeIn = 1.3f;
		}
	}
}
