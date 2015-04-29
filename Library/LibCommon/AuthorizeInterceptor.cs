using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Generators.Emitters;

namespace LibCommon
{
    public class AuthorizeInterceptor : IInterceptor
    {
        private void PreProceed(IInvocation invocation)
        {
            //使用反射读取Attribute
            System.Reflection.MemberInfo info = invocation.GetType(); //通过反射得到MyClass类的信息
            //得到施加在MyClass类上的定制Attribute 
            AuthorizeAttribute attr =
                (AuthorizeAttribute)Attribute.GetCustomAttribute(info, typeof(AuthorizeAttribute));

            if (attr != null)
            {
                //TODO:权限判断
                //Console.WriteLine("代码检查人:{0}", att.Reviewer);
                //Console.WriteLine("检查时间:{0}", att.Date);
                //Console.WriteLine("注释:{0}", att.Comment);
            }
        }

        public void Intercept(IInvocation invocation)
        {
            this.PreProceed(invocation);
            invocation.Proceed();
            //this.PostProceed(invocation);
        }
    }
}
