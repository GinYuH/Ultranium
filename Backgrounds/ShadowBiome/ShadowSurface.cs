using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Ultranium.Backgrounds.ShadowBiome;

public class ShadowSurface : ModSurfaceBackgroundStyle
{
	public override void ModifyFarFades(float[] fades, float transitionSpeed)
	{
		for (int i = 0; i < fades.Length; i++)
		{
			if (i == ((ModSurfaceBackgroundStyle)this).Slot)
			{
				fades[i] += transitionSpeed;
				if (fades[i] > 1f)
				{
					fades[i] = 1f;
				}
			}
			else
			{
				fades[i] -= transitionSpeed;
				if (fades[i] < 0f)
				{
					fades[i] = 0f;
				}
			}
		}
	}

	public override int ChooseFarTexture()
	{
		return BackgroundTextureLoader.GetBackgroundSlot(((ModSurfaceBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowMountain");
	}

	public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
	{
		return BackgroundTextureLoader.GetBackgroundSlot(((ModSurfaceBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowClose");
	}

	public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
	{
		float num = 1800f;
		float num2 = 1750f;
		int[] array = new int[3]
		{
			BackgroundTextureLoader.GetBackgroundSlot(((ModSurfaceBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowFar"),
			BackgroundTextureLoader.GetBackgroundSlot(((ModSurfaceBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowMiddle"),
			BackgroundTextureLoader.GetBackgroundSlot(((ModSurfaceBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowClose")
		};
		int num3 = array.Length;
		for (int i = 0; i < array.Length; i++)
		{
			float num4 = 0.57f - 0.1f * (float)(num3 - i);
			int num5 = array[i];
			Main.instance.LoadBackground(num5);
			float num6 = 2.5f;
			int num7 = (int)((float)Main.backgroundWidth[num5] * num6);
			SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1f / num4);
			float fieldValue = typeof(Main).GetFieldValue<float>("screenOff", Main.instance);
			float fieldValue2 = typeof(Main).GetFieldValue<float>("scAdj", Main.instance);
			int num8 = (int)(0.0 - Math.IEEERemainder(Main.screenPosition.X * num4, num7) - (double)(num7 / 2));
			int num9 = (int)((double)(0f - Main.screenPosition.Y + fieldValue / 2f) / (Main.worldSurface * 16.0) * (double)num + (double)num2) + (int)fieldValue2 - (num3 - i) * 150;
			if (Main.gameMenu)
			{
				num9 = 320;
			}
			Color fieldValue3 = typeof(Main).GetFieldValue<Color>("backColor", Main.instance);
			int num10 = Main.screenWidth / num7 + 2;
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int j = 0; j < num10; j++)
				{
					Main.spriteBatch.Draw(TextureAssets.Background[num5].Value, new Vector2(num8 + num7 * j, num9), new Rectangle(0, 0, Main.backgroundWidth[num5], Main.backgroundHeight[num5]), fieldValue3, 0f, default(Vector2), num6, SpriteEffects.None, 0f);
				}
			}
		}
		return false;
	}
}
