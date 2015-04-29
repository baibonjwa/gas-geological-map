
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace LibCommon
{
    /// <summary>
    /// 权限控制Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AuthorizeAttribute : Attribute
    {
        //public AuthorizeAttribute(String roles)
        //{
        //    Roles = roles;
        //}
        public String Roles { get; set; }

        protected virtual bool IsAuthorized()
        {
            return false;
        }
    }
}
