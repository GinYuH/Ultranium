using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ethereal;

public class AbyssSickle : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Abyssal Sickle");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 30;
		((ModProjectile)this).Projectile.height = 30;
		((ModProjectile)this).Projectile.aiStyle = 0;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
		((ModProjectile)this).Projectile.penetrate = 1;
		((ModProjectile)this).Projectile.timeLeft = 360;
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
		((ModProjectile)this).Projectile.rotation += -0.2f;
		((ModProjectile)this).Projectile.velocity = ((ModProjectile)this).Projectile.velocity.RotatedBy(MathHelper.ToRadians(1.5f * ((ModProjectile)this).Projectile.ai[0]));
		((ModProjectile)this).Projectile.velocity *= 1.005f;
	}
}
