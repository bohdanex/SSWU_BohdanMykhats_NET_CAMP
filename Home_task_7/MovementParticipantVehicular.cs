namespace Home_task_7
{
    public class MovementParticipantVehicular : MovementParticipant, ICloneable
    {
        public MovementParticipantVehicular()
        {
            Id = Guid.NewGuid();
            Intension = 0;
            CurrentDirection = 0;
            IsMoving = false;
        }

        public MovementParticipantVehicular(Guid id, Direction currentDirection, Direction intension,  bool isMoving)
        {
            Id = id;
            CurrentDirection = currentDirection;
            Intension = intension;
            IsMoving = isMoving;
        }

        public Guid Id { get; init; }
        public override Direction CurrentDirection { get; set; }
        public override Direction Intension { get; set; }
        public bool IsMoving { get; private set; }

        public override void ReactToIndicator(Direction direction)
        {
            if(Intension == direction)
            {
                Move();
            }
            else
            {
                if (IsMoving)
                {
                    Stop();
                }
                else
                {
                    Wait();
                }
            }
        }

        public override void Wait()
        {
            IsMoving = false;
        }

        public override void Stop()
        {
            IsMoving = false;
        }

        public override void Move()
        {
            IsMoving = true;
        }

        public override string ToString()
        {
            string guid = Id.ToString()[..10];
            return $"Id = {guid}..., current direction: {CurrentDirection.TranslateToUkrainian()}, intension: {Intension.TranslateToUkrainian()}, is moving? {IsMoving}";
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
