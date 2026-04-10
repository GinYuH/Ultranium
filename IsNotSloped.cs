using Terraria.WorldBuilding;

namespace Ultranium;

public class IsNotSloped : GenCondition
{
	protected override bool CheckValidity(int x, int y)
	{
		if (GenBase._tiles[x, y].HasTile && GenBase._tiles[x, y].Slope == 0)
		{
			return !GenBase._tiles[x, y].IsHalfBlock;
		}
		return false;
	}
}
