using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Magic;

public class IceBall : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 3;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Ice Mist");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 92;
		((ModProjectile)this).projectile.height = 102;
		((ModProjectile)this).projectile.aiStyle = 0;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.magic = true;
		((ModProjectile)this).projectile.penetrate = 2;
		((ModProjectile)this).projectile.timeLeft = 80;
		((ModProjectile)this).projectile.light = 0f;
		((ModProjectile)this).projectile.alpha = 150;
		((ModProjectile)this).projectile.extraUpdates = 1;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
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

	public override void AI()
	{
		((ModProjectile)this).projectile.rotation += 0.08f;
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 6; i++)
		{
			Vector2 vector = ((float)Math.PI / 3f * (float)i).ToRotationVector2();
			vector.Normalize();
			vector *= 10f;
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, 337, 70, 1f, Main.myPlayer, 0f, 0f);
		}
	}
}
