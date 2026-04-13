using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class DreadHeart : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Core of Fear");
		//Tooltip.SetDefault("All projectiles will inflict dread, which deals damage over time to enemies\nYou have a chance to erupt dread flame bolts from your body when you are struck");
	}

	public override void SetDefaults()
	{
		Item.width = 32;
		Item.height = 32;
		Item.value = Item.buyPrice(0, 10);
		Item.rare = 4;
		Item.accessory = true;
		Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<UltraniumPlayer>().DreadHeart = true;
	}
}
