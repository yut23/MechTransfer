﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace MechTransfer.Tiles
{
    class OmniTurretTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            dustType = 1;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int dropId; 
            switch(frameX)
            {
                case 36: dropId = mod.ItemType("SuperOmniTurretItem"); break;
                case 72: dropId = mod.ItemType("MatterProjectorItem"); break;
                default: dropId = mod.ItemType("OmniTurretItem"); break;
            }
            Item.NewItem(i * 16, j * 16, 16, 16, dropId);
        }

        public override void HitWire(int i, int j)
        {
            Rotate(i, j);
        }

        public override void RightClick(int i, int j)
        {
            Rotate(i, j);
        }

        public void Rotate(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile == null || !tile.active())
                return;

            int originX = i - (tile.frameX % 36) / 18;
            int originY = j - (tile.frameY % 36) / 18;

            Tile origin = Main.tile[originX, originY];
            if (origin == null || !origin.active())
                return;

            if (tile.frameX % 36 == 0)
            {
                if (origin.frameY > 0 && origin.frameY < 228)
                {
                    origin.frameY -= 38;
                    Main.tile[originX + 1, originY].frameY -= 38;
                    Main.tile[originX, originY + 1].frameY -= 38;
                    Main.tile[originX + 1, originY + 1].frameY -= 38;
                }
                else if(origin.frameY == 0)
                {
                    origin.frameY = 228;
                    Main.tile[originX + 1, originY].frameY = 228;
                    Main.tile[originX, originY + 1].frameY = 246;
                    Main.tile[originX + 1, originY + 1].frameY = 246;
                }
            }
            else
            {
                if (origin.frameY < 190)
                {
                    origin.frameY += 38;
                    Main.tile[originX + 1, originY].frameY += 38;
                    Main.tile[originX, originY + 1].frameY += 38;
                    Main.tile[originX + 1, originY + 1].frameY += 38;
                }
                else if(origin.frameY == 228)
                {
                    origin.frameY = 0;
                    Main.tile[originX + 1, originY].frameY = 0;
                    Main.tile[originX, originY + 1].frameY = 18;
                    Main.tile[originX + 1, originY + 1].frameY = 18;
                }
            }
        }

        public override void MouseOver(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            if (tile == null || !tile.active())
                return;

            Main.LocalPlayer.showItemIcon2 = mod.ItemType("OmniTurretItem");
            Main.LocalPlayer.showItemIcon = true;
        }
    }
}