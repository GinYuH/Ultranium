using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.BossSummon;
using Ultranium.Items.Fishing;
using Ultranium.NPCs.Aldin;
using Ultranium.NPCs.Dread;
using Ultranium.NPCs.Enemy.Critter;
using Ultranium.NPCs.Ethereal;
using Ultranium.NPCs.IceDragon;
using Ultranium.NPCs.Ignodium;
using Ultranium.NPCs.Ocean;
using Ultranium.NPCs.ShadowWorm;
using Ultranium.NPCs.TrueDread;
using Ultranium.NPCs.Ultrum;
using Ultranium.ShadowEvent;

namespace Ultranium;

public class UltraniumPlayer : ModPlayer
{
	private const int saveVersion = 0;

	public float damageTaken = 1f;

	public bool ZoneShadow;

	public bool ZoneDepth;

	public bool ZoneTemple;

	public bool SpawnComets;

	public bool BloodMinion;

	public bool PumpSlime;

	public bool ErebusMinion;

	public bool EyeMinion;

	public bool ShadeWisp;

	public bool BabySquid;

	public bool StarMinion;

	public bool DemonMinion;

	public bool BabyShroomMinion;

	public bool Wisp;

	public bool ShadowApparition;

	public bool DreadMinion;

	public bool UltrumMinion;

	public bool IgnodiumMinion;

	public bool AbyssalEye;

	public bool BabyWorm;

	public bool DreadBread;

	public bool CosmicDjinn;

	public bool Cacodemon;

	public bool StellarComet;

	public bool TacoDemon;

	public bool GuineaPig;

	public bool DragonHornet;

	public bool ZombiePet;

	public bool SlimePet;

	public bool WerewolfPet;

	public bool PetBat;

	public bool EtherealDebuff;

	public bool DarkDebuff;

	public bool EldritchSummonBuff;

	public double pressedSpecial;

	public bool DreadHeart;

	public bool TrueDreadHeart;

	public bool MysticTentacle;

	public bool IceTalon;

	public bool XenanisCore;

	public bool FlayerBrain;

	public bool VoidGauntlet;

	public bool VoidPouch;

	public bool EldritchScroll;

	public bool EldritchFlower;

	public bool ShadowflameSet;

	public bool MushroomSet;

	public bool StellarSet;

	public bool HorrorMeleeSet;

	public bool HorrorMagicSet;

	public bool HorrorRangedSet;

	public bool HorrorSummonSet;

	public bool EldritchMeleeSet;

	public bool EldritchRangedSet;

	public bool EldritchMagicSet;

	public bool EldritchSummonSet;

	public bool EldritchSummonEye;

	public override void UpdateBiomeVisuals()
	{
		bool zoneShadow = ZoneShadow;
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:ShadowBiome", zoneShadow, default(Vector2));
		bool flag = NPC.AnyNPCs(ModContent.NPCType<IceDragon>()) && IceDragon.BlizzardEffect;
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Blizzard", flag, default(Vector2));
		bool flag2 = NPC.AnyNPCs(ModContent.NPCType<DreadBoss>());
		bool flag3 = NPC.AnyNPCs(ModContent.NPCType<DreadBossP2>());
		bool flag4 = NPC.AnyNPCs(ModContent.NPCType<FakeDread>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:DreadBoss", flag2 || flag3 || flag4, default(Vector2));
		bool flag5 = NPC.AnyNPCs(ModContent.NPCType<Xenanis>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:EtherealBoss", flag5, default(Vector2));
		bool flag6 = NPC.AnyNPCs(ModContent.NPCType<Ultrum>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:Ultrum", flag6, default(Vector2));
		bool flag7 = NPC.AnyNPCs(ModContent.NPCType<TrueDread>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:TrueDread", flag7, default(Vector2));
		bool flag8 = NPC.AnyNPCs(ModContent.NPCType<Ignodium>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:Ignodium", flag8, default(Vector2));
		bool flag9 = ShadowEventWorld.ShadowEventActive && !ShadowEventWorld.Phase2 && !NPC.AnyNPCs(ModContent.NPCType<ErebusHead>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:ShadowEvent", flag9, default(Vector2));
		bool flag10 = ShadowEventWorld.ShadowEventActive && ShadowEventWorld.Phase2 && !NPC.AnyNPCs(ModContent.NPCType<ErebusHead>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:ShadowEvent2", flag10, default(Vector2));
		bool flag11 = NPC.AnyNPCs(ModContent.NPCType<ErebusHead>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:Erebus", flag11, default(Vector2));
		bool flag12 = NPC.AnyNPCs(ModContent.NPCType<Aldin>());
		((ModPlayer)this).player.ManageSpecialBiomeVisuals("Ultranium:Aldin", flag12, default(Vector2));
	}

	public override void PostUpdateEquips()
	{
		if (EldritchSummonEye)
		{
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("AbyssEyeBuff"), 3600, fromNetPvP: true);
			if (((ModPlayer)this).player.ownedProjectileCounts[((ModPlayer)this).mod.ProjectileType("AbyssalEye")] <= 0)
			{
				Projectile.NewProjectile(((ModPlayer)this).player.position, Vector2.Zero, ((ModPlayer)this).mod.ProjectileType("AbyssalEye"), 135, 0f, ((ModPlayer)this).player.whoAmI, 0f, 0f);
			}
		}
		if (MushroomSet)
		{
			Lighting.AddLight((int)(((ModPlayer)this).player.Center.X / 16f), (int)(((ModPlayer)this).player.Center.Y / 16f), 0f, 0f, 0.35f);
		}
	}

	public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
	{
		if (ShadowflameSet && proj.minion)
		{
			target.AddBuff(153, 180);
		}
		if (IceTalon && (proj.melee || proj.minion || proj.magic || proj.ranged || proj.thrown))
		{
			target.AddBuff(44, 180);
		}
		if ((DreadHeart || TrueDreadHeart) && (proj.melee || proj.minion || proj.magic || proj.ranged || proj.thrown))
		{
			target.AddBuff(((ModPlayer)this).mod.BuffType("DreadDebuff"), 180);
		}
		if (VoidGauntlet && proj.melee)
		{
			target.AddBuff(((ModPlayer)this).mod.BuffType("DarkDebuff"), 180);
		}
		if (VoidPouch && proj.ranged)
		{
			target.AddBuff(((ModPlayer)this).mod.BuffType("DarkDebuff"), 180);
		}
		if (EldritchScroll && proj.minion)
		{
			target.AddBuff(((ModPlayer)this).mod.BuffType("DarkDebuff"), 180);
		}
		if (EldritchFlower && proj.minion)
		{
			target.AddBuff(((ModPlayer)this).mod.BuffType("DarkDebuff"), 180);
		}
	}

	public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
	{
		if ((TrueDreadHeart & crit) && proj.type != 632 && Utils.NextBool(Main.rand, 5) && Main.myPlayer == ((ModPlayer)this).player.whoAmI)
		{
			Vector2 spinningpoint = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
			for (int i = 0; i < 3; i++)
			{
				Vector2 vector = spinningpoint.RotatedBy(Math.PI * 2.0 / 3.0 * ((double)i + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(target.Center, vector, ((ModPlayer)this).mod.ProjectileType("DreadFlameBlast"), 200, 0f, ((ModPlayer)this).player.whoAmI, 0f, 0f);
			}
		}
	}

	public override void ProcessTriggers(TriggersSet triggersSet)
	{
		if (!Ultranium.SpecialKey.JustPressed)
		{
			return;
		}
		if (EldritchSummonSet && ((ModPlayer)this).player.FindBuffIndex(((ModPlayer)this).mod.BuffType("EldritchCooldown")) < 0)
		{
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EldritchSummonBuff"), 360, fromNetPvP: true);
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EldritchCooldown"), 2400, fromNetPvP: true);
			Main.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, ((ModPlayer)this).player.Center);
			int num = 35;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)((ModPlayer)this).player.width / 2f, ((ModPlayer)this).player.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + ((ModPlayer)this).player.Center;
				Vector2 vector2 = vector - ((ModPlayer)this).player.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, 89, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.velocity = Vector2.Normalize(vector2) * 3f;
			}
		}
		if (EldritchMagicSet && ((ModPlayer)this).player.FindBuffIndex(((ModPlayer)this).mod.BuffType("EldritchCooldown")) < 0)
		{
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EldritchMagicBuff"), 360, fromNetPvP: true);
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EldritchCooldown"), 2400, fromNetPvP: true);
			Main.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, ((ModPlayer)this).player.Center);
			int num2 = 35;
			for (int j = 0; j < num2; j++)
			{
				Vector2 vector3 = (Vector2.One * new Vector2((float)((ModPlayer)this).player.width / 2f, ((ModPlayer)this).player.height) * 0.75f * 0.5f).RotatedBy((float)(j - (num2 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num2) + ((ModPlayer)this).player.Center;
				Vector2 vector4 = vector3 - ((ModPlayer)this).player.Center;
				Dust obj2 = Main.dust[Dust.NewDust(vector3 + vector4, 0, 0, 89, vector4.X * 2f, vector4.Y * 2f, 100, default(Color), 1.4f)];
				obj2.noGravity = true;
				obj2.velocity = Vector2.Normalize(vector4) * 3f;
			}
		}
		if (EldritchMeleeSet && ((ModPlayer)this).player.FindBuffIndex(((ModPlayer)this).mod.BuffType("EldritchCooldown")) < 0)
		{
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EldritchMeleeBuff"), 360, fromNetPvP: true);
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EldritchCooldown"), 2400, fromNetPvP: true);
			Main.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, ((ModPlayer)this).player.Center);
			int num3 = 35;
			for (int k = 0; k < num3; k++)
			{
				Vector2 vector5 = (Vector2.One * new Vector2((float)((ModPlayer)this).player.width / 2f, ((ModPlayer)this).player.height) * 0.75f * 0.5f).RotatedBy((float)(k - (num3 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num3) + ((ModPlayer)this).player.Center;
				Vector2 vector6 = vector5 - ((ModPlayer)this).player.Center;
				Dust obj3 = Main.dust[Dust.NewDust(vector5 + vector6, 0, 0, 89, vector6.X * 2f, vector6.Y * 2f, 100, default(Color), 1.4f)];
				obj3.noGravity = true;
				obj3.velocity = Vector2.Normalize(vector6) * 3f;
			}
		}
		if (EldritchRangedSet && ((ModPlayer)this).player.FindBuffIndex(((ModPlayer)this).mod.BuffType("EldritchCooldown")) < 0)
		{
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EldritchRangedBuff"), 360, fromNetPvP: true);
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EldritchCooldown"), 2400, fromNetPvP: true);
			Main.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, ((ModPlayer)this).player.Center);
			int num4 = 35;
			for (int l = 0; l < num4; l++)
			{
				Vector2 vector7 = (Vector2.One * new Vector2((float)((ModPlayer)this).player.width / 2f, ((ModPlayer)this).player.height) * 0.75f * 0.5f).RotatedBy((float)(l - (num4 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num4) + ((ModPlayer)this).player.Center;
				Vector2 vector8 = vector7 - ((ModPlayer)this).player.Center;
				Dust obj4 = Main.dust[Dust.NewDust(vector7 + vector8, 0, 0, 89, vector8.X * 2f, vector8.Y * 2f, 100, default(Color), 1.4f)];
				obj4.noGravity = true;
				obj4.velocity = Vector2.Normalize(vector8) * 3f;
			}
		}
		if (XenanisCore && ((ModPlayer)this).player.FindBuffIndex(((ModPlayer)this).mod.BuffType("EtherealCooldown")) < 0)
		{
			((ModPlayer)this).player.AddBuff(((ModPlayer)this).mod.BuffType("EtherealCooldown"), 900, fromNetPvP: true);
			for (int m = 0; m < 7; m++)
			{
				Vector2 vector9 = (0.8975979f * (float)m).ToRotationVector2();
				vector9.Normalize();
				vector9 *= 7f;
				Projectile.NewProjectile(((ModPlayer)this).player.Center.X, ((ModPlayer)this).player.Center.Y, vector9.X, vector9.Y, ((ModPlayer)this).mod.ProjectileType("EtherealCoreBolt"), 75, 1f, Main.myPlayer, 0f, 0f);
			}
			int num5 = 35;
			for (int n = 0; n < num5; n++)
			{
				Vector2 vector10 = (Vector2.One * new Vector2((float)((ModPlayer)this).player.width / 2f, ((ModPlayer)this).player.height) * 0.75f * 0.5f).RotatedBy((float)(n - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + ((ModPlayer)this).player.Center;
				Vector2 vector11 = vector10 - ((ModPlayer)this).player.Center;
				Dust obj5 = Main.dust[Dust.NewDust(vector10 + vector11, 0, 0, 62, vector11.X * 2f, vector11.Y * 2f, 100, default(Color), 1.4f)];
				obj5.noGravity = true;
				obj5.noLight = true;
				obj5.velocity = Vector2.Normalize(vector11) * 3f;
				obj5.fadeIn = 1.3f;
			}
		}
	}

	public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
	{
		damage = (int)((float)damage * damageTaken);
		return ((ModPlayer)this).PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
	}

	public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
	{
		if (MushroomSet && Main.rand.Next(2) == 0)
		{
			Projectile.NewProjectile(((ModPlayer)this).player.Center + Main.rand.NextVector2Square(-50f, 50f), Main.rand.NextVector2Square(-1f, 1f), 590, 15, 6f, ((ModPlayer)this).player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(((ModPlayer)this).player.Center + Main.rand.NextVector2Square(-50f, 50f), Main.rand.NextVector2Square(-1f, 1f), 590, 15, 6f, ((ModPlayer)this).player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(((ModPlayer)this).player.Center + Main.rand.NextVector2Square(-50f, 50f), Main.rand.NextVector2Square(-1f, 1f), 590, 15, 6f, ((ModPlayer)this).player.whoAmI, 0f, 0f);
		}
		if (DreadHeart && Main.rand.Next(3) == 0)
		{
			Vector2 spinningpoint = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
			for (int i = 0; i < 3; i++)
			{
				Vector2 vector = spinningpoint.RotatedBy(Math.PI * 2.0 / 3.0 * ((double)i + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(((ModPlayer)this).player.Center, vector, ((ModPlayer)this).mod.ProjectileType("DreadFlameBall"), 75, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (TrueDreadHeart && Main.rand.Next(2) == 0)
		{
			Vector2 spinningpoint2 = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
			for (int j = 0; j < 5; j++)
			{
				Vector2 vector2 = spinningpoint2.RotatedBy(Math.PI * 2.0 / 5.0 * ((double)j + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(((ModPlayer)this).player.Center, vector2, ((ModPlayer)this).mod.ProjectileType("DreadFlameBlast"), 200, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (StellarSet && Main.rand.Next(3) == 0)
		{
			for (int k = 0; k < 8; k++)
			{
				Vector2 vector3 = ((float)Math.PI / 4f * (float)k).ToRotationVector2();
				vector3.Normalize();
				vector3 *= 14f;
				Projectile.NewProjectile(((ModPlayer)this).player.Center.X, ((ModPlayer)this).player.Center.Y, vector3.X, vector3.Y, ((ModPlayer)this).mod.ProjectileType("StellarComet"), 70, 1f, Main.myPlayer, 0f, 0f);
			}
		}
	}

	private void CustomDeath(ref PlayerDeathReason reason)
	{
		if (((ModPlayer)this).player.FindBuffIndex(((ModPlayer)this).mod.BuffType("DarkDebuff")) >= 0)
		{
			reason = PlayerDeathReason.ByCustomReason(((ModPlayer)this).player.name + " was disintegrated by the shadows");
		}
	}

	public override void UpdateBiomes()
	{
		if (ZoneDepth)
		{
			Lighting.brightness = 0.65f;
		}
		ZoneShadow = UltraniumWorld.ShadowTiles > 100 && !ZoneDepth;
	}

	public override void PreUpdate()
	{
		int num = (int)((ModPlayer)this).player.Center.X / 16;
		int num2 = (int)((ModPlayer)this).player.Center.Y / 16;
		if (Main.tile[num, num2].wall == ((ModPlayer)this).mod.WallType("DarkStoneWall") || Main.tile[num, num2].wall == ((ModPlayer)this).mod.WallType("PurpleDarkstoneWall"))
		{
			ZoneDepth = true;
		}
		else
		{
			ZoneDepth = false;
		}
		if (Main.tile[num, num2].wall == 87)
		{
			ZoneTemple = true;
		}
		else
		{
			ZoneTemple = false;
		}
	}

	public override void CopyCustomBiomesTo(Player other)
	{
		UltraniumPlayer modPlayer = other.GetModPlayer<UltraniumPlayer>();
		modPlayer.ZoneShadow = ZoneShadow;
		modPlayer.ZoneDepth = ZoneDepth;
		modPlayer.ZoneDepth = ZoneTemple;
	}

	public override void SendCustomBiomes(BinaryWriter writer)
	{
		BitsByte bitsByte = default(BitsByte);
		bitsByte[0] = ZoneShadow;
		bitsByte[1] = ZoneDepth;
		bitsByte[2] = ZoneTemple;
		writer.Write(bitsByte);
	}

	public override void ReceiveCustomBiomes(BinaryReader reader)
	{
		BitsByte bitsByte = reader.ReadByte();
		ZoneShadow = bitsByte[0];
		ZoneDepth = bitsByte[1];
		ZoneTemple = bitsByte[2];
	}

	public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
	{
		Item item = new Item();
		item.SetDefaults(((ModPlayer)this).mod.ItemType("Cabbage"), false);
		items.Add(item);
	}

	public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
	{
		if (junk)
		{
			return;
		}
		UltraniumPlayer modPlayer = ((ModPlayer)this).player.GetModPlayer<UltraniumPlayer>();
		if (modPlayer.ZoneShadow && Utils.NextBool(Main.rand, 5))
		{
			caughtType = ModContent.ItemType<SpectreFishItem>();
		}
		if (modPlayer.ZoneDepth && Utils.NextBool(Main.rand, 10))
		{
			caughtType = ModContent.ItemType<DepthCrate>();
		}
		if (modPlayer.ZoneDepth && Utils.NextBool(Main.rand, 10))
		{
			caughtType = ModContent.ItemType<ShroomFish>();
		}
		if (!((ModPlayer)this).player.HasItem(ModContent.ItemType<CoralBait>()))
		{
			return;
		}
		Item[] inventory = ((ModPlayer)this).player.inventory;
		for (int i = 0; i < inventory.Length; i++)
		{
			int num = -1;
			if (inventory[i].type != ModContent.ItemType<CoralBait>() || NPC.AnyNPCs(ModContent.NPCType<ZephyrSquid>()) || !((ModPlayer)this).player.ZoneBeach || liquidType != 0)
			{
				continue;
			}
			for (int j = 0; j < 1000; j++)
			{
				if (((Entity)Main.projectile[j]).active && Main.projectile[j].owner == ((ModPlayer)this).player.whoAmI && Main.projectile[j].bobber)
				{
					num = j;
				}
			}
			if (num != -1)
			{
				inventory[i].stack--;
				Vector2 center = Main.projectile[num].Center;
				caughtType = NPC.NewNPC((int)center.X, (int)center.Y, ModContent.NPCType<ZephyrSquid>(), 0, 0f, 0f, 0f, 0f, Main.myPlayer);
				((Entity)Main.projectile[num]).active = false;
				if (Main.netMode == 1)
				{
					NetMessage.SendData(23, -1, -1, null, ModContent.NPCType<ZephyrSquid>());
				}
			}
		}
	}

	public override void UpdateBadLifeRegen()
	{
		if (DarkDebuff)
		{
			((ModPlayer)this).player.lifeRegen -= 25;
		}
	}

	public override void ResetEffects()
	{
		damageTaken = 1f;
		DarkDebuff = false;
		EtherealDebuff = false;
		EldritchSummonBuff = false;
		EyeMinion = false;
		PumpSlime = false;
		BloodMinion = false;
		ErebusMinion = false;
		ShadeWisp = false;
		BabySquid = false;
		StarMinion = false;
		DemonMinion = false;
		BabyShroomMinion = false;
		Wisp = false;
		ShadowApparition = false;
		DreadMinion = false;
		UltrumMinion = false;
		IgnodiumMinion = false;
		AbyssalEye = false;
		BabyWorm = false;
		DreadBread = false;
		CosmicDjinn = false;
		Cacodemon = false;
		StellarComet = false;
		TacoDemon = false;
		GuineaPig = false;
		DragonHornet = false;
		ZombiePet = false;
		SlimePet = false;
		WerewolfPet = false;
		PetBat = false;
		DreadHeart = false;
		TrueDreadHeart = false;
		MysticTentacle = false;
		IceTalon = false;
		XenanisCore = false;
		FlayerBrain = false;
		VoidGauntlet = false;
		VoidPouch = false;
		EldritchScroll = false;
		EldritchFlower = false;
		ShadowflameSet = false;
		MushroomSet = false;
		StellarSet = false;
		HorrorMeleeSet = false;
		HorrorMagicSet = false;
		HorrorRangedSet = false;
		HorrorSummonSet = false;
		EldritchMeleeSet = false;
		EldritchRangedSet = false;
		EldritchMagicSet = false;
		EldritchSummonSet = false;
		EldritchSummonEye = false;
	}
}
