using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Ultranium.Backgrounds.Cosmic;

public class AldinSky : CustomSky
{
	private struct Meteor
	{
		public Vector2 Position;

		public float Depth;

		public int FrameCounter;

		public float Scale;

		public float StartX;
	}

	private Meteor[] Meteors;

	public bool Active;

	public float Intensity;

	public static Texture2D SkyTexture;

	public static Texture2D MeteorTexture;

	private readonly UnifiedRandom _random = new UnifiedRandom();

	private readonly float num = 1200f;

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
		for (int i = 0; i < Meteors.Length; i++)
		{
			Meteor[] meteors = Meteors;
			int num = i;
			meteors[num].Position.X = meteors[num].Position.X - this.num * (float)gameTime.ElapsedGameTime.TotalSeconds;
			Meteor[] meteors2 = Meteors;
			int num2 = i;
			meteors2[num2].Position.Y = meteors2[num2].Position.Y + this.num * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if ((double)Meteors[i].Position.Y > Main.worldSurface * 16.0)
			{
				Meteors[i].Position.X = Meteors[i].StartX;
				Meteors[i].Position.Y = -10000f;
			}
		}
	}

	public override void OnLoad()
	{
		MeteorTexture = ModContent.GetTexture("Ultranium/Backgrounds/Cosmic/AldinMeteor");
		SkyTexture = ModContent.GetTexture("Ultranium/Backgrounds/Cosmic/CosmicSky");
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		if (maxDepth >= float.MaxValue && minDepth < float.MaxValue)
		{
			Color[] array = new Color[4]
			{
				new Color(153, 255, 178),
				new Color(83, 168, 222),
				new Color(72, 37, 169),
				new Color(74, 13, 105)
			};
			float amount = (float)(Main.GameUpdateCount % 60) / 60f;
			int num = (int)(Main.GameUpdateCount / 60 % 4);
			spriteBatch.Draw(SkyTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Lerp(array[num], array[(num + 1) % 4], amount));
		}
		int num2 = -1;
		int num3 = 0;
		for (int i = 0; i < Meteors.Length; i++)
		{
			float depth = Meteors[i].Depth;
			if (num2 == -1 && depth < maxDepth)
			{
				num2 = i;
			}
			if (depth <= minDepth)
			{
				break;
			}
			num3 = i;
		}
		if (num2 == -1)
		{
			return;
		}
		float num4 = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
		Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
		Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
		for (int j = num2; j < num3; j++)
		{
			Vector2 vector2 = new Vector2(1f / Meteors[j].Depth, 0.9f / Meteors[j].Depth);
			Vector2 position = (Meteors[j].Position - vector) * vector2 + vector - Main.screenPosition;
			int num5 = Meteors[j].FrameCounter / 3;
			Meteors[j].FrameCounter = (Meteors[j].FrameCounter + 1) % 12;
			if (rectangle.Contains((int)position.X, (int)position.Y))
			{
				Color[] array2 = new Color[2]
				{
					new Color(63, 148, 202),
					new Color(52, 17, 149)
				};
				float amount2 = (float)(Main.GameUpdateCount % 60) / 60f;
				int num6 = (int)(Main.GameUpdateCount / 60 % 2);
				spriteBatch.Draw(MeteorTexture, position, new Rectangle(0, num5 * (MeteorTexture.Height / 4), MeteorTexture.Width, MeteorTexture.Height / 4), Color.Lerp(array2[num6], array2[(num6 + 1) % 2], amount2) * num4 * Intensity, 0f, Vector2.Zero, vector2.X * 5f * Meteors[j].Scale, SpriteEffects.None, 0f);
			}
		}
		if (Main.gameMenu || !((Entity)Main.LocalPlayer).active)
		{
			Active = false;
		}
	}

	public override float GetCloudAlpha()
	{
		return 1f - Intensity;
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		Intensity = 0.002f;
		Active = true;
		Meteors = new Meteor[150];
		for (int i = 0; i < Meteors.Length; i++)
		{
			float num = (float)i / (float)Meteors.Length;
			Meteors[i].Position.X = num * ((float)Main.maxTilesX * 16f) + _random.NextFloat() * 40f - 20f;
			Meteors[i].Position.Y = _random.NextFloat() * (0f - ((float)Main.worldSurface * 16f + 10000f)) - 10000f;
			if (_random.Next(3) != 0)
			{
				Meteors[i].Depth = _random.NextFloat() * 3f + 1.8f;
			}
			else
			{
				Meteors[i].Depth = _random.NextFloat() * 5f + 4.8f;
			}
			Meteors[i].FrameCounter = _random.Next(12);
			Meteors[i].Scale = _random.NextFloat() * 0.5f + 1f;
			Meteors[i].StartX = Meteors[i].Position.X;
		}
		Array.Sort(Meteors, SortMethod);
	}

	private int SortMethod(Meteor meteor1, Meteor meteor2)
	{
		return meteor2.Depth.CompareTo(meteor1.Depth);
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
