using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.ShadowEvent;

public class ShadowEventWorld : ModWorld
{
	public static bool ShadowEventActive;

	public static bool StartShadowEvent;

	public static bool ShadowInvasionJustFinished;

	public static bool MindFlayer;

	public static bool Erebus;

	public static bool Phase2;

	public static int EventTimer;

	public override void Initialize()
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
			Main.NewText(text, (byte)61, byte.MaxValue, (byte)142, false);
		}
		else if (Main.netMode == 2)
		{
			NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f);
		}
	}

	public override void PostUpdate()
	{
		if (!ShadowEventActive)
		{
			return;
		}
		ShadowEvent.UpdateInvasion();
		Main.time = 0.0;
		if (!NPC.AnyNPCs(((ModWorld)this).mod.NPCType("ErebusHead")) && !NPC.AnyNPCs(((ModWorld)this).mod.NPCType("MindFlayer")))
		{
			EventTimer++;
		}
		if (EventTimer == 6300)
		{
			ErebusWarnings();
			Main.PlaySound(((ModWorld)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(0.7f), -1, -1);
		}
		if (EventTimer == 18900)
		{
			ErebusWarnings();
			Main.PlaySound(((ModWorld)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/ErebusRoar")?.WithVolume(1f), -1, -1);
		}
		for (int i = 0; i < Main.player.Length; i++)
		{
			Player player = Main.player[i];
			if (EventTimer == 12600 && !MindFlayer)
			{
				if (Main.netMode == 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y - 200f, 0f, 0f, ((ModWorld)this).mod.ProjectileType("MindFlayerSpawner"), 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.netMode == 2)
				{
					NPC.SpawnOnPlayer(player.whoAmI, ((ModWorld)this).mod.NPCType("MindFlayer"));
				}
				MindFlayer = true;
			}
			if (EventTimer == 24660 && !Erebus)
			{
				if (Main.netMode == 0)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y - 200f, 0f, 0f, ((ModWorld)this).mod.ProjectileType("ErebusSpawner"), 0, 1f, Main.myPlayer, 0f, 0f);
				}
				if (Main.netMode == 2)
				{
					NPC.SpawnOnPlayer(player.whoAmI, ((ModWorld)this).mod.NPCType("ErebusHead"));
				}
				Erebus = true;
			}
			if (Main.netMode == 0 && !NPC.AnyNPCs(((ModWorld)this).mod.NPCType("ErebusHead")) && !NPC.AnyNPCs(((ModWorld)this).mod.NPCType("MindFlayer")) && !ShadowEventSpawns.DisabledSpawns && Main.rand.Next(600) == 0)
			{
				Projectile.NewProjectile(player.Center + Main.rand.NextVector2Square(-750f, 750f), Main.rand.NextVector2Square(-1f, 1f), ((ModWorld)this).mod.ProjectileType("ShadowPortalSpawner"), 0, 6f, player.whoAmI, 0f, 0f);
			}
			if (((Entity)player).active)
			{
				if (!Phase2)
				{
					Lighting.brightness = 0.8f;
				}
				if (Phase2)
				{
					Lighting.brightness = 0.68f;
					player.AddBuff(80, 1, fromNetPvP: true);
				}
			}
		}
	}
}
