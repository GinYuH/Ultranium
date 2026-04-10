using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Banner.ShadowEvent;

public class ShadowEventBanners : ModTile
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
			text = "AbyssalWraithBanner";
			break;
		case 1:
			text = "ShadeSpiritBanner";
			break;
		case 2:
			text = "PhantomBanner";
			break;
		case 3:
			text = "Scp2521Banner";
			break;
		case 4:
			text = "AbyssalCultistBanner";
			break;
		case 5:
			text = "ShadeMassBanner";
			break;
		case 6:
			text = "FlayerWraithBanner";
			break;
		case 7:
			text = "MotherPhantomBanner";
			break;
		case 8:
			text = "AbyssBruteBanner";
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
				text = "AbyssalWraith";
				break;
			case 1:
				text = "ShadeSpirit";
				break;
			case 2:
				text = "Phantom";
				break;
			case 3:
				text = "Scp2521";
				break;
			case 4:
				text = "AbyssalCultist";
				break;
			case 5:
				text = "ShadeMass";
				break;
			case 6:
				text = "FlayerWraith";
				break;
			case 7:
				text = "MotherPhantom";
				break;
			case 8:
				text = "Warden";
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
