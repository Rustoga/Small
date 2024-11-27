using UnityEngine;

namespace SmallGame.Physics
{
    [System.Serializable]
    public class ExternalForce
    {
        public Vector2 Force;
        public string DebugName;


        public ExternalForce(string debugName, Vector2 force)
        {
            DebugName = debugName;
            Force = force;

        }

    }
}
