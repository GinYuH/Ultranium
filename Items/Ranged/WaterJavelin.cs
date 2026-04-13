using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class WaterJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ancient Javelin");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 18;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 56;
		Item.height = 56;
		Item.useTime = 28;
		Item.useAnimation = 28;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 20);
		Item.rare = 2;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("WaterJavelin").Type;
		Item.shootSpeed = 9f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}
}
