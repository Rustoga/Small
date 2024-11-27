

namespace SmallGame.Unit
{
    public abstract class AnimationState
    {
        // public virtual float Length {get; internal set;}
        public virtual string Name { get; }
        public AnimationState(string name)
        {
            Name = name;
        }

        // public virtual void SetClipLength(float length )
        // {
        //     Length = length;
        // }




    }
}
