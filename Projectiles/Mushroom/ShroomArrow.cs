using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Mushroom;

public class ShroomArrow : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		//DisplayName.SetDefault("Fungus Arrow");
	}

	public override void SetDefaults()
	{
		Projectile.scale = 1f;
		Projectile.width = 14;
		Projectile.height = 32;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.tileCollide = true;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 600;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
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

	public override void AI()
	{
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= 40f)
		{
			Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
		}
		int num = Dust.NewDust(Projectile.Center, 0, 0, DustID.GlowingMushroom, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 0, default(Color), 2f);
		Main.dust[num].velocity *= 0.3f;
		Main.dust[num].scale *= 0.3f;
		Main.dust[num].noGravity = false;
	}
}
