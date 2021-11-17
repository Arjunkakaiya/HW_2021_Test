using System;

[Serializable]
public class PlayerData
{
    public float speed;
}

[Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

[Serializable]
public class Doofus
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

[Serializable]
public enum Direction
{
    None,
    Left,
    Right,
    Top,
    Bottom
}