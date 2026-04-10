using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread.Projectiles;

public class DreadFireBlast : ModProjectile
{
	private Color[] ColorCycle = new Color[2]
	{
		new Color(200, 0, 0),
		new Color(124, 7, 31)
	};

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Dread Blast");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 74;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.friendly = false;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.alpha = 0;
		((ModProjectile)this).projectile.timeLeft = 300;
	}

	public override bool PreDraw(SpriteBatch sb, Color lightColor)
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter >= 11)
		{
			((ModProjectile)this).projectile.frame++;
			((ModProjectile)this).projectile.frameCounter = 0;
			if (((ModProjectile)this).projectile.frame >= 4)
			{
				((ModProjectile)this).projectile.frame = 0;
			}
		}
		Texture2D texture2D = Main.projectileTexture[((ModProjectile)this).projectile.type];
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[((ModProjectile)this).projectile.type] * ((ModProjectile)this).projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[((ModProjectile)this).projectile.type]);
			sb.Draw(texture2D, position, value, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
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
		((ModProjectile)this).projectile.localAI[0] += 1f;
		if (((ModProjectile)this).projectile.localAI[0] == 12f)
		{
			((ModProjectile)this).projectile.localAI[0] = 0f;
			for (int i = 0; i < 12; i++)
			{
				Vector2 spinningpoint = Vector2.UnitX * (0f - (float)((ModProjectile)this).projectile.width) / 2f;
				spinningpoint += -Vector2.UnitY.RotatedBy((float)i * (float)Math.PI / 6f) * new Vector2(8f, 16f);
				spinningpoint = spinningpoint.RotatedBy(((ModProjectile)this).projectile.rotation - (float)Math.PI / 2f);
				int num = Dust.NewDust(((ModProjectile)this).projectile.Center, 0, 0, 90, 0f, 0f, 160);
				Main.dust[num].scale = 1.5f;
				Main.dust[num].noGravity = true;
				Main.dust[num].position = ((ModProjectile)this).projectile.Center + spinningpoint;
				Main.dust[num].velocity = ((ModProjectile)this).projectile.velocity * 0.1f;
				Main.dust[num].velocity = Vector2.Normalize(((ModProjectile)this).projectile.Center - ((ModProjectile)this).projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
			}
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
	}
}
