using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.ShadowEvent;

public class DarkMatterArrowBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dark Matter Bolt");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 10;
		Projectile.height = 42;
		Projectile.aiStyle = -1;
		Projectile.hostile = false;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.tileCollide = false;
		Projectile.penetrate = 5;
		Projectile.timeLeft = 360;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
		Projectile.alpha = 0;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[Projectile.owner] = 4;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		int num = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemEmerald, Projectile.velocity.X * 0.4f, Projectile.velocity.Y * 0.4f, 0, default(Color), 2f);
		Main.dust[num].velocity *= 0.3f;
		Main.dust[num].scale *= 0.3f;
		Main.dust[num].noGravity = false;
		Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile.rotation += 0f * (float)Projectile.direction;
	}
}
