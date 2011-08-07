﻿using Machine.Specifications;
using typefoundry.Impl;

namespace typefoundry.tests.DI
{
    public class with_generics_registration : with_container
    {
        private Establish context = () =>
            {
                var def1 = DependencyExpression.For(typeof(ITakeGenericParams<>));
                def1.Use(typeof(OpenImpl<>));

                var def2 = DependencyExpression.For("closed", typeof(ITakeGenericParams<>));
                def2.Add<ClosedImpl>();

                Container.Register( def1 );
                Container.Register( def2 );
            };
    }
}