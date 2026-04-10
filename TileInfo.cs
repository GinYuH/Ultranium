namespace Ultranium;

public class TileInfo
{
	public int tileID = -1;

	public int tileStyle;

	public int wallID = -1;

	public int objectID;

	public int liquidType = -1;

	public int liquidAmt;

	public int slope = -2;

	public int wire = -1;

	public TileInfo(int id, int style, int wid = -1, int lType = -1, int lAmt = 0, int sl = -2, int ob = 0, int w = -1)
	{
		tileID = id;
		tileStyle = style;
		wallID = wid;
		liquidType = lType;
		liquidAmt = lAmt;
		slope = sl;
		objectID = ob;
		wire = w;
	}
}
