namespace CrossRoads
{
    public abstract class MovementParticipant : ICloneable
    {
        //поточна позиція може змінюватися відносно перехрестя під час руху
        public abstract Direction CurrentDirection { get; set; }
        //Учасник руху може захотіти змінити намір в любий момент часу
        public abstract Direction Intension { get; set; }

        public abstract void ReactToIndicator(Direction direction);
        public abstract void Move();
        public abstract void Stop();
        public abstract void Wait();

        public abstract object Clone();
    }
}
