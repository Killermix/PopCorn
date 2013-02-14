using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CasseBrique
{
    /// <summary>
    /// Classe de base d'un niveau gère l'update et le draw de toute les briques
    /// </summary>
    public class Monde : Sprite
    {
        public List<Brique> Briques;

        private Viewport _Viewport;
        public int Niveau = 1;

        public Monde(Viewport GameViewport)
        {
            _Viewport = GameViewport;
        }

        public override void Initialize()
        {
            Position = new Vector2(_Viewport.X, _Viewport.Y);

            Briques = new List<Brique>();

            //if (Niveau == 1)
            //{
            //    int EspacementHorizontal = 78;
            //    int EspacementVertical = 52;

            //    int InvertLigneToVie = 3;

            //    for (int Ligne = 0; Ligne < 3; Ligne++)
            //    {
            //        for (int Colonne = 0; Colonne < 5; Colonne++)
            //        {
            //            Brique UneBrique = new Brique((BriqueLevel)(InvertLigneToVie - Ligne), _screenHeight, _screenWidth);

            //            UneBrique.Initialize();

            //            UneBrique.PositionX += (Colonne * EspacementHorizontal);

            //            UneBrique.PositionY += (Ligne * EspacementVertical);

            //            Briques.Add(UneBrique);

            //            UneBrique.indexOf = Briques.IndexOf(UneBrique);
            //        }
            //    }
            //}

            if (Niveau == 1)
            {
                int EspacementHorizontal = 60;
                int EspacementVertical = 52;

                int FirstBriquePositionX = 40;
                int FirstBriquePositionY = 100;

                int InvertLigneToVie = 3;

                for (int Ligne = 0; Ligne < 5; Ligne++)
                {
                    for (int Colonne = 0; Colonne < 7; Colonne++)
                    {
                        Brique UneBrique;

                        if (Ligne < 3)
                        {
                            UneBrique = new Brique((BriqueLevel)(InvertLigneToVie - Ligne), _Viewport.Height, _Viewport.Width);
                        }
                        else
                        {
                            UneBrique = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);
                        }


                        UneBrique.PositionX = FirstBriquePositionX;
                        UneBrique.PositionY = FirstBriquePositionY;

                        UneBrique.PositionX += (Colonne * EspacementHorizontal);

                        UneBrique.PositionY += (Ligne * EspacementVertical);

                        Briques.Add(UneBrique);
                    }
                }
            }

            if (Niveau == 2)
            {
                int EspacementHorizontal = 60;
                int EspacementVertical = 24;

                int FirstBriquePositionX = 40;
                int FirstBriquePositionY = 50;

                for (int Ligne = 0; Ligne < 12; Ligne++)
                {
                    for (int Colonne = 0; Colonne < 7; Colonne++)
                    {
                        if (Ligne > 0 && Ligne < 11 && (Colonne == 0 || Colonne == 6))
                        {
                            Brique UneBrique = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                            UneBrique.PositionX = FirstBriquePositionX;
                            UneBrique.PositionY = FirstBriquePositionY;

                            UneBrique.PositionX += (Colonne * EspacementHorizontal);

                            UneBrique.PositionY += (Ligne * EspacementVertical);

                            Briques.Add(UneBrique);
                        }
                        else if (Ligne == 0 || (Ligne == 11 && Colonne != 3))
                        {
                            Brique UneBrique = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                            UneBrique.PositionX = FirstBriquePositionX;
                            UneBrique.PositionY = FirstBriquePositionY;

                            UneBrique.PositionX += (Colonne * EspacementHorizontal);

                            UneBrique.PositionY += (Ligne * EspacementVertical);

                            Briques.Add(UneBrique);
                        }
                        else if(Ligne > 0 && Ligne < 11 && (Colonne == 2 || Colonne == 4) && Ligne != 5 && Ligne != 6)
                        {
                            Brique UneBrique = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                            UneBrique.PositionX = FirstBriquePositionX;
                            UneBrique.PositionY = FirstBriquePositionY;

                            UneBrique.PositionX += (Colonne * EspacementHorizontal);

                            UneBrique.PositionY += (Ligne * EspacementVertical);

                            Briques.Add(UneBrique);
                        }
                    }
                }
            }


            if (Niveau == 3)
            {
                int EspacementHorizontal = 60;

                int FirstBriquePositionX = 11;
                int FirstBriquePositionY = 310;

                for (int Colonne = 0; Colonne < 8; Colonne++)
                {
                    if (Colonne < 3 || Colonne > 4)
                    {
                        Brique UneBrique = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                        UneBrique.PositionX = FirstBriquePositionX;
                        UneBrique.PositionY = FirstBriquePositionY;

                        UneBrique.PositionX += (Colonne * EspacementHorizontal);

                        Briques.Add(UneBrique);
                    }
                }

                Brique bla = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla.Position = new Vector2(FirstBriquePositionX + (3 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 286);

                Briques.Add(bla);

                Brique bla2 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla2.Position = new Vector2(FirstBriquePositionX + (4 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 286);

                Briques.Add(bla2);

                Brique bla3 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla3.Position = new Vector2(FirstBriquePositionX + (3 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 262);

                Briques.Add(bla3);

                Brique bla4 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla4.Position = new Vector2(FirstBriquePositionX + (4 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 262);

                Briques.Add(bla4);

                Brique bla5 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla5.Position = new Vector2(FirstBriquePositionX + (3 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 238);

                Briques.Add(bla5);

                Brique bla6 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla6.Position = new Vector2(FirstBriquePositionX + (3 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 214);

                Briques.Add(bla6);

                Brique bla7 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla7.Position = new Vector2(FirstBriquePositionX + (3 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 190);

                Briques.Add(bla7);

                Brique bla8 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla8.Position = new Vector2(FirstBriquePositionX + (4 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 190);

                Briques.Add(bla8);

                Brique bla9 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla9.Position = new Vector2(FirstBriquePositionX + (5 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 190);

                Briques.Add(bla9);

                Brique bla10 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla10.Position = new Vector2(FirstBriquePositionX + (5 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 262);

                Briques.Add(bla10);

                Brique bla11 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla11.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 262);

                Briques.Add(bla11);

                Brique bla12 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla12.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 238);

                Briques.Add(bla12);

                Brique bla13 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla13.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 214);

                Briques.Add(bla13);

                Brique bla14 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla14.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 190);

                Briques.Add(bla14);

                Brique bla15 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla15.Position = new Vector2(FirstBriquePositionX + (5 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 166);

                Briques.Add(bla15);

                Brique bla16 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla16.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 166);

                Briques.Add(bla16);

                Brique bla17 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla17.Position = new Vector2(FirstBriquePositionX + (5 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 142);

                Briques.Add(bla17);

                Brique bla18 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla18.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 142);

                Briques.Add(bla18);

                Brique bla19 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla19.Position = new Vector2(FirstBriquePositionX + (5 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 118);

                Briques.Add(bla19);

                Brique bla20 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla20.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 118);

                Briques.Add(bla20);

                Brique bla21 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla21.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 94);

                Briques.Add(bla21);

                Brique bla22 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla22.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 70);

                Briques.Add(bla22);

                Brique bla23 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla23.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 46);

                Briques.Add(bla23);

                Brique bla24 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla24.Position = new Vector2(FirstBriquePositionX + (5 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 46);

                Briques.Add(bla24);

                Brique bla25 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla25.Position = new Vector2(FirstBriquePositionX + (4 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 46);

                Briques.Add(bla25);

                Brique bla26 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla26.Position = new Vector2(FirstBriquePositionX + (3 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 46);

                Briques.Add(bla26);

                Brique bla27 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla27.Position = new Vector2(FirstBriquePositionX + (2 * EspacementHorizontal) + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 46);

                Briques.Add(bla27);

                Brique bla28 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla28.Position = new Vector2(FirstBriquePositionX + EspacementHorizontal + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 46);

                Briques.Add(bla28);

                for (int i = 0; i < 10; i++)
                {
                    Brique UneBrique = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                    UneBrique.Position = new Vector2(FirstBriquePositionX + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 46 + (i * 24));

                    Briques.Add(UneBrique);
                }

                for (int i = 0; i < 9; i++)
                {
                    Brique UneBrique = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                    UneBrique.Position = new Vector2(FirstBriquePositionX + EspacementHorizontal + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 70 + (i * 24));

                    Briques.Add(UneBrique);
                }

                for (int i = 0; i < 5; i++)
                {
                    Brique UneBrique = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                    UneBrique.Position = new Vector2(FirstBriquePositionX + 2 * EspacementHorizontal + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 70 + (i * 24));

                    Briques.Add(UneBrique);
                }

                for (int i = 0; i < 5; i++)
                {
                    Brique UneBrique = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                    UneBrique.Position = new Vector2(FirstBriquePositionX + 3 * EspacementHorizontal + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 70 + (i * 24));

                    Briques.Add(UneBrique);
                }

                for (int i = 0; i < 2; i++)
                {
                    Brique UneBrique = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                    UneBrique.Position = new Vector2(FirstBriquePositionX + 4 * EspacementHorizontal + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 70 + (i * 24));

                    Briques.Add(UneBrique);
                }

                Brique bla29 = new Brique(BriqueLevel.Incassable, _Viewport.Height, _Viewport.Width);

                bla29.Position = new Vector2(FirstBriquePositionX + EspacementHorizontal + (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) + 1, 262);

                Briques.Add(bla29);

                Brique test1 = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                test1.Position = new Vector2(FirstBriquePositionX + (4 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 262);

                Briques.Add(test1);

                Brique test2 = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                test2.Position = new Vector2(FirstBriquePositionX + (4 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 286);

                Briques.Add(test2);

                Brique test3 = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                test3.Position = new Vector2(FirstBriquePositionX + (5 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 214);

                Briques.Add(test3);

                Brique test4 = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                test4.Position = new Vector2(FirstBriquePositionX + (5 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 238);

                Briques.Add(test4);

                Brique test5 = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                test5.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 166);

                Briques.Add(test5);

                Brique test6 = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                test6.Position = new Vector2(FirstBriquePositionX + (6 * EspacementHorizontal) - (Brique.C_DEFAULT_BRIQUE_WIDTH / 2) - 1, 142);

                Briques.Add(test6);
            }
        }

        /// <summary>
        /// Charge les textures de chaque brique
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("BackgroundNiveau" + Niveau);

            foreach (Brique b in Briques)
            {
                b.LoadContent(content);
            }
        }


        /// <summary>
        /// Update de chaque brique
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="_Balle"></param>
        /// <param name="_Raquette"></param>
        public void Update(GameTime gameTime, Balle _Balle, Raquette _Raquette)
        {
            List<Brique> BriquesToDelete = new List<Brique>();

            foreach (Brique b in Briques)
            {
                b.Update(gameTime, _Balle, _Raquette);

                if (b.Vie == BriqueLevel.Morte && b.BonusEventuel.isObsolete) // On ne supprime la référence de la brique que lorsque que son bonus est également devenu inutile
                {
                    BriquesToDelete.Add(b);
                }
            }

            foreach (Brique b in BriquesToDelete)
            {
                Briques.Remove(b);
            }

            List<Bullets> BulletsToDelete = new List<Bullets>();

            foreach (Bullets Bullet in _Raquette.ListeBullets)
            {
                Bullet.Update(gameTime);

                if (Bullet.isObsolete)
                {
                    BulletsToDelete.Add(Bullet);
                }
            }

            foreach (Bullets Bullet in BulletsToDelete)
            {
                _Raquette.ListeBullets.Remove(Bullet);
            }
        }

        /// <summary>
        /// Dessine chaque brique
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);

            foreach (Brique b in Briques)
            {
                b.Draw(spriteBatch, gameTime);
            }
        }

        public void ReloadSomeContent(ContentManager content)
        {
            List<Brique> BriquesToReloadContent = (from b in Briques
                                                   where b.MustReloadContent
                                                   select b).ToList();

            foreach (Brique b in BriquesToReloadContent)
            {
                switch (b.VieOriginelle)
                {
                    case BriqueLevel.Acier:
                        b.LoadContent(content, "BriqueAcierCassee");
                        break;
                    case BriqueLevel.AcierDur:
                        if (b.Vie == BriqueLevel.Acier)
                        {
                            b.LoadContent(content, "BriqueAcierDurCasse1");
                        }
                        if (b.Vie == BriqueLevel.Pierre)
                        {
                            b.LoadContent(content, "BriqueAcierDurCasse2");
                        }
                        break;
                    case BriqueLevel.Incassable:
                    case BriqueLevel.Morte:
                    case BriqueLevel.Pierre:
                    default:
                        break;
                }


                b.MustReloadContent = false;
            }
        }
    }
}
