using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class NatureJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Jungle's Wrath");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.damage = 18;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 54;
		Item.height = 56;
		Item.useTime = 28;
		Item.useAnimation = 28;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 20);
		Item.rare = ItemRarityID.Green;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("NatureJavelin").Type;
		Item.shootSpeed = 9f;
		Item.useTurn = true;
		Item.maxStack = 1;
		Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.JungleSpores, 12);
		val.AddIngredient(ItemID.Stinger, 12);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
