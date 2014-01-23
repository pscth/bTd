using UnityEngine;
using System.Collections;

static class btdConstants 
{
    //Common states
    public const int PULGA_WAIT = 0;
    public const int PULGA_JUMP = 1;
    //Trapeze states
    public const int PULGA_TRAPEZE_EXTRA_JUMP = 2;
    public const int PULGA_TRAPEZE_SWING = 3;
    //Elastic states
    public const int PULGA_ELASTIC_STRETCH = 2;
    public const int PULGA_ELASTIC_STRETCH_BUT_JUMPING = 3;
    public const int PULGA_ELASTIC_TO_STRETCH_ON = 4;
    public const int PULGA_ELASTIC_STRETCH_ON = 5;
    public const int PULGA_ELASTIC_TO_WAIT = 6;
    public const int PULGA_ELASTIC_CLIMB = 7;
    public const int PULGA_ELASTIC_CLIMB_RDY = 8;
    public const int PULGA_ELASTIC_CLIMBING = 9;
    //Forzude Pull states	
	public const int PULGA_FORZUDE_NO_PULL = 0;
	public const int PULGA_FORZUDE_PULLING = 1;
	public const int PULGA_FORZUDE_PULL = 2;
    public const int PULGA_FORZUDE_DROP = 3;
    public const int PULGA_FORZUDE_HOLD_THROW = 4;
    public const int PULGA_FORZUDE_THROW = 5;
		
    public const int PULGA_FORZUDE_PULLUP = 2;
    public const int PULGA_FORZUDE_PULLUP_BUT_JUMPING = 3;
    public const int PULGA_FORZUDE_TO_PULLUP_ON = 4;
    public const int PULGA_FORZUDE_PULLUP_ON = 5;
	public const int PULGA_FORZUDE_PULLUP_OFF = 6;

    //Common
    public const int PULGA_UNSELECTED = 0;
    public const int PULGA_SELECTED = 1;
    public const float MOVE_SPEED = 5.0f;
    public const float JUMP_HIGHT = 5.0f;
    public const float PULGA_MASS = 0.1f;
    public const int LEFT = 0;
    public const int RIGHT = 1;
	public const int CENTER = 2;
    //Pulga number
    public const int PULGA_FORZUDE = 1;
    public const int PULGA_CLOWN = 2;
    public const int PULGA_TRAPEZE = 3;
    public const int PULGA_ELASTIC = 4;
    public const int PULGA_FAQUIR = 5;
    public const int PULGA_GROUP = 6;

    //Trapeze
    public const float TRAPEZE_FORCE = -25.0f;
    public const float TRAPEZE_MOVE_SPEED = 100.0f;

    //Elastic
    public const float ELASTIC_CLIMB_SPEED = 2.0f;

    //Forzude
    public const int PULGA_FORZUDE_ANGLE_NO_PULL = 0;
    public const int PULGA_FORZUDE_START_ANGLE_PULL = 90;
    public const int PULGA_FORZUDE_FINAL_ANGLE_PULL = 180;
    public const int PULGA_FORZUDE_INCREASE_PULL_ANGLE = 5;
    public const int PULGA_FORZUDE_INIT_THROW_ANGLE = 0;
    public const int PULGA_FORZUDE_INIT_THROW_POWER = 0;
}
