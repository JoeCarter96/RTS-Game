using System;

namespace RTS_Game
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (GameClass game = new GameClass())
            {
                game.Run();
            }
        }
    }
#endif
}

