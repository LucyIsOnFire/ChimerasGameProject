using Godot;

public partial class Global : Node
{
    public static MainScene MainScene {private set; get;}

    public static bool SetMainScene(MainScene _scene)
    {
        if (IsInstanceValid(MainScene)) 
        {
            GD.Print($"Error, Main Scene already set: {MainScene.Name}");
            return false;
        }

        if (IsInstanceValid(_scene))
        {
            MainScene = _scene;
            GD.Print($"Main Scene successfully set: {MainScene.Name}");
            return true;
        }
        else
        {
            GD.Print($"Error, invalid scene instance, Main Scene not set");
            return false;
        }
    }
}
