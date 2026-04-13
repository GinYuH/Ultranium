using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.ShadowEvent;

public class ShadowEventWorld : ModSystem
{
	public static bool ShadowEventActive;

	public static bool StartShadowEvent;

	public static bool ShadowInvasionJustFinished;

	public static bool MindFlayer;

	public static bool Erebus;

	public static bool Phase2;

	public static int EventTimer;

	public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
	{
		Main.invasionSize = 0;
		ShadowEventActive = false;
		StartShadowEvent = false;
		ShadowInvasionJustFinished = false;
		MindFlayer = false;
		Erebus = false;
		Phase2 = false;
		EventTimer = 0;
	}

	public static void ErebusWarnings()
	{
		string text = "";
		if (EventTimer == 6300)
		{
			text = "A strange roar echos in the distance...";
			Ultranium.seizureAmount = 10f;
		}
		if (EventTimer == 18900)
		{
			text = "The otherworldly roar grows louder...";
			Ultranium.seizureAmount = 20f;
		}
		if (Main.netMode == 0)
		{
			Main.NewText(text, (byte)61, byte.MaxValue, (byte)142);
		}
		else if (Main.netMode == 2)
		{
			NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f);
		}
	}

	public override void PostUpdateWorld()
	{
		if (!ShadowEventActive)
		{
			return;
		}
		ShadowEvent.UpdateInvasion();
		Main.time = 0.0;
		if (!NPC.AnyNPCs(Mod.Find<ModNPC>("ErebusHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("MindFlayer").Type))
		{
			EventTimer++;
		}
		if (EventTimer == 6300)
		{
			ErebusWarnings();
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/ErebusRoar"));
		}
		if (EventTimer == 18900)
		{
			ErebusWarnings();
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/ErebusRoar"));
		}
		for (int i = 0; i < Main.player.Length; i++)
		{
			Player player = Main.player[i];
			if (EventTimer == 12600 && !MindFlayer)
			{
				if (Main.netMode == 0)
				{
					Projectile.NewProjectile(new EntitySource_WorldEvent() , player.Center.X, player.Center.Y - 200f, 0f, 0f, Mod.Find<ModProjectile>("MindFlayerSpawner").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.netMode == 2)
				{
					NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("MindFlayer").Type);
				}
				MindFlayer = true;
			}
			if (EventTimer == 24660 && !Erebus)
			{
				if (Main.netMode == 0)
				{
					Projectile.NewProjectile(new EntitySource_WorldEvent(), player.Center.X, player.Center.Y - 200f, 0f, 0f, Mod.Find<ModProjectile>("ErebusSpawner").Type, 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.netMode == 2)
				{
					NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("ErebusHead").Type);
				}
				Erebus = true;
			}
			if (Main.netMode == 0 && !NPC.AnyNPCs(Mod.Find<ModNPC>("ErebusHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("MindFlayer").Type) && !ShadowEventSpawns.DisabledSpawns && Main.rand.Next(600) == 0)
			{
				Projectile.NewProjectile(new EntitySource_WorldEvent(), player.Center + Main.rand.NextVector2Square(-750f, 750f), Main.rand.NextVector2Square(-1f, 1f), Mod.Find<ModProjectile>("ShadowPortalSpawner").Type, 0, 6f, player.whoAmI, 0f, 0f);
			}
			if (((Entity)player).active)
			{
				if (!Phase2)
				{
					Lighting.GlobalBrightness = 0.8f;
				}
				if (Phase2)
				{
					Lighting.GlobalBrightness = 0.68f;
					player.AddBuff(80, 1, quiet: false);
				}
			}
		}
	}
}
