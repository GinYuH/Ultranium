using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Hell;

public class HellFlail : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Hell's Fury");
	}

	public override void SetDefaults()
	{
		Projectile.width = 26;
		Projectile.height = 26;
		Projectile.aiStyle = ProjAIStyleID.Flairon;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.alpha = 255;
		Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		Vector2 vector = Main.player[Projectile.owner].Center - Projectile.Center;
		Projectile.rotation = vector.ToRotation() - 1.57f;
		if (Main.player[Projectile.owner].dead)
		{
			Projectile.Kill();
			return;
		}
		Main.player[Projectile.owner].itemAnimation = 10;
		Main.player[Projectile.owner].itemTime = 10;
		if (vector.X < 0f)
		{
			Main.player[Projectile.owner].ChangeDir(1);
			Projectile.direction = 1;
		}
		else
		{
			Main.player[Projectile.owner].ChangeDir(-1);
			Projectile.direction = -1;
		}
		Main.player[Projectile.owner].itemRotation = (vector * -1f * Projectile.direction).ToRotation();
		Projectile.spriteDirection = ((!(vector.X > 0f)) ? 1 : (-1));
		if (Projectile.ai[0] == 0f && vector.Length() > 400f)
		{
			Projectile.ai[0] = 1f;
		}
		if (Projectile.ai[0] == 1f || Projectile.ai[0] == 2f)
		{
			float num = vector.Length();
			if (num > 1500f)
			{
				Projectile.Kill();
				return;
			}
			if (num > 600f)
			{
				Projectile.ai[0] = 2f;
			}
			Projectile.tileCollide = false;
			float num2 = 20f;
			if (Projectile.ai[0] == 2f)
			{
				num2 = 40f;
			}
			Projectile.velocity = Vector2.Normalize(vector) * num2;
			if (vector.Length() < num2)
			{
				Projectile.Kill();
				return;
			}
		}
		Projectile.ai[1] += 1f;
		if (Projectile.ai[1] > 5f)
		{
			Projectile.alpha = 0;
		}
		if ((int)Projectile.ai[1] % 6 == 0 && Projectile.owner == Main.myPlayer)
		{
			Vector2 spinningpoint = vector * -1f;
			spinningpoint.Normalize();
			spinningpoint *= (float)Main.rand.Next(25, 45) * 0.1f;
			spinningpoint = spinningpoint.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, spinningpoint.X, spinningpoint.Y, Mod.Find<ModProjectile>("FlareBlast").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, -10f, 0f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture = ModContent.Request<Texture2D>("Ultranium/Projectiles/Guardians/Hell/HellChain").Value;
		Vector2 center = Projectile.Center;
		Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
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
			color = Projectile.GetAlpha(color);
			Main.spriteBatch.Draw(texture, center - Main.screenPosition, sourceRectangle, color, rotation, origin, 1.35f, SpriteEffects.None, 0f);
		}
		return true;
	}
}
