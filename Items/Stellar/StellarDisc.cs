using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Stellar;

public class StellarDisc : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).Tooltip.SetDefault("Throws Returning Stellar Discs");
		// ((ModItem)this).DisplayName.SetDefault("Stellar Disc");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 40;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 42;
		((Entity)(object)((ModItem)this).Item).height = 42;
		((ModItem)this).Item.useTime = 20;
		((ModItem)this).Item.useAnimation = 20;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 8f;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.value = Item.buyPrice(0, 35, 45);
		((ModItem)this).Item.rare = 5;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("StellarDisc").Type;
		((ModItem)this).Item.shootSpeed = 17f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[((ModItem)this).Item.shoot] < 6;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "StellarBar", 10);
		val.AddTile(134);
		val.Register();
	}
}
