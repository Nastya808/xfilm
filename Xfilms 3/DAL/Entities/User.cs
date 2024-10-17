

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Pass { get; set;}=null!;
        public string Salt { get; set;}=null !;
        public bool IsAssert { get; set;}
        public DateTime RD { get; set; }
        public  int MaxCountProfile { get; set; }
        //to-do  relationship profile
        //to-do  relationship Subscribe

    }
}
