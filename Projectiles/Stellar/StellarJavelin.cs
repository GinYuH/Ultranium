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
		// ((ModProjectile)this).DisplayName.SetDefault("Stellar javelin");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 66;
		((ModProjectile)this).Projectile.height = 66;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.penetrate = 2;
		((ModProjectile)this).Projectile.timeLeft = 180;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/Projectiles/Stellar/StellarJavelinTrail").Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(ModContent.GetTexture("Ultranium/Projectiles/Stellar/StellarJavelinTrail"), position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void AI()
	{
		Lighting.AddLight(((ModProjectile)this).Projectile.Center, 0.1f, 0.6f, 0.7f);
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 0.8f;
	}
}
