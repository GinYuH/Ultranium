using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items;

public class GlobalItems : GlobalItem
{
	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		if (item.type == 579)
		{
			TooltipLine item2 = new TooltipLine(((GlobalItem)this).mod, "DraxToolTip", "Able to mine Depthstone");
			tooltips.Add(item2);
		}
		if (item.type == 990)
		{
			TooltipLine item3 = new TooltipLine(((GlobalItem)this).mod, "PickaxeAxeToolTip", "Able to mine Depthstone");
			tooltips.Add(item3);
		}
	}

	public override void UpdateAccessory(Item item, Player player, bool hideVisual)
	{
		if (item.type == 3090)
		{
			player.npcTypeNoAggro[((GlobalItem)this).mod.NPCType("TenebrisSlime")] = true;
			player.npcTypeNoAggro[((GlobalItem)this).mod.NPCType("DepthSlime")] = true;
		}
	}

	public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().HorrorMeleeSet && item.melee && Main.rand.Next(3) == 0)
		{
			int num = Projectile.NewProjectile(position, new Vector2(speedX, speedY), ((GlobalItem)this).mod.ProjectileType("DreadFlameBlast"), 200, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[num].hostile = false;
			Main.projectile[num].friendly = true;
		}
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().HorrorRangedSet && item.ranged && Main.rand.Next(3) == 0)
		{
			int num2 = Projectile.NewProjectile(position, new Vector2(speedX, speedY), ((GlobalItem)this).mod.ProjectileType("DreadFlameBlast"), 200, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].friendly = true;
		}
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().HorrorMagicSet && item.magic && Main.rand.Next(3) == 0)
		{
			int num3 = Projectile.NewProjectile(position, new Vector2(speedX, speedY), ((GlobalItem)this).mod.ProjectileType("DreadFlameBlast"), 200, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[num3].hostile = false;
			Main.projectile[num3].friendly = true;
		}
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().HorrorSummonSet && item.summon && Main.rand.Next(1) == 0)
		{
			int num4 = Projectile.NewProjectile(position, new Vector2(speedX, speedY), ((GlobalItem)this).mod.ProjectileType("DreadFlameBlast"), 200, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[num4].hostile = false;
			Main.projectile[num4].friendly = true;
		}
		return true;
	}

	public override void OpenVanillaBag(string context, Player player, int arg)
	{
		if (context == "bossBag" && (arg == 3326 || arg == 3325 || arg == 3327 || arg == 3328 || arg == 3329 || arg == 3332 || arg == ((GlobalItem)this).mod.ItemType("DreadBag") || arg == ((GlobalItem)this).mod.ItemType("EtherealBag") || arg == ((GlobalItem)this).mod.ItemType("UltrumBag") || arg == ((GlobalItem)this).mod.ItemType("IgnodiumBag") || arg == ((GlobalItem)this).mod.ItemType("TrueDreadBag") || arg == ((GlobalItem)this).mod.ItemType("ErebusBag")) && Main.rand.Next(20) == 0)
		{
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("LuxHead"), 1);
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("LuxBody"), 1);
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("LuxLegs"), 1);
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("LuxWings"), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("PoisonHead"), 1);
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("PoisonBody"), 1);
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("PoisonLegs"), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("FutabaHead"), 1);
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("FutabaBody"), 1);
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("FutabaLegs"), 1);
			}
			if (num == 3)
			{
				player.QuickSpawnItem(((GlobalItem)this).mod.ItemType("RockMask"), 1);
			}
		}
	}
}
