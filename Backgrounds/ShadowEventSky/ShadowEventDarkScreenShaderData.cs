using Terraria;
using Terraria.Graphics.Shaders;
using Ultranium.ShadowEvent;

namespace Ultranium.Backgrounds.ShadowEventSky;

public class ShadowEventDarkScreenShaderData : ScreenShaderData
{
	public ShadowEventDarkScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateShadowEventDarkIndex()
	{
	}

	public override void Apply()
	{
		UpdateShadowEventDarkIndex();
		if (!Main.dayTime && ShadowEventWorld.ShadowEventActive && ShadowEventWorld.Phase2)
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				UseTargetPosition(Main.player[Player.FindClosest(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height)].Center);
			}
		}
		base.Apply();
	}
}
