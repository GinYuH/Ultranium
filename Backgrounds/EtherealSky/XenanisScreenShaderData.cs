using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Ultranium.NPCs.Ethereal;

namespace Ultranium.Backgrounds.EtherealSky;

public class XenanisScreenShaderData : ScreenShaderData
{
	private int XenanisIndex;

	public XenanisScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateXenanisIndex()
	{
		int num = ModContent.NPCType<Xenanis>();
		if (XenanisIndex >= 0 && ((Entity)Main.npc[XenanisIndex]).active && Main.npc[XenanisIndex].type == num)
		{
			return;
		}
		XenanisIndex = -1;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == num)
			{
				XenanisIndex = i;
				break;
			}
		}
	}

	public override void Apply()
	{
		UpdateXenanisIndex();
		if (XenanisIndex != -1)
		{
			UseTargetPosition(Main.npc[XenanisIndex].Center);
		}
		base.Apply();
	}
}
