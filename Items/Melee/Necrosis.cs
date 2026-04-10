using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Necrosis : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Necrosis");
		// ((ModItem)this).Tooltip.SetDefault("Fires Skulls upon swing");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 27;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)((ModItem)this).Item).width = 42;
		((Entity)(object)((ModItem)this).Item).height = 42;
		((ModItem)this).Item.useTime = 50;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 30);
		((ModItem)this).Item.rare = 2;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = 270;
		((ModItem)this).Item.shootSpeed = 15f;
	}
}
