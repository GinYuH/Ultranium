using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ExpandingVortex : ModProjectile
{
	public float scale = 0.5f;

	public static int Timer;

	private int target;

	private int HomingDelay;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Expanding Vortex");
	}

	public override void SetDefaults()
	{
		Projectile.width = 100;
		Projectile.height = 50;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.tileCollide = false;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 620;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		float num = 620f;
		float num2 = Projectile.ai[0];
		float num3 = MathHelper.Clamp(num2 / 30f, 0f, 1f);
		if (num2 > num - 60f)
		{
			num3 = MathHelper.Lerp(1f, 0f, (num2 - (num - 60f)) / 60f);
		}
		float num4 = 0.2f;
		Vector2 top = Projectile.Top;
		Vector2 bottom = Projectile.Bottom;
		Vector2.Lerp(top, bottom, 0.5f);
		Vector2 vector = new Vector2(0f, bottom.Y - top.Y);
		vector.X = vector.Y * num4;
		new Vector2(top.X - vector.X / 2f, top.Y);
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		Rectangle rectangle = Utils.Frame(texture2D, 1, 1, 0, 0);
		Vector2 origin = rectangle.Size() / 2f;
		float num5 = -(float)Math.PI / 20f * num2 * (float)((!(Projectile.velocity.X > 0f)) ? 1 : (-1));
		SpriteEffects effects = ((Projectile.velocity.X > 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None);
		bool flag = Projectile.velocity.X > 0f;
		Vector2 unitY = Vector2.UnitY;
		double radians = num2 * 0.14f;
		Vector2 spinningpoint = unitY.RotatedBy(radians);
		float num6 = 0f;
		float num7 = 4.5f;
		if (num7 < 4.11f)
		{
			num7 = 4.11f;
		}
		Color value = new Color(19, 121, 95, 0);
		Color color = new Color(34, 166, 162, 0);
		float num8 = num2 % 60f;
		if (num8 < 30f)
		{
			color *= Utils.GetLerpValue(22f, 30f, num8, true);
		}
		else
		{
			color *= Utils.GetLerpValue(38f, 30f, num8, true);
		}
		bool flag2 = color == Color.Transparent;
		for (float num9 = (int)bottom.Y; num9 > (float)(int)top.Y; num9 -= num7)
		{
			num6 += num7;
			float num10 = num6 / vector.Y;
			float num11 = num6 * ((float)Math.PI * 2f) / -20f;
			if (flag)
			{
				num11 *= -1f;
			}
			float num12 = num10 - 0.35f;
			double radians2 = num11;
			Vector2 position = spinningpoint.RotatedBy(radians2);
			Vector2 vector2 = new Vector2(0f, num10 + 1f);
			vector2.X = vector2.Y * num4;
			Color color2 = Color.Lerp(Color.Transparent, value, num10 * 2f);
			if (num10 > 0.5f)
			{
				color2 = Color.Lerp(Color.Transparent, value, 2f - num10 * 2f);
			}
			color2.A = (byte)((float)(int)color2.A * 0.5f);
			color2 *= num3;
			position *= vector2 * 100f;
			position.Y = 0f;
			position.X = 0f;
			position += new Vector2(bottom.X, num9) - Main.screenPosition;
			if (flag2)
			{
				Color color3 = Color.Lerp(Color.Transparent, color, num10 * 2f);
				if (num10 > 0.5f)
				{
					color3 = Color.Lerp(Color.Transparent, color, 2f - num10 * 2f);
				}
				color3.A = (byte)((float)(int)color3.A * 0.5f);
				color3 *= num3;
				Main.spriteBatch.Draw(texture2D, position, rectangle, color3, num5 + num11, origin, (1f + num12) * scale * 0.8f, effects, 0f);
			}
			Main.spriteBatch.Draw(texture2D, position, rectangle, color2, num5 + num11, origin, (1f + num12) * scale, effects, 0f);
		}
		return false;
	}

	public override void AI()
	{
		float num = 620f;
		if (Projectile.localAI[0] >= 16f && Projectile.ai[0] < num - 15f)
		{
			Projectile.ai[0] = num - 15f;
		}
		Projectile.ai[0] += 1f;
		if (Projectile.ai[0] >= num)
		{
			Projectile.Kill();
		}
		Vector2 top = Projectile.Top;
		Vector2 bottom = Projectile.Bottom;
		Vector2 vector = Vector2.Lerp(top, bottom, 0.5f);
		Vector2 vector2 = new Vector2(0f, bottom.Y - top.Y);
		if (Projectile.ai[0] < num - 30f)
		{
			for (int i = 0; i < 1; i++)
			{
				float value = -1f;
				float value2 = 0.9f;
				float amount = Main.rand.NextFloat();
				Vector2 vector3 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value, value2, amount));
				vector3.X *= MathHelper.Lerp(2.2f, 0.6f, amount);
				vector3.X *= -1f;
				Vector2 vector4 = new Vector2(6f, 10f);
				_ = vector + vector2 * vector3 * 0.5f + vector4;
			}
		}
		HomingDelay++;
		if (HomingDelay >= 40)
		{
			if (Projectile.ai[0] == 0f && Main.netMode != 1)
			{
				target = -1;
				float num2 = 2000f;
				for (int j = 0; j < 255; j++)
				{
					if (((Entity)Main.player[j]).active && !Main.player[j].dead)
					{
						float num3 = Vector2.Distance(Main.player[j].Center, Projectile.Center);
						if (num3 < num2 || target == -1)
						{
							num2 = num3;
							target = j;
						}
					}
				}
				if (target != -1)
				{
					Projectile.ai[0] = 1f;
					Projectile.netUpdate = true;
				}
			}
			else
			{
				Player player = Main.player[target];
				if (!((Entity)player).active || player.dead)
				{
					target = -1;
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				else
				{
					float num4 = Projectile.velocity.ToRotation();
					Vector2 vector5 = player.Center - Projectile.Center;
					float targetAngle = vector5.ToRotation();
					if (vector5 == Vector2.Zero)
					{
						targetAngle = num4;
					}
					float num5 = num4.AngleLerp(targetAngle, 0.1f);
					Projectile.velocity = new Vector2(Projectile.velocity.Length(), 0f).RotatedBy(num5);
				}
			}
		}
		Timer++;
		if (Timer <= 180)
		{
			scale += 0.02f;
		}
		if (Timer == 100 || Timer == 200 || Timer == 300 || Timer == 400)
		{
			int num6 = (Main.expertMode ? 48 : 65);
			int num7 = Main.rand.Next(5, 12);
			for (int k = 0; k < num7; k++)
			{
				Vector2 vector6 = ((float)Math.PI * 2f / (float)num7 * (float)k).ToRotationVector2();
				vector6.Normalize();
				vector6 *= 14f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector6.X, vector6.Y, Mod.Find<ModProjectile>("EldritchBlast").Type, num6, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (Timer > 450 && Timer < 500)
		{
			int num8 = 25;
			for (int l = 0; l < num8; l++)
			{
				Vector2 vector7 = (Vector2.One * new Vector2((float)Projectile.width / 7f, (float)Projectile.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(l - (num8 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num8) + Projectile.Center;
				Vector2 vector8 = vector7 - Projectile.Center;
				Dust obj = Main.dust[Dust.NewDust(vector7 + vector8, 0, 0, 89, vector8.X * 2f, vector8.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector8) * 35f;
				obj.fadeIn = 5f;
			}
			Projectile.velocity *= 0f;
		}
		if (Timer < 500)
		{
			for (int m = 0; m < 200; m++)
			{
				if (((Entity)Main.npc[m]).active && (Main.npc[m].type == ModContent.NPCType<ErebusHead>() || Main.npc[m].type == ModContent.NPCType<ErebusBody>() || Main.npc[m].type == ModContent.NPCType<ErebusTail>()))
				{
					Main.npc[m].Center = Projectile.Center;
				}
			}
		}
		if (Projectile.timeLeft <= 1)
		{
			Timer = 0;
		}
	}
}
