using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

public class DreadHeart : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Core of Fear");
		// ((ModItem)this).Tooltip.SetDefault("All projectiles will inflict dread, which deals damage over time to enemies\nYou have a chance to erupt dread flame bolts from your body when you are struck");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 32;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.value = Item.buyPrice(0, 10);
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.accessory = true;
		((ModItem)this).Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<UltraniumPlayer>().DreadHeart = true;
	}
}
