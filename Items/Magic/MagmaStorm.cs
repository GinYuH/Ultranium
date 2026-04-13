using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class MagmaStorm : ModItem
{
	public override void SetStaticDefaults()
	{
		//Tooltip.SetDefault("Shoots magma bolts that burn enemies");
		//DisplayName.SetDefault("Magma Storm");
	}

	public override void SetDefaults()
	{
		Item.damage = 38;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 7;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 22;
		Item.useAnimation = 22;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(0, 10);
		Item.rare = ItemRarityID.LightRed;
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.scale = 0.8f;
		Item.shoot = Mod.Find<ModProjectile>("MagmaBolt").Type;
		Item.shootSpeed = 15f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.SpellTome, 1);
		val.AddIngredient(ItemID.LivingFireBlock, 5);
		val.AddTile(TileID.MythrilAnvil);
		val.Register();
	}
}
