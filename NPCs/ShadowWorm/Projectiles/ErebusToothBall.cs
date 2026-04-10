using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusToothBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Erebus Toothball");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 42;
		((ModProjectile)this).projectile.height = 44;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.timeLeft = 420;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowWorm/Projectiles/ErebusToothBallTrail").Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowWorm/Projectiles/ErebusToothBallTrail"), position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		if (((ModProjectile)this).projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).projectile.localAI[0] = 1f;
			Main.PlaySound(SoundID.Item20, ((ModProjectile)this).projectile.position);
		}
		if ((((ModProjectile)this).projectile.ai[0] -= 1f) > 0f)
		{
			float num = ((ModProjectile)this).projectile.velocity.Length();
			num += ((ModProjectile)this).projectile.ai[1];
			((ModProjectile)this).projectile.velocity = Vector2.Normalize(((ModProjectile)this).projectile.velocity) * num;
		}
		else if (((ModProjectile)this).projectile.ai[0] == 0f)
		{
			((ModProjectile)this).projectile.ai[1] = (int)Player.FindClosest(((ModProjectile)this).projectile.Center, 0, 0);
			if (((ModProjectile)this).projectile.ai[1] != -1f && ((Entity)Main.player[(int)((ModProjectile)this).projectile.ai[1]]).active && !Main.player[(int)((ModProjectile)this).projectile.ai[1]].dead)
			{
				((ModProjectile)this).projectile.velocity = ((ModProjectile)this).projectile.DirectionTo(Main.player[(int)((ModProjectile)this).projectile.ai[1]].Center);
				((ModProjectile)this).projectile.netUpdate = true;
			}
			else
			{
				((ModProjectile)this).projectile.Kill();
			}
		}
		else
		{
			((ModProjectile)this).projectile.tileCollide = true;
			if ((((ModProjectile)this).projectile.localAI[1] += 1f) < 90f)
			{
				((ModProjectile)this).projectile.velocity *= 1.04f;
			}
			if (((ModProjectile)this).projectile.localAI[1] < 120f)
			{
				float curAngle = ((ModProjectile)this).projectile.velocity.ToRotation();
				float targetAngle = (Main.player[(int)((ModProjectile)this).projectile.ai[1]].Center - ((ModProjectile)this).projectile.Center).ToRotation();
				((ModProjectile)this).projectile.velocity = new Vector2(((ModProjectile)this).projectile.velocity.Length(), 0f).RotatedBy(curAngle.AngleLerp(targetAngle, 0.025f));
			}
		}
		((ModProjectile)this).projectile.rotation += 0.2f;
	}

	public override void Kill(int timeLeft)
	{
		int num = 15;
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = (Vector2.One * new Vector2((float)((ModProjectile)this).projectile.width / 5f, ((ModProjectile)this).projectile.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModProjectile)this).projectile.Center;
			Vector2 vector2 = vector - ((ModProjectile)this).projectile.Center;
			Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, 89, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
			obj.noGravity = true;
			obj.noLight = false;
			obj.velocity = Vector2.Normalize(vector2) * 3f;
			obj.fadeIn = 1.3f;
		}
	}
}
