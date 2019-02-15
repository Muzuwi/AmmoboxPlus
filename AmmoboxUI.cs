using Terraria.UI;
using Terraria.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using ReLogic.Graphics;
using Terraria.UI.Chat;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI;
using System.Collections.Generic;
using System;
using Terraria.ModLoader;

namespace AmmoboxPlus.UI {
    class AmmoIconUI : UIState{
        internal static bool vis = false;
        internal AmmoIconSprite icon;
        internal UIPanel ammoPanel;
        static float ratioX = 1530f / 1920;
        static float ratioY = 35f   / 1080;
        //float lastUIscale = 0f;
        //float posX = ratioX * Main.screenWidth;
        //float posY = ratioY * Main.screenHeight;

        public override void OnInitialize() {
            icon = new AmmoIconSprite();
            icon.itemID = -1;

            ammoPanel = new UIPanel();
            ammoPanel.Top.Set(10, 0f);
            ammoPanel.Left.Set(-80, 0f);
            ammoPanel.Width.Set(48f, 0f);
            ammoPanel.Height.Set(48f, 0f);
            ammoPanel.BackgroundColor = new Color(73, 94, 171);
            ammoPanel.Append(icon);
            ammoPanel.VAlign = ratioY;
            ammoPanel.HAlign = ratioX;
            base.Append(ammoPanel);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime); 
            ammoPanel.Top.Set(10 * Main.UIScale, 0f);
            ammoPanel.Left.Set(-80 * Main.UIScale, 0f);

            //  Reset the pos, in case the resolution was changed, so no mod reload is necessary
            //posX = ratioX * Main.screenWidth;
            //posY = ratioY * Main.screenHeight;
            /*Main.NewText(Main.instance.invBottom);
            if (Main.UIScale != 1 && lastUIscale != Main.UIScale) {
                lastUIscale = Main.UIScale;
                ammoPanel.Left.Set((posX / Main.UIScale) - Main.UIScale*50f, 0f);
                ammoPanel.Top.Set( (posY / Main.UIScale) + Main.UIScale*6f, 0f);
            } else if (Main.UIScale == 1) {
                ammoPanel.Left.Set(posX, 0f);
                ammoPanel.Top.Set(posY, 0f);
            }*/
        }
    }

    class AmmoIconSprite : UIElement {
        internal int itemID;
        internal string mouseText = "";
        internal int rarity;

        public AmmoIconSprite() {
            Width.Set(32f, 0f);
            Height.Set(32f, 0f);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if(itemID != -1) {
                Width.Set(Main.itemTexture[itemID].Width, 0);
                Height.Set(Main.itemTexture[itemID].Height, 0);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);

            CalculatedStyle innerDim = base.Parent.GetInnerDimensions();
            if (itemID != -1) {
                float baseX = innerDim.X;
                float baseY = innerDim.Y;

                float textureWidth = Main.itemTexture[itemID].Width;
                float textureHeight = Main.itemTexture[itemID].Height;

                float centerX = baseX + ((innerDim.Width - textureWidth) / 2f);
                float centerY = baseY + ((innerDim.Height - textureHeight) / 2f);
                spriteBatch.Draw(Main.itemTexture[itemID], new Vector2(centerX, centerY), Color.White);
            }
            Vector2 fontSize = Main.fontMouseText.MeasureString("Ammo: ");
            Vector2 textPos = new Vector2(innerDim.X - 14f, innerDim.Y - (fontSize.Y + 5f));


            float alphaOrSomething = Main.mouseTextColor / 255f;
            Color baseColor = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, "Ammo: ", textPos, baseColor, 0, Vector2.Zero, Vector2.One);


            Color fontColor = AmmoboxHelpfulMethods.getRarityColor(rarity);
            Vector2 mousePos = new Vector2(Main.mouseX, Main.mouseY);
            if (base.Parent.ContainsPoint(mousePos) && mouseText != "") {
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, mouseText, mousePos + new Vector2(15, 15), fontColor, 0, Vector2.Zero, Vector2.One);
            }

        }
    }


    class AmmoSelectorUI : UIState {
        //  Is the UI visible?
        internal static bool visible = false;
        //  Spawn position, i.e. mouse position at the time of pressing the hotkey
        internal static Vector2 spawnPosition;
        internal static Vector2 leftCorner;

        //  Circle diameter
        internal static int mainDiameter = 36;
        //  Circle radius
        internal static int mainRadius = 36 / 2;

        //  Amount of spawned circles
        internal static int circleAmount = -1;
        //  Held item type
        internal static int heldItemType = -1;
        //  List of ammo types matching the properties of the held weapon
        internal static List<int> ammoTypes;
        //  Ammo count of ammo types used
        internal static Dictionary<int, int> ammoCount;
        //  Which ammo type is currently highlighted?
        internal static int selectedAmmoType = -1;
        //  Which ammo type is in the first ammo slot?
        internal static int currentFirstAmmoType = -1;
        //  Is held item allowed to be used with the ammo switcher?
        internal static bool itemAllowed = false;

        //  Initialization
        public override void OnInitialize() {
            spawnPosition = new Vector2();
            leftCorner = new Vector2();
            ammoTypes = new List<int>();
            ammoCount = new Dictionary<int, int>();
        }

        //  Update ammo type list with any changes made during display of the UI
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            UpdateAmmoTypeList();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            Texture2D bgTexture = Main.wireUITexture[0];

            //  Draw weapon circle
            Rectangle outputRect = new Rectangle((int)leftCorner.X, (int)leftCorner.Y, mainDiameter, mainDiameter);
            spriteBatch.Draw(Main.wireUITexture[CheckMouseWithinCircle(Main.MouseScreen, mainRadius, spawnPosition) ? 1 : 0], outputRect, Color.White);

            //  Draw weapon inside circle
            if (heldItemType != -1) {
                int finalWidth = Main.itemTexture[heldItemType].Width / 2,
                    finalHeight = Main.itemTexture[heldItemType].Height / 2;
                Rectangle outputWeaponRect = new Rectangle((int)spawnPosition.X - (finalWidth / 2), (int)spawnPosition.Y - (finalHeight / 2), finalWidth, finalHeight);
                spriteBatch.Draw(Main.itemTexture[heldItemType], outputWeaponRect, Color.White);
            }

            //  Check how many ammo types are available
            //  TODO: Add the one slot thing
            int outerRadius = 48;
            double offset = 0;
            //  Apply offset depending on the amount of circles to be drawn
            offset = (circleAmount == 3) ? 0.5
                                         : (circleAmount == 4) ? 0.25
                                                                : (circleAmount == 5) ? (1d/9d) : 0;
            //  Angle between each circle
            //  (*Math.PI is @ Line 186)
            double angleSteps = 2.0d / circleAmount;

            //  TODO: Fix weird position of lower circle on circleAmount == 3
            int done = 0;
            //  Starting angle
            double i = offset;
            //  done --> ID of currently drawn circle
            for (done = 0; done < circleAmount; ++done) {
                double x = outerRadius * Math.Cos(i * Math.PI), 
                       y = outerRadius * Math.Sin(i * Math.PI);

                Rectangle ammoBgRect = new Rectangle((int)(leftCorner.X + x), (int)(leftCorner.Y + y), mainDiameter, mainDiameter);
                //  Check if mouse is within the circle checked
                //bool isMouseWithin = CheckMouseWithinCircle(Main.MouseScreen, mainRadius + 8, new Vector2((int)(spawnPosition.X + x), (int)(spawnPosition.Y + y)));
                bool isMouseWithin = CheckMouseWithinWheel(Main.MouseScreen, spawnPosition, 96, mainRadius, offset*Math.PI, circleAmount, done);

                //  Actually draw the bg circle
                spriteBatch.Draw(Main.wireUITexture[isMouseWithin ? 1 : 0], ammoBgRect, (ammoTypes[done] == currentFirstAmmoType) ? Color.Gray : Color.White);

                //  Draw ammo sprites over the icons
                int ammoWidth = Main.itemTexture[ammoTypes[done]].Width,
                    ammoHeight = Main.itemTexture[ammoTypes[done]].Height;
                Rectangle ammoRect = new Rectangle((int)(spawnPosition.X + x) - (ammoWidth / 2), (int)(spawnPosition.Y + y) - (ammoHeight / 2), ammoWidth, ammoHeight);
                spriteBatch.Draw(Main.itemTexture[ammoTypes[done]], ammoRect, (ammoTypes[done] == currentFirstAmmoType ) ? Color.Gray : Color.White);
                if (isMouseWithin) {
                    selectedAmmoType = ammoTypes[done];

                    //  For some reason CloneDefaults doesn't actually clone the type
                    //  Set the type for proper cloning
                    Item onetwo = new Item();
                    onetwo.type = ammoTypes[done];
                    onetwo.CloneDefaults(ammoTypes[done]);

                    //  Draw the tooltip
                    Color fontColor = AmmoboxHelpfulMethods.getRarityColor(onetwo.rare);
                    Vector2 mousePos = new Vector2(Main.mouseX, Main.mouseY);
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, onetwo.Name + " (" + ammoCount[ammoTypes[done]] + ")", mousePos + new Vector2(15, 15), fontColor, 0, Vector2.Zero, Vector2.One);
                }

                /*string temp = "";
                if (done == 0) temp = "0";
                if (done == 1) temp = "1";
                if (done == 2) temp = "2";
                if (done == 3) temp = "3";
                if (done == 4) temp = "4";
                spriteBatch.DrawString(Main.fontItemStack, temp, new Vector2(ammoRect.X, ammoRect.Y), Color.White);*/



                i += angleSteps;
            }
            //ErrorLogger.Log("##############");
            //Main.NewText("############");
        }

        internal void UpdateAmmoTypeList() {
            ammoCount.Clear();
            ammoTypes.Clear();
            if (heldItemType != -1) {
                Item held = new Item();
                held.type = heldItemType;
                held.CloneDefaults(heldItemType);
                List<int> ammos = new List<int>();
                //  Check ammo slots for ammo usable by the currently held weapon
                for (int j = 54; j <= 57; j++) {
                    int chosenType = Main.LocalPlayer.inventory[j].ammo,
                        itemType = Main.LocalPlayer.inventory[j].type;
                    //  If ammo is usable by the weapon
                    if (chosenType == held.useAmmo && itemType > 0) {
                        //  Store the ammo type
                        if (!FindInList(ammos, itemType)) {
                            ammos.Add(itemType);
                        }
                        //  Count the total amount of this ammo type in the ammo slots
                        if (ammoCount.ContainsKey(itemType)) {
                            ammoCount[itemType] += Main.LocalPlayer.inventory[j].stack;
                        } else {
                            ammoCount[itemType] = Main.LocalPlayer.inventory[j].stack;
                        }
                    }
                }

                //  Additionally, search for one ammo type beyond the ammo slots
                if (AmmoboxPlayer.apCanUseBeltAdvanced) {
                    for(int j = 0; j < 54; j++) {
                        int chosenType = Main.LocalPlayer.inventory[j].ammo,
                            itemType = Main.LocalPlayer.inventory[j].type;
                        if(chosenType == held.useAmmo && itemType > 0) {
                            if (!FindInList(ammos, itemType)) {
                                ammos.Add(itemType);
                                if (ammoCount.ContainsKey(itemType)) {
                                    ammoCount[itemType] += Main.LocalPlayer.inventory[j].stack;
                                } else {
                                    ammoCount[itemType] = Main.LocalPlayer.inventory[j].stack;
                                }
                                break;
                            }
                        }
                    }
                }


                circleAmount = ammos.Count;
                //  Sort types, to allow for static ammo positions on the selector wheel
                ammos.Sort();
                ammoTypes = ammos;
            }
            currentFirstAmmoType = Main.LocalPlayer.inventory[54].type;
        }

        internal bool CheckMouseWithinCircle(Vector2 mousePos, int radius, Vector2 center) {
            return ((mousePos.X - center.X) * (mousePos.X - center.X) + (mousePos.Y - center.Y)*(mousePos.Y - center.Y) <= radius * radius);
        }

        internal bool CheckMouseWithinWheel(Vector2 mousePos, Vector2 center, int outerRadius, int innerRadius, double offset, int pieceCount, int elementNumber) {
            //  Check if mouse cursor is inside the outer circle
            bool first = ((mousePos.X - center.X) * (mousePos.X - center.X) + (mousePos.Y - center.Y) * (mousePos.Y - center.Y) <= outerRadius * outerRadius);
            //  Check if mouse cursor is outside the inner circle
            bool second = ((mousePos.X - center.X) * (mousePos.X - center.X) + (mousePos.Y - center.Y) * (mousePos.Y - center.Y) > innerRadius * innerRadius);


            double finalOffset = offset;
            finalOffset *= 180 / Math.PI;
            if (pieceCount == 2) finalOffset -= 90;
            if (pieceCount == 3) finalOffset -= 60;
            if (pieceCount == 4) finalOffset -= 45;
            if (pieceCount == 5) finalOffset -= 35;

            double step = 360 / pieceCount;
            double beginAngle = (finalOffset + step * (elementNumber)) % 360, endAngle = (beginAngle + step) % 360;
            if (beginAngle < 0) beginAngle = 360 + beginAngle;
            
            //  Calculate x,y coords on outer circle
            double calculatedAngle = Math.Atan2(mousePos.Y - center.Y, mousePos.X - center.X);
            calculatedAngle = calculatedAngle * 180/Math.PI;

            if(calculatedAngle < 0) {
                calculatedAngle = 360 + calculatedAngle;
            }

            double calculatedDistance = Math.Sqrt((mousePos.X - center.X) * (mousePos.X - center.X) + (mousePos.Y - center.Y) * (mousePos.Y - center.Y));

            bool third = false;
            //(calculatedAngle <= endAngle && calculatedAngle >= beginAngle);
            if(beginAngle < endAngle) {
                if(beginAngle < calculatedAngle && calculatedAngle < endAngle) {
                    third = true;
                }
            } else {
                if(calculatedAngle > beginAngle && calculatedAngle > endAngle) {
                    third = true;
                }
                if (calculatedAngle < beginAngle && calculatedAngle < endAngle) {
                    third = true;
                }

            }




            //Main.NewText(beginAngle + " " + endAngle + " " + calculatedAngle);
            bool fourth = calculatedDistance <= outerRadius;
            
            //Main.NewText(first + " " + second + " " + third + " " + fourth);
            return first && second && third && fourth;
        }


        internal bool FindInList(List<int> list, int match) {
            foreach(int el in list) {
                if(el == match) {
                    return true;
                }
            }
            return false;
        }

    }
}