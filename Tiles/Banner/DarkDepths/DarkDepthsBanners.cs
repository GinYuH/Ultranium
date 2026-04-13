using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Banner.DarkDepths;

public class DarkDepthsBanners : ModTile
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
        Terraria.ID.TileID.Sets.DisableSmartCursor[Type] = true;
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Banner");
		AddMapEntry(new Color(13, 88, 130), val);
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
				text = "DepthSlime";
				break;
			case 1:
				text = "ShadeBat";
				break;
			case 2:
				text = "DepthCrawler";
				break;
			case 3:
				text = "DepthMonger";
				break;
			case 4:
				text = "ShroomMonster";
				break;
			case 5:
				text = "DepthsMimic";
				break;
			case 6:
				text = "AbyssJelly";
				break;
			case 7:
				text = "AbyssalAngler";
				break;
			case 8:
				text = "AbyssShark";
				break;
			case 9:
				text = "AbyssEel";
				break;
			}
			//localPlayer.NPCBannerBuff[Mod.Find<ModNPC>(text).Type] = true;
			//localPlayer.hasBanner = true;
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
