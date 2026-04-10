using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Ultranium.NPCs.TrueDread;

namespace Ultranium.Backgrounds.TrueDread;

public class TrueDreadScreenShaderData : ScreenShaderData
{
	private int TrueDreadIndex;

	public TrueDreadScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateTrueDreadIndex()
	{
		int num = ModContent.NPCType<global::Ultranium.NPCs.TrueDread.TrueDread>();
		if (TrueDreadIndex >= 0 && ((Entity)Main.npc[TrueDreadIndex]).active && Main.npc[TrueDreadIndex].type == num)
		{
			return;
		}
		TrueDreadIndex = -1;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == num)
			{
				TrueDreadIndex = i;
				break;
			}
		}
	}

	public override void Apply()
	{
		UpdateTrueDreadIndex();
		if (TrueDreadIndex != -1)
		{
			UseTargetPosition(Main.npc[TrueDreadIndex].Center);
		}
		base.Apply();
	}
}
