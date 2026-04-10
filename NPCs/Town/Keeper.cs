using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town;

[AutoloadHead]
public class Keeper : ModNPC
{
	public static bool CanSpawnAldin;

	public static bool ShouldSpawnAldin;

	public static bool SellFinalShroom;

	public static int SpawnTimer;

	public override string Texture => "Ultranium/NPCs/Town/Keeper";

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Keeper");
		Main.npcFrameCount[NPC.type] = 25;
	}

	public override void SetDefaults()
	{
		NPC.townNPC = true;
		NPC.friendly = true;
		NPC.width = 18;
		NPC.height = 40;
		NPC.aiStyle = 7;
		NPC.damage = 10;
		NPC.defense = 30;
		NPC.lifeMax = 500;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.knockBackResist = 0.5f;
		NPCID.Sets.AttackFrameCount[NPC.type] = 4;
		NPCID.Sets.DangerDetectRange[NPC.type] = 700;
		NPCID.Sets.AttackType[NPC.type] = 0;
		NPCID.Sets.AttackTime[NPC.type] = 90;
		NPCID.Sets.AttackAverageChance[NPC.type] = 30;
		base.AnimationType = 17;
	}

	public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
	{
		return Main.player.Any((Player x) => ((Entity)x).active && x.inventory.Any((Item y) => y.type == 73) && NPC.CountNPCS(Mod.Find<ModNPC>("Aldin").Type) < 1);
	}

	public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
	{
		string[] array = new string[1] { "Aldin" };
		return new List<string>(array);
    }

	public override string GetChat()
	{
		List<string> list = new List<string>
		{
			"I'm not that short shut up.", "Now this do indeed be a bruh moment.", "Yes, i'll have two number nines, a number eight large, and 2 number sevens with a 2 liter coke.", "you ever slap someone so hard that YOU explode?", "yo this ugly ass dog once told me this story about mcdonalds and thought it was neat. It's too long for any mortal to handle though.", "I have indeed been in a number of cults, both as a leader and a follower. You should see some of the stuff people worship.", "I still owe the flying spaghetti monster 21 million.", "I tried telling Glacieron a joke once. All he did was roar and try to eat me, how rude.", "Don't tell Ultrum this but I like Ignodium better lowkey.", "Got dam they done squinch bop dirty",
			"I can sell you boss summons, but your money is worthless, I want the funny glowing mushrooms. Eating them makes me feel happy and tingly."
		};
		return Utils.Next<string>(Main.rand, (IList<string>)list);
	}

	public override void SetChatButtons(ref string button, ref string button2)
	{
		button = Language.GetTextValue("LegacyInterface.28");
		if (Main.LocalPlayer.HasItem(Mod.Find<ModItem>("StrangeUndergrowth").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("SoulCrushingDisappointment").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("TruffleShroom").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("ExistentialDread").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("TheFart").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("Moorhsum").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("SolarShroom").Type) && UltraniumWorld.Moorhsum && UltraniumWorld.StrangeUndergrowth && UltraniumWorld.SoulCrushingDisappointment && UltraniumWorld.TheFart && UltraniumWorld.TruffleShroom && UltraniumWorld.SolarShroom && UltraniumWorld.ExistentialDread)
		{
			button2 = "Mushrooms";
		}
		if (Main.LocalPlayer.HasItem(Mod.Find<ModItem>("RealityBendingShroom").Type) && UltraniumWorld.downedErebus && UltraniumWorld.Moorhsum && UltraniumWorld.StrangeUndergrowth && UltraniumWorld.SoulCrushingDisappointment && UltraniumWorld.TheFart && UltraniumWorld.TruffleShroom && UltraniumWorld.SolarShroom && UltraniumWorld.ExistentialDread)
		{
			button2 = "???";
		}
	}

	public override void OnChatButtonClicked(bool firstButton, ref string shopName)
	{
		if (firstButton)
		{
			shop = true;
		}
		else if (Main.LocalPlayer.HasItem(Mod.Find<ModItem>("StrangeUndergrowth").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("SoulCrushingDisappointment").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("TruffleShroom").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("ExistentialDread").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("TheFart").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("Moorhsum").Type) && Main.LocalPlayer.HasItem(Mod.Find<ModItem>("SolarShroom").Type) && UltraniumWorld.Moorhsum && UltraniumWorld.StrangeUndergrowth && UltraniumWorld.SoulCrushingDisappointment && UltraniumWorld.TheFart && UltraniumWorld.TruffleShroom && UltraniumWorld.SolarShroom && UltraniumWorld.ExistentialDread)
		{
			Main.npcChatText = "djswfsdegerdshtr what?? How did you even find these? I literally had to search across multiple dimensions to find them and you find all of them. phenomenal.";
			int num = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("StrangeUndergrowth").Type);
			int num2 = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("SoulCrushingDisappointment").Type);
			int num3 = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("TruffleShroom").Type);
			int num4 = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("ExistentialDread").Type);
			int num5 = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("TheFart").Type);
			int num6 = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("Moorhsum").Type);
			int num7 = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("SolarShroom").Type);
			Main.LocalPlayer.inventory[num].TurnToAir();
			Main.LocalPlayer.inventory[num2].TurnToAir();
			Main.LocalPlayer.inventory[num3].TurnToAir();
			Main.LocalPlayer.inventory[num4].TurnToAir();
			Main.LocalPlayer.inventory[num5].TurnToAir();
			Main.LocalPlayer.inventory[num6].TurnToAir();
			Main.LocalPlayer.inventory[num7].TurnToAir();
			Main.LocalPlayer.QuickSpawnItem(Mod.Find<ModItem>("RealityBendingShroom").Type, 1);
		}
		else if (Main.LocalPlayer.HasItem(Mod.Find<ModItem>("RealityBendingShroom").Type) && NPC.CountNPCS(Mod.Find<ModNPC>("Aldin").Type) < 1 && UltraniumWorld.Moorhsum && UltraniumWorld.StrangeUndergrowth && UltraniumWorld.SoulCrushingDisappointment && UltraniumWorld.TheFart && UltraniumWorld.TruffleShroom && UltraniumWorld.SolarShroom && UltraniumWorld.ExistentialDread)
		{
			Main.npcChatText = "Y E S";
			int num8 = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("RealityBendingShroom").Type);
			Main.LocalPlayer.inventory[num8].TurnToAir();
			CanSpawnAldin = true;
			ShouldSpawnAldin = true;
		}
	}

	public override void AI()
	{
		if (!CanSpawnAldin || !ShouldSpawnAldin || !((Entity)NPC).active)
		{
			return;
		}
		SpawnTimer++;
		if (SpawnTimer < 120)
		{
			int num = 15;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)NPC.width / 5f, NPC.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + NPC.Center;
				Vector2 vector2 = vector - NPC.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, Mod.Find<ModDust>("StellarDust").Type, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector2) * 2f;
				obj.fadeIn = 1.3f;
			}
		}
		if (SpawnTimer == 180)
		{
			NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("Aldin").Type, 0, 0f, 0f, 0f, 0f, 255);
			Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShockWave").Type, 0, 0f, 255, 0f, 0f);
			Ultranium.seizureAmount = 20f;
			Main.NewText("Aldin's true form has been unleashed!", (byte)175, (byte)75, byte.MaxValue);
			SpawnTimer = 0;
			((Entity)NPC).active = false;
			ShouldSpawnAldin = false;
			SellFinalShroom = true;
		}
	}

	public override void ModifyActiveShop(string shopName, Item[] items)
	{
		if (NPC.downedSlimeKing)
		{
			shop.item[nextSlot].SetDefaults(560, false);
			shop.item[nextSlot].shopCustomPrice = 6;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedBoss1)
		{
			shop.item[nextSlot].SetDefaults(43, false);
			shop.item[nextSlot].shopCustomPrice = 8;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModLoader.GetMod("Ultranium").Find<ModItem>("BloodMoonSummon").Type, false);
			shop.item[nextSlot].shopCustomPrice = 8;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedBoss2)
		{
			shop.item[nextSlot].SetDefaults(70, false);
			shop.item[nextSlot].shopCustomPrice = 10;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(1331, false);
			shop.item[nextSlot].shopCustomPrice = 10;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(361, false);
			shop.item[nextSlot].shopCustomPrice = 10;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (UltraniumWorld.downedSquid)
		{
			shop.item[nextSlot].SetDefaults(ModLoader.GetMod("Ultranium").Find<ModItem>("CoralBait").Type, false);
			shop.item[nextSlot].shopCustomPrice = 11;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedQueenBee)
		{
			shop.item[nextSlot].SetDefaults(1133, false);
			shop.item[nextSlot].shopCustomPrice = 12;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedBoss3)
		{
			shop.item[nextSlot].SetDefaults(1307, false);
			shop.item[nextSlot].shopCustomPrice = 13;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (UltraniumWorld.downedDragon)
		{
			shop.item[nextSlot].SetDefaults(ModLoader.GetMod("Ultranium").Find<ModItem>("IceFood").Type, false);
			shop.item[nextSlot].shopCustomPrice = 14;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (Main.hardMode)
		{
			shop.item[nextSlot].SetDefaults(267, false);
			shop.item[nextSlot].shopCustomPrice = 14;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(1315, false);
			shop.item[nextSlot].shopCustomPrice = 15;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (UltraniumWorld.downedDread)
		{
			shop.item[nextSlot].SetDefaults(ModLoader.GetMod("Ultranium").Find<ModItem>("DreadBeacon").Type, false);
			shop.item[nextSlot].shopCustomPrice = 15;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedMechBoss1)
		{
			shop.item[nextSlot].SetDefaults(602, false);
			shop.item[nextSlot].shopCustomPrice = 15;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
		{
			shop.item[nextSlot].SetDefaults(544, false);
			shop.item[nextSlot].shopCustomPrice = 15;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(556, false);
			shop.item[nextSlot].shopCustomPrice = 15;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(557, false);
			shop.item[nextSlot].shopCustomPrice = 15;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedPlantBoss)
		{
			shop.item[nextSlot].SetDefaults(2767, false);
			shop.item[nextSlot].shopCustomPrice = 15;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(1844, false);
			shop.item[nextSlot].shopCustomPrice = 20;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(1958, false);
			shop.item[nextSlot].shopCustomPrice = 20;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (UltraniumWorld.downedXenanis)
		{
			shop.item[nextSlot].SetDefaults(ModLoader.GetMod("Ultranium").Find<ModItem>("EtherealLantern").Type, false);
			shop.item[nextSlot].shopCustomPrice = 22;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedGolemBoss)
		{
			shop.item[nextSlot].SetDefaults(1293, false);
			shop.item[nextSlot].shopCustomPrice = 25;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ModLoader.GetMod("Ultranium").Find<ModItem>("MiniProbe").Type, false);
			shop.item[nextSlot].shopCustomPrice = 25;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedFishron)
		{
			shop.item[nextSlot].SetDefaults(2673, false);
			shop.item[nextSlot].shopCustomPrice = 30;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (NPC.downedMoonlord)
		{
			shop.item[nextSlot].SetDefaults(3601, false);
			shop.item[nextSlot].shopCustomPrice = 45;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
		if (SellFinalShroom)
		{
			shop.item[nextSlot].SetDefaults(ModLoader.GetMod("Ultranium").Find<ModItem>("RealityBendingShroom").Type, false);
			shop.item[nextSlot].shopCustomPrice = 99;
			shop.item[nextSlot].shopSpecialCurrency = Ultranium.GlowShroomCurrencyID;
			nextSlot++;
		}
	}

	public override void TownNPCAttackStrength(ref int damage, ref float knockback)
	{
		damage = 45;
		knockback = 6f;
	}

	public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
	{
		cooldown = 5;
		randExtraCooldown = 5;
	}

	public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
	{
		projType = Mod.Find<ModProjectile>("CosmicBolt").Type;
		attackDelay = 1;
	}

	public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
	{
		multiplier = 10f;
		randomOffset = 2f;
	}
}
