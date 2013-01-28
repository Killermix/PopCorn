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

                Brique test = new Brique(BriqueLevel.Pierre, _Viewport.Height, _Viewport.Width);

                test.Position = new Vector2(10, 10);

                Briques.Add(test);
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
