using Terraria.WorldBuilding;

namespace Ultranium;

public class IsSloped : GenCondition
{
	protected override bool CheckValidity(int x, int y)
	{
		if (GenBase._tiles[x, y].HasTile)
		{
			if (GenBase._tiles[x, y].Slope <= 0)
			{
				return GenBase._tiles[x, y].IsHalfBlock;
			}
			return true;
		}
		return false;
	}
}
