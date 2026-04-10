using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarDisc : ModItem
{
	public override void SetStaticDefaults()
	{
		// Tooltip.SetDefault("Throws Returning Stellar Discs");
		// DisplayName.SetDefault("Stellar Disc");
	}

	public override void SetDefaults()
	{
		Item.damage = 40;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 42;
		((Entity)(object)Item).height = 42;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.knockBack = 8f;
		Item.noUseGraphic = true;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = 5;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("StellarDisc").Type;
		Item.shootSpeed = 17f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 6;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.Register();
	}
}
