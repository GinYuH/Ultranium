using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Necrosis : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Necrosis");
		Tooltip.SetDefault("Fires Skulls upon swing");
	}

	public override void SetDefaults()
	{
		Item.damage = 27;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 50;
		Item.useAnimation = 25;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 30);
		Item.rare = 2;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = 270;
		Item.shootSpeed = 15f;
	}
}
