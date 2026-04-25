using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Ultranium.Items.BossSummon;
using Ultranium.Items.Fishing;
using Ultranium.Items.Pets.Console;
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

	public override void PostUpdateEquips()
	{
		if (EldritchSummonEye)
		{
			Player.AddBuff(Mod.Find<ModBuff>("AbyssEyeBuff").Type, 3600, quiet: false);
			if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("AbyssalEye").Type] <= 0)
			{
				Projectile.NewProjectile(Player.GetSource_FromThis(), Player.position, Vector2.Zero, Mod.Find<ModProjectile>("AbyssalEye").Type, 135, 0f, Player.whoAmI, 0f, 0f);
			}
		}
		if (MushroomSet)
		{
			Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0f, 0f, 0.35f);
		}
	}

	public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
	{
		if (ShadowflameSet && proj.minion)
		{
			target.AddBuff(BuffID.ShadowFlame, 180);
		}
		if (IceTalon && (proj.CountsAsClass(DamageClass.Melee) || proj.minion || proj.CountsAsClass(DamageClass.Magic) || proj.CountsAsClass(DamageClass.Ranged) || proj.CountsAsClass(DamageClass.Throwing)))
		{
			target.AddBuff(BuffID.Frostburn, 180);
		}
		if ((DreadHeart || TrueDreadHeart) && (proj.CountsAsClass(DamageClass.Melee) || proj.minion || proj.CountsAsClass(DamageClass.Magic) || proj.CountsAsClass(DamageClass.Ranged) || proj.CountsAsClass(DamageClass.Throwing)))
		{
			target.AddBuff(Mod.Find<ModBuff>("DreadDebuff").Type, 180);
		}
		if (VoidGauntlet && proj.CountsAsClass(DamageClass.Melee))
		{
			target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 180);
		}
		if (VoidPouch && proj.CountsAsClass(DamageClass.Ranged))
		{
			target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 180);
		}
		if (EldritchScroll && proj.minion)
		{
			target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 180);
		}
		if (EldritchFlower && proj.minion)
		{
			target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 180);
        }
        if ((TrueDreadHeart & hit.Crit) && proj.type != ProjectileID.LastPrismLaser && Utils.NextBool(Main.rand, 5) && Main.myPlayer == Player.whoAmI)
        {
            Vector2 spinningpoint = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
            for (int i = 0; i < 3; i++)
            {
                Vector2 vector = spinningpoint.RotatedBy(Math.PI * 2.0 / 3.0 * ((double)i + Main.rand.NextDouble() - 0.5));
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, vector, Mod.Find<ModProjectile>("DreadFlameBlast").Type, 200, 0f, Player.whoAmI, 0f, 0f);
            }
        }
    }

	public override void ProcessTriggers(TriggersSet triggersSet)
	{
		if (!Ultranium.SpecialKey.JustPressed)
		{
			return;
		}
		if (EldritchSummonSet && Player.FindBuffIndex(Mod.Find<ModBuff>("EldritchCooldown").Type) < 0)
		{
			Player.AddBuff(Mod.Find<ModBuff>("EldritchSummonBuff").Type, 360, quiet: false);
			Player.AddBuff(Mod.Find<ModBuff>("EldritchCooldown").Type, 2400, quiet: false);
			SoundEngine.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, Player.Center);
			int num = 35;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (Vector2.One * new Vector2((float)Player.width / 2f, Player.height) * 0.75f * 0.5f).RotatedBy((float)(i - (num / 2 - 1)) * ((float)Math.PI * 2f) / (float)num) + Player.Center;
				Vector2 vector2 = vector - Player.Center;
				Dust obj = Main.dust[Dust.NewDust(vector + vector2, 0, 0, DustID.GemEmerald, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.velocity = Vector2.Normalize(vector2) * 3f;
			}
		}
		if (EldritchMagicSet && Player.FindBuffIndex(Mod.Find<ModBuff>("EldritchCooldown").Type) < 0)
		{
			Player.AddBuff(Mod.Find<ModBuff>("EldritchMagicBuff").Type, 360, quiet: false);
			Player.AddBuff(Mod.Find<ModBuff>("EldritchCooldown").Type, 2400, quiet: false);
			SoundEngine.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, Player.Center);
			int num2 = 35;
			for (int j = 0; j < num2; j++)
			{
				Vector2 vector3 = (Vector2.One * new Vector2((float)Player.width / 2f, Player.height) * 0.75f * 0.5f).RotatedBy((float)(j - (num2 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num2) + Player.Center;
				Vector2 vector4 = vector3 - Player.Center;
				Dust obj2 = Main.dust[Dust.NewDust(vector3 + vector4, 0, 0, DustID.GemEmerald, vector4.X * 2f, vector4.Y * 2f, 100, default(Color), 1.4f)];
				obj2.noGravity = true;
				obj2.velocity = Vector2.Normalize(vector4) * 3f;
			}
		}
		if (EldritchMeleeSet && Player.FindBuffIndex(Mod.Find<ModBuff>("EldritchCooldown").Type) < 0)
		{
			Player.AddBuff(Mod.Find<ModBuff>("EldritchMeleeBuff").Type, 360, quiet: false);
			Player.AddBuff(Mod.Find<ModBuff>("EldritchCooldown").Type, 2400, quiet: false);
			SoundEngine.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, Player.Center);
			int num3 = 35;
			for (int k = 0; k < num3; k++)
			{
				Vector2 vector5 = (Vector2.One * new Vector2((float)Player.width / 2f, Player.height) * 0.75f * 0.5f).RotatedBy((float)(k - (num3 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num3) + Player.Center;
				Vector2 vector6 = vector5 - Player.Center;
				Dust obj3 = Main.dust[Dust.NewDust(vector5 + vector6, 0, 0, DustID.GemEmerald, vector6.X * 2f, vector6.Y * 2f, 100, default(Color), 1.4f)];
				obj3.noGravity = true;
				obj3.velocity = Vector2.Normalize(vector6) * 3f;
			}
		}
		if (EldritchRangedSet && Player.FindBuffIndex(Mod.Find<ModBuff>("EldritchCooldown").Type) < 0)
		{
			Player.AddBuff(Mod.Find<ModBuff>("EldritchRangedBuff").Type, 360, quiet: false);
			Player.AddBuff(Mod.Find<ModBuff>("EldritchCooldown").Type, 2400, quiet: false);
			SoundEngine.PlaySound(SoundID.DD2_EtherianPortalSpawnEnemy, Player.Center);
			int num4 = 35;
			for (int l = 0; l < num4; l++)
			{
				Vector2 vector7 = (Vector2.One * new Vector2((float)Player.width / 2f, Player.height) * 0.75f * 0.5f).RotatedBy((float)(l - (num4 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num4) + Player.Center;
				Vector2 vector8 = vector7 - Player.Center;
				Dust obj4 = Main.dust[Dust.NewDust(vector7 + vector8, 0, 0, DustID.GemEmerald, vector8.X * 2f, vector8.Y * 2f, 100, default(Color), 1.4f)];
				obj4.noGravity = true;
				obj4.velocity = Vector2.Normalize(vector8) * 3f;
			}
		}
		if (XenanisCore && Player.FindBuffIndex(Mod.Find<ModBuff>("EtherealCooldown").Type) < 0)
		{
			Player.AddBuff(Mod.Find<ModBuff>("EtherealCooldown").Type, 900, quiet: false);
			for (int m = 0; m < 7; m++)
			{
				Vector2 vector9 = (0.8975979f * (float)m).ToRotationVector2();
				vector9.Normalize();
				vector9 *= 7f;
				Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, vector9.X, vector9.Y, Mod.Find<ModProjectile>("EtherealCoreBolt").Type, 75, 1f, Main.myPlayer, 0f, 0f);
			}
			int num5 = 35;
			for (int n = 0; n < num5; n++)
			{
				Vector2 vector10 = (Vector2.One * new Vector2((float)Player.width / 2f, Player.height) * 0.75f * 0.5f).RotatedBy((float)(n - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + Player.Center;
				Vector2 vector11 = vector10 - Player.Center;
				Dust obj5 = Main.dust[Dust.NewDust(vector10 + vector11, 0, 0, DustID.PurpleTorch, vector11.X * 2f, vector11.Y * 2f, 100, default(Color), 1.4f)];
				obj5.noGravity = true;
				obj5.noLight = true;
				obj5.velocity = Vector2.Normalize(vector11) * 3f;
				obj5.fadeIn = 1.3f;
			}
		}
	}

	public override void ModifyHurt(ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage.Base = (int)((float)modifiers.SourceDamage.Base * damageTaken);
	}

	public override void OnHurt(Player.HurtInfo info)
	{
		if (MushroomSet && Main.rand.Next(2) == 0)
		{
			Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center + Main.rand.NextVector2Square(-50f, 50f), Main.rand.NextVector2Square(-1f, 1f), ProjectileID.TruffleSpore, 15, 6f, Player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center + Main.rand.NextVector2Square(-50f, 50f), Main.rand.NextVector2Square(-1f, 1f), ProjectileID.TruffleSpore, 15, 6f, Player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center + Main.rand.NextVector2Square(-50f, 50f), Main.rand.NextVector2Square(-1f, 1f), ProjectileID.TruffleSpore, 15, 6f, Player.whoAmI, 0f, 0f);
		}
		if (DreadHeart && Main.rand.Next(3) == 0)
		{
			Vector2 spinningpoint = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
			for (int i = 0; i < 3; i++)
			{
				Vector2 vector = spinningpoint.RotatedBy(Math.PI * 2.0 / 3.0 * ((double)i + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, vector, Mod.Find<ModProjectile>("DreadFlameBall").Type, 75, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (TrueDreadHeart && Main.rand.Next(2) == 0)
		{
			Vector2 spinningpoint2 = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
			for (int j = 0; j < 5; j++)
			{
				Vector2 vector2 = spinningpoint2.RotatedBy(Math.PI * 2.0 / 5.0 * ((double)j + Main.rand.NextDouble() - 0.5));
				Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, vector2, Mod.Find<ModProjectile>("DreadFlameBlast").Type, 200, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (StellarSet && Main.rand.Next(3) == 0)
		{
			for (int k = 0; k < 8; k++)
			{
				Vector2 vector3 = ((float)Math.PI / 4f * (float)k).ToRotationVector2();
				vector3.Normalize();
				vector3 *= 14f;
				Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, vector3.X, vector3.Y, Mod.Find<ModProjectile>("StellarComet").Type, 70, 1f, Main.myPlayer, 0f, 0f);
			}
		}
	}

	private void CustomDeath(ref PlayerDeathReason reason)
	{
		if (Player.FindBuffIndex(Mod.Find<ModBuff>("DarkDebuff").Type) >= 0)
		{
			reason = PlayerDeathReason.ByCustomReason(Player.name + " was disintegrated by the shadows");
		}
	}

	public override void PreUpdate()
	{
		int num = (int)Player.Center.X / 16;
		int num2 = (int)Player.Center.Y / 16;
		if (Main.tile[num, num2].WallType == Mod.Find<ModWall>("DarkStoneWall").Type || Main.tile[num, num2].WallType == Mod.Find<ModWall>("PurpleDarkstoneWall").Type)
		{
			ZoneDepth = true;
		}
		else
		{
			ZoneDepth = false;
		}
		if (Main.tile[num, num2].WallType == WallID.LihzahrdBrickUnsafe)
		{
			ZoneTemple = true;
		}
		else
		{
			ZoneTemple = false;
		}
	}

    public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
    {
        static Item createItem(int type)
        {
            Item i = new Item();
            i.SetDefaults(type);
            return i;
        }

        if (!mediumCoreDeath)
            yield return createItem(ModContent.ItemType<Cabbage>());
    }

    public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
	{
        if (itemDrop == ItemID.OldShoe || itemDrop == ItemID.FishingSeaweed || itemDrop == ItemID.TinCan || itemDrop == ItemID.JojaCola)
            return;
        UltraniumPlayer modPlayer = Player.GetModPlayer<UltraniumPlayer>();
		if (modPlayer.ZoneShadow && Utils.NextBool(Main.rand, 5))
		{
            attempt.rolledItemDrop = ModContent.ItemType<SpectreFishItem>();
		}
		if (modPlayer.ZoneDepth && Utils.NextBool(Main.rand, 10))
		{
            attempt.rolledItemDrop = ModContent.ItemType<DepthCrate>();
		}
		if (modPlayer.ZoneDepth && Utils.NextBool(Main.rand, 10))
		{
            attempt.rolledItemDrop = ModContent.ItemType<ShroomFish>();
		}
		if (!Player.HasItem(ModContent.ItemType<CoralBait>()))
		{
			return;
		}
		if (!NPC.AnyNPCs(ModContent.NPCType<ZephyrSquid>()) && Player.ZoneBeach && !attempt.inLava && !attempt.inHoney)
		{
			npcSpawn = ModContent.NPCType<ZephyrSquid>();
			attempt.rolledEnemySpawn = ModContent.NPCType<ZephyrSquid>();
		}
	}

	public override void UpdateBadLifeRegen()
	{
		if (DarkDebuff)
		{
			Player.lifeRegen -= 25;
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
