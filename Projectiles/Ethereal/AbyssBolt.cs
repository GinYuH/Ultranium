using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class AbyssBolt : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Abysmal Bolt");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 36;
		((ModProjectile)this).Projectile.height = 36;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.penetrate = 2;
		((ModProjectile)this).Projectile.timeLeft = 200;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		((ModProjectile)this).Projectile.ignoreWater = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 vector = new Vector2((float)TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value.Width * 0.5f, (float)((ModProjectile)this).Projectile.height * 0.5f);
		for (int i = 0; i < ((ModProjectile)this).Projectile.oldPos.Length; i++)
		{
			Vector2 position = ((ModProjectile)this).Projectile.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModProjectile)this).Projectile.gfxOffY);
			Color color = ((ModProjectile)this).Projectile.GetAlpha(lightColor) * ((float)(((ModProjectile)this).Projectile.oldPos.Length - i) / (float)((ModProjectile)this).Projectile.oldPos.Length);
			spriteBatch.Draw(TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value, position, null, color, ((ModProjectile)this).Projectile.rotation, vector, ((ModProjectile)this).Projectile.scale, SpriteEffects.None, 0f);
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 5;
	}

	public override void AI()
	{
		if (Utils.NextBool(Main.rand))
		{
			Dust dust = Dust.NewDustDirect(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 179);
			dust.noGravity = true;
			dust.scale = 1f;
		}
		((ModProjectile)this).Projectile.rotation = (float)Math.Atan2(((ModProjectile)this).Projectile.velocity.Y, ((ModProjectile)this).Projectile.velocity.X) + 0.8f;
		Lighting.AddLight(((ModProjectile)this).Projectile.Center, 0.3f, 0f, 0.5f);
	}
}
