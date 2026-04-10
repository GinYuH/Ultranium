using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Guardians.Nature;

public class UltraFlail : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Ultranium Power Whip");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.width = 16;
		((ModProjectile)this).projectile.height = 16;
		((ModProjectile)this).projectile.friendly = true;
		((ModProjectile)this).projectile.alpha = 255;
		((ModProjectile)this).projectile.penetrate = -1;
		((ModProjectile)this).projectile.hide = true;
		((ModProjectile)this).projectile.tileCollide = false;
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.ignoreWater = true;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		float num = (float)Math.PI / 2f;
		player.RotatedRelativePoint(player.MountedCenter, true);
		if (((ModProjectile)this).projectile.localAI[1] > 0f)
		{
			((ModProjectile)this).projectile.localAI[1] -= 1f;
		}
		((ModProjectile)this).projectile.alpha -= 42;
		if (((ModProjectile)this).projectile.alpha < 0)
		{
			((ModProjectile)this).projectile.alpha = 0;
		}
		if (((ModProjectile)this).projectile.localAI[0] == 0f)
		{
			((ModProjectile)this).projectile.localAI[0] = ((ModProjectile)this).projectile.velocity.ToRotation();
		}
		float num2 = ((((ModProjectile)this).projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : (-1));
		if (((ModProjectile)this).projectile.ai[1] <= 0f)
		{
			num2 *= -1f;
		}
		Vector2 spinningpoint = (num2 * (((ModProjectile)this).projectile.ai[0] / 30f * ((float)Math.PI * 2f) - (float)Math.PI / 2f)).ToRotationVector2();
		spinningpoint.Y *= (float)Math.Sin(((ModProjectile)this).projectile.ai[1]);
		if (((ModProjectile)this).projectile.ai[1] <= 0f)
		{
			spinningpoint.Y *= -1f;
		}
		spinningpoint = spinningpoint.RotatedBy(((ModProjectile)this).projectile.localAI[0]);
		((ModProjectile)this).projectile.ai[0] += 1f;
		if (((ModProjectile)this).projectile.ai[0] < 30f)
		{
			((ModProjectile)this).projectile.velocity += 60f * spinningpoint;
		}
		else
		{
			((ModProjectile)this).projectile.Kill();
		}
		((ModProjectile)this).projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - ((ModProjectile)this).projectile.Size / 2f;
		((ModProjectile)this).projectile.rotation = ((ModProjectile)this).projectile.velocity.ToRotation() + num;
		((ModProjectile)this).projectile.spriteDirection = ((ModProjectile)this).projectile.direction;
		((ModProjectile)this).projectile.timeLeft = 2;
		player.ChangeDir(((ModProjectile)this).projectile.direction);
		player.heldProj = ((ModProjectile)this).projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(((ModProjectile)this).projectile.velocity.Y * (float)((ModProjectile)this).projectile.direction, ((ModProjectile)this).projectile.velocity.X * (float)((ModProjectile)this).projectile.direction);
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
		((ModProjectile)this).projectile.Center = player.RotatedRelativePoint(player.position + vector, true) - ((ModProjectile)this).projectile.velocity;
		if (((ModProjectile)this).projectile.alpha == 0)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust obj = Main.dust[Dust.NewDust(((ModProjectile)this).projectile.position + ((ModProjectile)this).projectile.velocity * 2f, ((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height, ((ModProjectile)this).mod.DustType("UltraniumDust"), 0f, 0f, 100, default(Color), 2f)];
				obj.noGravity = true;
				obj.velocity *= 2f;
				obj.velocity += ((ModProjectile)this).projectile.localAI[0].ToRotationVector2();
				obj.fadeIn = 1.5f;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
	{
		Vector2 mountedCenter = Main.player[((ModProjectile)this).projectile.owner].MountedCenter;
		Color color = Lighting.GetColor((int)((double)((ModProjectile)this).projectile.position.X + (double)((ModProjectile)this).projectile.width * 0.5) / 16, (int)(((double)((ModProjectile)this).projectile.position.Y + (double)((ModProjectile)this).projectile.height * 0.5) / 16.0));
		if (((ModProjectile)this).projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[((ModProjectile)this).projectile.type])
		{
			color = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
		}
		_ = ((ModProjectile)this).projectile.position;
		_ = new Vector2(((ModProjectile)this).projectile.width, ((ModProjectile)this).projectile.height) / 2f + Vector2.UnitY * ((ModProjectile)this).projectile.gfxOffY - Main.screenPosition;
		Texture2D texture2D = Main.projectileTexture[((ModProjectile)this).projectile.type];
		Color alpha = ((ModProjectile)this).projectile.GetAlpha(color);
		if (((ModProjectile)this).projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		float num = ((ModProjectile)this).projectile.velocity.Length() + 16f;
		bool flag = num < 100f;
		Vector2 vector = Vector2.Normalize(((ModProjectile)this).projectile.velocity);
		Rectangle rectangle = new Rectangle(0, 0, texture2D.Width, 42);
		Vector2 vector2 = new Vector2(0f, Main.player[((ModProjectile)this).projectile.owner].gfxOffY);
		float rotation = ((ModProjectile)this).projectile.rotation + (float)Math.PI;
		Main.spriteBatch.Draw(texture2D, ((ModProjectile)this).projectile.Center.Floor() - Main.screenPosition + vector2, rectangle, alpha, rotation, rectangle.Size() / 2f - Vector2.UnitY * 4f, ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
		num -= 40f * ((ModProjectile)this).projectile.scale;
		Vector2 vector3 = ((ModProjectile)this).projectile.Center.Floor();
		vector3 += vector * ((ModProjectile)this).projectile.scale * 24f;
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
				Main.spriteBatch.Draw(texture2D, vector3 - Main.screenPosition + vector2, rectangle, alpha, rotation, new Vector2(rectangle.Width / 2, 0f), ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
				num2 += (float)rectangle.Height * ((ModProjectile)this).projectile.scale;
				vector3 += vector * rectangle.Height * ((ModProjectile)this).projectile.scale;
			}
		}
		Vector2 vector4 = vector3;
		vector3 = ((ModProjectile)this).projectile.Center.Floor();
		vector3 += vector * ((ModProjectile)this).projectile.scale * 24f;
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
				Main.spriteBatch.Draw(texture2D, vector3 - Main.screenPosition + vector2, rectangle, alpha, rotation, new Vector2(rectangle.Width / 2, 0f), ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
				num5 += num7;
				vector3 += vector * num7;
			}
		}
		rectangle = new Rectangle(0, 90, texture2D.Width, 48);
		Main.spriteBatch.Draw(texture2D, vector4 - Main.screenPosition + vector2, rectangle, alpha, rotation, Utils.Frame(texture2D, 1, 1, 0, 0).Top(), ((ModProjectile)this).projectile.scale, SpriteEffects.None, 0f);
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
		Vector2 velocity = ((ModProjectile)this).projectile.velocity;
		Utils.PlotTileLine(((ModProjectile)this).projectile.Center, ((ModProjectile)this).projectile.Center + velocity * ((ModProjectile)this).projectile.localAI[1], (float)((ModProjectile)this).projectile.width * ((ModProjectile)this).projectile.scale, new PerLinePoint(DelegateMethods.CutTiles));
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (projHitbox.Intersects(targetHitbox))
		{
			return true;
		}
		float collisionPoint = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), ((ModProjectile)this).projectile.Center, ((ModProjectile)this).projectile.Center + ((ModProjectile)this).projectile.velocity, 16f * ((ModProjectile)this).projectile.scale, ref collisionPoint))
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		_ = Main.player[((ModProjectile)this).projectile.owner];
		if (Main.rand.Next(3) == 0)
		{
			Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ((ModProjectile)this).mod.ProjectileType("UltraniumLingerBolt"), ((ModProjectile)this).projectile.damage, 10f, ((ModProjectile)this).projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
		}
	}
}
