using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusDustBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Eldritch Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.width = 16;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.hostile = false;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.tileCollide = true;
		((ModProjectile)this).projectile.penetrate = 1;
		((ModProjectile)this).projectile.timeLeft = 600;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = false;
	}

	public override bool PreDraw(SpriteBatch sb, Color lightColor)
	{
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

	public override void AI()
	{
		int num = Dust.NewDust(((ModProjectile)this).projectile.Center, 0, 0, 89, ((ModProjectile)this).projectile.velocity.X * 0.4f, ((ModProjectile)this).projectile.velocity.Y * 0.4f, 0, default(Color), 2f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 1f;
		Main.dust[num].noGravity = true;
		((ModProjectile)this).projectile.localAI[1] += 1f;
		if (((ModProjectile)this).projectile.localAI[1] == 1f)
		{
			double num2 = Math.Atan2(Main.player[Main.myPlayer].position.Y - ((ModProjectile)this).projectile.position.Y, Main.player[Main.myPlayer].position.X - ((ModProjectile)this).projectile.position.X);
			((ModProjectile)this).projectile.velocity = new Vector2((float)Math.Cos(num2), (float)Math.Sin(num2)) * 16f;
		}
	}
}
