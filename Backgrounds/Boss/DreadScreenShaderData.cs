using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Ultranium.Backgrounds.Boss;

public class DreadScreenShaderData : ScreenShaderData
{
	private int DreadIndex;

	public DreadScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateDreadIndex()
	{
		int num = ModLoader.GetMod("Ultranium").Find<ModNPC>("DreadBoss").Type;
		if (DreadIndex >= 0 && ((Entity)Main.npc[DreadIndex]).active && Main.npc[DreadIndex].type == num)
		{
			return;
		}
		DreadIndex = -1;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == num)
			{
				DreadIndex = i;
				break;
			}
		}
	}

	public override void Apply()
	{
		UpdateDreadIndex();
		if (DreadIndex != -1)
		{
			UseTargetPosition(Main.npc[DreadIndex].Center);
		}
		base.Apply();
	}
}
