using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion;

public class StaffOfCthulhu : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Staff of Cthulhu");
		//Tooltip.SetDefault("Summons a Servant of Cthulhu to fight for you");
	}

	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Summon;
		Item.mana = 20;
		Item.damage = 7;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 0, 80);
		Item.rare = ItemRarityID.Blue;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("EyeMinion").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("EyeBuff").Type;
		Item.buffTime = 3600;
	}
}
