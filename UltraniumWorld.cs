using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Generation;
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
		if (NPC.AnyNPCs(((ModSystem)this).Mod.Find<ModNPC>("ErebusHead").Type))
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
		ShadowTiles = tileCounts[((ModSystem)this).Mod.Find<ModTile>("ShadowGrass").Type] + tileCounts[((ModSystem)this).Mod.Find<ModTile>("ShadowStoneTile").Type];
	}

	private void GenGuardianShrine(GenerationProgress progress)
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

	private void GenShadow(GenerationProgress progress)
	{
		progress.Message = "Spreading the shadows...";
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
						Main.tile[i, j].WallType = (ushort)((ModSystem)this).Mod.Find<ModWall>("ShadowWall").Type;
					}
					if (Main.tile[i, j].WallType == 64 || Main.tile[i, j].WallType == 67 || Main.tile[i, j].WallType == 63 || Main.tile[i, j].WallType == 66 || Main.tile[i, j].WallType == 65 || Main.tile[i, j].WallType == 68 || Main.tile[i, j].WallType == 69 || Main.tile[i, j].WallType == 81)
					{
						Main.tile[i, j].WallType = (ushort)((ModSystem)this).Mod.Find<ModWall>("ShadowGrassWall").Type;
					}
					if (Main.tile[i, j].WallType == 83 || Main.tile[i, j].WallType == 3 || Main.tile[i, j].WallType == 40 || Main.tile[i, j].WallType == 1)
					{
						Main.tile[i, j].WallType = (ushort)((ModSystem)this).Mod.Find<ModWall>("ShadowStoneWall").Type;
					}
					if (Main.tile[i, j].TileType == 60 || Main.tile[i, j].TileType == 59 || Main.tile[i, j].TileType == 147 || Main.tile[i, j].TileType == 53 || Main.tile[i, j].TileType == 112 || Main.tile[i, j].TileType == 234 || Main.tile[i, j].TileType == 199 || Main.tile[i, j].TileType == 59 || Main.tile[i, j].TileType == 23 || Main.tile[i, j].TileType == 0 || Main.tile[i, j].TileType == 2)
					{
						Main.tile[i, j].TileType = (ushort)((ModSystem)this).Mod.Find<ModTile>("ShadowGrass").Type;
					}
					if (Main.tile[i, j].TileType == 1 || Main.tile[i, j].TileType == 25 || Main.tile[i, j].TileType == 203 || Main.tile[i, j].TileType == 163 || Main.tile[i, j].TileType == 200 || Main.tile[i, j].TileType == 161)
					{
						Main.tile[i, j].TileType = (ushort)((ModSystem)this).Mod.Find<ModTile>("ShadowStoneTile").Type;
					}
					if (Main.tile[i, j].TileType == 6 || Main.tile[i, j].TileType == 7 || Main.tile[i, j].TileType == 8 || Main.tile[i, j].TileType == 9 || Main.tile[i, j].TileType == 221 || Main.tile[i, j].TileType == 222 || Main.tile[i, j].TileType == 223 || Main.tile[i, j].TileType == 204 || Main.tile[i, j].TileType == 166 || Main.tile[i, j].TileType == 167 || Main.tile[i, j].TileType == 168 || Main.tile[i, j].TileType == 169 || Main.tile[i, j].TileType == 221 || Main.tile[i, j].TileType == 107 || Main.tile[i, j].TileType == 108 || Main.tile[i, j].TileType == 22 || Main.tile[i, j].TileType == 111 || Main.tile[i, j].TileType == 123 || Main.tile[i, j].TileType == 224 || Main.tile[i, j].TileType == 40 || Main.tile[i, j].TileType == 59)
					{
						Main.tile[i, j].TileType = (ushort)((ModSystem)this).Mod.Find<ModTile>("ShadowOreTile").Type;
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
			if (Main.tile[num6, num7].TileType == ((ModSystem)this).Mod.Find<ModTile>("ShadowGrass").Type || Main.tile[num6, num7].TileType == ((ModSystem)this).Mod.Find<ModTile>("ShadowStoneTile").Type)
			{
				WorldGen.TileRunner(num6, num7, (double)WorldGen.genRand.Next(15, 15), WorldGen.genRand.Next(12, 15), (int)(ushort)((ModSystem)this).Mod.Find<ModTile>("ShadowOreTile").Type, false, 0f, 0f, false, true);
			}
		}
		for (int l = 0; l < Main.maxTilesX; l++)
		{
			for (int m = 0; m < Main.maxTilesY; m++)
			{
				if (Main.tile[l, m].TileType == ((ModSystem)this).Mod.Find<ModTile>("ShadowGrass").Type && (!Main.tile[l + 1, m].HasTile || !Main.tile[l, m - 1].HasTile || !Main.tile[l - 1, m].HasTile || !Main.tile[l, m + 1].HasTile))
				{
					Main.tile[l, m].TileType = (ushort)((ModSystem)this).Mod.Find<ModTile>("ShadowGrass").Type;
				}
			}
		}
	}

	private void GenDepths(GenerationProgress progress)
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

	private void GenDepthsClear(GenerationProgress progress)
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

	public unsafe override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
	{
		int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Slush"));
		if (num != -1)
		{
			tasks.Insert(num + 1, (GenPass)(object)new PassLegacy("ShadowBiome", new WorldGenLegacyMethod(this, (nint)__ldftn(UltraniumWorld.GenShadow))));
			tasks.Insert(num + 1, (GenPass)(object)new PassLegacy("Depths", new WorldGenLegacyMethod(this, (nint)__ldftn(UltraniumWorld.GenDepthsClear))));
			int num2 = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Micro Biomes"));
			if (num2 != -1)
			{
				tasks.Insert(num2 + 1, (GenPass)(object)new PassLegacy("Depths", new WorldGenLegacyMethod(this, (nint)__ldftn(UltraniumWorld.GenDepths))));
				tasks.Insert(num2 + 1, (GenPass)(object)new PassLegacy("Shrine", new WorldGenLegacyMethod(this, (nint)__ldftn(UltraniumWorld.GenGuardianShrine))));
			}
		}
	}

	public override void PostWorldGen()
	{
		int[] array = new int[1] { ((ModSystem)this).Mod.Find<ModItem>("BrokenUltrumSummon").Type };
		int[] array2 = new int[1] { ((ModSystem)this).Mod.Find<ModItem>("BrokenIgnodiumSummon").Type };
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < 1000; i++)
		{
			Chest chest = Main.chest[i];
			if (chest != null && Main.tile[chest.x, chest.y].TileType == ((ModSystem)this).Mod.Find<ModTile>("ShrineChest").Type)
			{
				num = Main.rand.Next(array.Length);
				num2 = Main.rand.Next(array2.Length);
				chest.item[0].SetDefaults(array[num], false);
				chest.item[1].SetDefaults(array2[num2], false);
				break;
			}
		}
		int[] array3 = new int[1] { ((ModSystem)this).Mod.Find<ModItem>("DarkResonatorBroken").Type };
		int num3 = 0;
		for (int j = 0; j < 1000; j++)
		{
			Chest chest2 = Main.chest[j];
			if (chest2 != null && Main.tile[chest2.x, chest2.y].TileType == ((ModSystem)this).Mod.Find<ModTile>("ShadowChest").Type)
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
				WorldGen.OreRunner(num4, num5, (double)WorldGen.genRand.Next(5, 12), WorldGen.genRand.Next(5, 12), (ushort)((ModSystem)this).Mod.Find<ModTile>("AuroraOre").Type);
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
}
