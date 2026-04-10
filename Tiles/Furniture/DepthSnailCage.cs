using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class DepthSnailCage : ModTile
{
	public override void SetDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.animationFrameHeight = 36;
		ModTranslation val = ((ModTile)this).CreateMapEntryName((string)null);
		val.SetDefault("Depth Snail Cage");
		((ModTile)this).AddMapEntry(new Color(122, 217, 232), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(i * 16, j * 16, 48, 32, ((ModTile)this).mod.ItemType("DepthSnailCageItem"), 1, false, 0, false, false);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		Tile tile = Main.tile[i, j];
		Main.critterCage = true;
		int num = i - tile.frameX / 18;
		int num2 = j - tile.frameY / 18;
		int num3 = num / 3 * (num2 / 3);
		num3 %= Main.cageFrames;
		frameYOffset = Main.snail2CageFrame[num3] * base.animationFrameHeight;
	}
}
