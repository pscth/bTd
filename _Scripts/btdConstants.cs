using UnityEngine;
using System.Collections;

static class btdConstants 
{
    //Common states
    public const int PULGA_WAIT = 0;
    public const int PULGA_JUMP = 1;
    //Trapeze states
    public const int PULGA_TRAPEZE_JUMP = 2;
    public const int PULGA_TRAPEZE_SWING = 3;
    //Elastic states
    public const int PULGA_ELASTIC_STRETCH = 2;

    //Trapeze
    public const float TRAPEZE_FORCE = -5.0f;
}
