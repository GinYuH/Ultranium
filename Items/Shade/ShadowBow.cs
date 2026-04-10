using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class ShadowBow : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Shadow Bow");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 20;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.value = Item.buyPrice(0, 8);
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = 10;
		((ModItem)this).item.shootSpeed = 6.5f;
		((ModItem)this).item.useAmmo = AmmoID.Arrow;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "NightmareBar", 12);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
