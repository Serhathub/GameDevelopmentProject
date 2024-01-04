using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerDemo.States
{
    // State Pattern
    // Het gedrag van het spel verandert op basis van de interne staat (gameState), wat het State Pattern demonstreert.
    public enum GameState
    {
        Menu,
        Playing,
        GameOver,
        MenuTransition,
        VictoryScreen
    }
}