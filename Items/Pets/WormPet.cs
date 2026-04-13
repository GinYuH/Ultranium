using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets;

public class WormPet : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Mysterious Scale");
		//Tooltip.SetDefault("Summons a long lost serpent");
	}

	public override void SetDefaults()
	{
		Item.CloneDefaults(2420);
		Item.shoot = ModContent.ProjectileType<BabyWorm>();
		Item.buffType = ModContent.BuffType<BabyWormBuff>();
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
