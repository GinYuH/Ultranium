using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Ultranium.Backgrounds.Boss;

public class FlameScreenShaderData : ScreenShaderData
{
	private int FlameIndex;

	public FlameScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateFlameIndex()
	{
		int num = ModLoader.GetMod("Ultranium").Find<ModNPC>("Ignodium").Type;
		if (FlameIndex >= 0 && ((Entity)Main.npc[FlameIndex]).active && Main.npc[FlameIndex].type == num)
		{
			return;
		}
		FlameIndex = -1;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == num)
			{
				FlameIndex = i;
				break;
			}
		}
	}

	public override void Apply()
	{
		UpdateFlameIndex();
		if (FlameIndex != -1)
		{
			UseTargetPosition(Main.npc[FlameIndex].Center);
		}
		base.Apply();
	}
}
