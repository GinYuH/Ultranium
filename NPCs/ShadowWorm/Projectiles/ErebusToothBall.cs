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
		// ((ModProjectile)this).DisplayName.SetDefault("Erebus Toothball");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 42;
		((ModProjectile)this).Projectile.height = 44;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.timeLeft = 420;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowWorm/Projectiles/ErebusToothBallTrail").Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowWorm/Projectiles/ErebusToothBallTrail"), position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (((ModProjectile)this).Projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item20, ((ModProjectile)this).Projectile.position);
		}
		if ((((ModProjectile)this).Projectile.ai[0] -= 1f) > 0f)
		{
			float num = ((ModProjectile)this).Projectile.velocity.Length();
			num += ((ModProjectile)this).Projectile.ai[1];
			((ModProjectile)this).Projectile.velocity = Vector2.Normalize(((ModProjectile)this).Projectile.velocity) * num;
		}
		else if (((ModProjectile)this).Projectile.ai[0] == 0f)
		{
			((ModProjectile)this).Projectile.ai[1] = (int)Player.FindClosest(((ModProjectile)this).Projectile.Center, 0, 0);
			if (((ModProjectile)this).Projectile.ai[1] != -1f && ((Entity)Main.player[(int)((ModProjectile)this).Projectile.ai[1]]).active && !Main.player[(int)((ModProjectile)this).Projectile.ai[1]].dead)
			{
				((ModProjectile)this).Projectile.velocity = ((ModProjectile)this).Projectile.DirectionTo(Main.player[(int)((ModProjectile)this).Projectile.ai[1]].Center);
				((ModProjectile)this).Projectile.netUpdate = true;
			}
			else
			{
				((ModProjectile)this).Projectile.Kill();
			}
		}
		else
		{
			((ModProjectile)this).Projectile.tileCollide = true;
			if ((((ModProjectile)this).Projectile.localAI[1] += 1f) < 90f)
			{
				((ModProjectile)this).Projectile.velocity *= 1.04f;
			}
			if (((ModProjectile)this).Projectile.localAI[1] < 120f)
			{
				float curAngle = ((ModProjectile)this).Projectile.velocity.ToRotation();
				float targetAngle = (Main.player[(int)((ModProjectile)this).Projectile.ai[1]].Center - ((ModProjectile)this).Projectile.Center).ToRotation();
				((ModProjectile)this).Projectile.velocity = new Vector2(((ModProjectile)this).Projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
			}
		}
		((ModProjectile)this).Projectile.rotation += 0.2f;
	}

	public override void OnKill(int timeLeft)
	{
		int num = 15;
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).Projectile.width / 5f, ((ModProjectile)this).Projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).Projectile.Center;
			Vector2 vector2 = vector - ((ModProjectile)this).Projectile.Center;
			Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, 89, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
			obj.noGravity = true;
			obj.noLight = false;
			obj.velocity = Vector2.Normalize(vector2) * 3f;
			obj.fadeIn = 1.3f;
		}
	}
}
