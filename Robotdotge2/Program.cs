using System;
using SplashKitSDK;

namespace Robotdotge2
{
    public class Program
    {
        public static void Main()
        {
            //create a new window for the game
            Window gameWindow = new Window("game", 800, 600); 
            Robotdodge game = new Robotdodge(gameWindow);

            //game loop
            while(! gameWindow.CloseRequested && !game.Quit && game._player.IsAlive())
            {
            //process events such as key presses and window closing    
            SplashKit.ProcessEvents(); 
        
            //Handle player input
            game.HandleInput();

            //update the game
            game.Update();
            
            //draw game object
            game.Draw();
            }
            
        }
    }

}
