using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.Items.BossSummon;

public class BloodMoonSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Blood Moon Idol");
		((ModItem)this).Tooltip.SetDefault("Summons the Blood moon if used at night");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 20;
		((ModItem)this).item.maxStack = 20;
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.useAnimation = 45;
		((ModItem)this).item.useTime = 45;
		((ModItem)this).item.useStyle = 4;
		((ModItem)this).item.UseSound = SoundID.Item44;
		((ModItem)this).item.consumable = true;
	}

	public override bool CanUseItem(Player player)
	{
		if (!Main.bloodMoon && !Main.dayTime)
		{
			return !ShadowEventWorld.ShadowEventActive;
		}
		return false;
	}

	public override bool UseItem(Player player)
	{
		Main.bloodMoon = true;
		Main.PlaySound(15, player.position, 0);
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "BloodClot", 15);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
