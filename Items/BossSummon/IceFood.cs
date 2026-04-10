using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossSummon;

public class IceFood : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Frozen Food");
		((ModItem)this).Tooltip.SetDefault("Attracts the ice dragon\nCan only be used in the snow biome");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.maxStack = 20;
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.UseSound = SoundID.Item44;
		((ModItem)this).item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(((ModItem)this).mod.NPCType("IceDragon")))
		{
			return player.ZoneSnow;
		}
		return false;
	}

	public override bool UseItem(Player player)
	{
		NPC.SpawnOnPlayer(player.whoAmI, ((ModItem)this).mod.NPCType("IceDragon"));
		Main.PlaySound(15, player.position, 0);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(664, 50);
		val.AddIngredient(154, 15);
		val.AddRecipeGroup("Ultranium:RottenChunk/Vetebrae", 6);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
