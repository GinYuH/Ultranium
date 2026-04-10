using Terraria;
using Terraria.Graphics.Shaders;
using Ultranium.ShadowEvent;

namespace Ultranium.Backgrounds.ShadowEventSky;

public class ShadowEventScreenShaderData : ScreenShaderData
{
	public ShadowEventScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateShadowEventIndex()
	{
	}

	public override void Apply()
	{
		UpdateShadowEventIndex();
		if (!Main.dayTime && ShadowEventWorld.ShadowEventActive && !ShadowEventWorld.Phase2)
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				UseTargetPosition(Main.player[Player.FindClosest(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height)].Center);
			}
		}
		base.Apply();
	}
}
