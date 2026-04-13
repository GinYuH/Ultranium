using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets;

public class DemonicSingularity : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(210, 0, 0),
		new Color(230, 160, 0)
	};

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Demonic Singularity");
		//Tooltip.SetDefault("Summons a pet cacodemon\n~Developer Item~");
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
		Item.value = Item.sellPrice(0, 20);
		Item.noMelee = true;
		Item.shoot = ModContent.ProjectileType<Cacodemon>();
		Item.buffType = ModContent.BuffType<CacodemonBuff>();
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
