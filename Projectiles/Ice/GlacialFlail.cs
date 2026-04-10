using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ice;

public class GlacialFlail : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Glacial Flail");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.height = 30;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.aiStyle = 15;
	}

	public override void AI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter < 30)
		{
			return;
		}
		((ModProjectile)this).projectile.frameCounter = 0;
		float num = 800f;
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
			int num6 = Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, vector.X, vector.Y, 119, ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack / 2f, ((ModProjectile)this).projectile.owner, 0f, 0f);
			Main.projectile[num6].friendly = true;
			Main.projectile[num6].hostile = false;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Texture2D texture = ModContent.GetTexture("Ultranium/Projectiles/Ice/GlacialFlailChain");
		Vector2 center = ((ModProjectile)this).projectile.Center;
		Vector2 mountedCenter = Main.player[((ModProjectile)this).projectile.owner].MountedCenter;
		Rectangle? sourceRectangle = null;
		Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
		float num = texture.Height;
		Vector2 vector = mountedCenter - center;
		float rotation = (float)Math.Atan2(vector.Y, vector.X) - 1.57f;
		bool flag = true;
		if (float.IsNaN(center.X) && float.IsNaN(center.Y))
		{
			flag = false;
		}
		if (float.IsNaN(vector.X) && float.IsNaN(vector.Y))
		{
			flag = false;
		}
		while (flag)
		{
			if ((double)vector.Length() < (double)num + 1.0)
			{
				flag = false;
				continue;
			}
			Vector2 vector2 = vector;
			vector2.Normalize();
			center += vector2 * num;
			vector = mountedCenter - center;
			Color color = Lighting.GetColor((int)center.X / 16, (int)((double)center.Y / 16.0));
			color = ((ModProjectile)this).projectile.GetAlpha(color);
			Main.spriteBatch.Draw(texture, center - Main.screenPosition, sourceRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0f);
		}
		return true;
	}
}
