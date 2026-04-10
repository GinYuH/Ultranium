using Terraria.World.Generation;

namespace Ultranium;

public class IsSloped : GenCondition
{
	protected override bool CheckValidity(int x, int y)
	{
		if (GenBase._tiles[x, y].active())
		{
			if (GenBase._tiles[x, y].slope() <= 0)
			{
				return GenBase._tiles[x, y].halfBrick();
			}
			return true;
		}
		return false;
	}
}
