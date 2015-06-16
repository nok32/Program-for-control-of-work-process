
namespace Aplication.Model
{
    public class SecurityOperator:Security
    {
        public int SecurityOperatorId { get; set; }
        public bool IsDelited { get; set; }

        public SecurityOperator()
        {
            this.IsDelited = false;
        }

    }
}
