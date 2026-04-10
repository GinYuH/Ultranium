using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class GlacialWand : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glacial Wand");
		((ModItem)this).Tooltip.SetDefault("Casts a slow moving ice twister");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 25;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 20;
		((Entity)(object)((ModItem)this).item).width = 32;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 2f;
		((ModItem)this).item.value = Item.buyPrice(0, 20);
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("SnowTwister");
		((ModItem)this).item.shootSpeed = 6.5f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(664, 10);
		val.AddIngredient((Mod)null, "IcePelt", 7);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
