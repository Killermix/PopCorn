using System;

namespace CasseBrique
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MyPopCorn game = new MyPopCorn())
            {
                game.Run();
            }
        }
    }
#endif
}

