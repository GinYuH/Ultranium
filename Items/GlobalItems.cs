using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Ultranium.Items.Vanity.Futaba;
using Ultranium.Items.Vanity.Lux;
using Ultranium.Items.Vanity.Pois1x;
using Ultranium.Items.Vanity.Rock;

namespace Ultranium.Items;

public class GlobalItems : GlobalItem
{
	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		if (item.type == 579)
		{
			TooltipLine item2 = new TooltipLine(((GlobalItem)this).Mod, "DraxToolTip", "Able to mine Depthstone");
			tooltips.Add(item2);
		}
		if (item.type == 990)
		{
			TooltipLine item3 = new TooltipLine(((GlobalItem)this).Mod, "PickaxeAxeToolTip", "Able to mine Depthstone");
			tooltips.Add(item3);
		}
	}

	public override void UpdateAccessory(Item item, Player player, bool hideVisual)
	{
		if (item.type == 3090)
		{
			player.npcTypeNoAggro[((GlobalItem)this).Mod.Find<ModNPC>("TenebrisSlime").Type] = true;
			player.npcTypeNoAggro[((GlobalItem)this).Mod.Find<ModNPC>("DepthSlime").Type] = true;
		}
	}

	public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float speedX = velocity.X;
		float speedY = velocity.Y;
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().HorrorMeleeSet && item.CountsAsClass(DamageClass.Melee) && Main.rand.Next(3) == 0)
		{
			int num = Projectile.NewProjectile(source, position, new Vector2(velocity.X, velocity.Y), ((GlobalItem)this).Mod.Find<ModProjectile>("DreadFlameBlast").Type, 200, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[num].hostile = false;
			Main.projectile[num].friendly = true;
		}
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().HorrorRangedSet && item.CountsAsClass(DamageClass.Ranged) && Main.rand.Next(3) == 0)
		{
			int num2 = Projectile.NewProjectile(source, position, new Vector2(velocity.X, velocity.Y), ((GlobalItem)this).Mod.Find<ModProjectile>("DreadFlameBlast").Type, 200, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[num2].hostile = false;
			Main.projectile[num2].friendly = true;
		}
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().HorrorMagicSet && item.CountsAsClass(DamageClass.Magic) && Main.rand.Next(3) == 0)
		{
			int num3 = Projectile.NewProjectile(source, position, new Vector2(velocity.X, velocity.Y), ((GlobalItem)this).Mod.Find<ModProjectile>("DreadFlameBlast").Type, 200, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[num3].hostile = false;
			Main.projectile[num3].friendly = true;
		}
		if (Main.player[Main.myPlayer].GetModPlayer<UltraniumPlayer>().HorrorSummonSet && item.CountsAsClass(DamageClass.Summon) && Main.rand.Next(1) == 0)
		{
			int num4 = Projectile.NewProjectile(source, position, new Vector2(velocity.X, velocity.Y), ((GlobalItem)this).Mod.Find<ModProjectile>("DreadFlameBlast").Type, 200, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[num4].hostile = false;
			Main.projectile[num4].friendly = true;
		}
		return true;
	}

    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
		int arg = item.type;
		if ((arg == 3326 || arg == 3325 || arg == 3327 || arg == 3328 || arg == 3329 || arg == 3332 || arg == ((GlobalItem)this).Mod.Find<ModItem>("DreadBag").Type || arg == ((GlobalItem)this).Mod.Find<ModItem>("EtherealBag").Type || arg == ((GlobalItem)this).Mod.Find<ModItem>("UltrumBag").Type || arg == ((GlobalItem)this).Mod.Find<ModItem>("IgnodiumBag").Type || arg == ((GlobalItem)this).Mod.Find<ModItem>("TrueDreadBag").Type || arg == ((GlobalItem)this).Mod.Find<ModItem>("ErebusBag").Type))
		{
			IItemDropRule luxRule = ItemDropRule.Common(ModContent.ItemType<LuxHead>(), 4);
            luxRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<LuxBody>()));
            luxRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<LuxLegs>()));
            luxRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<LuxWings>()));
            IItemDropRule poisonRule = ItemDropRule.Common(ModContent.ItemType<PoisonHead>(), 3);
			poisonRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PoisonBody>()));
            poisonRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PoisonLegs>()));
            IItemDropRule futaba = ItemDropRule.Common(ModContent.ItemType<FutabaHead>(), 2);
            futaba.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FutabaBody>()));
            futaba.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FutabaLegs>()));
			IItemDropRule rock = ItemDropRule.Common(ModContent.ItemType<RockMask>());
			futaba.OnFailedRoll(rock);
            poisonRule.OnFailedRoll(futaba);
            luxRule.OnFailedRoll(poisonRule);

            itemLoot.Add(luxRule);
        }
    }
}
