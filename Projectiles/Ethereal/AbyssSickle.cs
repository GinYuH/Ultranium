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
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		DisplayName.SetDefault("Abyssal Sickle");
	}

	public override void SetDefaults()
	{
		Projectile.width = 30;
		Projectile.height = 30;
		Projectile.aiStyle = 0;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.penetrate = 1;
		Projectile.timeLeft = 360;
		Projectile.light = 0f;
		Projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
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

	public override void AI()
	{
		Projectile.rotation += -0.2f;
		Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(1.5f * Projectile.ai[0]));
		Projectile.velocity *= 1.005f;
	}
}
