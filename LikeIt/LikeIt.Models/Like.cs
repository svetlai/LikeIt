namespace LikeIt.Models
{
    using System;

    public class Like
    {
        public Like()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
