using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarHammer : ModItem
{
	public override void SetStaticDefaults()
	{
		//Tooltip.SetDefault("Throws Stellar Hammers that fall after a while");
		//DisplayName.SetDefault("Stellar Hammer");
	}

	public override void SetDefaults()
	{
		Item.damage = 60;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((Entity)(object)Item).width = 42;
		((Entity)(object)Item).height = 42;
		Item.useTime = 45;
		Item.useAnimation = 50;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 8f;
		Item.noUseGraphic = true;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = ItemRarityID.Pink;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("StellarHammer").Type;
		Item.shootSpeed = 9f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
