using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Banner.DarkDepths;

public class DarkDepthsBanners : ModTile
{
	public override void SetDefaults()
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
		base.dustType = -1;
		base.disableSmartCursor = true;
		ModTranslation val = ((ModTile)this).CreateMapEntryName((string)null);
		val.SetDefault("Banner");
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
			text = "DepthSlimeBanner";
			break;
		case 1:
			text = "ShadeBatBanner";
			break;
		case 2:
			text = "DepthCrawlerBanner";
			break;
		case 3:
			text = "DepthMongerBanner";
			break;
		case 4:
			text = "ShroomMonsterBanner";
			break;
		case 5:
			text = "DepthsMimicBanner";
			break;
		case 6:
			text = "AbyssJellyBanner";
			break;
		case 7:
			text = "AbyssalAnglerBanner";
			break;
		case 8:
			text = "AbyssSharkBanner";
			break;
		case 9:
			text = "AbyssEelBanner";
			break;
		}
		Item.NewItem(i * 16, j * 16, 16, 48, ((ModTile)this).mod.ItemType(text), 1, false, 0, false, false);
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer)
		{
			Player localPlayer = Main.LocalPlayer;
			string text;
			switch (Main.tile[i, j].frameX / 18)
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
			localPlayer.NPCBannerBuff[((ModTile)this).mod.NPCType(text)] = true;
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
