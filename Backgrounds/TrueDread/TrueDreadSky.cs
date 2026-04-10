using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Ultranium.Backgrounds.TrueDread;

public class TrueDreadSky : CustomSky
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
		Texture2D texture = ModContent.GetTexture("Ultranium/Backgrounds/TrueDread/DreadBeam");
		Texture2D texture2 = ModContent.GetTexture("Ultranium/Backgrounds/TrueDread/DreadSky");
		if (maxDepth >= float.MaxValue && minDepth < float.MaxValue && !Main.dayTime)
		{
			spriteBatch.Draw(texture2, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
			_ = (double)(0f - Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0);
			Main.bgColor = Color.White;
			if (!Main.gameMenu)
			{
				_ = Main.netMode;
				_ = 2;
			}
		}
		int num = -1;
		int num2 = 0;
		for (int i = 0; i < Pillars.Length; i++)
		{
			float depth = Pillars[i].Depth;
			if (num == -1 && depth < maxDepth)
			{
				num = i;
			}
			if (depth <= minDepth)
			{
				break;
			}
			num2 = i;
		}
		if (num == -1)
		{
			return;
		}
		Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
		Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
		float num3 = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
		for (int j = num; j < num2; j++)
		{
			Vector2 vector2 = new Vector2(1f / Pillars[j].Depth, 0.9f / Pillars[j].Depth);
			Vector2 position = Pillars[j].Position;
			position = (position - vector) * vector2 + vector - Main.screenPosition;
			if (rectangle.Contains((int)position.X, (int)position.Y))
			{
				float num4 = vector2.X * 450f;
				spriteBatch.Draw(texture, position, null, Color.White * 0.2f * num3 * Intensity, 0f, Vector2.Zero, new Vector2(num4 / 70f, num4 / 45f), SpriteEffects.None, 0f);
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
