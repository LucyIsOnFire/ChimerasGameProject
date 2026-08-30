using Godot;

namespace FRIDLerp
{
    ///<summary>
    ///An extension class that adds the <c>FRIDLerp</c> method to the float, Vector2 and Vector3 classes
    ///</summary>

    public static class FRIDLerpExtensions
    {
        #region Extension Description
        /// <summary>
        /// Framerate Independent Linear Interpolation, 
        /// used to create smooth movement from <c>from</c> 
        /// to <c>to</c>, using <c>decay</c> and <c>delta</c>
        /// </summary>
        /// <param name="to">The value you are going to from this value</param>
        /// <param name="decay">The decay constant determines how close to <c>to</c> the returned value is. The larger the value of <c>decay</c>, the further from <c>to</c> the returned value is</param>
        /// <param name="delta">Delta time, the time between the last frame and this current frame. Accessable in <c>_Process</c> and <c>_PhysicsProcess</c> as <c>delta</c></param>
        #endregion Extension Description
        public static float FRIDLerp(this float from, float to, float decay, float delta)
        {
            return to + (from - to) * Mathf.Exp(-decay * delta);
        }

        #region Extension Description
        /// <summary>
        /// Framerate Independent Linear Interpolation, 
        /// used to create smooth movement from <c>from</c> 
        /// to <c>to</c>, using <c>decay</c> and <c>delta</c>
        /// </summary>
        /// <param name="to">The value you are going to from this value</param>
        /// <param name="decay">The decay constant determines how close to <c>to</c> the returned value is. The larger the value of <c>decay</c>, the further from <c>to</c> the returned value is</param>
        /// <param name="delta">Delta time, the time between the last frame and this current frame. Accessable in <c>_Process</c> and <c>_PhysicsProcess</c> as <c>delta</c></param>
        #endregion Extension Description
        public static Vector2 FRIDLerp(this Vector2 from, Vector2 to, float decay, float delta)
        {
            return new Vector2(from.X.FRIDLerp(to.X, decay, delta), from.Y.FRIDLerp(to.Y, decay, delta));
        }

        #region Extension Description
        /// <summary>
        /// Framerate Independent Linear Interpolation, 
        /// used to create smooth movement from <c>from</c> 
        /// to <c>to</c>, using <c>decay</c> and <c>delta</c>
        /// </summary>
        /// <param name="to">The value you are going to from this value</param>
        /// <param name="decay">The decay constant determines how close to <c>to</c> the returned value is. The larger the value of <c>decay</c>, the further from <c>to</c> the returned value is</param>
        /// <param name="delta">Delta time, the time between the last frame and this current frame. Accessable in <c>_Process</c> and <c>_PhysicsProcess</c> as <c>delta</c></param>
        #endregion Extension Description
        public static Vector3 FRIDLerp(this Vector3 from, Vector3 to, float decay, float delta)
        {
            return new Vector3(from.X.FRIDLerp(to.X, decay, delta), from.Y.FRIDLerp(to.Y, decay, delta), from.Z.FRIDLerp(to.Z, decay, delta));
        }
    }
}
