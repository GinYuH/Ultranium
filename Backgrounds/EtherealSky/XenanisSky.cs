using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Ultranium.Backgrounds.EtherealSky;

public class XenanisSky : CustomSky
{
	private struct LightPillar
	{
		public Vector2 Position;

		public float Depth;
	}

	public bool Active;

	public float Intensity;

	private LightPillar[] Pillars;

	private readonly UnifiedRandom _random = new UnifiedRandom();

	public override void Update(GameTime gameTime)
	{
		if (Active)
		{
			Intensity = Math.Min(1f, 0.01f + Intensity);
		}
		else
		{
			Intensity = Math.Max(0f, Intensity - 0.01f);
		}
	}

	public override Color OnTileColor(Color inColor)
	{
		return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, Intensity * 0.5f));
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		Texture2D texture = ModContent.Request<Texture2D>("Ultranium/Backgrounds/EtherealSky/EtherealBeam").Value;
		Texture2D[] array = new Texture2D[3];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = ModContent.Request<Texture2D>("Ultranium/Backgrounds/EtherealSky/Rock" + i).Value;
		}
		Texture2D texture2 = ModContent.Request<Texture2D>("Ultranium/Backgrounds/EtherealSky/XenanisSky").Value;
		if (maxDepth >= float.MaxValue && minDepth < float.MaxValue && !Main.dayTime)
		{
			spriteBatch.Draw(texture2, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
			_ = (double)(0f - Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0);
			Main.ColorOfTheSkies = Color.White;
			if (!Main.gameMenu)
			{
				_ = Main.netMode;
				_ = 2;
			}
		}
		int num = -1;
		int num2 = 0;
		for (int j = 0; j < Pillars.Length; j++)
		{
			float depth = Pillars[j].Depth;
			if (num == -1 && depth < maxDepth)
			{
				num = j;
			}
			if (depth <= minDepth)
			{
				break;
			}
			num2 = j;
		}
		if (num == -1)
		{
			return;
		}
		Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
		Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
		float num3 = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
		for (int k = num; k < num2; k++)
		{
			Vector2 vector2 = new Vector2(1f / Pillars[k].Depth, 0.9f / Pillars[k].Depth);
			Vector2 position = Pillars[k].Position;
			position = (position - vector) * vector2 + vector - Main.screenPosition;
			if (rectangle.Contains((int)position.X, (int)position.Y))
			{
				float num4 = vector2.X * 450f;
				spriteBatch.Draw(texture, position, null, Color.White * 0.2f * num3 * Intensity, 0f, Vector2.Zero, new Vector2(num4 / 70f, num4 / 45f), SpriteEffects.None, 0f);
				int num5 = 0;
				for (float num6 = 0f; num6 <= 1f; num6 += 0.03f)
				{
					float num7 = 1f - (num6 + Main.GlobalTimeWrappedHourly * 0.02f + (float)Math.Sin((float)k)) % 1f;
					spriteBatch.Draw(array[num5], position + new Vector2((float)Math.Sin(num6 * 1582f) * (num4 * 0.5f) + num4 * 0.5f, num7 * 2000f), null, Color.White * num7 * num3 * Intensity, num7 * 20f, new Vector2(array[num5].Width >> 1, array[num5].Height >> 1), 0.9f, SpriteEffects.None, 0f);
					num5 = (num5 + 1) % array.Length;
				}
			}
		}
		if (Main.gameMenu || !((Entity)Main.LocalPlayer).active)
		{
			Active = false;
		}
	}

	public override float GetCloudAlpha()
	{
		return 0f - Intensity;
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		Intensity = 0.002f;
		Active = true;
		Pillars = new LightPillar[40];
		for (int i = 0; i < Pillars.Length; i++)
		{
			Pillars[i].Position.X = (float)i / (float)Pillars.Length * ((float)Main.maxTilesX * 16f + 20000f) + _random.NextFloat() * 40f - 20f - 20000f;
			Pillars[i].Position.Y = _random.NextFloat() * 200f - 2000f;
			Pillars[i].Depth = _random.NextFloat() * 8f + 7f;
		}
		Array.Sort(Pillars, SortMethod);
	}

	private int SortMethod(LightPillar pillar1, LightPillar pillar2)
	{
		return pillar2.Depth.CompareTo(pillar1.Depth);
	}

	public override void Deactivate(params object[] args)
	{
		Active = false;
	}

	public override void Reset()
	{
		Active = false;
	}

	public override bool IsActive()
	{
		if (!Active)
		{
			return Intensity > 0.001f;
		}
		return true;
	}
}
