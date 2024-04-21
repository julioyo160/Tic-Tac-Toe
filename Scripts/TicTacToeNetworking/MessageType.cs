using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MessageType
{
    ServerStartGame,
    ServerTogglePlayer,
    ServerShowPodium,
    ClientMakeMove,
    ClientPlayAgain,
    ClientQuit
}