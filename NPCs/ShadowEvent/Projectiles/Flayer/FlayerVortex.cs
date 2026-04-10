using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent.Projectiles.Flayer;

public class FlayerVortex : ModProjectile
{
	public float scale = 1.2f;

	private int ShootTimer;

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Erebus Vortex");
	}

	public override void SetDefaults()
	{
		Projectile.width = 250;
		Projectile.height = 50;
		Projectile.hostile = true;
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
		bool flag2 = color != Color.Transparent;
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
		float num = 300f;
		if (Projectile.localAI[0] >= 16f && Projectile.ai[0] < num - 15f)
		{
			Projectile.ai[0] = num - 8f;
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
		ShootTimer++;
		if (ShootTimer == 120 || ShootTimer == 240)
		{
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(Projectile.position.X, Projectile.position.Y));
			for (int j = 0; j < 10; j++)
			{
				Vector2 vector5 = ((float)Math.PI / 5f * (float)j).ToRotationVector2();
				vector5.Normalize();
				vector5 *= 6f;
				Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y, vector5.X, vector5.Y, Mod.Find<ModProjectile>("EldritchBlast").Type, 50, 1f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}
