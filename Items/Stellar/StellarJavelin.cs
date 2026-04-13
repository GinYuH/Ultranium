using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Javelin");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 45;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 20;
		Item.height = 20;
		Item.useTime = 28;
		Item.useAnimation = 28;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 45);
		Item.rare = ItemRarityID.Pink;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("StellarJavelin").Type;
		Item.shootSpeed = 11.5f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "StellarBar", 10);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
