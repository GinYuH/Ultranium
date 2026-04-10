using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread.Projectiles;

public class DreadScythe : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 11;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Dread Scythe");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.width = 48;
		((ModProjectile)this).Projectile.height = 48;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.penetrate = 2;
		((ModProjectile)this).Projectile.timeLeft = 180;
		((ModProjectile)this).Projectile.light = 0f;
		((ModProjectile)this).Projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D = TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value;
		Vector2 vector = new Vector2((float)texture2D.Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			Rectangle value = new Rectangle(0, texture2D.Height / Main.projFrames[((ModProjectile)this).Projectile.type] * ((ModProjectile)this).Projectile.frame, texture2D.Width, texture2D.Height / Main.projFrames[((ModProjectile)this).Projectile.type]);
			sb.Draw(texture2D, position, value, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation += 0.2f;
		if (((ModProjectile)this).Projectile.localAI[1] < 60f)
		{
			((ModProjectile)this).Projectile.velocity *= 0.95f;
		}
		((ModProjectile)this).Projectile.localAI[1] += 1f;
		if (((ModProjectile)this).Projectile.localAI[1] == 60f)
		{
			double num = Math.Atan2(Main.player[Main.myPlayer].position.Y - ((ModProjectile)this).Projectile.position.Y, Main.player[Main.myPlayer].position.X - ((ModProjectile)this).Projectile.position.X);
			((ModProjectile)this).Projectile.velocity = new Vector2((float)Math.Cos(num), (float)Math.Sin(num)) * 16f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		((Entity)((ModProjectile)this).Projectile).active = false;
		((ModProjectile)this).Projectile.position.X = ((ModProjectile)this).Projectile.position.X + (float)(((ModProjectile)this).Projectile.width / 2);
		((ModProjectile)this).Projectile.position.Y = ((ModProjectile)this).Projectile.position.Y + (float)(((ModProjectile)this).Projectile.height / 2);
		((ModProjectile)this).Projectile.width = 30;
		((ModProjectile)this).Projectile.height = 30;
		((ModProjectile)this).Projectile.position.X = ((ModProjectile)this).Projectile.position.X - (float)(((ModProjectile)this).Projectile.width / 2);
		((ModProjectile)this).Projectile.position.Y = ((ModProjectile)this).Projectile.position.Y - (float)(((ModProjectile)this).Projectile.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y), ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 2f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
