using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class ShadeArrow : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Shade Arrow");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 14;
		((ModProjectile)this).projectile.height = 38;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = 2;
		((ModProjectile)this).projectile.timeLeft = 250;
		((ModProjectile)this).projectile.extraUpdates = 1;
		((ModProjectile)this).projectile.ignoreWater = false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 3;
	}

	public override void AI()
	{
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("ShadowDustPurple"));
			dust.noGravity = true;
			dust.scale = 1.2f;
		}
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		((ModProjectile)this).projectile.rotation += 0f * (float)((ModProjectile)this).projectile.direction;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 vector = new Vector2((float)Main.projectileTexture[((ModProjectile)this).projectile.type].Width * 0.5f, (float)((ModProjectile)this).projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).projectile.gfxOffY);
			Color color = ((ModProjectile)this).projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).projectile.oldPos.Length - i) / (float)((ModProjectile)this).projectile.oldPos.Length);
			spriteBatch.Draw(Main.projectileTexture[((ModProjectile)this).projectile.type], position, null, color, ((ModProjectile)this).projectile.rotation, vector, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}
}
