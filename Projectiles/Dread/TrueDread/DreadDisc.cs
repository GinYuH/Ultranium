using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadDisc : ModProjectile
{
	private int ProjectileTimer;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Disc of Dismay");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 48;
		((ModProjectile)this).projectile.height = 48;
		((ModProjectile)this).projectile.aiStyle = 3;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.ranged = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.penetrate = 3;
		((ModProjectile)this).projectile.timeLeft = 700;
		((ModProjectile)this).projectile.extraUpdates = 1;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 2;
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
		ProjectileTimer++;
		if (ProjectileTimer >= 60)
		{
			int num = 4;
			int num2 = Main.rand.Next(0, 180);
			for (int i = 0; i < num; i++)
			{
				float num3 = MathHelper.ToRadians(360 / num * i + num2);
				Vector2 vector = new Vector2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y).RotatedBy(num3);
				vector.Normalize();
				vector.X *= 3f;
				vector.Y *= 3f;
				Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).mod.ProjectileType("DreadParticleBolt"), ((ModProjectile)this).projectile.damage, 2f, ((ModProjectile)this).projectile.owner, 0f, 0f);
			}
			ProjectileTimer = 0;
		}
		Vector2 position = ((ModProjectile)this).projectile.Center + Vector2.Normalize(((ModProjectile)this).projectile.velocity) * 10f;
		Dust obj = Main.dust[Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90)];
		obj.position = position;
		obj.velocity = ((ModProjectile)this).projectile.velocity.RotatedBy(Math.PI / 2.0) * 0.33f + ((ModProjectile)this).projectile.velocity / 4f;
		obj.position += ((ModProjectile)this).projectile.velocity.RotatedBy(Math.PI / 2.0);
		obj.fadeIn = 0.5f;
		obj.noGravity = true;
		Dust obj2 = Main.dust[Dust.NewDust(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, 90)];
		obj2.position = position;
		obj2.velocity = ((ModProjectile)this).projectile.velocity.RotatedBy(-Math.PI / 2.0) * 0.33f + ((ModProjectile)this).projectile.velocity / 4f;
		obj2.position += ((ModProjectile)this).projectile.velocity.RotatedBy(-Math.PI / 2.0);
		obj2.fadeIn = 0.5f;
		obj2.noGravity = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
