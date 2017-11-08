using UnityEngine;

namespace AISandbox {
    public interface IActor {
        void SetInput( float y1_axis,float y2_axis);
        float MaxSpeed { get; }
        Vector3 Velocity { get; }
    }
}