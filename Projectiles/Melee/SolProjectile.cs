using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class SolProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
		((ModProjectile)this).DisplayName.SetDefault("Sol Throw");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.scale = 1f;
		((ModProjectile)this).projectile.width = 18;
		((ModProjectile)this).projectile.height = 18;
		((ModProjectile)this).projectile.aiStyle = 99;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.scale = 1f;
		ProjectileID.Sets.YoyosLifeTimeMultiplier[((ModProjectile)this).projectile.type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[((ModProjectile)this).projectile.type] = 320f;
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

	public override void PostAI()
	{
		((ModProjectile)this).projectile.rotation -= 20f;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter < 30)
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
		if (num2 != -1 && Collision.CanHit(((ModProjectile)this).projectile.position, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, Main.npc[num2].position, Main.npc[num2].width, Main.npc[num2].height))
		{
			Vector2 vector = Main.npc[num2].Center - ((ModProjectile)this).projectile.Center;
			float num4 = 9f;
			float num5 = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (num5 > num4)
			{
				num5 = num4 / num5;
			}
			vector *= num5;
			int num6 = Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, 259, 70, ((ModProjectile)this).projectile.knockBack / 2f, ((ModProjectile)this).projectile.owner, 0f, 0f);
			Main.projectile[num6].friendly = true;
			Main.projectile[num6].hostile = false;
		}
	}
}
