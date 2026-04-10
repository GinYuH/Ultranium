using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class TrueDreadHeart : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("True Core of Fear");
		((ModItem)this).Tooltip.SetDefault("All projectiles will inflict dread, which deals damage over time to enemies\nYou have a chance to erupt dread blasts from your body when you are struck\nEnemies have a chance to explode into dread flame blasts when critically hit");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 32;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.value = Item.buyPrice(0, 10);
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.accessory = true;
		((ModItem)this).item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<UltraniumPlayer>().TrueDreadHeart = true;
	}
}
