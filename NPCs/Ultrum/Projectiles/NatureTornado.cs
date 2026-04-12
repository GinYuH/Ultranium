using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ultrum.Projectiles;

public class NatureTornado : ModProjectile
{
	public float scale = 2.5f;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Nature Tornado");
	}

	public override void SetDefaults()
	{
		Projectile.width = 100;
		Projectile.height = 350;
		Projectile.hostile = true;
		Projectile.friendly = false;
		Projectile.tileCollide = false;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 300;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		float num = 300f;
		float num2 = Projectile.ai[0];
		float num3 = MathHelper.Clamp(num2 / 30f, 0f, 1f);
		if (num2 > num - 60f)
		{
			num3 = MathHelper.Lerp(1f, 0f, (num2 - (num - 60f)) / 60f);
		}
		Vector2 top = Projectile.Top;
		Vector2 bottom = Projectile.Bottom;
		Vector2.Lerp(top, bottom, 0.5f);
		Vector2 vector = new Vector2(0f, bottom.Y - top.Y);
		vector.X = vector.Y;
		new Vector2(top.X - vector.X / 2f, top.Y);
		Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
		Rectangle rectangle = Utils.Frame(texture2D, 1, 1, 0, 0);
		Vector2 origin = rectangle.Size() / 2f;
		float num4 = -(float)Math.PI / 20f * num2 * (float)((!(Projectile.velocity.X > 0f)) ? 1 : (-1));
		SpriteEffects effects = ((Projectile.velocity.X > 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None);
		bool flag = Projectile.velocity.X > 0f;
		Vector2 unitY = Vector2.UnitY;
		double radians = num2 * 0.14f;
		Vector2 spinningpoint = unitY.RotatedBy(radians);
		float num5 = 0f;
		float num6 = 4.5f;
		Color value = new Color(75, 178, 12, 100);
		Color color = new Color(75, 178, 12, 100);
		float num7 = num2 % 60f;
		if (num7 < 30f)
		{
			color *= Utils.GetLerpValue(22f, 30f, num7, true);
		}
		else
		{
			color *= Utils.GetLerpValue(38f, 30f, num7, true);
		}
		_ = color != Color.Transparent;
		for (float num8 = (int)bottom.Y; num8 > (float)(int)top.Y; num8 -= num6)
		{
			num5 += num6;
			float num9 = num5 / vector.Y;
			float num10 = num5 * ((float)Math.PI * 2f) / -20f;
			if (flag)
			{
				num10 *= -1f;
			}
			float num11 = num9 - 0.35f;
			double radians2 = num10;
			Vector2 position = spinningpoint.RotatedBy(radians2);
			Vector2 vector2 = new Vector2(0f, num9 + 1f);
			vector2.X = vector2.Y;
			Color color2 = Color.Lerp(Color.Transparent, value, num9 * 2f);
			if (num9 > 0.5f)
			{
				color2 = Color.Lerp(Color.Transparent, value, 2f - num9 * 2f);
			}
			color2.A = (byte)((float)(int)color2.A * 0.5f);
			color2 *= num3;
			position *= vector2 * 100f;
			position.Y = 0f;
			position.X = 0f;
			position += new Vector2(bottom.X, num8) - Main.screenPosition;
			Main.spriteBatch.Draw(texture2D, position, rectangle, color2, num4 + num10, origin, (1f + num11) * scale, effects, 0f);
		}
		return false;
	}

	public override void AI()
	{
		float num = 300f;
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
		Projectile.velocity *= 1.02f;
	}
}
