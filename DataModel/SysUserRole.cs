//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATEC.Core.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class SysUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool AllowView { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowAccess { get; set; }
        public bool AllowPrint { get; set; }
        public bool AllowExport { get; set; }
        public bool AllowImport { get; set; }
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
        public bool Actived { get; set; }
        public int CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual SysRole SysRole { get; set; }
        public virtual SysUser SysUser { get; set; }
    }
}
