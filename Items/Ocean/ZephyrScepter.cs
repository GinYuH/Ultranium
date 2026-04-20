using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ocean;

public class ZephyrScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Zephyr Scepter");
		//Tooltip.SetDefault("Summons a baby zephyr squid to fight with you");
	}

	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Summon;
		Item.mana = 20;
		Item.damage = 14;
		Item.width = 26;
		Item.height = 26;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 35, 45);
		Item.rare = ItemRarityID.Green;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("BabySquid").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("BabySquidBuff").Type;
		Item.buffTime = 3600;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(null, "OceanScale", 8);
		val.AddIngredient(ItemID.Coral, 5);
		val.AddTile(TileID.Anvils);
		val.Register();
	}
}
