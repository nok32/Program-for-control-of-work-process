using System;
namespace Aplication.Model
{
    public class PersonCommentLog
    {
        public int PersonCommentLogId { get; set; }
        public string Coments { get; set; }
        public DateTime TimeOffPersonComent { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public PersonCommentLog(string contextOffComent, int personId)
        {
            this.Coments = contextOffComent;
            this.TimeOffPersonComent = DateTime.Now;
            this.PersonId = personId;
        }
        public PersonCommentLog()
        { }
    }
}
