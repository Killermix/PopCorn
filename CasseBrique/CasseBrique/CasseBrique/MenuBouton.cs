using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace CasseBrique
{
    public enum MenuPosition
    {
        First = 1,
        Second = 2,
        Third = 3,
        Last = 4
    }

    public class MenuBouton
    {
        private SpriteFont _PopCornFontBig;
        private MenuPosition _MenuPosition;
        private MenuAction _MenuAction;

        private int _screenWidth;
        private int _screenHeight;
        private string _Text = string.Empty;
        private float _CenterPosition
        {
            get
            {
                return (_screenWidth / 2) - (_PopCornFontBig.MeasureString(_Text).X / 2);
            }
        }
        private bool _Selected = false;

        public MenuPosition BoutonPosition
        {
            get
            {
                return _MenuPosition;
            }
        }
        public MenuAction BoutonAction
        {
            get
            {
                return _MenuAction;
            }
        }

        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
            }
        }

        public MenuBouton(string Text, MenuPosition Position, MenuAction Action, int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _Text = Text;
            _MenuPosition = Position;
            _MenuAction = Action;
        }

        public void LoadContent(ContentManager content)
        {
            _PopCornFontBig = content.Load<SpriteFont>("PopCornFontBig");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int Yposition = 0;

            switch (_MenuPosition)
            {
                case MenuPosition.First:
                    Yposition = 150;
                    break;
                case MenuPosition.Second:
                    Yposition = 250;
                    break;
                case MenuPosition.Third:
                    Yposition = 350;
                    break;
                case MenuPosition.Last:
                    Yposition = 450;
                    break;
            }

            Color c = Color.Black;

            if (Selected)
            {
                c = Color.White;
            }

            spriteBatch.DrawString(_PopCornFontBig, _Text, new Vector2(_CenterPosition, Yposition), c);
        }
    }
}
