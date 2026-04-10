using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Ultranium.NPCs.ShadowWorm;

namespace Ultranium.Backgrounds.ShadowEventSky;

public class ErebusScreenShaderData : ScreenShaderData
{
	private int ErebusIndex;

	public ErebusScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateErebusIndex()
	{
		int num = ModContent.NPCType<ErebusHead>();
		if (ErebusIndex >= 0 && ((Entity)Main.npc[ErebusIndex]).active && Main.npc[ErebusIndex].type == num)
		{
			return;
		}
		ErebusIndex = -1;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == num)
			{
				ErebusIndex = i;
				break;
			}
		}
	}

	public override void Apply()
	{
		UpdateErebusIndex();
		if (ErebusIndex != -1)
		{
			UseTargetPosition(Main.npc[ErebusIndex].Center);
		}
		base.Apply();
	}
}
