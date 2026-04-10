using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;

namespace Ultranium.Backgrounds.Boss;

public class DreadSky : CustomSky
{
	public bool Active;

	public float Intensity;

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
		if (!(maxDepth >= float.MaxValue) || !(minDepth < float.MaxValue))
		{
			return;
		}
		if (Main.dayTime)
		{
			Lighting.brightness = 0.5f;
		}
		if (!Main.dayTime || Main.dayTime)
		{
			_ = (double)(0f - Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0);
			Main.bgColor = Color.White;
			if (!Main.gameMenu)
			{
				_ = Main.netMode;
				_ = 2;
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
