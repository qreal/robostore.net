//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tests.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProgramRobot
    {
        public int ProgramRobotID { get; set; }
        public int ProgramID { get; set; }
        public int RobotID { get; set; }
        public int CurrentVersion { get; set; }
    
        public virtual Program Program { get; set; }
        public virtual Robot Robot { get; set; }
    }
}
