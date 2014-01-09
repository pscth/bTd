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
    public const int PULGA_ELASTIC_TO_STRETCH_ON = 3;
    public const int PULGA_ELASTIC_STRETCH_ON = 4;
    public const int PULGA_ELASTIC_TO_WAIT = 5;

    //Common
    public const int PULGA_UNSELECTED = 0;
    public const int PULGA_SELECTED = 1;

    public const int PULGA_TRAPEZE = 1;
    public const int PULGA_ELASTIC = 2;
    public const int PULGA_FORZUDE = 3;

    //Trapeze
    public const float TRAPEZE_FORCE = -1.0f;

    //Elastic
    public const int GORGE_LEFT = 0;
    public const int GORGE_RIGHT = 1;
}
