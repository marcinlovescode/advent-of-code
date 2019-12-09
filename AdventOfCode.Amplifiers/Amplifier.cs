namespace AdventOfCode.Amplifiers
{
    public class Amplifier
    {
        public long Phase { get; }
        private bool PhaseSet { get; set; }
        private Intcode.Intcode _intcode { get; }
        
        public Amplifier(int phase)
        {
            Phase = phase;
            PhaseSet = false;
            _intcode = new Intcode.Intcode();
        }

        public void LoadIntcodeInstructions(long[] instructions)
        {
            _intcode.LoadMemory(instructions);
        }

        public long? Amplify(long input)
        {
            long? output = null;
            _intcode.Run(GetInput(input), i => output = i);
            return output;
        }

        private long[] GetInput(long input)
        {
            if (PhaseSet == false)
            {
                PhaseSet = true;
                return new[] {Phase, input};
            }
            return new[] {input};
        }
        
    }
}