
namespace Movement
{
    public interface IEntityMovement
    {
        public abstract void Move();
        public abstract void Rotate();

        public abstract float GetSpeed();
        public abstract float GetRotSpeed();
    }
}
