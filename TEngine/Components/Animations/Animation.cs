using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEngine.Components.Renderers;

namespace TEngine.Components.Animations
{
    public abstract class Animation<T> : Component
    {
        public uint FrameCount { get; protected set; } = 0;
        public bool IsPlaying { get; private set; } = true;
        public bool Loop { get; set; } = true;

        public virtual void Tick()
        {
            if (!IsPlaying) return;

            FrameCount++;
            if (!Loop && IsFinished())
                Pause();
        }

        public void Play() => IsPlaying = true;
        public void Pause() => IsPlaying = false;
        public void Reset()
        {
            FrameCount = 0;
            IsPlaying = true;
        }

        public virtual bool IsFinished()
        {
            // Can be overridden with actual frame limits
            return false;
        }

        public abstract T Apply(T target); // Or overload this depending on use-case

        public override void Update()
        {
            base.Update();

            if (IsPlaying)
            {
                Tick();
                if(HasComponent<Renderer<T>>())
                {
                    Apply(GetComponent<Renderer<T>>().RenderData); // Replace with actual target
                }
            }
        }
    }

}
