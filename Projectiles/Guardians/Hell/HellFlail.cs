using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Hell;

public class HellFlail : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Hell's Fury");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 26;
		((ModProjectile)this).projectile.height = 26;
		((ModProjectile)this).projectile.aiStyle = 69;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.melee = true;
	}

	public override void AI()
	{
		Vector2 vector = Main.player[((ModProjectile)this).projectile.owner].Center - ((ModProjectile)this).projectile.Center;
		((ModProjectile)this).projectile.rotation = vector.ToRotation() - 1.57f;
		if (Main.player[((ModProjectile)this).projectile.owner].dead)
		{
			((ModProjectile)this).projectile.Kill();
			return;
		}
		Main.player[((ModProjectile)this).projectile.owner].itemAnimation = 10;
		Main.player[((ModProjectile)this).projectile.owner].itemTime = 10;
		if (vector.X < 0f)
		{
			Main.player[((ModProjectile)this).projectile.owner].ChangeDir(1);
			((ModProjectile)this).projectile.direction = 1;
		}
		else
		{
			Main.player[((ModProjectile)this).projectile.owner].ChangeDir(-1);
			((ModProjectile)this).projectile.direction = -1;
		}
		Main.player[((ModProjectile)this).projectile.owner].itemRotation = (vector * -1f * ((ModProjectile)this).projectile.direction).ToRotation();
		((ModProjectile)this).projectile.spriteDirection = ((!(vector.X > 0f)) ? 1 : (-1));
		if (((ModProjectile)this).projectile.ai[0] == 0f && vector.Length() > 400f)
		{
			((ModProjectile)this).projectile.ai[0] = 1f;
		}
		if (((ModProjectile)this).projectile.ai[0] == 1f || ((ModProjectile)this).projectile.ai[0] == 2f)
		{
			float num = vector.Length();
			if (num > 1500f)
			{
				((ModProjectile)this).projectile.Kill();
				return;
			}
			if (num > 600f)
			{
				((ModProjectile)this).projectile.ai[0] = 2f;
			}
			((ModProjectile)this).projectile.tileCollide = false;
			float num2 = 20f;
			if (((ModProjectile)this).projectile.ai[0] == 2f)
			{
				num2 = 40f;
			}
			((ModProjectile)this).projectile.velocity = Vector2.Normalize(vector) * num2;
			if (vector.Length() < num2)
			{
				((ModProjectile)this).projectile.Kill();
				return;
			}
		}
		((ModProjectile)this).projectile.ai[1] += 1f;
		if (((ModProjectile)this).projectile.ai[1] > 5f)
		{
			((ModProjectile)this).projectile.alpha = 0;
		}
		if ((int)((ModProjectile)this).projectile.ai[1] % 6 == 0 && ((ModProjectile)this).projectile.owner == Main.myPlayer)
		{
			Vector2 spinningpoint = vector * -1f;
			spinningpoint.Normalize();
			spinningpoint *= (float)Main.rand.Next(25, 45) * 0.1f;
			spinningpoint = spinningpoint.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866);
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center.X, ((ModProjectile)this).projectile.Center.Y, spinningpoint.X, spinningpoint.Y, ((ModProjectile)this).mod.ProjectileType("FlareBlast"), ((ModProjectile)this).projectile.damage, ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, -10f, 0f);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Texture2D texture = ModContent.GetTexture("Ultranium/Projectiles/Guardians/Hell/HellChain");
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
			Main.spriteBatch.Draw(texture, center - Main.screenPosition, sourceRectangle, color, rotation, origin, 1.35f, SpriteEffects.None, 0f);
		}
		return true;
	}
}
