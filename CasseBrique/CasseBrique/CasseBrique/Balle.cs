using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    public class Balle : Sprite
    {
        private bool _Launched;
        private bool _Stuck;
        private bool _Out;
        private float _DistanceFromRaquette;
        private float _StartTimer; //Indispensable lorsque l'on relance une balle après une vie perdu pour pas quelle se relance directement en apuiyant la premiere fois sur space car handle input en mm temps qu'update
        private Viewport _Viewport;

        public static int Rayon = 8;

        public float CenterPositionX
        {
            get
            {
                return (Position.X + (Texture.Width / 2));
            }
        }
        public float CenterPositionY
        {
            get
            {
                return (Position.Y + (Texture.Height / 2));
            }
        }
        public bool Out
        {
            get
            {
                return _Out;
            }
            set
            {
                _Out = value;
            }
        }
        public bool PowerBall = false;

        public Balle(Viewport GameViewport)
        {
            _Viewport = GameViewport;
        }

        public override void Initialize()
        {
            base.Initialize();

            Direction = new Vector2(0.7f, -1f);

            _Launched = false;

            _Stuck = false;

            _Out = false;

            _StartTimer = 100.0f;
        }

        public void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState keyboardState, Raquette _Raquette)
        {
            if (keyboardState.IsKeyDown(Keys.Space) && !_Launched && _StartTimer <= 0.0f)
            {
                _Launched = true;
            }

             if (keyboardState.IsKeyDown(Keys.Space) && _Stuck)
            {
                _Stuck = false;
                PositionY = _Raquette.Position.Y - 20;
                Speed = 0.4f;
            }


            //a revoir
            if (((keyboardState.IsKeyDown(Keys.Left) && (Position.X > _Viewport.X)) || (keyboardState.IsKeyDown(Keys.Right) && (Position.X + Texture.Width) < (_Viewport.Width + _Viewport.X) )) && _Stuck)
            {
                PositionX = _Raquette.PositionX + _DistanceFromRaquette;
            }
        }

        public void Update(GameTime gameTime, Raquette _Raquette)
        {
            int PositionCentreRaquette = _Raquette.CollisionRectangle.X + (_Raquette.CollisionRectangle.Width / 2);

            _StartTimer -= ElapsedTime;

            if (_Launched)
            {
                if (!_Stuck)
                {
                    Speed = 0.4f;

                    if ((CenterPositionY >= (_Raquette.CollisionRectangle.Y - Rayon)) &&
                    (CenterPositionY <= (_Raquette.CollisionRectangle.Y + Rayon)) && // On ajoute qd mm le rayon en dessous pour avoir une zone de colision plus épaisse
                    (CenterPositionX >= (_Raquette.CollisionRectangle.X - Rayon)) &&
                    (CenterPositionX <= (_Raquette.CollisionRectangle.X + _Raquette.CollisionRectangle.Width + Rayon))) //Si Touche la raquette
                    {
                        float Ecart = 0;

                        Ecart = Position.X - PositionCentreRaquette;

                        if (Position.X < PositionCentreRaquette)
                        {
                            Ecart = -Ecart;
                        }

                        float EcartMax = _Raquette.CollisionRectangle.Width / 2 + Rayon; // On rajoute le rayon de la balle pour avoir les bords cf shemas raquette

                        float DirectionX = Ecart / EcartMax;
                        float DirectionY = 1 - ((0.8f * Ecart) / EcartMax);

                        if (Direction.X < 0)
                        {
                            DirectionX = -DirectionX;
                        }

                        Direction = new Vector2(DirectionX, -DirectionY);

                        if (_Raquette.ListeBonus.ContainsKey(BonusType.Adhesive))
                        {
                            _DistanceFromRaquette = CenterPositionX - _Raquette.Position.X;
                            _Stuck = true;
                            Speed = 0f;
                        }
                    }
                }

                if ((Position.Y <= _Viewport.Y)) // Si tape le haut du cadre
                {
                    Direction = new Vector2(Direction.X, -Direction.Y);
                }

                if (Position.Y + Texture.Height >= _Viewport.Height + _Viewport.Y)  // Si tape le bas du cadre
                {
                    Speed = 0f;
                    _Out = true;
                }

                if ((Position.X <= _Viewport.X) || (Position.X + Texture.Width >= _Viewport.Width + _Viewport.X))  // Si tape un coté du cadre
                {
                    Direction = new Vector2(-Direction.X, Direction.Y);
                }
            }
            else
            {
                Speed = 0f;

                PositionX = PositionCentreRaquette - Rayon;
                PositionY = _Raquette.CollisionRectangle.Y - Texture.Height;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Color c = Color.White;

            if (PowerBall)
            {
                c = Color.Black;
            }

            spriteBatch.Draw(Texture, Position, c);
        }

        public void ToucheBrique(bool InverserX, bool InverserY)
        {
            if (InverserX)
            {
                Direction = new Vector2(-Direction.X, Direction.Y);
            }

            if (InverserY)
            {
                Direction = new Vector2(Direction.X, -Direction.Y);
            }
        }
    }
}
