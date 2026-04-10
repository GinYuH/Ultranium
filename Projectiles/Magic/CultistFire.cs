using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Magic;

public class CultistFire : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Fireblast");
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 32;
		((ModProjectile)this).projectile.height = 32;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ignoreWater = true;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.timeLeft = 200;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
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

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.AddBuff(24, 180);
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
				int num = Dust.NewDust(((ModProjectile)this).projectile.Center, 0, 0, 6, 0f, 0f, 160);
				Main.dust[num].scale = 1.1f;
				Main.dust[num].noGravity = true;
				Main.dust[num].position = ((ModProjectile)this).projectile.Center + spinningpoint;
				Main.dust[num].velocity = ((ModProjectile)this).projectile.velocity * 0.1f;
				Main.dust[num].velocity = Vector2.Normalize(((ModProjectile)this).projectile.Center - ((ModProjectile)this).projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
			}
		}
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 1.57f;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 14, 1f, 0f);
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, 0f, 0f, ((ModProjectile)this).mod.ProjectileType("CultFireExplosion"), ((ModProjectile)this).projectile.damage, 0f, Main.myPlayer, 0f, 0f);
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 6, 0f, -2f, 0, default(Color), 1.5f);
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
