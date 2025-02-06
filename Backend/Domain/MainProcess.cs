namespace Backend.Domain;

/// <summary>
/// MainProcess is the main act which caused the damage. Can either be Water, Landslide or a Collapse.
/// </summary>
public enum MainProcess
{
    Water = 1,
    Landslide = 2,
    Collapse = 3
}