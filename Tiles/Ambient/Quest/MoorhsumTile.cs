using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Ultranium.NPCs.Town.Shrooms;

namespace Ultranium.Tiles.Ambient.Quest;

public class MoorhsumTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileCut[Type] = true;
		Main.tileNoFail[Type] = true;
		AddMapEntry(new Color(51, 49, 95), (LocalizedText)null);
		TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
		TileObjectData.newTile.AnchorValidTiles = new int[1] { 70 };
		TileObjectData.newTile.AnchorAlternateTiles = new int[2] { 78, 380 };
		TileObjectData.addTile((int)Type);
		RegisterItemDrop(ModContent.ItemType<Moorhsum>());
    }

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.85f;
		g = 0.2f;
		b = 0.85f;
	}

    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!UltraniumWorld.Moorhsum)
        {
            UltraniumWorld.Moorhsum = true;
            UltraniumWorld.StrangeUndergrowth = true;
            if (Main.netMode == 2)
            {
                NetMessage.SendData(7);
            }
        }
    }

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
	{
		if (i % 2 == 1)
		{
			spriteEffects = SpriteEffects.FlipHorizontally;
		}
	}

	public override void RandomUpdate(int i, int j)
	{
		if (Main.tile[i, j].TileFrameX == 0)
		{
			Main.tile[i, j].TileFrameX += 18;
		}
		else if (Main.tile[i, j].TileFrameX == 18)
		{
			Main.tile[i, j].TileFrameX += 18;
		}
	}
}
