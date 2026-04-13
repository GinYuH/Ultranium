using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Ambient.Quest;

public class GlobalTiles : GlobalTile
{
	public override void RandomUpdate(int i, int j, int type)
	{
		if (type == 70 && Framing.GetTileSafely(i, j - 1).TileType == TileID.Dirt && Framing.GetTileSafely(i, j - 2).TileType == TileID.Dirt && Main.rand.Next(200) == 0)
		{
			WorldGen.PlaceObject(i, j - 1, ((GlobalTile)this).Mod.Find<ModTile>("MoorhsumTile").Type);
			NetMessage.SendObjectPlacement(-1, i, j - 1, ((GlobalTile)this).Mod.Find<ModTile>("MoorhsumTile").Type, 0, 0, -1, -1);
		}
		if (type == 2 && Framing.GetTileSafely(i, j - 1).TileType == TileID.Dirt && Framing.GetTileSafely(i, j - 2).TileType == TileID.Dirt && Main.rand.Next(50) == 0)
		{
			WorldGen.PlaceObject(i, j - 1, 254);
			NetMessage.SendObjectPlacement(-1, i, j - 1, 254, 0, 0, -1, -1);
		}
	}
}
