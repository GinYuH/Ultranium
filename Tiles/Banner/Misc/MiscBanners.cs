using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Banner.Misc;

public class MiscBanners : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 111;
		TileObjectData.addTile((int)Type);
		base.DustType = -1;
		base.disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Banner");
		AddMapEntry(new Color(13, 88, 130), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		string text;
		switch (frameX / 18)
		{
		default:
			return;
		case 0:
			text = "DreadApparitionBanner";
			break;
		case 1:
			text = "DreadChaserBanner";
			break;
		case 2:
			text = "StellarChaserBanner";
			break;
		case 3:
			text = "StellarSlimeBanner";
			break;
		case 4:
			text = "OrcaBanner";
			break;
		}
		Item.NewItem(null, i * 16, j * 16, 16, 48, Mod.Find<ModItem>(text).Type, 1, false, 0, false, false);
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
				text = "DreadApparition";
				break;
			case 1:
				text = "DreadChaser";
				break;
			case 2:
				text = "StellarChaser";
				break;
			case 3:
				text = "StellarSlime";
				break;
			case 4:
				text = "Orca";
				break;
			}
			localPlayer.NPCBannerBuff[Mod.Find<ModNPC>(text).Type] = true;
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
