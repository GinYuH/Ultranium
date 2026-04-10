using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ultrum.Projectiles;

public class UltrumEnergyBolt : ModProjectile
{
	private int target;

	private int timer;

	public override void SetStaticDefaults()
	{
		Main.projFrames[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Nature Energy Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 18;
		((ModProjectile)this).projectile.height = 26;
		((ModProjectile)this).projectile.hostile = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = 10;
		((ModProjectile)this).projectile.timeLeft = 660;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = true;
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
			if (((ModProjectile)this).projectile.frame >= 5)
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

	public override bool PreAI()
	{
		timer++;
		if (timer >= 90)
		{
			((ModProjectile)this).projectile.velocity *= 0.99f;
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + 1.57f;
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(target);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		target = reader.Read();
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"), 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModProjectile)this).projectile.Center)
			{
				Main.dust[num].velocity = ((ModProjectile)this).projectile.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		for (int j = 0; j < 5; j++)
		{
			Vector2 vector = ((float)Math.PI * 2f / 5f * (float)j).ToRotationVector2();
			vector.Normalize();
			vector *= 6f;
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).mod.ProjectileType("UltrumBolt"), 30, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
