using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Ultranium.NPCs.Aldin;

namespace Ultranium.Backgrounds.Cosmic;

public class CosmicScreenShaderData : ScreenShaderData
{
	private int AldinIndex;

	public CosmicScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateAldinIndex()
	{
		int num = ModContent.NPCType<Aldin>();
		if (AldinIndex >= 0 && ((Entity)Main.npc[AldinIndex]).active && Main.npc[AldinIndex].type == num)
		{
			return;
		}
		AldinIndex = -1;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == num)
			{
				AldinIndex = i;
				break;
			}
		}
	}

	public override void Apply()
	{
		UpdateAldinIndex();
		if (AldinIndex != -1)
		{
			UseTargetPosition(Main.npc[AldinIndex].Center);
		}
		base.Apply();
	}
}
