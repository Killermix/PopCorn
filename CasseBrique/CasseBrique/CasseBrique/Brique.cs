﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasseBrique
{
    public enum BriqueLevel
    {
        Morte = 0,
        Pierre = 1,
        Acier = 2,
        AcierDur = 3,
        Incassable = 4
    }

    public class Brique : Sprite
    {
        private int _screenHeight;
        private int _screenWidth;
        
        private BriqueLevel _Vie = BriqueLevel.Pierre;
        public BriqueLevel Vie
        {
            get
            {
                return _Vie;
            }
        }
        private Bonus _BonusEventuel;
        public Bonus BonusEventuel
        {
            get
            {
                return _BonusEventuel;
            }
            set
            {
                _BonusEventuel = value;
            }
        }

        public Rectangle CollisionRectangleHaut
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y - (Balle.Rayon), Texture.Width, Balle.Rayon);
            }
        }

        public Rectangle CollisionRectangleBas
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y + Texture.Height, Texture.Width, Balle.Rayon);
            }
        }

        public Rectangle CollisionRectangleGauche
        {
            get
            {
                return new Rectangle((int)Position.X - Balle.Rayon, (int)Position.Y, Balle.Rayon, Texture.Height);
            }
        }

        public Rectangle CollisionRectangleDroite
        {
            get
            {
                return new Rectangle((int)Position.X + Texture.Width, (int)Position.Y, Balle.Rayon, Texture.Height);
            }
        }

        public Rectangle CollisionRectangleHautGauche
        {
            get
            {
                return new Rectangle((int)Position.X - (Balle.Rayon / 2), (int)Position.Y - (Balle.Rayon / 2), Balle.Rayon / 2 , Balle.Rayon / 2);
            }
        }

        public Rectangle CollisionRectangleHautDroit
        {
            get
            {
                return new Rectangle((int)Position.X + Texture.Width, (int)Position.Y - (Balle.Rayon / 2), Balle.Rayon / 2, Balle.Rayon / 2);
            }
        }

        public Rectangle CollisionRectangleBasGauche
        {
            get
            {
                return new Rectangle((int)Position.X - (Balle.Rayon / 2), (int)Position.Y + Texture.Height, Balle.Rayon / 2, Balle.Rayon / 2);
            }
        }

        public Rectangle CollisionRectangleBasDroit
        {
            get
            {
                return new Rectangle((int)Position.X + Texture.Width, (int)Position.Y + Texture.Height, Balle.Rayon / 2, Balle.Rayon / 2);
            }
        }

        public Brique(BriqueLevel Vie, int screenHeight, int screenWidth)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            _Vie = Vie;
            _BonusEventuel = new Bonus();
        }

        public override void Initialize()
        {
            base.Initialize();

            PositionX = 40;
            PositionY = 200;
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            if (_Vie == BriqueLevel.Pierre)
            {
                base.LoadContent(content, "brique2");
            }

            if (_Vie == BriqueLevel.Acier)
            {
                base.LoadContent(content, "BriqueAcier2");
            }

            if (_Vie == BriqueLevel.AcierDur)
            {
                base.LoadContent(content, "BriqueAcierDur2");
            }

            if (_Vie == BriqueLevel.Incassable)
            {
                base.LoadContent(content, "BriqueIncassable");
            }

            if (!_BonusEventuel.isObsolete)
            {
                _BonusEventuel.LoadContent(content);

                _BonusEventuel.Initialize(_screenHeight, this);
            }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime, Balle _Balle, Raquette _Raquette)
        {
            if (_Vie != BriqueLevel.Morte)
            {
                if (_Raquette.ListeBonus.ContainsKey(BonusType.PowerBall))
                {
                    _Balle.PowerBall = true;
                }
                else
                {
                    _Balle.PowerBall = false;
                }

                //Colision Balle Venant du haut et de gauche
                if (_Balle.Direction.Y > 0 && _Balle.Direction.X > 0)
                {
                    bool InvertX = false;
                    bool InvertY = false;
                    bool Collision = false;

                    //Colision a gauche
                    if(CollisionRectangleGauche.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        Collision = true;
                        InvertX = true;
                    }

                    //Colision en haut
                    if (CollisionRectangleHaut.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        Collision = true;
                        InvertY = true;
                    }

                    if (!Collision && CollisionRectangleHautGauche.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        if ((Position.X - _Balle.CenterPositionX) < (Position.Y - _Balle.CenterPositionY)) //Balle plus proche du rectangle du haut
                        {
                            Collision = true;
                            InvertY = true;
                        }
                        else //Balle plus proche du rectangle du côté
                        {
                            Collision = true;
                            InvertX = true;
                        }
                    }

                    if(Collision)
                    {
                        if (_Raquette.ListeBonus.ContainsKey(BonusType.PowerBall))
                        {
                            _Vie = BriqueLevel.Morte;
                        }
                        else
                        {
                            if (_Vie != BriqueLevel.Incassable)
                            {
                                _Vie--;
                            }

                            _Balle.ToucheBrique(InvertX, InvertY);
                        }

                        _Raquette.UpdateScore(100);
                    }
                }

                //Collision Balle venant du haut et de droite
                if (_Balle.Direction.Y > 0 && _Balle.Direction.X < 0)
                {
                    bool InvertX = false;
                    bool InvertY = false;
                    bool Collision = false;

                    //Colision a droite
                    if (CollisionRectangleDroite.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        Collision = true;
                        InvertX = true;
                    }

                    //Colision en haut
                    if (CollisionRectangleHaut.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        Collision = true;
                        InvertY = true;
                    }

                    if (!Collision && CollisionRectangleHautDroit.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        if ((_Balle.CenterPositionX - (Position.X + Texture.Width)) < (Position.Y - _Balle.CenterPositionY)) //Balle plus proche du rectangle du haut
                        {
                            Collision = true;
                            InvertY = true;
                        }
                        else //Balle plus proche du rectangle du côté
                        {
                            Collision = true;
                            InvertX = true;
                        }
                    }

                    if (Collision)
                    {
                        if (_Raquette.ListeBonus.ContainsKey(BonusType.PowerBall))
                        {
                            _Vie = BriqueLevel.Morte;
                        }
                        else
                        {
                            if (_Vie != BriqueLevel.Incassable)
                            {
                                _Vie--;
                            }

                            _Balle.ToucheBrique(InvertX, InvertY);
                        }

                        _Raquette.UpdateScore(100);
                    }
                }

                //Collision Balle venant du bas et de droite
                if (_Balle.Direction.Y < 0 && _Balle.Direction.X < 0)
                {
                    bool InvertX = false;
                    bool InvertY = false;
                    bool Collision = false;

                    //Colision a droite
                    if (CollisionRectangleDroite.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        Collision = true;
                        InvertX = true;
                    }

                    //Colision en bas
                    if (CollisionRectangleBas.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        Collision = true;
                        InvertY = true;
                    }

                    if (!Collision && CollisionRectangleBasDroit.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        if ((_Balle.CenterPositionX - (Position.X + Texture.Width)) < (_Balle.CenterPositionY - (Position.Y + Texture.Height))) //Balle plus proche du rectangle du bas
                        {
                            Collision = true;
                            InvertY = true;
                        }
                        else //Balle plus proche du rectangle du côté
                        {
                            Collision = true;
                            InvertX = true;
                        }
                    }

                    if (Collision)
                    {
                        if (_Raquette.ListeBonus.ContainsKey(BonusType.PowerBall))
                        {
                            _Vie = BriqueLevel.Morte;
                        }
                        else
                        {
                            if (_Vie != BriqueLevel.Incassable)
                            {
                                _Vie--;
                            }

                            _Balle.ToucheBrique(InvertX, InvertY);
                        }

                        _Raquette.UpdateScore(100);
                    }
                }

                //Collision Balle venant du bas et de Gauche
                if (_Balle.Direction.Y < 0 && _Balle.Direction.X > 0)
                {
                    bool InvertX = false;
                    bool InvertY = false;
                    bool Collision = false;

                    //Colision a gauche
                    if (CollisionRectangleGauche.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        Collision = true;
                        InvertX = true;
                    }

                    //Colision en bas
                    if (CollisionRectangleBas.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        Collision = true;
                        InvertY = true;
                    }

                    if (!Collision && CollisionRectangleBasGauche.Contains((int)_Balle.CenterPositionX, (int)_Balle.CenterPositionY))
                    {
                        if ((Position.X - _Balle.CenterPositionX) < (_Balle.CenterPositionY - (Position.Y + Texture.Height))) //Balle plus proche du rectangle du bas
                        {
                            Collision = true;
                            InvertY = true;
                        }
                        else //Balle plus proche du rectangle du côté
                        {
                            Collision = true;
                            InvertX = true;
                        }
                    }

                    if (Collision)
                    {
                        if (_Raquette.ListeBonus.ContainsKey(BonusType.PowerBall))
                        {
                            _Vie = BriqueLevel.Morte;
                        }
                        else
                        {
                            if (_Vie != BriqueLevel.Incassable)
                            {
                                _Vie--;
                            }

                            _Balle.ToucheBrique(InvertX, InvertY);
                        }

                        _Raquette.UpdateScore(100);
                    }
                }

                if (_Vie == BriqueLevel.Morte)
                {
                    _Raquette.UpdateScore(500);
                }
            }
            else
            {
                if (!_BonusEventuel.isObsolete)
                {
                    _BonusEventuel.Update(gameTime, _Raquette);
                }
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_Vie != BriqueLevel.Morte)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
            else
            {
                if (!_BonusEventuel.isObsolete)
                {
                    _BonusEventuel.Draw(spriteBatch, gameTime);
                }
            }
        }
    }
}
