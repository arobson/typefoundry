﻿using Machine.Specifications;
using typefoundry.Impl;

namespace typefoundry.tests.DI
{
    public class with_simple_registration : with_container
    {
        private Establish context = () =>
            {
                var def1 = DependencyExpression.For<IHazzaMessage>();
                def1.Use<MessageHazzer>();

                var def2 = DependencyExpression.For<IMessageProvider>();
                def2.Use<MessageProvider>();

                Container.Register( def1 );
                Container.Register( def2 );
            };
    }
}