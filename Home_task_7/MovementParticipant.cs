namespace Home_task_7
{
    public abstract class MovementParticipant
    {
        public abstract void ReactToIndicator(Direction direction);
        public abstract void Move();
        public abstract void Stop();
        public abstract void Wait();
    }
}
