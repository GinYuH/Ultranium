using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadDisc : ModProjectile
{
	private int ProjectileTimer;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
		// ((ModProjectile)this).DisplayName.SetDefault("Disc of Dismay");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 48;
		((ModProjectile)this).Projectile.height = 48;
		((ModProjectile)this).Projectile.aiStyle = 3;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Ranged;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.penetrate = 3;
		((ModProjectile)this).Projectile.timeLeft = 700;
		((ModProjectile)this).Projectile.extraUpdates = 1;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.immune[((ModProjectile)this).Projectile.owner] = 2;
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
		ProjectileTimer++;
		if (ProjectileTimer >= 60)
		{
			int num = 4;
			int num2 = Main.rand.Next(0, 180);
			for (int i = 0; i < num; i++)
			{
				float num3 = MathHelper.ToRadians(360 / num * i + num2);
				Vector2 vector = new Vector2(((ModProjectile)this).Projectile.velocity.X, ((ModProjectile)this).Projectile.velocity.Y).RotatedBy(num3);
				vector.Normalize();
				vector.X *= 3f;
				vector.Y *= 3f;
				Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, vector.X, vector.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("DreadParticleBolt").Type, ((ModProjectile)this).Projectile.damage, 2f, ((ModProjectile)this).Projectile.owner, 0f, 0f);
			}
			ProjectileTimer = 0;
		}
		Vector2 position = ((ModProjectile)this).Projectile.Center + Vector2.Normalize(((ModProjectile)this).Projectile.velocity) * 10f;
		Dust obj = Main.dust[Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90)];
		obj.position = position;
		obj.velocity = ((ModProjectile)this).Projectile.velocity.RotatedBy(Math.PI / 2.0) * 0.33f + ((ModProjectile)this).Projectile.velocity / 4f;
		obj.position += ((ModProjectile)this).Projectile.velocity.RotatedBy(Math.PI / 2.0);
		obj.fadeIn = 0.5f;
		obj.noGravity = true;
		Dust obj2 = Main.dust[Dust.NewDust(((ModProjectile)this).Projectile.position, ((ModProjectile)this).Projectile.width, ((ModProjectile)this).Projectile.height, 90)];
		obj2.position = position;
		obj2.velocity = ((ModProjectile)this).Projectile.velocity.RotatedBy(-Math.PI / 2.0) * 0.33f + ((ModProjectile)this).Projectile.velocity / 4f;
		obj2.position += ((ModProjectile)this).Projectile.velocity.RotatedBy(-Math.PI / 2.0);
		obj2.fadeIn = 0.5f;
		obj2.noGravity = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
