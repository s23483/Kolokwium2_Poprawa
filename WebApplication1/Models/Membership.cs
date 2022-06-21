using System;
using Microsoft.VisualBasic.CompilerServices;

namespace WebApplication1.Models
{
    public class Membership
    {
        public int MemberID { get; set; }
        public int TeamID { get; set; }
        public DateTime MembershipDate { get; set; }
        public virtual Member Member { get; set; }
        public virtual Team Team { get; set; }
    }
}