using System;
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
        public const int C_DEFAULT_BRIQUE_WIDTH = 58;
        public const int C_DEFAULT_BRIQUE_HEIGHT = 22;
        
        private BriqueLevel _Vie = BriqueLevel.Pierre;
        public BriqueLevel Vie
        {
            get
            {
                return _Vie;
            }
        }
        private BriqueLevel _VieOriginelle;
        public BriqueLevel VieOriginelle
        {
            get
            {
                return _VieOriginelle;
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
        public bool MustReloadContent = false;

        public Rectangle CollisionRectangleHaut
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y - (Balle.Rayon), C_DEFAULT_BRIQUE_WIDTH, Balle.Rayon);
            }
        }

        public Rectangle CollisionRectangleBas
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y + C_DEFAULT_BRIQUE_HEIGHT, C_DEFAULT_BRIQUE_WIDTH, Balle.Rayon);
            }
        }

        public Rectangle CollisionRectangleGauche
        {
            get
            {
                return new Rectangle((int)Position.X - Balle.Rayon, (int)Position.Y, Balle.Rayon, C_DEFAULT_BRIQUE_HEIGHT);
            }
        }

        public Rectangle CollisionRectangleDroite
        {
            get
            {
                return new Rectangle((int)Position.X + C_DEFAULT_BRIQUE_WIDTH, (int)Position.Y, Balle.Rayon, C_DEFAULT_BRIQUE_HEIGHT);
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
                return new Rectangle((int)Position.X + C_DEFAULT_BRIQUE_WIDTH, (int)Position.Y - (Balle.Rayon / 2), Balle.Rayon / 2, Balle.Rayon / 2);
            }
        }

        public Rectangle CollisionRectangleBasGauche
        {
            get
            {
                return new Rectangle((int)Position.X - (Balle.Rayon / 2), (int)Position.Y + C_DEFAULT_BRIQUE_HEIGHT, Balle.Rayon / 2, Balle.Rayon / 2);
            }
        }

        public Rectangle CollisionRectangleBasDroit
        {
            get
            {
                return new Rectangle((int)Position.X + C_DEFAULT_BRIQUE_WIDTH, (int)Position.Y + C_DEFAULT_BRIQUE_HEIGHT, Balle.Rayon / 2, Balle.Rayon / 2);
            }
        }

        public Rectangle CollisionRectangleBrique
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, C_DEFAULT_BRIQUE_WIDTH, C_DEFAULT_BRIQUE_HEIGHT);
            }
        }

        public Brique(BriqueLevel Vie, int screenHeight, int screenWidth)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            _Vie = Vie;
            _VieOriginelle = Vie;
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
                base.LoadContent(content, "Brique");
            }

            if (_Vie == BriqueLevel.Acier)
            {
                base.LoadContent(content, "BriqueAcier");
            }

            if (_Vie == BriqueLevel.AcierDur)
            {
                base.LoadContent(content, "BriqueAcierDur");
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

                bool InvertX = false;
                bool InvertY = false;
                bool Collision = false;

                //Colision Balle Venant du haut et de gauche
                if (_Balle.Direction.Y > 0 && _Balle.Direction.X > 0)
                {
                    InvertX = false;
                    InvertY = false;
                    Collision = false;

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
                }
                //Collision Balle venant du haut et de droite
                else if (_Balle.Direction.Y > 0 && _Balle.Direction.X < 0)
                {
                    InvertX = false;
                    InvertY = false;
                    Collision = false;

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
                        if ((_Balle.CenterPositionX - (Position.X + C_DEFAULT_BRIQUE_WIDTH)) < (Position.Y - _Balle.CenterPositionY)) //Balle plus proche du rectangle du haut
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
                }
                //Collision Balle venant du bas et de droite
                else if (_Balle.Direction.Y < 0 && _Balle.Direction.X < 0)
                {
                    InvertX = false;
                    InvertY = false;
                    Collision = false;

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
                        if ((_Balle.CenterPositionX - (Position.X + C_DEFAULT_BRIQUE_WIDTH)) < (_Balle.CenterPositionY - (Position.Y + C_DEFAULT_BRIQUE_HEIGHT))) //Balle plus proche du rectangle du bas
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
                }
                //Collision Balle venant du bas et de Gauche
                else if (_Balle.Direction.Y < 0 && _Balle.Direction.X > 0)
                {
                    InvertX = false;
                    InvertY = false;
                    Collision = false;

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
                        if ((Position.X - _Balle.CenterPositionX) < (_Balle.CenterPositionY - (Position.Y + C_DEFAULT_BRIQUE_HEIGHT))) //Balle plus proche du rectangle du bas
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

                    if (_VieOriginelle > _Vie)
                    {
                        MustReloadContent = true;
                    }
                }


                foreach (Bullets Bullet in _Raquette.ListeBullets)
                {
                    if (CollisionRectangleBrique.Intersects(Bullet.CollisionRectangle))
                    {
                        if (_Vie != BriqueLevel.Incassable)
                        {
                            _Vie--;
                        }

                        Bullet.isObsolete = true;

                        _Raquette.UpdateScore(100);

                        if (_VieOriginelle > _Vie)
                        {
                            MustReloadContent = true;
                        }
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
