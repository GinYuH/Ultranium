using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ghost;

public class SoulScytheProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 7;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Soul Sickle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 62;
		((ModProjectile)this).Projectile.height = 62;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.timeLeft = 240;
		((ModProjectile)this).Projectile.light = 0f;
		((ModProjectile)this).Projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
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

	public override void AI()
	{
		((ModProjectile)this).Projectile.rotation += 0.1f * (float)((ModProjectile)this).Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 7;
	}
}
