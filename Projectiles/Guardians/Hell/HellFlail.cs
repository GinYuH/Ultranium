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
		// ((ModProjectile)this).DisplayName.SetDefault("Hell's Fury");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 26;
		((ModProjectile)this).Projectile.height = 26;
		((ModProjectile)this).Projectile.aiStyle = 69;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.alpha = 255;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		Vector2 vector = Main.player[((ModProjectile)this).Projectile.owner].Center - ((ModProjectile)this).Projectile.Center;
		((ModProjectile)this).Projectile.rotation = vector.ToRotation() - 1.57f;
		if (Main.player[((ModProjectile)this).Projectile.owner].dead)
		{
			((ModProjectile)this).Projectile.Kill();
			return;
		}
		Main.player[((ModProjectile)this).Projectile.owner].itemAnimation = 10;
		Main.player[((ModProjectile)this).Projectile.owner].itemTime = 10;
		if (vector.X < 0f)
		{
			Main.player[((ModProjectile)this).Projectile.owner].ChangeDir(1);
			((ModProjectile)this).Projectile.direction = 1;
		}
		else
		{
			Main.player[((ModProjectile)this).Projectile.owner].ChangeDir(-1);
			((ModProjectile)this).Projectile.direction = -1;
		}
		Main.player[((ModProjectile)this).Projectile.owner].itemRotation = (vector * -1f * ((ModProjectile)this).Projectile.direction).ToRotation();
		((ModProjectile)this).Projectile.spriteDirection = ((!(vector.X > 0f)) ? 1 : (-1));
		if (((ModProjectile)this).Projectile.ai[0] == 0f && vector.Length() > 400f)
		{
			((ModProjectile)this).Projectile.ai[0] = 1f;
		}
		if (((ModProjectile)this).Projectile.ai[0] == 1f || ((ModProjectile)this).Projectile.ai[0] == 2f)
		{
			float num = vector.Length();
			if (num > 1500f)
			{
				((ModProjectile)this).Projectile.Kill();
				return;
			}
			if (num > 600f)
			{
				((ModProjectile)this).Projectile.ai[0] = 2f;
			}
			((ModProjectile)this).Projectile.tileCollide = false;
			float num2 = 20f;
			if (((ModProjectile)this).Projectile.ai[0] == 2f)
			{
				num2 = 40f;
			}
			((ModProjectile)this).Projectile.velocity = Vector2.Normalize(vector) * num2;
			if (vector.Length() < num2)
			{
				((ModProjectile)this).Projectile.Kill();
				return;
			}
		}
		((ModProjectile)this).Projectile.ai[1] += 1f;
		if (((ModProjectile)this).Projectile.ai[1] > 5f)
		{
			((ModProjectile)this).Projectile.alpha = 0;
		}
		if ((int)((ModProjectile)this).Projectile.ai[1] % 6 == 0 && ((ModProjectile)this).Projectile.owner == Main.myPlayer)
		{
			Vector2 spinningpoint = vector * -1f;
			spinningpoint.Normalize();
			spinningpoint *= (float)Main.rand.Next(25, 45) * 0.1f;
			spinningpoint = spinningpoint.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866);
			Projectile.NewProjectile(((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, spinningpoint.X, spinningpoint.Y, ((ModProjectile)this).Mod.Find<ModProjectile>("FlareBlast").Type, ((ModProjectile)this).Projectile.damage, ((ModProjectile)this).Projectile.knockBack, ((ModProjectile)this).Projectile.owner, -10f, 0f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture = ModContent.GetTexture("Ultranium/Projectiles/Guardians/Hell/HellChain");
		Vector2 center = ((ModProjectile)this).Projectile.Center;
		Vector2 mountedCenter = Main.player[((ModProjectile)this).Projectile.owner].MountedCenter;
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
			color = ((ModProjectile)this).Projectile.GetAlpha(color);
			Main.spriteBatch.Draw(texture, center - Main.screenPosition, sourceRectangle, color, rotation, origin, 1.35f, SpriteEffects.None, 0f);
		}
		return true;
	}
}
