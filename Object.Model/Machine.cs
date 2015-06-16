

namespace Aplication.Model
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string Name { get; set; }
        public bool IsDelited { get; set; }

        public Machine(string name)
        {
            this.Name = name;
        }
        public Machine()
        { }

        public override string ToString()
        {
            string newToString = string.Format("--------------------\n\r{0} - {1}\n\r--------------------", this.MachineId, this.Name);
            return newToString;
        }
    }
}
