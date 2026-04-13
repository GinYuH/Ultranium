using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets.Console;

public class Cabbage : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Mysterious Cabbage");
		//Tooltip.SetDefault("Summons a strange guinea pig\n'Makes a great first companion!'");
	}

	public override void SetDefaults()
	{
		Item.rare = ItemRarityID.Orange;
		Item.CloneDefaults(ItemID.Fish);
		Item.shoot = ModContent.ProjectileType<GuineaPig>();
		Item.buffType = ModContent.BuffType<GuineaPigBuff>();
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(Item.buffType, 3600, quiet: false);
		}
	}

	public override bool CanUseItem(Player player)
	{
		return player.miscEquips[0].IsAir;
	}
}
