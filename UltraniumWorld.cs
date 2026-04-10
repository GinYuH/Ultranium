using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Generation;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using Ultranium.Generation;
using Ultranium.Generation.Shrine;
using Ultranium.ShadowEvent;

namespace Ultranium;

public class UltraniumWorld : ModSystem
{
	public int MessageTimer;

	public int MessageTimer2;

	public static int ShadowTiles;

	public static bool SavedTruffle;

	public static bool Moorhsum;

	public static bool StrangeUndergrowth;

	public static bool SoulCrushingDisappointment;

	public static bool TheFart;

	public static bool TruffleShroom;

	public static bool SolarShroom;

	public static bool ExistentialDread;

	public static bool AllMushrooms;

	public static bool downedSquid;

	public static bool downedDragon;

	public static bool downedDread;

	public static bool downedXenanis;

	public static bool downedUltrum;

	public static bool downedIgnodium;

	public static bool downedTrueDread;

	public static bool downedShadowEvent;

	public static bool downedErebus;

	public static bool downedAldin;

	public static bool ErebusMessage;

	public static bool EtherealMessage;

	public static bool HardmodeMessage;

	public static bool DreadMessage;

	public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
	{
		SavedTruffle = false;
		Moorhsum = false;
		StrangeUndergrowth = false;
		SoulCrushingDisappointment = false;
		TheFart = false;
		TruffleShroom = false;
		SolarShroom = false;
		ExistentialDread = false;
		downedSquid = false;
		downedDragon = false;
		downedDread = false;
		downedXenanis = false;
		downedUltrum = false;
		downedIgnodium = false;
		downedTrueDread = false;
		downedShadowEvent = false;
		downedErebus = false;
		downedAldin = false;
		if (Main.hardMode)
		{
			HardmodeMessage = true;
		}
		else
		{
			HardmodeMessage = false;
		}
		if (NPC.downedPlantBoss)
		{
			EtherealMessage = true;
		}
		else
		{
			EtherealMessage = false;
		}
		if (downedUltrum && downedIgnodium && !downedTrueDread)
		{
			DreadMessage = true;
		}
		else
		{
			DreadMessage = false;
		}
		if (downedTrueDread && !downedShadowEvent && !downedErebus)
		{
			ErebusMessage = true;
		}
		else
		{
			ErebusMessage = false;
		}
		MessageTimer = 0;
		MessageTimer2 = 0;
	}

	public override void SaveWorldData(TagCompound tag)
	{
		List<string> list = new List<string>();
		if (downedSquid)
		{
			list.Add("Squid");
		}
		if (downedDragon)
		{
			list.Add("IceDragon");
		}
		if (downedDread)
		{
			list.Add("Dread");
		}
		if (downedXenanis)
		{
			list.Add("Xenanis");
		}
		if (downedUltrum)
		{
			list.Add("Ultrum");
		}
		if (downedIgnodium)
		{
			list.Add("Ignodium");
		}
		if (downedTrueDread)
		{
			list.Add("TrueDread");
		}
		if (downedShadowEvent)
		{
			list.Add("ShadowEvent");
		}
		if (downedErebus)
		{
			list.Add("Erebus");
		}
		if (downedAldin)
		{
			list.Add("Aldin");
		}
		if (SavedTruffle)
		{
			list.Add("SavedTruffle");
		}
		if (Moorhsum)
		{
			list.Add("Moorhsum");
		}
		if (StrangeUndergrowth)
		{
			list.Add("StrangeUndergrowth");
		}
		if (SoulCrushingDisappointment)
		{
			list.Add("SoulCrushingDisappointment");
		}
		if (TheFart)
		{
			list.Add("TheFart");
		}
		if (TruffleShroom)
		{
			list.Add("TruffleShroom");
		}
		if (SolarShroom)
		{
			list.Add("SolarShroom");
		}
		if (ExistentialDread)
		{
			list.Add("ExistentialDread");
		}
		tag["downed"] = list;
	}

	public override void LoadWorldData(TagCompound tag)
	{
		IList<string> list = tag.GetList<string>("downed");
		downedSquid = list.Contains("Squid");
		downedDragon = list.Contains("IceDragon");
		downedDread = list.Contains("Dread");
		downedXenanis = list.Contains("Xenanis");
		downedUltrum = list.Contains("Ultrum");
		downedIgnodium = list.Contains("Ignodium");
		downedTrueDread = list.Contains("TrueDread");
		downedShadowEvent = list.Contains("ShadowEvent");
		downedErebus = list.Contains("Erebus");
		downedAldin = list.Contains("Aldin");
		SavedTruffle = list.Contains("SavedTruffle");
		Moorhsum = list.Contains("Moorhsum");
		StrangeUndergrowth = list.Contains("StrangeUndergrowth");
		SoulCrushingDisappointment = list.Contains("SoulCrushingDisappointment");
		TheFart = list.Contains("TheFart");
		TruffleShroom = list.Contains("TruffleShroom");
		SolarShroom = list.Contains("SolarShroom");
		ExistentialDread = list.Contains("ExistentialDread");
	}

	public override void NetSend(BinaryWriter writer)
	{
		BitsByte bitsByte = default(BitsByte);
		bitsByte[0] = downedSquid;
		bitsByte[1] = downedDragon;
		bitsByte[2] = downedDread;
		bitsByte[3] = downedXenanis;
		bitsByte[4] = downedUltrum;
		bitsByte[5] = downedIgnodium;
		bitsByte[6] = downedTrueDread;
		writer.Write(bitsByte);
		BitsByte bitsByte2 = default(BitsByte);
		bitsByte2[0] = downedShadowEvent;
		bitsByte2[1] = downedErebus;
		bitsByte2[2] = ShadowEventWorld.ShadowEventActive;
		bitsByte2[3] = downedAldin;
		writer.Write(bitsByte2);
		BitsByte bitsByte3 = default(BitsByte);
		bitsByte3[0] = Moorhsum;
		bitsByte3[1] = StrangeUndergrowth;
		bitsByte3[2] = SoulCrushingDisappointment;
		bitsByte3[3] = TheFart;
		bitsByte3[4] = TruffleShroom;
		bitsByte3[5] = SolarShroom;
		bitsByte3[6] = ExistentialDread;
		writer.Write(bitsByte3);
	}

	public override void NetReceive(BinaryReader reader)
	{
		BitsByte bitsByte = reader.ReadByte();
		downedSquid = bitsByte[0];
		downedDragon = bitsByte[1];
		downedDread = bitsByte[2];
		downedXenanis = bitsByte[3];
		downedUltrum = bitsByte[4];
		downedIgnodium = bitsByte[5];
		downedTrueDread = bitsByte[6];
		BitsByte bitsByte2 = reader.ReadByte();
		downedShadowEvent = bitsByte2[0];
		downedErebus = bitsByte2[1];
		ShadowEventWorld.ShadowEventActive = bitsByte2[2];
		downedAldin = bitsByte2[3];
		BitsByte bitsByte3 = reader.ReadByte();
		Moorhsum = bitsByte3[0];
		StrangeUndergrowth = bitsByte3[1];
		SoulCrushingDisappointment = bitsByte3[2];
		TheFart = bitsByte3[3];
		TruffleShroom = bitsByte3[4];
		SolarShroom = bitsByte3[5];
		ExistentialDread = bitsByte3[6];
	}

	public override void PostUpdateWorld()
	{
		if (NPC.AnyNPCs(Mod.Find<ModNPC>("ErebusHead").Type))
		{
			MoonlordShake(0.8f);
		}
		else
		{
			MoonlordShake(0f);
		}
		if (Main.hardMode && !HardmodeMessage)
		{
			Main.NewText("Something dreadful wanders through out the lands...", (byte)160, (byte)0, (byte)0);
			Main.NewText("Spacial creatures wander in the sky", (byte)66, (byte)197, byte.MaxValue);
			HardmodeMessage = true;
		}
		if (NPC.downedPlantBoss && !EtherealMessage)
		{
			Main.NewText("An ethereal aura draws near...", (byte)137, (byte)131, byte.MaxValue);
			EtherealMessage = true;
		}
		if (downedUltrum && downedIgnodium && !downedTrueDread && !DreadMessage)
		{
			MessageTimer++;
			if (MessageTimer == 600)
			{
				SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DreadRoar"));
				Main.NewText("Fear flows through your veins...", (byte)200, (byte)0, (byte)0);
				DreadMessage = true;
			}
		}
		if (downedTrueDread && !downedShadowEvent && !downedErebus && !ErebusMessage)
		{
			MessageTimer2++;
			if (MessageTimer2 == 600)
            {
                SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/ErebusRoar"));
				Main.NewText("An otherworldy roar echoes through out the world...", (byte)90, (byte)72, (byte)169);
				ErebusMessage = true;
			}
		}
	}

	public override void ResetNearbyTileEffects()
	{
		Main.LocalPlayer.GetModPlayer<UltraniumPlayer>();
		ShadowTiles = 0;
	}

	public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
	{
		ShadowTiles = tileCounts[Mod.Find<ModTile>("ShadowGrass").Type] + tileCounts[Mod.Find<ModTile>("ShadowStoneTile").Type];
	}

	private static void GenGuardianShrine()
	{
		bool flag = false;
		while (!flag)
		{
			int num = WorldGen.genRand.Next(50, Main.maxTilesX / 3);
			if (Utils.NextBool(WorldGen.genRand))
			{
				num = Main.maxTilesX - num;
			}
			int i;
			for (i = 0; !WorldGen.SolidTile(num, i) && (double)i <= Main.worldSurface; i++)
			{
			}
			if (!((double)i > Main.worldSurface))
			{
				Tile tile = Main.tile[num, i];
				if (tile.TileType == 0 || tile.TileType == 2 || tile.TileType == 1)
				{
					GuardianShrine.Generate(num, i - 22);
					flag = true;
				}
			}
		}
	}

	private static void GenShadow()
	{
		Mod Mod = ModLoader.GetMod("Ultranium");
		int num = (int)((float)Main.maxTilesX * 0f);
		int num2 = (int)((float)Main.maxTilesY * 0f);
		int num3 = 0;
		if (Main.maxTilesX == 4200 && Main.maxTilesY == 1200)
		{
			num = (int)((float)Main.maxTilesX * 0.32f);
			num2 = (int)((float)Main.maxTilesY * 0.29f);
			num3 = 190;
		}
		else if (Main.maxTilesX == 6400 && Main.maxTilesY == 1800)
		{
			num = (int)((float)Main.maxTilesX * 0.32f);
			num2 = (int)((float)Main.maxTilesY * 0.3f);
			num3 = 240;
		}
		else if (Main.maxTilesX == 8400 && Main.maxTilesY == 2400)
		{
			num = (int)((float)Main.maxTilesX * 0.32f);
			num2 = (int)((float)Main.maxTilesY * 0.3f);
			num3 = 300;
		}
		else
		{
			num = (int)((float)Main.maxTilesX * 0.32f);
			num2 = (int)((float)Main.maxTilesY * 0.3f);
			num3 = 300;
		}
		int num4 = num;
		int num5 = num2;
		num4 -= 100;
		num5 -= 100;
		num5++;
		for (int i = num4 - num3; i <= num4 + num3; i++)
		{
			for (int j = num5 - num3; j <= num5 + num3; j++)
			{
				if (Vector2.Distance(new Vector2(num4, num5), new Vector2(i, j)) <= (float)num3)
				{
					if (Main.tile[i, j].WallType == 15 || Main.tile[i, j].WallType == 59 || Main.tile[i, j].WallType == 2)
					{
						Main.tile[i, j].WallType = (ushort)Mod.Find<ModWall>("ShadowWall").Type;
					}
					if (Main.tile[i, j].WallType == 64 || Main.tile[i, j].WallType == 67 || Main.tile[i, j].WallType == 63 || Main.tile[i, j].WallType == 66 || Main.tile[i, j].WallType == 65 || Main.tile[i, j].WallType == 68 || Main.tile[i, j].WallType == 69 || Main.tile[i, j].WallType == 81)
					{
						Main.tile[i, j].WallType = (ushort)Mod.Find<ModWall>("ShadowGrassWall").Type;
					}
					if (Main.tile[i, j].WallType == 83 || Main.tile[i, j].WallType == 3 || Main.tile[i, j].WallType == 40 || Main.tile[i, j].WallType == 1)
					{
						Main.tile[i, j].WallType = (ushort)Mod.Find<ModWall>("ShadowStoneWall").Type;
					}
					if (Main.tile[i, j].TileType == 60 || Main.tile[i, j].TileType == 59 || Main.tile[i, j].TileType == 147 || Main.tile[i, j].TileType == 53 || Main.tile[i, j].TileType == 112 || Main.tile[i, j].TileType == 234 || Main.tile[i, j].TileType == 199 || Main.tile[i, j].TileType == 59 || Main.tile[i, j].TileType == 23 || Main.tile[i, j].TileType == 0 || Main.tile[i, j].TileType == 2)
					{
						Main.tile[i, j].TileType = (ushort)Mod.Find<ModTile>("ShadowGrass").Type;
					}
					if (Main.tile[i, j].TileType == 1 || Main.tile[i, j].TileType == 25 || Main.tile[i, j].TileType == 203 || Main.tile[i, j].TileType == 163 || Main.tile[i, j].TileType == 200 || Main.tile[i, j].TileType == 161)
					{
						Main.tile[i, j].TileType = (ushort)Mod.Find<ModTile>("ShadowStoneTile").Type;
					}
					if (Main.tile[i, j].TileType == 6 || Main.tile[i, j].TileType == 7 || Main.tile[i, j].TileType == 8 || Main.tile[i, j].TileType == 9 || Main.tile[i, j].TileType == 221 || Main.tile[i, j].TileType == 222 || Main.tile[i, j].TileType == 223 || Main.tile[i, j].TileType == 204 || Main.tile[i, j].TileType == 166 || Main.tile[i, j].TileType == 167 || Main.tile[i, j].TileType == 168 || Main.tile[i, j].TileType == 169 || Main.tile[i, j].TileType == 221 || Main.tile[i, j].TileType == 107 || Main.tile[i, j].TileType == 108 || Main.tile[i, j].TileType == 22 || Main.tile[i, j].TileType == 111 || Main.tile[i, j].TileType == 123 || Main.tile[i, j].TileType == 224 || Main.tile[i, j].TileType == 40 || Main.tile[i, j].TileType == 59)
					{
						Main.tile[i, j].TileType = (ushort)Mod.Find<ModTile>("ShadowOreTile").Type;
					}
					if (Main.tile[i, j].LiquidAmount == 3)
					{
						WorldGen.PlaceTile(i, j, 162);
					}
				}
			}
		}
		for (int k = 0; k < 1000; k++)
		{
			int num6 = WorldGen.genRand.Next(0, Main.maxTilesX);
			int num7 = WorldGen.genRand.Next(0, Main.maxTilesY);
			if (Main.tile[num6, num7].TileType == Mod.Find<ModTile>("ShadowGrass").Type || Main.tile[num6, num7].TileType == Mod.Find<ModTile>("ShadowStoneTile").Type)
			{
				WorldGen.TileRunner(num6, num7, (double)WorldGen.genRand.Next(15, 15), WorldGen.genRand.Next(12, 15), (int)(ushort)Mod.Find<ModTile>("ShadowOreTile").Type, false, 0f, 0f, false, true);
			}
		}
		for (int l = 0; l < Main.maxTilesX; l++)
		{
			for (int m = 0; m < Main.maxTilesY; m++)
			{
				if (Main.tile[l, m].TileType == Mod.Find<ModTile>("ShadowGrass").Type && (!Main.tile[l + 1, m].HasTile || !Main.tile[l, m - 1].HasTile || !Main.tile[l - 1, m].HasTile || !Main.tile[l, m + 1].HasTile))
				{
					Main.tile[l, m].TileType = (ushort)Mod.Find<ModTile>("ShadowGrass").Type;
				}
			}
		}
	}

	private static void GenDepths()
	{
		int x = (int)((float)Main.maxTilesX * 0f);
		int y = (int)((float)Main.maxTilesY * 0f);
		if (Main.maxTilesX == 4200 && Main.maxTilesY == 1200)
		{
			x = (int)((float)Main.maxTilesX * 0.3f);
			y = (int)((float)Main.maxTilesY * 0.38f);
		}
		if (Main.maxTilesX == 6400 && Main.maxTilesY == 1800)
		{
			x = (int)((float)Main.maxTilesX * 0.3f);
			y = (int)((float)Main.maxTilesY * 0.32f);
		}
		if (Main.maxTilesX == 8400 && Main.maxTilesY == 2400)
		{
			x = (int)((float)Main.maxTilesX * 0.3f);
			y = (int)((float)Main.maxTilesY * 0.28f);
		}
		Depths.Generate(x, y);
	}

	private static void GenDepthsClear()
	{
		int x = (int)((float)Main.maxTilesX * 0f);
		int y = (int)((float)Main.maxTilesY * 0f);
		if (Main.maxTilesX == 4200 && Main.maxTilesY == 1200)
		{
			x = (int)((float)Main.maxTilesX * 0.3f);
			y = (int)((float)Main.maxTilesY * 0.38f);
		}
		if (Main.maxTilesX == 6400 && Main.maxTilesY == 1800)
		{
			x = (int)((float)Main.maxTilesX * 0.3f);
			y = (int)((float)Main.maxTilesY * 0.32f);
		}
		if (Main.maxTilesX == 8400 && Main.maxTilesY == 2400)
		{
			x = (int)((float)Main.maxTilesX * 0.3f);
			y = (int)((float)Main.maxTilesY * 0.28f);
		}
		DepthsClear.Generate(x, y);
	}

	public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
	{
		int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Slush"));
		if (num != -1)
		{
			tasks.Insert(num + 1, (GenPass)(object)new PassLegacy("ShadowBiome", (progress, config) => { GenShadow(); progress.Message = "Spreading the shadows..."; }));
            tasks.Insert(num + 1, (GenPass)(object)new PassLegacy("Depths", (progress, config) => GenDepthsClear()));
            int num2 = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Micro Biomes"));
			if (num2 != -1)
			{
				tasks.Insert(num2 + 1, (GenPass)(object)new PassLegacy("Depths", (progress, config) => GenDepths()));
				tasks.Insert(num2 + 1, (GenPass)(object)new PassLegacy("Shrine", (progress, config) => GenGuardianShrine()));
            }
		}
	}

	public override void PostWorldGen()
	{
		int[] array = new int[1] { Mod.Find<ModItem>("BrokenUltrumSummon").Type };
		int[] array2 = new int[1] { Mod.Find<ModItem>("BrokenIgnodiumSummon").Type };
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < 1000; i++)
		{
			Chest chest = Main.chest[i];
			if (chest != null && Main.tile[chest.x, chest.y].TileType == Mod.Find<ModTile>("ShrineChest").Type)
			{
				num = Main.rand.Next(array.Length);
				num2 = Main.rand.Next(array2.Length);
				chest.item[0].SetDefaults(array[num], false);
				chest.item[1].SetDefaults(array2[num2], false);
				break;
			}
		}
		int[] array3 = new int[1] { Mod.Find<ModItem>("DarkResonatorBroken").Type };
		int num3 = 0;
		for (int j = 0; j < 1000; j++)
		{
			Chest chest2 = Main.chest[j];
			if (chest2 != null && Main.tile[chest2.x, chest2.y].TileType == Mod.Find<ModTile>("ShadowChest").Type)
			{
				num3 = Main.rand.Next(array3.Length);
				chest2.item[0].SetDefaults(array3[num3], false);
				break;
			}
		}
		for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00015); k++)
		{
			int num4 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
			int num5 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 300);
			if (Main.tile[num4, num5] != null && Main.tile[num4, num5].HasTile && Main.tile[num4, num5].TileType == 161)
			{
				WorldGen.OreRunner(num4, num5, (double)WorldGen.genRand.Next(5, 12), WorldGen.genRand.Next(5, 12), (ushort)Mod.Find<ModTile>("AuroraOre").Type);
			}
		}
	}

	private void MoonlordShake(float intensity)
	{
		if (Main.netMode == 0)
		{
			Player localPlayer = Main.LocalPlayer;
			if (!Filters.Scene["MoonLordShake"].IsActive())
			{
				Filters.Scene.Activate("MoonLordShake", localPlayer.position);
			}
		}
		else
		{
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player = Main.player[i];
				if (((Entity)player).active && !Filters.Scene["MoonLordShake"].IsActive())
				{
					Filters.Scene.Activate("MoonLordShake", player.position);
				}
			}
		}
		Filters.Scene["MoonLordShake"].GetShader().UseIntensity(intensity);
	}


    public override void AddRecipes()
    {
        Recipe val = Recipe.Create(285, 1);
        val.AddRecipeGroup("Ultranium:Iron/Lead", 5);
        val.AddTile(16);
        val.Register();
        Recipe val2 = Recipe.Create(54, 1);
        val2.AddRecipeGroup("Ultranium:Silver/Tungsten", 10);
        val2.AddTile(16);
        val2.Register();
        Recipe val3 = Recipe.Create(212, 1);
        val3.AddIngredient(210, 6);
        val3.AddTile(16);
        val3.Register();
        Recipe val4 = Recipe.Create(53, 1);
        val4.AddIngredient(31, 1);
        val4.AddIngredient(751, 35);
        val4.AddTile(16);
        val4.Register();
        Recipe val5 = Recipe.Create(857, 1);
        val5.AddIngredient(53, 1);
        val5.AddIngredient(169, 35);
        val5.AddTile(16);
        val5.Register();
        Recipe val6 = Recipe.Create(987, 1);
        val6.AddIngredient(53, 1);
        val6.AddIngredient(593, 35);
        val6.AddTile(16);
        val6.Register();
        Recipe val7 = Recipe.Create(1291, 1);
        val7.AddIngredient(331, 5);
        val7.AddIngredient(947, 20);
        val7.AddTile(16);
        val7.Register();
        Recipe val8 = Recipe.Create(29, 1);
        val8.AddIngredient(178, 10);
        val8.AddIngredient((Mod)null, "BloodClot", 10);
        val8.AddTile(16);
        val8.Register();
        Recipe val9 = Recipe.Create(3052, 1);
        val9.AddIngredient((Mod)null, "ShadowFlame", 8);
        val9.AddTile(134);
        val9.Register();
        Recipe val10 = Recipe.Create(3053, 1);
        val10.AddIngredient((Mod)null, "ShadowFlame", 8);
        val10.AddTile(134);
        val10.Register();
        Recipe val11 = Recipe.Create(3054, 1);
        val11.AddIngredient((Mod)null, "ShadowFlame", 8);
        val11.AddTile(134);
        val11.Register();
        Recipe val12 = Recipe.Create(3063, 1);
        val12.AddIngredient(3467, 12);
        val12.AddTile(412);
        val12.Register();
        Recipe val13 = Recipe.Create(3065, 1);
        val13.AddIngredient(3467, 12);
        val13.AddTile(412);
        val13.Register();
        Recipe val14 = Recipe.Create(3389, 1);
        val14.AddIngredient(3467, 12);
        val14.AddTile(412);
        val14.Register();
        Recipe val15 = Recipe.Create(1553, 1);
        val15.AddIngredient(3467, 12);
        val15.AddTile(412);
        val15.Register();
        Recipe val16 = Recipe.Create(3546, 1);
        val16.AddIngredient(3467, 12);
        val16.AddTile(412);
        val16.Register();
        Recipe val17 = Recipe.Create(3570, 1);
        val17.AddIngredient(3467, 12);
        val17.AddTile(412);
        val17.Register();
        Recipe val18 = Recipe.Create(3541, 1);
        val18.AddIngredient(3467, 12);
        val18.AddTile(412);
        val18.Register();
        Recipe val19 = Recipe.Create(3569, 1);
        val19.AddIngredient(3467, 12);
        val19.AddTile(412);
        val19.Register();
        Recipe val20 = Recipe.Create(3571, 1);
        val20.AddIngredient(3467, 12);
        val20.AddTile(412);
        val20.Register();
    }

    public override void AddRecipeGroups()
    {
        RecipeGroup recipeGroup = new RecipeGroup(() => " Iron/Lead Bar", 22, 704);
        RecipeGroup.RegisterGroup("Ultranium:Iron/Lead", recipeGroup);
        recipeGroup = new RecipeGroup(() => " Silver/Tungsten Bar", 21, 705);
        RecipeGroup.RegisterGroup("Ultranium:Silver/Tungsten", recipeGroup);
        recipeGroup = new RecipeGroup(() => " Adamantite/Titanium Bar", 391, 1198);
        RecipeGroup.RegisterGroup("Ultranium:Adamantite/Titanium", recipeGroup);
        recipeGroup = new RecipeGroup(() => " Shadow Scale/Tissue Samples", 86, 1329);
        RecipeGroup.RegisterGroup("Ultranium:ShadowScale/TissueSample", recipeGroup);
        recipeGroup = new RecipeGroup(() => " Rotten Chunk/Vetebrae", 68, 1330);
        RecipeGroup.RegisterGroup("Ultranium:RottenChunk/Vetebrae", recipeGroup);
        recipeGroup = new RecipeGroup(() => " Demonite Javelin/Crimtane Pike", Mod.Find<ModItem>("DemoniteJavelin").Type, Mod.Find<ModItem>("CrimsonJavelin").Type);
        RecipeGroup.RegisterGroup("Ultranium:DemoniteJavelin/CrimtanePike", recipeGroup);
    }


    public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)/* tModPorter Note: Removed. Use ModSystem.ModifySunLightColor */
    {
        if (UltraniumWorld.ShadowTiles > 0)
        {
            float val = (float)UltraniumWorld.ShadowTiles / 200f;
            val = Math.Min(val, 1f);
            int r = backgroundColor.R;
            int g = backgroundColor.G;
            int b = backgroundColor.B;
            r -= (int)(155f * val * ((float)(int)backgroundColor.R / 255f));
            g -= (int)(90f * val * ((float)(int)backgroundColor.G / 255f));
            b -= (int)(120f * val * ((float)(int)backgroundColor.B / 255f));
            r = Utils.Clamp(r, 15, 255);
            g = Utils.Clamp(g, 15, 255);
            b = Utils.Clamp(b, 15, 255);
            backgroundColor.R = (byte)r;
            backgroundColor.G = (byte)g;
            backgroundColor.B = (byte)b;
        }
    }

    public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
    {
        if (!Main.gameMenu)
        {
            Ultranium.seizureTimer++;
            if (Ultranium.seizureAmount >= 0f && Ultranium.seizureTimer >= 5)
            {
                Ultranium.seizureAmount -= 0.1f;
            }
            if (Ultranium.seizureAmount < 0f)
            {
                Ultranium.seizureAmount = 0f;
            }
            Main.screenPosition += new Vector2(Ultranium.seizureAmount * Main.rand.NextFloat(), Ultranium.seizureAmount * Main.rand.NextFloat());
        }
        else
        {
            Ultranium.seizureAmount = 0f;
            Ultranium.seizureTimer = 0;
        }
    }

}
