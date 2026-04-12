using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.ShadowEvent;

public class ShadowEvent
{
	public static Mod mod = ModLoader.GetMod("Ultranium");

	public static int[] NPCs = new int[13]
	{
		mod.Find<ModNPC>("Scp2521").Type,
		mod.Find<ModNPC>("AbyssalWraith").Type,
		mod.Find<ModNPC>("ShadeSpirit").Type,
		mod.Find<ModNPC>("Phantom").Type,
		mod.Find<ModNPC>("FlayerWraith").Type,
		mod.Find<ModNPC>("ShadeMass").Type,
		mod.Find<ModNPC>("AbyssalCultist").Type,
		mod.Find<ModNPC>("Warden").Type,
		mod.Find<ModNPC>("MindFlayer").Type,
		mod.Find<ModNPC>("MotherPhantom").Type,
		mod.Find<ModNPC>("ErebusHead").Type,
		mod.Find<ModNPC>("ErebusBody").Type,
		mod.Find<ModNPC>("ErebusTail").Type
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
				Main.NewText(text, (byte)61, byte.MaxValue, (byte)142);
			}
			else if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f);
			}
		}
	}
}
