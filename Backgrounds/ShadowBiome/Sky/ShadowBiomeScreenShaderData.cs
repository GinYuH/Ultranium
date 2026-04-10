using Terraria;
using Terraria.Graphics.Shaders;
using Ultranium.ShadowEvent;

namespace Ultranium.Backgrounds.ShadowBiome.Sky;

public class ShadowBiomeScreenShaderData : ScreenShaderData
{
	public ShadowBiomeScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateShadowBiomeIndex()
	{
	}

	public override void Apply()
	{
		UpdateShadowBiomeIndex();
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().ZoneShadow && !ShadowEventWorld.ShadowEventActive)
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				UseTargetPosition(Main.player[Player.FindClosest(Main.npc[i].position, Main.npc[i].width, Main.npc[i].height)].Center);
			}
		}
		base.Apply();
	}
}
