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
    public const int PULGA_ELASTIC_STRETCH_BUT_JUMPING = 3;
    public const int PULGA_ELASTIC_TO_STRETCH_ON = 4;
    public const int PULGA_ELASTIC_STRETCH_ON = 5;
    public const int PULGA_ELASTIC_TO_WAIT = 6;
    //Forzude states
    public const int PULGA_FORZUDE_PULLUP = 2;
    public const int PULGA_FORZUDE_PULLUP_BUT_JUMPING = 3;
    public const int PULGA_FORZUDE_TO_PULLUP_ON = 4;
    public const int PULGA_FORZUDE_PULLUP_ON = 5;

    //Common
    public const int PULGA_UNSELECTED = 0;
    public const int PULGA_SELECTED = 1;
    public const float MOVE_SPEED = 5.0f;
    public const float JUMP_HIGHT = 30.0f;//20.0f;
    public const float PULGA_MASS = 0.1f;
    public const int LEFT = 0;
    public const int RIGHT = 1;
    //Pulga number
    public const int PULGA_TRAPEZE = 1;
    public const int PULGA_ELASTIC = 2;
    public const int PULGA_FORZUDE = 3;

    //Trapeze
    public const float TRAPEZE_FORCE = -1.0f;
    public const float TRAPECE_JUMP_HIGHT = 50.0f;

    //Elastic
}
