using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Spirit;

public class GhostBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Phantom's Razor");
		((ModItem)this).Tooltip.SetDefault("Throws out phantom razors");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.noUseGraphic = true;
		((ModItem)this).item.damage = 67;
		((ModItem)this).item.ranged = true;
		((Entity)(object)((ModItem)this).item).width = 80;
		((Entity)(object)((ModItem)this).item).height = 80;
		((ModItem)this).item.useTime = 17;
		((ModItem)this).item.useAnimation = 17;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 55, 50);
		((ModItem)this).item.rare = 8;
		((ModItem)this).item.UseSound = SoundID.Item69;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("GhostBlade");
		((ModItem)this).item.shootSpeed = 16f;
		((ModItem)this).item.alpha = 60;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[((ModItem)this).item.shoot] < 10;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(3261, 12);
		val.AddTile(412);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
