using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class DepthSnailCage : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileLighted[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.StyleSmallCage);
		TileObjectData.addTile((int)Type);
		base.AnimationFrameHeight = 36;
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Depth Snail Cage");
		AddMapEntry(new Color(122, 217, 232), val);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		Tile tile = Main.tile[i, j];
		Main.critterCage = true;
		int num = i - tile.TileFrameX / 18;
		int num2 = j - tile.TileFrameY / 18;
		int num3 = num / 3 * (num2 / 3);
		num3 %= Main.cageFrames;
		frameYOffset = Main.snail2CageFrame[num3] * base.AnimationFrameHeight;
	}
}
