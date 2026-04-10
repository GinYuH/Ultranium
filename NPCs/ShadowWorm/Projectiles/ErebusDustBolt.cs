using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusDustBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Eldritch Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.width = 16;
		((ModProjectile)this).Projectile.height = 16;
		((ModProjectile)this).Projectile.hostile = false;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.timeLeft = 600;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = false;
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
		int num = Dust.NewDust(((ModProjectile)this).Projectile.Center, 0, 0, 89, ((ModProjectile)this).Projectile.velocity.X * 0.4f, ((ModProjectile)this).Projectile.velocity.Y * 0.4f, 0, default(Color), 2f);
		Main.dust[num].velocity *= 0.5f;
		Main.dust[num].scale *= 1f;
		Main.dust[num].noGravity = true;
		((ModProjectile)this).Projectile.localAI[1] += 1f;
		if (((ModProjectile)this).Projectile.localAI[1] == 1f)
		{
			double num2 = Math.Atan2(Main.player[Main.myPlayer].position.Y - ((ModProjectile)this).Projectile.position.Y, Main.player[Main.myPlayer].position.X - ((ModProjectile)this).Projectile.position.X);
			((ModProjectile)this).Projectile.velocity = new Vector2((float)Math.Cos(num2), (float)Math.Sin(num2)) * 16f;
		}
	}
}
