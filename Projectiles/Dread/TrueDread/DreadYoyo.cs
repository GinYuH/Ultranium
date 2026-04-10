using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadYoyo : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("The Toothball");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 24;
		((ModProjectile)this).projectile.aiStyle = 99;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.scale = 1f;
		ProjectileID.Sets.YoyosLifeTimeMultiplier[((ModProjectile)this).projectile.type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[((ModProjectile)this).projectile.type] = 455f;
		ProjectileID.Sets.YoyosTopSpeed[((ModProjectile)this).projectile.type] = 17f;
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

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void PostAI()
	{
		((ModProjectile)this).projectile.rotation -= 20f;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 4;
		if (Main.rand.Next(3) == 0)
		{
			int num = Main.rand.Next(2, 4);
			float num2 = 0.2088f;
			double num3 = Math.Atan2(((ModProjectile)this).projectile.velocity.X, ((ModProjectile)this).projectile.velocity.Y) - (double)(num2 / 2f);
			double num4 = num2 / (float)num;
			for (int i = 0; i < num; i++)
			{
				double num5 = num3 + num4 * (double)(i + i * i) / 2.0 + (double)(32f * (float)i);
				Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, (float)(Math.Sin(num5) * 3.0) * 2f, (float)(Math.Cos(num5) * 3.0) * 2f, ((ModProjectile)this).mod.ProjectileType("DreadYoyoTooth"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, 0f, 0f);
				Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, (float)((0.0 - Math.Sin(num5)) * 3.0) * 2f, (float)((0.0 - Math.Cos(num5)) * 3.0) * 2f, ((ModProjectile)this).mod.ProjectileType("DreadYoyoTooth"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, 0f, 0f);
			}
		}
	}
}
