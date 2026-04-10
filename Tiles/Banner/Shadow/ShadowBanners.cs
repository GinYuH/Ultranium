using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Banner.Shadow;

public class ShadowBanners : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 111;
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.DustType = -1;
		base.disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
		LocalizedText val = ((ModTile)this).CreateMapEntryName((string)null);
		// val.SetDefault("Banner");
		((ModTile)this).AddMapEntry(new Color(13, 88, 130), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		string text;
		switch (frameX / 18)
		{
		default:
			return;
		case 0:
			text = "TenebrisSlimeBanner";
			break;
		case 1:
			text = "DarkDemonBanner";
			break;
		case 2:
			text = "EldritchCasterBanner";
			break;
		case 3:
			text = "ShadowGhoulBanner";
			break;
		case 4:
			text = "ShadowBatBanner";
			break;
		}
		Item.NewItem(i * 16, j * 16, 16, 48, ((ModTile)this).Mod.Find<ModItem>(text).Type, 1, false, 0, false, false);
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer)
		{
			Player localPlayer = Main.LocalPlayer;
			string text;
			switch (Main.tile[i, j].TileFrameX / 18)
			{
			default:
				return;
			case 0:
				text = "TenebrisSlime";
				break;
			case 1:
				text = "DarkDemon";
				break;
			case 2:
				text = "EldritchCaster";
				break;
			case 3:
				text = "ShadeGhoul";
				break;
			case 4:
				text = "ShadowBat";
				break;
			}
			localPlayer.NPCBannerBuff[((ModTile)this).Mod.Find<ModNPC>(text).Type] = true;
			localPlayer.hasBanner = true;
		}
	}

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
	{
		if (i % 3 == 1)
		{
			spriteEffects = SpriteEffects.FlipHorizontally;
		}
	}
}
