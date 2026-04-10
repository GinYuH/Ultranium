using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Items.BossSummon;
using Ultranium.NPCs.Town.Shrooms;

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
			shopName = "Shop";
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
			Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_FromThis(), Mod.Find<ModItem>("RealityBendingShroom").Type, 1);
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

    public override void AddShops()
    {
		NPCShop shop = new NPCShop(Type);
		shop.Add(new Item(500) { shopCustomPrice = 6, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID });
		shop.Add(new Item(43) { shopCustomPrice = 8, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedEyeOfCthulhu);
		shop.Add(new Item(ModContent.ItemType<BloodMoonSummon>()) { shopCustomPrice = 8, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedEyeOfCthulhu);
		shop.Add(new Item(70) { shopCustomPrice = 10, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedEowOrBoc);
		shop.Add(new Item(1331) { shopCustomPrice = 10, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedEowOrBoc);
		shop.Add(new Item(361) { shopCustomPrice = 10, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedEowOrBoc);
		shop.Add(new Item(ModContent.ItemType<CoralBait>()) { shopCustomPrice = 11, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, new Condition("Zephyr", () => UltraniumWorld.downedSquid));
		shop.Add(new Item(1133) { shopCustomPrice = 12, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedQueenBee);
		shop.Add(new Item(1307) { shopCustomPrice = 13, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedSkeletron);
		shop.Add(new Item(ModContent.ItemType<IceFood>() ) { shopCustomPrice = 15, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, new Condition("Dragon", () => UltraniumWorld.downedDragon));
        shop.Add(new Item(267) { shopCustomPrice = 14, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.Hardmode);
		shop.Add(new Item(1315) { shopCustomPrice = 15, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.Hardmode);
		shop.Add(new Item(ModContent.ItemType<DreadBeacon>() ) { shopCustomPrice = 15, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, new Condition("Dread", () => UltraniumWorld.downedDread));
        shop.Add(new Item(602) { shopCustomPrice = 15, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedDestroyer);
		shop.Add(new Item(544) { shopCustomPrice = 15, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedMechBossAll);
		shop.Add(new Item(556) { shopCustomPrice = 15, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedMechBossAll);
		shop.Add(new Item(557) { shopCustomPrice = 15, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedMechBossAll);
		shop.Add(new Item(2767) { shopCustomPrice = 15, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedPlantera);
		shop.Add(new Item(1844) { shopCustomPrice = 20, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedPlantera);
		shop.Add(new Item(1958) { shopCustomPrice = 20, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedPlantera);
		shop.Add(new Item(ModContent.ItemType<EtherealLantern>() ) { shopCustomPrice = 22, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, new Condition("Xenanis", () => UltraniumWorld.downedXenanis));
        shop.Add(new Item(1293) { shopCustomPrice = 25, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedGolem);
		shop.Add(new Item(ModContent.ItemType<MiniProbe>() ) { shopCustomPrice = 25, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedGolem);
		shop.Add(new Item(2673) { shopCustomPrice = 30, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedDukeFishron);
		shop.Add(new Item(3601) { shopCustomPrice = 45, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, Condition.DownedMoonLord);
		shop.Add(new Item(ModContent.ItemType<RealityBendingShroom>() ) { shopCustomPrice = 99, shopSpecialCurrency = Ultranium.GlowShroomCurrencyID }, new Condition("Sell Final Shroom", () => SellFinalShroom));
		shop.Register();
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
