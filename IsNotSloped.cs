using Terraria.World.Generation;

namespace Ultranium;

public class IsNotSloped : GenCondition
{
	protected override bool CheckValidity(int x, int y)
	{
		if (GenBase._tiles[x, y].active() && GenBase._tiles[x, y].slope() == 0)
		{
			return !GenBase._tiles[x, y].halfBrick();
		}
		return false;
	}
}
