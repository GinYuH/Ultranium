using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets;

public class Necromicon : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(200, 0, 0),
		new Color(0, 200, 0)
	};

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Necromicon");
		//Tooltip.SetDefault("Summons a horrifying demon\n~Dedicated Item~");
	}

	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 30;
		Item.damage = 0;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 20;
		Item.useTime = 20;
		Item.UseSound = SoundID.Item2;
		Item.rare = ItemRarityID.Purple;
		Item.value = Item.sellPrice(2);
		Item.noMelee = true;
		Item.shoot = ModContent.ProjectileType<TacoDemon>();
		Item.buffType = ModContent.BuffType<TacoBuff>();
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(Item.buffType, 3600, quiet: false);
		}
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.OverrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
