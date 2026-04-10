using Terraria;

namespace Ultranium;

public class TexGen
{
	public int width;

	public int height;

	public TileInfo[,] tileGen;

	public int torchStyle;

	public int platformStyle;

	public TexGen(int w, int h)
	{
		width = w;
		height = h;
		tileGen = new TileInfo[width, height];
	}

	public void Generate(int x, int y, bool silent, bool sync)
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				int num = x + i;
				int num2 = y + j;
				TileInfo tileInfo = tileGen[i, j];
				if (tileInfo.liquidType == -2)
				{
					tileInfo.liquidType = 0;
					tileInfo.liquidAmt = 0;
				}
				if (tileInfo.tileID != -1 || tileInfo.wallID != -1 || tileInfo.liquidType != -1 || tileInfo.wire != -1)
				{
					if (tileInfo.tileID != -1 || tileInfo.wallID > -1 || tileInfo.wire > -1)
					{
						BaseWorldGen.GenerateTile(num, num2, tileInfo.tileID, tileInfo.wallID, (tileInfo.tileStyle != 0) ? tileInfo.tileStyle : ((tileInfo.tileID == 4) ? torchStyle : ((tileInfo.tileID == 19) ? platformStyle : 0)), tileInfo.tileID > -1, tileInfo.liquidAmt == 0, tileInfo.slope, silent: false, sync);
					}
					if (tileInfo.liquidType != -1)
					{
						BaseWorldGen.GenerateLiquid(num, num2, tileInfo.liquidType, updateFlow: false, tileInfo.liquidAmt, sync);
					}
					if (tileInfo.objectID != 0)
					{
						WorldGen.PlaceObject(num, num2, tileInfo.objectID);
						NetMessage.SendObjectPlacement(-1, num, num2, tileInfo.objectID, 0, 0, -1, -1);
					}
				}
			}
		}
	}
}
