using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class EldritchFlail : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Azathoth");
	}

	public override void SetDefaults()
	{
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.friendly = true;
		Projectile.alpha = 255;
		Projectile.penetrate = -1;
		Projectile.hide = true;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		float num = (float)Math.PI / 2f;
		player.RotatedRelativePoint(player.MountedCenter, true);
		if (Projectile.localAI[1] > 0f)
		{
			Projectile.localAI[1] -= 1f;
		}
		Projectile.alpha -= 42;
		if (Projectile.alpha < 0)
		{
			Projectile.alpha = 0;
		}
		if (Projectile.localAI[0] == 0f)
		{
			Projectile.localAI[0] = Projectile.velocity.ToRotation();
		}
		float num2 = ((Projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : (-1));
		if (Projectile.ai[1] <= 0f)
		{
			num2 *= -1f;
		}
		Vector2 spinningpoint = (num2 * (Projectile.ai[0] / 30f * ((float)Math.PI * 2f) - (float)Math.PI / 2f)).ToRotationVector2();
		spinningpoint.Y *= (float)Math.Sin(Projectile.ai[1]);
		if (Projectile.ai[1] <= 0f)
		{
			spinningpoint.Y *= -1f;
		}
		spinningpoint = spinningpoint.RotatedBy(Projectile.localAI[0]);
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] < 30f)
		{
			Projectile.velocity += 48f * spinningpoint;
		}
		else
		{
			Projectile.Kill();
		}
		Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
		Projectile.rotation = Projectile.velocity.ToRotation() + num;
		Projectile.spriteDirection = Projectile.direction;
		Projectile.timeLeft = 2;
		player.ChangeDir(Projectile.direction);
		player.heldProj = Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * (float)Projectile.direction, Projectile.velocity.X * (float)Projectile.direction);
		Vector2 vector = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
		if (player.direction != 1)
		{
			vector.X = (float)player.bodyFrame.Width - vector.X;
		}
		if (player.gravDir != 1f)
		{
			vector.Y = (float)player.bodyFrame.Height - vector.Y;
		}
		vector -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
		Projectile.Center = player.RotatedRelativePoint(player.position + vector, true) - Projectile.velocity;
		if (Projectile.alpha == 0)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust obj = Main.dust[Dust.NewDust(Projectile.position + Projectile.velocity * 2f, Projectile.width, Projectile.height, 89, 0f, 0f, 100, default(Color), 2f)];
				obj.noGravity = true;
				obj.velocity *= 2f;
				obj.velocity += Projectile.localAI[0].ToRotationVector2();
				obj.fadeIn = 1.5f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
		Color color = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
		if (Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type])
		{
			color = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
		}
		_ = Projectile.position;
		_ = new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		Color alpha = Projectile.GetAlpha(color);
		if (Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		float num = Projectile.velocity.Length() + 16f;
		bool flag = num < 100f;
		Vector2 vector = Vector2.Normalize(Projectile.velocity);
		Rectangle rectangle = new Rectangle(0, 0, texture2D.Width, 42);
		Vector2 vector2 = new Vector2(0f, Main.player[Projectile.owner].gfxOffY);
		float rotation = Projectile.rotation + (float)Math.PI;
		Main.spriteBatch.Draw(texture2D, Projectile.Center.Floor() - Main.screenPosition + vector2, rectangle, alpha, rotation, rectangle.Size() / 2f - Vector2.UnitY * 4f, Projectile.scale, SpriteEffects.None, 0f);
		num -= 40f * Projectile.scale;
		Vector2 vector3 = Projectile.Center.Floor();
		vector3 += vector * Projectile.scale * 24f;
		rectangle = new Rectangle(0, 68, texture2D.Width, 18);
		if (num > 0f)
		{
			float num2 = 0f;
			while (num2 + 1f < num)
			{
				if (num - num2 < (float)rectangle.Height)
				{
					rectangle.Height = (int)(num - num2);
				}
				Main.spriteBatch.Draw(texture2D, vector3 - Main.screenPosition + vector2, rectangle, alpha, rotation, new Vector2(rectangle.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0f);
				num2 += (float)rectangle.Height * Projectile.scale;
				vector3 += vector * rectangle.Height * Projectile.scale;
			}
		}
		Vector2 vector4 = vector3;
		vector3 = Projectile.Center.Floor();
		vector3 += vector * Projectile.scale * 24f;
		rectangle = new Rectangle(0, 46, texture2D.Width, 18);
		int num3 = 18;
		if (flag)
		{
			num3 = 9;
		}
		float num4 = num;
		if (num > 0f)
		{
			float num5 = 0f;
			float num6 = num4 / (float)num3;
			num5 += num6 * 0.25f;
			vector3 += vector * num6 * 0.25f;
			for (int i = 0; i < num3; i++)
			{
				float num7 = num6;
				if (i == 0)
				{
					num7 *= 0.75f;
				}
				Main.spriteBatch.Draw(texture2D, vector3 - Main.screenPosition + vector2, rectangle, alpha, rotation, new Vector2(rectangle.Width / 2, 0f), Projectile.scale, SpriteEffects.None, 0f);
				num5 += num7;
				vector3 += vector * num7;
			}
		}
		rectangle = new Rectangle(0, 90, texture2D.Width, 48);
		Main.spriteBatch.Draw(texture2D, vector4 - Main.screenPosition + vector2, rectangle, alpha, rotation, Utils.Frame(texture2D, 1, 1, 0, 0).Top(), Projectile.scale, SpriteEffects.None, 0f);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void CutTiles()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 velocity = Projectile.velocity;
		Utils.PlotTileLine(Projectile.Center, Projectile.Center + velocity * Projectile.localAI[1], (float)Projectile.width * Projectile.scale, new PerLinePoint(DelegateMethods.CutTiles));
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (projHitbox.Intersects(targetHitbox))
		{
			return true;
		}
		float collisionPoint = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity, 16f * Projectile.scale, ref collisionPoint))
		{
			return true;
		}
		return false;
	}
}
