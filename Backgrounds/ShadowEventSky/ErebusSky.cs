using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Ultranium.NPCs.ShadowWorm;
using Ultranium.ShadowEvent;

namespace Ultranium.Backgrounds.ShadowEventSky;

public class ErebusSky : CustomSky
{
	public bool Active;

	public float Intensity;

	public static Texture2D Rift;

	public static Texture2D SkyTexture;

	public float Rotation;

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

	public override void OnLoad()
	{
		SkyTexture = ModContent.GetTexture("Ultranium/Backgrounds/ShadowEventSky/ErebusSky");
		Rift = ModContent.GetTexture("Ultranium/Backgrounds/ShadowEventSky/ErebusRift");
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		if (maxDepth >= float.MaxValue && minDepth < float.MaxValue && !Main.dayTime && ShadowEventWorld.ShadowEventActive && NPC.AnyNPCs(ModContent.NPCType<ErebusHead>()))
		{
			spriteBatch.Draw(SkyTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
			Vector2 position = new Vector2(Main.screenWidth / 2, Main.screenHeight / 3);
			Rotation -= 0.002f;
			new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
			_ = 0f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
			spriteBatch.Draw(Rift, position, null, Color.White * 0.5f * Intensity, Rotation, new Vector2(Rift.Width >> 1, Rift.Height >> 1), 1f, SpriteEffects.None, 1f);
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
