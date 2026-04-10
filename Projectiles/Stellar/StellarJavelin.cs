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
		((ModProjectile)this).DisplayName.SetDefault("Stellar javelin");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 66;
		((ModProjectile)this).projectile.height = 66;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = 2;
		((ModProjectile)this).projectile.timeLeft = 180;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = true;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/Projectiles/Stellar/StellarJavelinTrail").Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Stellar/StellarJavelinTrail"), position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Lighting.AddLight(((ModProjectile)this).projectile.Center, 0.1f, 0.6f, 0.7f);
		((ModProjectile)this).projectile.rotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y, ((ModProjectile)this).projectile.velocity.X) + 0.8f;
	}
}
