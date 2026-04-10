using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.Backgrounds.ShadowBiome.Sky;

public class ShadowBiomeSky : CustomSky
{
	public static Asset<Texture2D> SkyTexture;

	public bool Active;

	public float Intensity;

	public float Alpha;

	public float Rotation;

	public override void OnLoad()
	{
		SkyTexture = ModContent.Request<Texture2D>("Ultranium/Backgrounds/ShadowBiome/Sky/ShadowBiomeSky");
	}

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

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		if (maxDepth >= float.MaxValue && minDepth < float.MaxValue && Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().ZoneShadow)
		{
			_ = ShadowEventWorld.ShadowEventActive;
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
