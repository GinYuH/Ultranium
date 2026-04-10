using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm.Projectiles;

public class ErebusSpawner : ModProjectile
{
	public float scale = 0.5f;

	private int SpawnTimer;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Erebus Rift");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.width = 100;
		((ModProjectile)this).Projectile.height = 50;
		((ModProjectile)this).Projectile.hostile = true;
		((ModProjectile)this).Projectile.friendly = false;
		((ModProjectile)this).Projectile.tileCollide = false;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.timeLeft = 420;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		float num = 420f;
		float num2 = ((ModProjectile)this).Projectile.ai[0];
		float num3 = MathHelper.Clamp(num2 / 30f, 0f, 1f);
		if (num2 > num - 60f)
		{
			num3 = MathHelper.Lerp(1f, 0f, (num2 - (num - 60f)) / 60f);
		}
		float num4 = 0.2f;
		Vector2 top = ((ModProjectile)this).Projectile.Top;
		Vector2 bottom = ((ModProjectile)this).Projectile.Bottom;
		Vector2.Lerp(top, bottom, 0.5f);
		Vector2 vector = new Vector2(0f, bottom.Y - top.Y);
		vector.X = vector.Y * num4;
		new Vector2(top.X - vector.X / 2f, top.Y);
		Texture2D texture2D = TextureAssets.Projectile[((ModProjectile)this).Projectile.type].Value;
		Rectangle rectangle = Utils.Frame(texture2D, 1, 1, 0, 0);
		Vector2 origin = rectangle.Size() / 2f;
		float num5 = -(float)Math.PI / 20f * num2 * (float)((!(((ModProjectile)this).Projectile.velocity.X > 0f)) ? 1 : (-1));
		SpriteEffects effects = ((((ModProjectile)this).Projectile.velocity.X > 0f) ? SpriteEffects.FlipVertically : SpriteEffects.None);
		bool flag = ((ModProjectile)this).Projectile.velocity.X > 0f;
		Vector2 unitY = Vector2.UnitY;
		double radians = num2 * 0.14f;
		Vector2 spinningpoint = unitY.RotatedBy(radians);
		float num6 = 0f;
		float num7 = 4.5f;
		if (num7 < 4.11f)
		{
			num7 = 4.11f;
		}
		Color value = new Color(51, 49, 95, 0);
		Color color = new Color(54, 19, 95, 0);
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
		float num = 420f;
		if (((ModProjectile)this).Projectile.localAI[0] >= 16f && ((ModProjectile)this).Projectile.ai[0] < num - 15f)
		{
			((ModProjectile)this).Projectile.ai[0] = num - 15f;
		}
		((ModProjectile)this).Projectile.ai[0] += 1f;
		if (((ModProjectile)this).Projectile.ai[0] >= num)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		Vector2 top = ((ModProjectile)this).Projectile.Top;
		Vector2 bottom = ((ModProjectile)this).Projectile.Bottom;
		Vector2 vector = Vector2.Lerp(top, bottom, 0.5f);
		Vector2 vector2 = new Vector2(0f, bottom.Y - top.Y);
		if (((ModProjectile)this).Projectile.ai[0] < num - 30f)
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
		SpawnTimer++;
		if (SpawnTimer == 10)
		{
			Ultranium.seizureAmount = 16f;
		}
		if (SpawnTimer <= 180)
		{
			scale += 0.02f;
		}
		if (SpawnTimer < 300)
		{
			Player localPlayer = Main.LocalPlayer;
			((ModProjectile)this).Projectile.position.X = localPlayer.Center.X - 31f;
			((ModProjectile)this).Projectile.position.Y = localPlayer.Center.Y - 250f;
		}
		else
		{
			((ModProjectile)this).Projectile.velocity *= 0f;
		}
		if (SpawnTimer == 300)
		{
			NPC.NewNPC(null, (int)((ModProjectile)this).Projectile.Center.X, (int)((ModProjectile)this).Projectile.Center.Y, ((ModProjectile)this).Mod.Find<ModNPC>("ErebusHead").Type, 0, 0f, 0f, 0f, 0f, 255);
			Projectile.NewProjectile(null, ((ModProjectile)this).Projectile.Center.X, ((ModProjectile)this).Projectile.Center.Y, 0f, 0f, ((ModProjectile)this).Mod.Find<ModProjectile>("ShockWave").Type, 0, 0f, 255, 0f, 0f);
			Main.NewText("The Eldritch Beast has been awoken!", (byte)175, (byte)75, byte.MaxValue, false);
		}
	}
}
