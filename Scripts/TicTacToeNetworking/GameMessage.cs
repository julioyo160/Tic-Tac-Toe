using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameMessage
{
    public MessageType MessageType;
    public int playerId;
    public int[] boardState;
}
