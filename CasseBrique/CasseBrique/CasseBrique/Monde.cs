using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasseBrique
{
    /// <summary>
    /// Classe de base d'un niveau gère l'update et le draw de toute les briques
    /// </summary>
    public class Monde : Sprite
    {
        public List<Brique> Briques;

        private int _screenHeight;
        private int _screenWidth;

        public Monde(int screenWidth, int screenHeight)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
        }

        public void Initialize(int Niveau)
        {
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

            if (Niveau == 2)
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
                            UneBrique = new Brique((BriqueLevel)(InvertLigneToVie - Ligne), _screenHeight, _screenWidth);
                        }
                        else
                        {
                            UneBrique = new Brique(BriqueLevel.Pierre, _screenHeight, _screenWidth);
                        }


                        UneBrique.PositionX = FirstBriquePositionX;
                        UneBrique.PositionY = FirstBriquePositionY;

                        UneBrique.PositionX += (Colonne * EspacementHorizontal);

                        UneBrique.PositionY += (Ligne * EspacementVertical);

                        Briques.Add(UneBrique);
                    }
                }
            }

            if (Niveau == 1)
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
                            Brique UneBrique = new Brique(BriqueLevel.Incassable, _screenHeight, _screenWidth);

                            UneBrique.PositionX = FirstBriquePositionX;
                            UneBrique.PositionY = FirstBriquePositionY;

                            UneBrique.PositionX += (Colonne * EspacementHorizontal);

                            UneBrique.PositionY += (Ligne * EspacementVertical);

                            Briques.Add(UneBrique);
                        }
                        else if (Ligne == 0 || (Ligne == 11 && Colonne != 3))
                        {
                            Brique UneBrique = new Brique(BriqueLevel.Incassable, _screenHeight, _screenWidth);

                            UneBrique.PositionX = FirstBriquePositionX;
                            UneBrique.PositionY = FirstBriquePositionY;

                            UneBrique.PositionX += (Colonne * EspacementHorizontal);

                            UneBrique.PositionY += (Ligne * EspacementVertical);

                            Briques.Add(UneBrique);
                        }
                        else if(Ligne > 0 && Ligne < 11 && (Colonne == 2 || Colonne == 4) && Ligne != 5 && Ligne != 6)
                        {
                            Brique UneBrique = new Brique(BriqueLevel.Pierre, _screenHeight, _screenWidth);

                            UneBrique.PositionX = FirstBriquePositionX;
                            UneBrique.PositionY = FirstBriquePositionY;

                            UneBrique.PositionX += (Colonne * EspacementHorizontal);

                            UneBrique.PositionY += (Ligne * EspacementVertical);

                            Briques.Add(UneBrique);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Charge les textures de chaque brique
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
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
        }

        /// <summary>
        /// Dessine chaque brique
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Brique b in Briques)
            {
                b.Draw(spriteBatch, gameTime);
            }
        }
    }
}
