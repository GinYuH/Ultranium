using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.ShadowEvent;

public class ShadowEvent
{
	public static Mod mod = ModLoader.GetMod("Ultranium");

	public static int[] NPCs = new int[13]
	{
		mod.NPCType("Scp2521"),
		mod.NPCType("AbyssalWraith"),
		mod.NPCType("ShadeSpirit"),
		mod.NPCType("Phantom"),
		mod.NPCType("FlayerWraith"),
		mod.NPCType("ShadeMass"),
		mod.NPCType("AbyssalCultist"),
		mod.NPCType("Warden"),
		mod.NPCType("MindFlayer"),
		mod.NPCType("MotherPhantom"),
		mod.NPCType("ErebusHead"),
		mod.NPCType("ErebusBody"),
		mod.NPCType("ErebusTail")
	};

	public static void UpdateInvasion()
	{
		if (ShadowEventWorld.ShadowEventActive && (ShadowEventWorld.EventTimer > 25200 || Main.dayTime))
		{
			Main.invasionType = 0;
			Main.invasionDelay = 0;
			UltraniumWorld.downedShadowEvent = true;
			ShadowEventWorld.ShadowEventActive = false;
			ShadowEventWorld.ShadowInvasionJustFinished = true;
			ShadowEventWorld.MindFlayer = false;
			ShadowEventWorld.Erebus = false;
			ShadowEventWorld.Phase2 = false;
			ShadowEventWorld.EventTimer = 0;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
			string text = "The darkness fades...";
			if (Main.netMode == 0)
			{
				Main.NewText(text, (byte)61, byte.MaxValue, (byte)142, false);
			}
			else if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f);
			}
		}
	}
}
