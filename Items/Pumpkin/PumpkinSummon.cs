using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin;

public class PumpkinSummon : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Pumpkin Scepter");
		//Tooltip.SetDefault("Summons a small pumpkin to fight with you");
	}

	public override void SetDefaults()
	{
		Item.damage = 12;
		Item.mana = 15;
		Item.DamageType = DamageClass.Summon;
		Item.width = 26;
		Item.height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 0, 50);
		Item.rare = ItemRarityID.Blue;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("PumpSlime").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("PumpBuff").Type;
		Item.buffTime = 3600;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.Pumpkin, 20);
        val.AddRecipeGroup(RecipeGroupID.Wood, 20);
        val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}
