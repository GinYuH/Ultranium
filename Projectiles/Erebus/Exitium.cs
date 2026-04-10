using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus;

public class Exitium : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Exitium");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 24;
		((ModProjectile)this).projectile.height = 18;
		((ModProjectile)this).projectile.aiStyle = 99;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.scale = 1f;
		ProjectileID.Sets.YoyosLifeTimeMultiplier[((ModProjectile)this).projectile.type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[((ModProjectile)this).projectile.type] = 500f;
		ProjectileID.Sets.YoyosTopSpeed[((ModProjectile)this).projectile.type] = 18f;
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

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.immune[((ModProjectile)this).projectile.owner] = 6;
	}

	public override void PostAI()
	{
		((ModProjectile)this).projectile.rotation -= 20f;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter < 45)
		{
			return;
		}
		((ModProjectile)this).projectile.frameCounter = 0;
		float num = 8000f;
		int num2 = -1;
		for (int i = 0; i < 200; i++)
		{
			float num3 = Vector2.Distance(((ModProjectile)this).projectile.Center, Main.npc[i].Center);
			if (num3 < num && num3 < 640f && Main.npc[i].CanBeChasedBy(((ModProjectile)this).projectile))
			{
				num2 = i;
				num = num3;
			}
		}
		if (num2 == -1 || !Collision.CanHit(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, Main.npc[num2].position, Main.npc[num2].width, Main.npc[num2].height))
		{
			return;
		}
		Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
		for (int j = 0; j < 10; j++)
		{
			Vector2 vector = spinningpoint.RotatedBy(Math.PI / 3.0 * ((double)j + Main.rand.NextDouble() - 0.5));
			float num4 = (float)Main.rand.Next(10, 80) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num4 *= -1f;
			}
			float num5 = (float)Main.rand.Next(10, 80) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num5 *= -1f;
			}
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center, vector, ((ModProjectile)this).mod.ProjectileType("ExitiumTentacle"), ((ModProjectile)this).projectile.damage, 0f, Main.myPlayer, num5, num4);
		}
	}
}
