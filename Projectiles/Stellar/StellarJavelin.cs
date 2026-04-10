using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Stellar;

public class StellarJavelin : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Stellar javelin");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.width = 66;
		Projectile.height = 66;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.tileCollide = false;
		Projectile.penetrate = 2;
		Projectile.timeLeft = 180;
		Projectile.extraUpdates = 1;
		Projectile.ignoreWater = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/Projectiles/Stellar/StellarJavelinTrail").Width() * 0.5f, (float)Projectile.height * 0.5f);
		for (int i = 0; i < Projectile.oldPos.Length; i++)
		{
			Vector2 position = Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, Projectile.gfxOffY);
			Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/Projectiles/Stellar/StellarJavelinTrail").Value, position, null, color, Projectile.rotation, vector, Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Lighting.AddLight(Projectile.Center, 0.1f, 0.6f, 0.7f);
		Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 0.8f;
	}
}
