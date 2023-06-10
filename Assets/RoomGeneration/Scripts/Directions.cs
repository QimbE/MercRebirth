using UnityEngine;

public class Directions : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        T,
        R,
        B,
        L,
        TR,
        TB,
        TL,
        RB,
        RL,
        BL,
        TRB,
        TRL,
        TBL,
        RBL,
        TRBL
    }
}
