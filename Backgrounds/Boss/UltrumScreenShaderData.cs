using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Ultranium.Backgrounds.Boss;

public class UltrumScreenShaderData : ScreenShaderData
{
	private int UltrumIndex;

	public UltrumScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateUltrumIndex()
	{
		int num = ModLoader.GetMod("Ultranium").NPCType("Ultrum");
		if (UltrumIndex >= 0 && ((Entity)Main.npc[UltrumIndex]).active && Main.npc[UltrumIndex].type == num)
		{
			return;
		}
		UltrumIndex = -1;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == num)
			{
				UltrumIndex = i;
				break;
			}
		}
	}

	public override void Apply()
	{
		UpdateUltrumIndex();
		if (UltrumIndex != -1)
		{
			UseTargetPosition(Main.npc[UltrumIndex].Center);
		}
		base.Apply();
	}
}
